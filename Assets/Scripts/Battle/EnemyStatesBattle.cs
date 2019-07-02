using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatesBattle : MonoBehaviour
{

    private BattleStateManager Bsm;
    public BaseEnemy enemy;


    public enum TurnState
    {
        Processing, ChooseAction, Waiting, Action, Dead
    }

    public TurnState currentState;  // keeps track of what state enemy unit is in  eg. Dead.
    //for progress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    //this gameobject
    private Vector3 startPosition;
    public GameObject Selector;
    //ienumorator TimeForAction() stuff
    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed = 5f;



    // Start is called before the first frame update
    void Start()
    {

        currentState = TurnState.Processing;
        Selector.SetActive(false);
        Bsm = GameObject.Find("BattleManager").GetComponent<BattleStateManager>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.Processing):
                updateProgressBar();
                //Debug.Log("Enemy Processing");
                break;

            case (TurnState.ChooseAction):
                ChooseAction();
                currentState = TurnState.Waiting;
               // Debug.Log("Enemy Choose Action");
                break;

            case (TurnState.Waiting):
                //idle state.
              //  Debug.Log("Enemy Waiting");
                break;

            case (TurnState.Action):
              //  Debug.Log("Enemy action");
                
                
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.Dead):
               // Debug.Log("Enemy Dead");
                break;

            
        }
        void updateProgressBar()
        {
            cur_cooldown = cur_cooldown + Time.deltaTime;
           
            if (cur_cooldown >= max_cooldown)
            {
                currentState = TurnState.ChooseAction;
            }


        }
        //enemy choose action.
        void ChooseAction()
        {

            //takes from handle turn script and battle state manager script.
            HandleTurn myAttack = new HandleTurn();

            //still need to make the child class for enemies
            myAttack.Attacker = enemy.theName + "(Clone)"; 
            myAttack.Type = "Enemy";
            myAttack.AttackersGameObject = this.gameObject;
            myAttack.AttackersTarget = Bsm.HerosInBattle[Random.Range(0, Bsm.HerosInBattle.Count)];
            int num = Random.Range(0, enemy.attacks.Count);
            myAttack.chosenAttack = enemy.attacks[num]; // enemy choose random attack from their list.
            Debug.Log(this.gameObject.name + "Has chosen " + myAttack.chosenAttack.abilityName + " and does " + myAttack.chosenAttack.abilityDamage + "damage");
            Bsm.CollectActions(myAttack);
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;

        //animate the enemy near hero to attack
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x-1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
        while (MoveTowardsHero(heroPosition)){yield return null;}


        //wait a bit

        yield return new WaitForSeconds(0.5f);
        //do damage
        doDamage();
        //animate back to start posiiton.
        Vector3 firstPostion = startPosition;
        
        while (MoveTowardsStart(firstPostion))
        {
           Bsm.stopAttackText();
            yield return null;
        }

        //remove action performer from list in Bsm.
        Bsm.PerformList.RemoveAt(0);
       
        
        //reset Bsm -> Wait.
        Bsm.battleState = BattleStateManager.PerformAction.Wait;
        actionStarted = false;
        //reset this enemy state.
        cur_cooldown = 0f;
        currentState = TurnState.Processing;
    }

    private bool MoveTowardsHero(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position,target,animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    void doDamage()
    {
        int calc_damage = enemy.strength + Bsm.PerformList[0].chosenAttack.abilityDamage;
        HeroToAttack.GetComponent<HeroStatesBattle>().TakeDamage(calc_damage);
        HeroToAttack.GetComponent<HeroStatesBattle>().AdjustHerosMinHp();  //subtract heros hp from the total on bar
        HeroToAttack.GetComponent<HeroStatesBattle>().DisplayHeroDamage(calc_damage);
    }
    public void TakeDamage(int getDamageAmount)
    {
        enemy.currHP -= getDamageAmount;

        if (enemy.currHP <= 0)
        {
            currentState = TurnState.Dead;
        }
    }

    public IEnumerator EnemyDamageDisplayOnOff(int getDamageAmount)
    {
        DisplayDamageTaken(getDamageAmount);
        yield return new WaitForSeconds(1);
        displayDamageTaken.SetActive(false);

        Debug.Log("I should subtract " + getDamageAmount);
    }

    public GameObject displayDamageTaken;

    public void DisplayDamageTaken(int getDamageAmount)
    {
        Text txtDamageTaken = displayDamageTaken.GetComponent<Text>();
        txtDamageTaken.text = getDamageAmount.ToString();
        displayDamageTaken.SetActive(true);
    }

    public void DisplayEnemyDamage(int getDamageAmount)
    {
        StartCoroutine(EnemyDamageDisplayOnOff(getDamageAmount));
    }
}
