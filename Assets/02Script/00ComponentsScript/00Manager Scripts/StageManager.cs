using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public PoolingManager monsterPoolingManager;    //monsterpoolingManager
    public GameObject monsterPortal;
    public GameObject[] stageMonsters;              //row is theme, column is list // theme 1:forest, 2:... // i think [4,5]
    public float breakTime;
    public bool onBreak;
    public List<GameObject> livingMonsters;

    public int curStage;

    GameObject curMonster;
    float checkTime;
    int controlCoroutine = 0;

    void Awake()
    {
        livingMonsters = new List<GameObject>();

        onBreak = false;
        checkTime = 0;      //before stage1, there needs more time
        curStage = 1;       //
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
            Debug.Log("Breaktime");
            onBreak = true;
            InGameUI.inGameUI.mainButton.CheckButtonState(ManagerGrouping.managerGrouping.soM.selectedObject);
        }
        else
        {
            onBreak = false;
            InGameUI.inGameUI.mainButton.CheckButtonState(ManagerGrouping.managerGrouping.soM.selectedObject);  //it should be faster than StartStage() because after running PathFinding() it should be never changed
            Debug.Log(curStage + " Stage Start");
            StartStage();
        }
    }

    void StartStage()
    {
        ManagerGrouping.managerGrouping.rtM.PathFinding();

        curMonster = stageMonsters[curStage - 1];
        int curMonsterCost = curMonster.GetComponent<MonsterStatus>().cost;
        Debug.Log(curMonsterCost);

        StartCoroutine(GenerateMonster(curMonsterCost));
    }

    IEnumerator GenerateMonster(int cmC)
    {
        Debug.Log("Start Coroutine");
        for (int i = 0; i < 100; i += cmC)
        {
            livingMonsters.Add(monsterPoolingManager.GetObject(curStage-1));  //livingMonsters.Add(monsterPoolingManager.GetObject(curStage - 1, gameObject));   //it is not pooling but temporary method
            controlCoroutine += cmC;
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void CheckLeftMonster() //Call when monster die
    {
        if (livingMonsters.Count == 0)
        {
            Debug.Log(curStage + "Stage Clear");
            curStage += 1;
            StateChange();
        }
    }
}