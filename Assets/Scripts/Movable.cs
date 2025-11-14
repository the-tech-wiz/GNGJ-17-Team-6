using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

[Serializable]
public class Movable : MonoBehaviour
{
    [SerializeField]
    private Tilemap collisionTilemap;
    [SerializeField]
    private Tilemap bgTilemap;
    [SerializeField] private LayerMask wallLayer;

    public const float DefaultDistance = 1f;
    private const float weight = 1f;
    public const float delayPerStep = 0.25f;
    bool isMoving;
    bool origin;
    bool connected;
    void Awake()
    {
        isMoving = false;
        origin = false;
        connected = false;
    }

    public void MoveUntilStopped(Vector3 direction)
    {
        bool inMotion = false;
        foreach (Movable slime in transform.parent.GetComponentsInChildren<Movable>())
        {
            if (slime.origin == true) inMotion = true;
        }
        if (!inMotion)
        {
            baseDirection = direction;
            origin = true;
            InvokeRepeating("MUS", 0f, delayPerStep);
        }
        /*while (CanMove(direction) != Mathf.Infinity){ 
            Move(direction);
            yield return new WaitForSeconds(1f);
        }
        origin = false;*/
    }

    private Vector3 baseDirection;
    //private float delayTime = 1f;
    void MUS()
    {
        Vector3 direction = baseDirection;
        GetAhead(direction);
        if (CanMove(direction) == Mathf.Infinity)
        {
            origin = false;
            CancelInvoke();
        }
    }

    public void GetAhead(Vector3 direction)
    {
        //origin = true;
        Move(direction);
        //origin = false;
        Broken(direction);
    }

    void Move(Vector3 direction)
    {
        isMoving = true;
        if (!MoveForward(direction) && !origin)
        {
            Vector3 direction1 = new(-direction.y, direction.x, direction.z);
            Vector3 direction2 = new(direction.y, -direction.x, direction.z);
            if (CanMoveForward(direction1) <= CanMoveForward(direction2))
                MoveForward(direction1);
            else
                MoveForward(direction2);
        }
        isMoving = false;
    }
    private bool MoveForward(Vector3 direction)
    {
        if (CanMoveForward(direction) != Mathf.Infinity)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (collisionTilemap.WorldToCell(transform.parent.GetChild(i).transform.position) == collisionTilemap.WorldToCell(transform.position + (Vector3)direction))
                    transform.parent.GetChild(i).GetComponent<Movable>().Move(direction);
            }
            transform.position = (transform.position + direction * DefaultDistance).Snap();
            return true;
        }
        return false;
    }

    float CanMove(Vector2 direction)
    {
        if (isMoving) return Mathf.Infinity;
        if (origin) return CanMoveForward(direction);
        return Mathf.Min(CanMoveForward(direction), CanMoveForward(new Vector2(-direction.y, direction.x)), CanMoveForward(new Vector2(direction.y, -direction.x)));
    }

    float CanMoveForward(Vector2 direction)
    {
        Vector3Int gridPos = collisionTilemap.WorldToCell(transform.position + (Vector3)direction);
        float nobody = 0f;
        /*foreach(Transform slime in transform.parent.GetComponentsInChildren<Transform>()){
            if(collisionTilemap.WorldToCell(slime.position) == gridPos)
                nobody = false;
        }*/
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform slime = transform.parent.GetChild(i);
            if (collisionTilemap.WorldToCell(slime.transform.position) == gridPos)
                if (!slime.GetComponent<Movable>().isMoving)
                    nobody = slime.GetComponent<Movable>().CanMove(direction);
        }
        if (collisionTilemap.HasTile(gridPos) || nobody == Mathf.Infinity)
            return Mathf.Infinity;
        return nobody + weight;

        /* return obstacle == null
               || (withMovable && obstacle.TryGetComponent(out Movable movable)
                   && !ReferenceEquals(this, movable)
                   && movable.CanMove(direction, distance)); */
    }
    /**
    Basic neighbour check for visual purposes
    */
    public Direction Neighbours()
    {
        Direction hasNeighs = 0;
        Movable[] allSlimes = transform.parent.GetComponentsInChildren<Movable>();
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            Vector3Int gridPos = collisionTilemap.WorldToCell(transform.position + (Vector3)dir.ToVec());
            foreach (var slime in allSlimes)
            {
                Vector3Int slimeGridPos = collisionTilemap.WorldToCell(slime.transform.position);
                if (slimeGridPos == gridPos)
                {
                    hasNeighs |= dir;
                }
            }
        }

        return hasNeighs;

    }
    void Broken(Vector3 direction)
    {
        Connect();
        bool fullGraph = true;
        foreach (Movable slime in transform.parent.GetComponentsInChildren<Movable>())
        {
            if (!slime.connected)
                fullGraph = false;
        }
        foreach (Movable slime in transform.parent.GetComponentsInChildren<Movable>())
        {
            slime.connected = false;
        }
        if (!fullGraph)
        {
            Snake(direction);
        }
    }

    public void Connect()
    {
        if (connected) return;
        connected = true;
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform slime = transform.parent.GetChild(i);
            int myX = collisionTilemap.WorldToCell(transform.position).x;
            int myY = collisionTilemap.WorldToCell(transform.position).y;
            int slimeX = collisionTilemap.WorldToCell(slime.transform.position).x;
            int slimeY = collisionTilemap.WorldToCell(slime.transform.position).y;
            if ((myX == slimeX && myY + 1 == slimeY) || (myX == slimeX && myY - 1 == slimeY) || (myX + 1 == slimeX && myY == slimeY) || (myX - 1 == slimeX && myY == slimeY))
                slime.GetComponent<Movable>().Connect();
        }
    }

    private void Snake(Vector3 direction)
    {
        Vector3Int oldPos = collisionTilemap.WorldToCell(transform.position - (Vector3)direction);
        if (!Pull(direction, oldPos))
        {
            if (!Pull(new Vector3(-direction.y, direction.x, direction.z), oldPos))
            {
                Pull(new Vector3(direction.y, -direction.x, direction.z), oldPos);
            }
        }
    }

    private bool Pull(Vector3 direction, Vector3Int oldPos)
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform slime = transform.parent.GetChild(i);
            int myX = oldPos.x;
            int myY = oldPos.y;
            int slimeX = collisionTilemap.WorldToCell(slime.transform.position).x;
            int slimeY = collisionTilemap.WorldToCell(slime.transform.position).y;
            if ((myX - slimeX == direction.x) && (myY - slimeY == direction.y))
            {
                slime.GetComponent<Movable>().MoveForward(direction);
                slime.GetComponent<Movable>().Broken(direction);
                return true;
            }
        }
        return false;
    }


}