using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : BaseAbility
{
    public Slash()
    {
        abilityDescription = "Slash all enemies";   
        abilityName = "Slash";
        abilityDamage = 10;
        APCost = 0;  // will work  in later.
    }
}
