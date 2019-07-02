using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTargetButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EnemyPrefab;
    

    public void SelectEnemyTarget()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateManager>().Input2(EnemyPrefab); //save input of enemy prefab.

    }

    public void HideSelector()
    {

        EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(false);
        Debug.Log("Hover");
        

    }

    public void ShowSelector()
    {
        
            EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(true);
        Debug.Log("exit");
    }
}
