using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileMan : MonoBehaviour
{
    public bool upDoor;
    public bool rightDoor;
    public bool downDoor;
    public bool leftDoor;
    public GameObject upDoorSprite;
    public GameObject rightDoorSprite;
    public GameObject downDoorSprite;
    public GameObject leftDoorSprite;

    bool trackMouse;
    Vector2 mouseStart;
    Vector2 mouseEnd;
    public Tween movement;

    #region Spawnables
    public List<GameObject> thingsThatCanBeInRooms;
    int numberOfDoors;
    #endregion

    void OpenDoor(int d)
    {
        switch (d)
        {
            case 0:
                if (!upDoor){
                    numberOfDoors++;
                    upDoor = true;
                }
                break;
            case 1:
                if (!rightDoor){
                    numberOfDoors++;
                    rightDoor = true;
                }
                break;
            case 2:
                if (!downDoor){
                    numberOfDoors++;
                    downDoor = true;
                }
                break;
            case 3:
                if (!leftDoor) {
                    numberOfDoors++;
                    leftDoor = true;
                }
                break;
        }
            
            
    }

    private void Start()
    {
        int door = Random.Range(0, 4);
        OpenDoor(door);
        if(Random.Range(0,1)==0)
        {
            door = Random.Range(0, 4);
            OpenDoor(door);
            if (Random.Range(0, 1) == 0)
            {
                door = Random.Range(0, 4);
                OpenDoor(door);
                if (Random.Range(0, 1) == 0)
                {
                    door = Random.Range(0, 4);
                    OpenDoor(door);
                }
            }
        }
        int plyrLayerMask = 1 << 9;
        Collider2D thisRoom = Physics2D.OverlapPoint(transform.position, plyrLayerMask);
        if (thisRoom != null)
        {
            upDoor = true;
            leftDoor = true;
            rightDoor = true;
            downDoor = true;

        }

        movement.SetAutoKill(false);
        Vector3 doorPos = transform.position;
        doorPos.z -= 1.1f;
        Quaternion doorRot = transform.rotation;

        if (Random.Range(0, 6) + numberOfDoors > 4)
        {
            Instantiate(thingsThatCanBeInRooms[Random.Range(0, thingsThatCanBeInRooms.Count)], doorPos, doorRot, transform);
        }

        doorPos.z += .1f;

        if (upDoor)
            Instantiate(upDoorSprite, doorPos, doorRot, transform);
        if (rightDoor)
            Instantiate(rightDoorSprite, doorPos, doorRot, transform);
        if (downDoor)
            Instantiate(downDoorSprite, doorPos, doorRot, transform);
        if (leftDoor)
            Instantiate(leftDoorSprite, doorPos, doorRot, transform);
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

        if(Mathf.Abs(mouseMov.x)>Mathf.Abs(mouseMov.y))
        {
            if(mouseMov.x>0)
            {
                RaycastHit2D isEmpty = Physics2D.Raycast(transform.position, Vector2.right,1,mapLayerMask);
                
                if (isEmpty.collider==null)
                {
                    moved = true;
                    newPos += Vector3.right;
                }
            }
            else
            {
                RaycastHit2D isEmpty = Physics2D.Raycast(transform.position, Vector2.left, 1, mapLayerMask);

                if (isEmpty.collider == null)
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
                RaycastHit2D isEmpty = Physics2D.Raycast(transform.position, Vector2.up, 1, mapLayerMask);

                if (isEmpty.collider == null)
                {
                    moved = true;
                    newPos += Vector3.up;
                }
            }
            else
            {
                RaycastHit2D isEmpty = Physics2D.Raycast(transform.position, Vector2.down, 1,mapLayerMask);

                if (isEmpty.collider == null)
                {
                    moved = true;
                    newPos += Vector3.down;
                }
            }
        }

        if(moved)
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

    private void FixedUpdate()
    {

    }
}
