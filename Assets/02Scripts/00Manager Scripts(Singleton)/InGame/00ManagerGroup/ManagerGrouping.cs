using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGrouping : MonoBehaviour
{
    public static ManagerGrouping managerGrouping;  //ManagerGrouping is singleton

    //These are GameEnvironmentGroup
    public GridGenerateManager ggM;     //Make Grids
    public GameProgressManager gpM;     //Manage GameProgress
    public GameEventManager geM;        //Manage GameEvents
    public RoutingManager rtM;          //Calculate Route
    public PoolingManager opM;          //Provide Pooling function

    //These are Related with Player
    public PlayerInfoManager piM;       //Manage Player information, data, 
    //These are PlayerActionGroup
    public KeyInputManager kiM;         //Action about Pressing a Key
    public DragAndDropManager ddM;      //Action about Drag and Drop
    public SelectedObjectManager soM;   //Action about Selecting Object

    //These are UIManagerGroup
    public UiWindowManager uwM;


    void Awake()    //for singleton
    {
        if (ManagerGrouping.managerGrouping == null)
        {
            ManagerGrouping.managerGrouping = this;
        }
    }
}
