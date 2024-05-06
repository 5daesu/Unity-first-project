using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGrouping : MonoBehaviour
{
    public static ManagerGrouping managerGrouping;  //ManagerGrouping is singleton

    //These Group are Related with GameEnvironment
    public GridGenerateManager ggM;     //Make Grids
    public GameProgressManager gpM;     //Manage GameProgress
    public GameEventManager geM;        //Manage GameEvents
    public RoutingManager rtM;          //Calculate Route
    public ObjectPoolingManager opM;    //Provide Pooling function

    //These Group are Related with GamePlayer
    public PlayerInfoManager piM;       //Manage Player information, data,
    public SelectedObjectManager soM;   //About Selecting Object

    //These Group are Related with UserAction
    public KeyInputManager kiM;         //Action about Pressing a Key
    public KeyBindingManager kbM;       //About KeyBinding
    public UnitDragManager udM;         //Action about Drag and Drop Unit

    //These Group are Related with UserInterface
    public UiWindowManager uwM;         //


    void Awake()    //for singleton
    {
        if (ManagerGrouping.managerGrouping == null)
        {
            ManagerGrouping.managerGrouping = this;
        }
    }
}
