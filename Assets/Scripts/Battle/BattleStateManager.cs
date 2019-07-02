using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleStateManager : MonoBehaviour
{

    public BaseHero hero;

    public int howManyEnemies = 0;
    // Start is called before the first frame update
    // Start is called before the first frame update

    public enum PerformAction
    {
        Wait, TakeAction, PerformAction
    }

    public PerformAction battleState;

    public List<HandleTurn> PerformList = new List<HandleTurn>();
    public List<GameObject> HerosInBattle = new List<GameObject>();

    public List<GameObject> EnemiesInBattle = new List<GameObject>();
    public List<GameObject> LearnedAbilities = new List<GameObject>();


    public enum HeroGui
    {
        Activate, Wait, Input1,Input2,Done 
    }

    public HeroGui HeroInput;

    public List<GameObject> HerosToManage = new List<GameObject>();
    private HandleTurn TargetSelection;
    public GameObject enemyButtons;
    public GameObject abilityButtons;
    public Transform Spacer;
    public GameObject AttackPanel;
    public GameObject EnemySelectPanel;

    void Start()
    {
        battleState = PerformAction.Wait;
        EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        HeroInput = HeroGui.Activate;

        ShowAttackPanel.SetActive(false);
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        EnemyButtons();
       

    }

    // Update is called once per frame
    void Update()
    {
        switch (battleState)
        {
            case (PerformAction.Wait):
                if (PerformList.Count > 0)
                {
                    battleState = PerformAction.TakeAction;
                }
                break;

            case (PerformAction.TakeAction):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if (PerformList[0].Type == "Enemy")
                {
                    EnemyStatesBattle Esm = performer.GetComponent<EnemyStatesBattle>();
                    Esm.HeroToAttack = PerformList[0].AttackersTarget;
                    displayAttack();
                    Esm.currentState = EnemyStatesBattle.TurnState.Action;
                   
                }

                if (PerformList[0].Type == "Hero")
                {
                    HeroStatesBattle Hsb = performer.GetComponent<HeroStatesBattle>();
                    Hsb.EnemyToAttack = PerformList[0].AttackersTarget;
                    Hsb.currentState = HeroStatesBattle.TurnState.Action;
                    
                }

                battleState = PerformAction.PerformAction;
                break;

            case (PerformAction.PerformAction):
                
                break;

        }

        switch (HeroInput)
        {
            case (HeroGui.Activate):
                if (HerosToManage.Count > 0)
                {
                    HerosToManage[0].transform.FindChild("Selector").gameObject.SetActive(true);
                    TargetSelection = new HandleTurn();
                    AttackPanel.SetActive(true);
                    HeroInput = HeroGui.Wait;
                }
                break;

            case (HeroGui.Wait):
                
                break;

            case (HeroGui.Done):
                HeroInputDone();
                break;

            case (HeroGui.Input1):

                break;

            case (HeroGui.Input2):

                break;
        }
    }

    public void CollectActions(HandleTurn input)
    {

        
        PerformList.Add(input);
    }
    void EnemyButtons()
    {
        foreach (GameObject enemy in EnemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButtons) as GameObject;
            SelectTargetButton button = newButton.GetComponent<SelectTargetButton>();

            EnemyStatesBattle cur_enemy = enemy.GetComponent<EnemyStatesBattle>();

            Text ButtonText = newButton.transform.FindChild("Text").gameObject.GetComponent<Text>();

            ButtonText.text = cur_enemy.enemy.theName;

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(Spacer);
        }
    }

    void AbilityButtons()
    {
        foreach (GameObject ability in LearnedAbilities)
        {
            GameObject newButton = Instantiate(abilityButtons) as GameObject;
            SelectAbilityButton button = newButton.GetComponent<SelectAbilityButton>();

        }
    }

    
    public void Input1() //attack button.
    {
        BasicAttack attack = new BasicAttack();    // call script for a basic attack.
        
        Debug.Log("Hero attck size is:" + hero.attacks.Count);
        TargetSelection.Attacker = HerosToManage[0].name;
        TargetSelection.AttackersGameObject = HerosToManage[0];
        TargetSelection.chosenAttack = attack;
       // TargetSelection.chosenAttack = hero.attacks[0];
        TargetSelection.Type = "Hero";
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }

    public void Input2(GameObject chosenEnemy)// enemy selection.
    {
        TargetSelection.AttackersTarget = chosenEnemy;
        HeroInput = HeroGui.Done;
    }

    public void HeroInputDone()
    {
        PerformList.Add(TargetSelection);
        EnemySelectPanel.SetActive(false);
        HerosToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
        HerosToManage.RemoveAt(0);
        HeroInput = HeroGui.Activate;
    }

    //Display Current attack on top panel.
    public GameObject showAttackText;
    public GameObject ShowAttackPanel;
    public void displayAttack()
    {
        
        if (PerformList[0].chosenAttack.abilityName != "Attack")
        {
            ShowAttackPanel.SetActive(true);
            showAttackText.SetActive(true);
            Text attackDiaplayPanelText = showAttackText.GetComponent<Text>();
            attackDiaplayPanelText.text = PerformList[0].chosenAttack.abilityName.ToString();
        }
    }

    public void stopAttackText()
    {
        showAttackText.SetActive(false);
        ShowAttackPanel.SetActive(false);
    }

}
