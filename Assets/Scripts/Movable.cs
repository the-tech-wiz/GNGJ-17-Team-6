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
    [SerializeField] private LayerMask obstacleLayer;

    public const float DefaultDistance = 1f;
    //private const float RayCastOffset = DefaultDistance * 0.6f;
    //private const float RayCastDistanceMultiplier = 0.8f;

    [HideInInspector]
    public bool isMoving;

    public void MoveUntilStopped(Vector3 direction)
    {
        while (CanMove(direction)) Move(direction);
    }
    public void Move(Vector3 direction)
    {
        isMoving = true;
        if (!CanMove(direction)) return;
        
        for(int i = 0; i < transform.parent.childCount; i++){
            if (collisionTilemap.WorldToCell(transform.parent.GetChild(i).transform.position) == collisionTilemap.WorldToCell(transform.position + (Vector3)direction))
                transform.parent.GetChild(i).GetComponent<Movable>().Move(direction);
        }
        transform.position = (transform.position + direction * DefaultDistance).Snap();
        isMoving = false;
    }

    public bool CanMove(Vector2 direction)
    {
        Vector3Int gridPos = collisionTilemap.WorldToCell(transform.position + (Vector3)direction);
        bool nobody = true;
        /*foreach(Transform slime in transform.parent.GetComponentsInChildren<Transform>()){
            if(collisionTilemap.WorldToCell(slime.position) == gridPos)
                nobody = false;
        }*/
        for(int i = 0; i < transform.parent.childCount; i++){
            if (collisionTilemap.WorldToCell(transform.parent.GetChild(i).transform.position) == gridPos)
                nobody = transform.parent.GetChild(i).GetComponent<Movable>().CanMove(direction);
        }
        return !collisionTilemap.HasTile(gridPos) && nobody;
        /* return obstacle == null
               || (withMovable && obstacle.TryGetComponent(out Movable movable)
                   && !ReferenceEquals(this, movable)
                   && movable.CanMove(direction, distance)); */
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