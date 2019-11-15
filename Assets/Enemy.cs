using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{

    public int HP;
    public int ATK;
    public int DEF;
    public bool turning = false;

    //returns true if it has 0 HP
    public bool Fight(int eATK)
    {
        if(eATK-DEF>0)
        {
            HP -= (eATK - DEF);
            if (HP <= 0)
                return true;
        }
        return false;
    }

    public virtual void Turn()
    {

    }

    
}
