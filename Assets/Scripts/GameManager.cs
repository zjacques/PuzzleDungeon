using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject currentParty;
    GameObject[] currentGrid;
    GameObject[] enemies;

    bool movement = false;
    bool wasMovement = false;

    public Text GPdisplay;
    public Text HPdisplay;
    public Text ATKdisplay;
    public Text DEFdisplay;

    int GP = 0;

    // Start is called before the first frame update
    void Start()
    {
        GPdisplay.text = GP.ToString();
        currentParty = GameObject.FindGameObjectWithTag("Player");
        currentGrid = GameObject.FindGameObjectsWithTag("Room");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");


        int mapLayerMask = 1 << 8;
        Collider2D thisRoom = Physics2D.OverlapPoint(currentParty.transform.position, mapLayerMask);
        AdventurerScript adv = currentParty.GetComponent<AdventurerScript>();

        if (adv.movement != null && adv.movement.active)
        {
            movement = true;
        }
        foreach (GameObject room in currentGrid)
        {
            if (room.GetComponent<TileMan>().movement != null && room.GetComponent<TileMan>().movement.active)
            {
                movement = true;
            }
        }

        //fight
        if (thisRoom.gameObject != null)
        {
            foreach (Transform child in thisRoom.transform)
            {
                if (child.CompareTag("Gold"))
                {
                    Destroy(child.gameObject);
                    GP++;
                }
                if (child.CompareTag("Enemy"))
                {
                    Enemy e = child.GetComponent<Enemy>();
                    if (e.Fight(adv.ATK))
                        DestroyImmediate(child.gameObject);
                        //Destroy(child.gameObject);
                    adv.Fight(e.ATK);
                }
            }
        }

        //turns
        adv.turning = false;
        if(movement)
            adv.turning = true;
        foreach(GameObject en in enemies)
        {
            if (en.GetComponent<Enemy>().turning)
                adv.turning = true;
        }
        if (movement && !wasMovement)
        {
            Debug.Log("started movement");
            wasMovement = true;
        }
        if(wasMovement&&!movement)
        {
            Debug.Log("stopped movement");
            wasMovement = false;
            foreach (GameObject en in enemies)
            {
                en.GetComponent<Enemy>().Turn();
            }
        }

        //fight
        if (thisRoom.gameObject != null)
        {
            foreach (Transform child in thisRoom.transform)
            {
                if (child.CompareTag("Gold"))
                {
                    Destroy(child.gameObject);
                    GP++;
                }
                if (child.CompareTag("Enemy"))
                {
                    Enemy e = child.GetComponent<Enemy>();
                    if (e.Fight(adv.ATK))
                        DestroyImmediate(child.gameObject);
                        //Destroy(child.gameObject);
                    adv.Fight(e.ATK);
                }
            }
        }

        movement = false;

        GPdisplay.text = GP.ToString();
        HPdisplay.text = adv.HP.ToString();
        ATKdisplay.text = adv.ATK.ToString();
        DEFdisplay.text = adv.DEF.ToString();
    }
}
