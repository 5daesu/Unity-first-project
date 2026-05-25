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
    public ObjectPoolingManager monsterPoolingManager;    //monsterpoolingManager
    public GameObject monsterPortal;
    public GameObject[] stageMonsters;              //row is theme, column is list // theme 1:forest, 2:... // i think [4,5]
    public float breakTime;
    public bool onBreak;
    public List<GameObject> livingMonsters;

    public int curRound { get; set; }

    private GameObject curMonster;
    private int controlCoroutine = 0;
    private bool isSpawningWave;

    void Awake()
    {
        livingMonsters = new List<GameObject>();

        onBreak = false;
        curRound = 1;
    }

    void Start()
    {
        gameObject.transform.position = SingletonTable.singletonTable.ggM.nodeArray[0, 0].grid.transform.position;
        monsterPortal.transform.position = monsterPortal.transform.parent.transform.position;

        UpdateRoundWindows();
        StateChange();
    }

    public void StartRoundByUser()
    {
        if (onBreak == false) return;

        StateChange();
    }

    void Update()   //
    {
    }

    void StateChange()
    {
        UpdateRoundWindows();

        if (onBreak == false)
        {
            Debug.Log(curRound + " Round Breaktime");
            onBreak = true;

            SingletonTable.singletonTable.geM.DrawGameEvent();                                                                    //Drawing GameEvent
            SingletonTable.singletonTable.uwM.gameEventWindow.GetComponent<GameEventWindow>().OpenWindow();                       //Setting Active
            
            InGameUI.inGameUI.mainButton.CheckButtonState(SingletonTable.singletonTable.soM.selectedObject);
        }
        else
        {
            onBreak = false;
            InGameUI.inGameUI.mainButton.CheckButtonState(SingletonTable.singletonTable.soM.selectedObject);  //it should be faster than StartStage() because after running PathFinding() it should be never changed
            Debug.Log(curRound + " Wave Start");
            StartWave();
        }
    }

    void StartWave()
    {
        SingletonTable.singletonTable.rtM.PathFinding();

        curMonster = stageMonsters[curRound - 1];
        int curMonsterCost = curMonster.GetComponent<MonsterInfo>().cost;
        Debug.Log(curMonsterCost);

        isSpawningWave = true;
        StartCoroutine(GenerateMonster(curRound, curMonsterCost));
    }

    IEnumerator GenerateMonster(int round, int curMonsterCost)
    {
        for (int i = 0; i < 100; i += curMonsterCost)
        {
            livingMonsters.Add(monsterPoolingManager.GetObject(round - 1));  //livingMonsters.Add(monsterPoolingManager.GetObject(curStage - 1, gameObject));   //it is not pooling but temporary method
            controlCoroutine += curMonsterCost;
            yield return new WaitForSecondsRealtime(1f);
        }

        isSpawningWave = false;
        TryCompleteRound();
    }

    public void CheckLeftMonster() //Call when monster die
    {
        TryCompleteRound();
    }

    private void TryCompleteRound()
    {
        if (isSpawningWave || livingMonsters.Count > 0) return;

        Debug.Log(curRound + "Stage Clear");
        curRound += 1;
        UpdateRoundWindows();
        StateChange();
    }

    private void UpdateRoundWindows()
    {
        if (SingletonTable.singletonTable == null) return;
        if (SingletonTable.singletonTable.uwM == null) return;

        if (SingletonTable.singletonTable.uwM.stageInfoWindow != null)
        {
            SingletonTable.singletonTable.uwM.stageInfoWindow.UpdateWindowContent();
        }
    }
}
