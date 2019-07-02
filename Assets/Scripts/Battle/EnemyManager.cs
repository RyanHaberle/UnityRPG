using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    //use thugs untill I understand how to randomize enemies based on location.
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public int howManyEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        howManyEnemies = Random.Range(1,4);
        if(howManyEnemies == 1)
        {
            Instantiate(enemy1, new Vector3(-7, 0, -1), transform.rotation);

            
        }
        else if (howManyEnemies == 2)
        {
            Instantiate(enemy1, new Vector3(-7, 0, -1), transform.rotation);
            Instantiate(enemy2, new Vector3(-7, 2, -1), transform.rotation);
            
        }
        else if (howManyEnemies == 3)
        {
            Instantiate(enemy1, new Vector3(-7, 0, -1), transform.rotation);
            Instantiate(enemy2, new Vector3(-7, 2, -1), transform.rotation);
            Instantiate(enemy3, new Vector3(-7, 4, -1), transform.rotation);
            
        }

        //loadEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadEnemies()
    {
       /* int numOfEnemies = Random.Range(1, 4);
        string npcNum;
         Sprite[] enemyList = new Sprite[numOfEnemies];

        for (int i = 0; i < numOfEnemies; i++)
        {
            npcNum = i.ToString();
            enemyList[i] = Resources.Load("Thug", typeof(Sprite)) as Sprite;
        }*/
    }
}
