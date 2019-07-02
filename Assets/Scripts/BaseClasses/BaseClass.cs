using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass 
{
    public string theName;

    public int maxHP;
    public int currHP;

    public float maxMP;
    public int currMP;

    public int stamina;
    public int intelligence;
    public int strength;
    public int speed;

    public List<BaseAbility> attacks = new List<BaseAbility>();
}
