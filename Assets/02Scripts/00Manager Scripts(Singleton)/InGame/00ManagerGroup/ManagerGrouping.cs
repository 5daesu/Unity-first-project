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
    public ObjectPoolingManager opM;          //Provide Pooling function

    //These are Related with Player
    public PlayerInfoManager piM;       //Manage Player information, data,
    public SelectedObjectManager soM;   //About Selecting Object

    //These are UserActionGroup
    public KeyInputManager kiM;         //Action about Pressing a Key
    public InputBindingManager ibM;     //Make InputBinding
    public DragAndDropManager ddM;      //Action about Drag and Drop

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
