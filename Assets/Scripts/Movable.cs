using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    //private const float RayCastOffset = DefaultDistance * 0.6f;
    //private const float RayCastDistanceMultiplier = 0.8f;

    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public bool origin;
    [HideInInspector]
    public bool connected;
    void Awake()
    {
        isMoving = false;
        origin = false;
        connected = false;
    }

    public void MoveUntilStopped(Vector3 direction)
    {
        origin = true;
        while (CanMove(direction) != Mathf.Infinity) getAhead(direction);
        origin = false;
    }

    public void getAhead(Vector3 direction)
    {
        Move(direction);
        Broken(direction);

    }

    public void Move(Vector3 direction)
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

    public float CanMove(Vector2 direction)
    {
        if (isMoving) return Mathf.Infinity;
        if (origin) return CanMoveForward(direction);
        return Mathf.Min(CanMoveForward(direction), CanMoveForward(new Vector2(-direction.y, direction.x)), CanMoveForward(new Vector2(direction.y, -direction.x)));
    }

    public float CanMoveForward(Vector2 direction)
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

    public void Broken(Vector3 direction)
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

    /*     public GameObject GetObstacle(Vector3 direction)
        {
            direction.Normalize();

            Vector3 origin = transform.position + new Vector3(0.5f, 1.5f) + direction.normalized * RayCastOffset;
            Debug.Log("Origin: " + origin.ToString());
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance * RayCastDistanceMultiplier, obstacleLayer);

            Debug.Log("Hit: " + hit.ToString());
            return hit.collider != null ? hit.collider.gameObject : null;
        } */

    /* #region Editor Debugging
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        float raycastDistance = DefaultDistance * RayCastDistanceMultiplier;

        DrawRay(origin + Vector3.up * RayCastOffset, Vector3.up, raycastDistance);
        DrawRay(origin + Vector3.down * RayCastOffset, Vector3.down, raycastDistance);
        DrawRay(origin + Vector3.left * RayCastOffset, Vector3.left, raycastDistance);
        DrawRay(origin + Vector3.right * RayCastOffset, Vector3.right, raycastDistance);
    } */

    // private void DrawRay(Vector3 origin, Vector3 direction, float distance)
    // {
    //     direction.Normalize();

    //     bool canMove = CanMove(direction, distance, withMovable: true);
    //     Gizmos.color = canMove ? Color.green : Color.red;
    //     Gizmos.DrawRay(origin, direction * distance);
    // }
    /*  #endif
     #endregion */
}