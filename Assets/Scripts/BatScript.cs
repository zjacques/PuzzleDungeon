using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatScript : Enemy
{
    bool left = true;
    Tween movement;

    public override void Turn()
    {
        bool moved = false;
        Vector3 newPos = transform.position;

        int mapLayerMask = 1 << 8;

        if (left)
        {
            RaycastHit2D nextRoom = Physics2D.Raycast(transform.position, Vector2.left, 1, mapLayerMask, -1, 1);
            Collider2D thisRoom = Physics2D.OverlapPoint(transform.position, mapLayerMask);

            if (((nextRoom.collider != null && nextRoom.collider.tag == "Room" && nextRoom.collider.gameObject.GetComponent<TileMan>().rightDoor) ||
                (nextRoom.collider != null && nextRoom.collider.tag == "Goal"))
                &&
                thisRoom.gameObject != null && thisRoom.gameObject.GetComponent<TileMan>().leftDoor)
            {
                moved = true;
                newPos += Vector3.left;
                transform.parent = nextRoom.transform;
            }
            left = false;
        }
        else
        {
            RaycastHit2D nextRoom = Physics2D.Raycast(transform.position, Vector2.right, 1, mapLayerMask, -1, 1);
            Collider2D thisRoom = Physics2D.OverlapPoint(transform.position, mapLayerMask);

            if (((nextRoom.collider != null && nextRoom.collider.tag == "Room" && nextRoom.collider.gameObject.GetComponent<TileMan>().leftDoor) ||
                (nextRoom.collider != null && nextRoom.collider.tag == "Goal"))
                && thisRoom.gameObject != null && thisRoom.gameObject.GetComponent<TileMan>().rightDoor)
            {
                moved = true;
                newPos += Vector3.right;
                transform.parent = nextRoom.transform;
            }
            left = true;
        }
        if (moved)
        {
            movement = transform.DOMove(newPos, 0.3f);
            turning = true;
            movement.OnComplete(() => {
                turning = false;
                Vector3 resetPos = transform.position;
                if (resetPos.x > 0)
                    resetPos.x = Mathf.Ceil(resetPos.x);
                else
                    resetPos.x = Mathf.Floor(resetPos.x);

                if (resetPos.y > 0)
                    resetPos.y = Mathf.Ceil(resetPos.y);
                else
                    resetPos.y = Mathf.Floor(resetPos.y);

                transform.position = resetPos;
            });
        }
    }

}
