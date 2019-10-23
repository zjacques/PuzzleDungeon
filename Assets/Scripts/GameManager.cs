using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject currentParty;
    GameObject currentGrid;

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
    }

    // Update is called once per frame
    void Update()
    {
        int mapLayerMask = 1 << 8;
        Collider2D thisRoom = Physics2D.OverlapPoint(currentParty.transform.position, mapLayerMask);

        AdventurerScript adv = currentParty.GetComponent<AdventurerScript>();

        if (thisRoom.gameObject!=null)
        {
            foreach(Transform child in thisRoom.transform)
            {
                if(child.CompareTag("Gold"))
                {
                    Destroy(child.gameObject);
                    GP++;
                }
                if(child.CompareTag("Enemy"))
                {
                    Enemy e = child.GetComponent<Enemy>();
                    if(e.Fight(adv.ATK))
                        Destroy(child.gameObject);
                    adv.Fight(e.ATK);
                }
            }
        }

        GPdisplay.text = GP.ToString();
        HPdisplay.text = adv.HP.ToString();
        HPdisplay.text = adv.HP.ToString();
        HPdisplay.text = adv.HP.ToString();
    }
}
