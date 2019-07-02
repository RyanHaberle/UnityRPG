using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThugData : MonoBehaviour
{
    int thugHP = 50;
    int thugMaxHp = 50;

    int thugATK = 5;
    int thugDEF = 6;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thugHP > thugMaxHp)
        {
            thugHP = thugMaxHp;
        }    
    }
}
