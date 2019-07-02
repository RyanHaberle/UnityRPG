using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BaseMagic
{
    public Heal()
    {
        magicName = "Heal";
        mpCost = 5;
    }

    int healAmt = 50;
}
