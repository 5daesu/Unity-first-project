using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Explanation of Class's term
//
//  1. Round = Wave + BreakTime  //  ex) 6th stage = 6th wave + 6th breaktime
//  2. Breaktime = GameEvent + The time user can build castle
//  3. Wave is monsterwave
//  4. prior breaktime( 0th breaktime ) -> 1st wave -> 1st breaktime -> 2nd wave -> 2nd breaktime...

public class GameProgressManager : MonoBehaviour
{
    public PoolingManager monsterPoolingManager;    //monsterpoolingManager
    public GameObject monsterPortal;
    public GameObject[] stageMonsters;              //row is theme, column is list // theme 1:forest, 2:... // i think [4,5]
    public float breakTime;
    public bool onBreak;
    public List<GameObject> livingMonsters;

    public int curRound { get; set; }

    private GameObject curMonster;
    private float checkTime;
    private int controlCoroutine = 0;

    void Awake()
    {
        livingMonsters = new List<GameObject>();

        onBreak = false;
        checkTime = 0;      //before stage1, there needs more time
        curRound = 1;       //
    }

    void Start()
    {
        gameObject.transform.position = ManagerGrouping.managerGrouping.ggM.nodeArray[0, 0].grid.transform.position;
        monsterPortal.transform.position = monsterPortal.transform.parent.transform.position;

        StateChange();
    }

    void Update()   //
    {
        if (onBreak == true)
        {
            checkTime += Time.deltaTime;
            if (checkTime > breakTime)
            {
                checkTime = 0;
                StateChange();
            }
        }
    }

    void StateChange()
    {
        if (onBreak == false)
        {
            Debug.Log(curRound + " Round Breaktime");
            onBreak = true;

            ManagerGrouping.managerGrouping.geM.DrawGameEvent();                                                                    //Drawing GameEvent
            ManagerGrouping.managerGrouping.uwM.gameEventWindow.GetComponent<GameEventWindow>().OpenWindow();                       //Setting Active
            
            InGameUI.inGameUI.mainButton.CheckButtonState(ManagerGrouping.managerGrouping.soM.selectedObject);
        }
        else
        {
            onBreak = false;
            InGameUI.inGameUI.mainButton.CheckButtonState(ManagerGrouping.managerGrouping.soM.selectedObject);  //it should be faster than StartStage() because after running PathFinding() it should be never changed
            Debug.Log(curRound + " Wave Start");
            StartWave();
        }
    }

    void StartWave()
    {
        ManagerGrouping.managerGrouping.rtM.PathFinding();

        curMonster = stageMonsters[curRound - 1];
        int curMonsterCost = curMonster.GetComponent<MonsterInfo>().cost;
        Debug.Log(curMonsterCost);

        StartCoroutine(GenerateMonster(curMonsterCost));
    }

    IEnumerator GenerateMonster(int curMonsterCost)
    {
        for (int i = 0; i < 100; i += curMonsterCost)
        {
            livingMonsters.Add(monsterPoolingManager.GetObject(curRound - 1));  //livingMonsters.Add(monsterPoolingManager.GetObject(curStage - 1, gameObject));   //it is not pooling but temporary method
            controlCoroutine += curMonsterCost;
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void CheckLeftMonster() //Call when monster die
    {
        if (livingMonsters.Count == 0)
        {
            Debug.Log(curRound + "Stage Clear");
            curRound += 1;
            StateChange();
        }
    }
}