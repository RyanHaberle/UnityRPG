using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroStatesBattle : MonoBehaviour
    {

    public BaseHero hero;
    private BattleStateManager Bsm;
    

    public enum TurnState
    {
        Processing, AddToList, Waiting, Action, Dead, Selecting
    }

    public TurnState currentState;  //Keeps track of the state the hero unit is in, eg Dead\Alive, In action.
    //for progress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image ActionBar;
    public GameObject Selector;
    //IEnumerator stuff

    public GameObject EnemyToAttack;
    private bool actionStarted;
    private Vector3 startPosition;
    private float animSpeed = 10f;



    // Start is called before the first frame update
    void Start()
    {
        AdjustHerosMinHp();
        AdjustHerosMaxHP();
        startPosition = transform.position;
        Selector.SetActive(false);
        Bsm = GameObject.Find("BattleManager").GetComponent<BattleStateManager>();
        currentState = TurnState.Processing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.Processing):
                updateProgressBar();
                break;

            case (TurnState.AddToList):
                Bsm.HerosToManage.Add(this.gameObject);
                currentState = TurnState.Waiting;
                break;

            case (TurnState.Waiting):
                //wait for inputs
                break;

            case (TurnState.Action):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.Dead):

                break;

            case (TurnState.Selecting):

                break;
        }   
        void updateProgressBar()
        {
            cur_cooldown = cur_cooldown + Time.deltaTime;
            float calc_cooldown = cur_cooldown / max_cooldown;
            ActionBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ActionBar.transform.localScale.y, ActionBar.transform.localScale.z);
            if (cur_cooldown >= max_cooldown)
            {
                currentState = TurnState.AddToList;
            }

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
        Vector3 enemyPosition = new Vector3(EnemyToAttack.transform.position.x + 1.5f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
        while (MoveTowardsEnemy(enemyPosition)) { yield return null; }


        //wait a bit

        yield return new WaitForSeconds(0.5f);
        //do damage
        //Debug.Log("Method started");
        doDamage();
        //animate back to start posiiton.
        Vector3 firstPostion = startPosition;
        while (MoveTowardsStart(firstPostion)) { yield return null; }

        //remove action performer from list in Bsm.
        Bsm.PerformList.RemoveAt(0);


        //reset Bsm -> Wait.
        Bsm.battleState = BattleStateManager.PerformAction.Wait;
        actionStarted = false;
        //reset this enemy state.
        cur_cooldown = 0f;
        currentState = TurnState.Processing;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    void doDamage()
    {
        Debug.Log("Do Damage entered");
         Debug.Log("STR: " + hero.strength);
        //Debug.Log("ability: " + Bsm.PerformList[0].chosenAttack.abilityDamage); // add hero ability to ability list.
        int calc_damage = hero.strength + Bsm.PerformList[0].chosenAttack.abilityDamage;
        EnemyToAttack.GetComponent<EnemyStatesBattle>().TakeDamage(calc_damage);
        EnemyToAttack.GetComponent<EnemyStatesBattle>().DisplayEnemyDamage(calc_damage);
        //adjust enemy hp and mp once sorted on the battle hud.
        
    }

    public void TakeDamage(int getDamageAmount)
    {
        hero.currHP -= getDamageAmount;
       if (hero.currHP <= 0)
        {
            currentState = TurnState.Dead;
        }
    }

    //Adjust UI Hero Texts...................................................................................................................................Adjust Hero UI Texts...........................................

    public GameObject displayDamageTaken;

    public void DisplayDamageTaken(int getDamageAmount)
    {
        Text txtDamageTaken = displayDamageTaken.GetComponent<Text>();
        txtDamageTaken.text = getDamageAmount.ToString();
        displayDamageTaken.SetActive(true);
        //Debug.Log("I should subtract " + getDamageAmount);
    }

    public IEnumerator HeroDamageDisplayOnOff(int getDamageAmount)
    {
        DisplayDamageTaken(getDamageAmount);
        
        yield return new WaitForSeconds(1);
        
        displayDamageTaken.SetActive(false);
    }

    public void DisplayHeroDamage(int getDamageAmount)
    {
        StartCoroutine(HeroDamageDisplayOnOff(getDamageAmount));
    }


    public GameObject editHeroMinHp; // for the text on the battle hero bar.
    public GameObject editHerosMaxHP; //for the battle hero bar


    public void AdjustHerosMinHp()  // adjusts heros HP display on the HEro Bar
    {
        Text minHPText = editHeroMinHp.GetComponent<Text>();
        minHPText.text = hero.currHP.ToString();
    }

    public void AdjustHerosMaxHP()   // use when heros level up to change their hp on the battleField
    {
        Text maxHPText = editHerosMaxHP.GetComponent<Text>();
        maxHPText.text = hero.maxHP.ToString();
    }

    
    
}
