using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]



//Base Class for all enemies
public class BaseEnemy :BaseClass
{

    public enum Type
    {
        Normal, Fire, Water, Ice, Light, Earth, Electricity 
    }


   

    public Type enemyType;
}
