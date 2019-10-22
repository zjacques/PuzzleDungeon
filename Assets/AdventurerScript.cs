using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AdventurerScript : MonoBehaviour
{

    bool trackMouse;
    Vector2 mouseStart;
    Vector2 mouseEnd;
    Tween movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        trackMouse = true;
        mouseStart = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        bool moved = false;
        Vector3 newPos = transform.position;

        trackMouse = false;
        mouseEnd = Input.mousePosition;
        Vector2 mouseMov = mouseEnd - mouseStart;

        int mapLayerMask = 1 << 8;

        if (Mathf.Abs(mouseMov.x) > Mathf.Abs(mouseMov.y))
        {
            if (mouseMov.x > 0)
            {
                RaycastHit2D nextRoom = Physics2D.Raycast(transform.position, Vector2.right, 1, mapLayerMask, -1, 1);
                Collider2D thisRoom = Physics2D.OverlapPoint(transform.position, mapLayerMask);

                if (((nextRoom.collider != null && nextRoom.collider.tag == "Room" && nextRoom.collider.gameObject.GetComponent<TileMan>().leftDoor) ||
                    (nextRoom.collider != null && nextRoom.collider.tag == "Goal"))
                    && thisRoom.gameObject != null && thisRoom.gameObject.GetComponent<TileMan>().rightDoor)
                {
                    moved = true;
                    newPos += Vector3.right;
                }
            }
            else
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
                }
            }
        }
        else
        {
            if (mouseMov.y > 0)
            {
                RaycastHit2D nextRoom = Physics2D.Raycast(transform.position, Vector2.up, 1, mapLayerMask, -1, 1);
                Collider2D thisRoom = Physics2D.OverlapPoint(transform.position, mapLayerMask);

                if (((nextRoom.collider != null && nextRoom.collider.tag == "Room" && nextRoom.collider.gameObject.GetComponent<TileMan>().downDoor) ||
                    (nextRoom.collider != null && nextRoom.collider.tag == "Goal"))
                    && thisRoom.gameObject != null && thisRoom.gameObject.GetComponent<TileMan>().upDoor)
                {
                    moved = true;
                    newPos += Vector3.up;
                }
            }
            else
            {
                RaycastHit2D nextRoom = Physics2D.Raycast(transform.position, Vector2.down, 1, mapLayerMask, -1, 1);
                Collider2D thisRoom = Physics2D.OverlapPoint(transform.position, mapLayerMask);

                if (((nextRoom.collider != null && nextRoom.collider.tag == "Room" && nextRoom.collider.gameObject.GetComponent<TileMan>().upDoor)||
                    (nextRoom.collider != null && nextRoom.collider.tag == "Goal")) 
                    && thisRoom.gameObject != null && thisRoom.gameObject.GetComponent<TileMan>().downDoor)
                {
                    moved = true;
                    newPos += Vector3.down;
                }
            }
        }

        if (moved)
        {
            movement = transform.DOMove(newPos, 0.3f);
            movement.OnComplete(() => {
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
