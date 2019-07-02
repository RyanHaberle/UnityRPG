using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    public enum elementalType
    {
        Normal, Fire, Water, Ice, Light, Earth, Electricity
    }
    public string abilityName;
    public string abilityDescription;
    public int abilityDamage;
    public int APCost;   //ap fills as you take damage.

    bool hitAll;
}
