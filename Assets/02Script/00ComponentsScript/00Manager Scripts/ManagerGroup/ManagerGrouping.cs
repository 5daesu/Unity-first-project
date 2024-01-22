using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGrouping : MonoBehaviour
{
    public static ManagerGrouping managerGrouping;
    public GridGenerateManager ggM;     // ManagerGrouping.managerGruoping.ggM.~
    public SelectedObjectManager soM;   // ManagerGrouping.managerGruoping.soM.~
    public RoutingManager rtM;          // ManagerGrouping.managerGruoping.rtM.~
    public PoolingManager opM;          //
    public StageManager stM;            //
    public PlayerActionManager paM;           //
    public DragAndDropManager ddM;      //

    void Awake()    //for singleton
    {
        if (ManagerGrouping.managerGrouping == null)
        {
            ManagerGrouping.managerGrouping = this;
        }
    }
}
