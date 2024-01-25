using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //when script is for ui, need this namespace 

public class MainButton : MonoBehaviour
{
    Button button;

    public Text buttonText;
    public int buttonState;

    void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void CheckButtonState(GameObject selctedObject)
    {
        Debug.Log("Call CheckButtonState");
        if (selctedObject == null)
        {
            ChangeButtonState(0);
        }
        else if (selctedObject.tag == "Grid")
        {
            bool castle = selctedObject.GetComponent<Grid>().castle;

            if (castle == false)
            {
                bool onBreak = ManagerGrouping.managerGrouping.stM.onBreak;
                Node selectedNode = ManagerGrouping.managerGrouping.ggM.nodeArray[selctedObject.GetComponent<Grid>().i_Row - 1, selctedObject.GetComponent<Grid>().i_Column - 1];
                bool checkPath = ManagerGrouping.managerGrouping.rtM.CheckPath(selectedNode);
                int playerMoney = ManagerGrouping.managerGrouping.plM.pim.playerMoney;
                int castleCost = ManagerGrouping.managerGrouping.plM.pim.castleCost;

                if (onBreak == false || checkPath == false || playerMoney < castleCost) ChangeButtonState(0);
                else ChangeButtonState(1);
            }
            else
            {
                bool summon = selctedObject.GetComponent<Grid>().summon;
                int playerMoney = ManagerGrouping.managerGrouping.plM.pim.playerMoney;
                int summonCost = ManagerGrouping.managerGrouping.plM.pim.summonCost;

                if (summon == false)
                {
                    if (playerMoney < summonCost) ChangeButtonState(2);
                    else ChangeButtonState(3);
                }
                else    //This scope can run well because selectedObject is ref value
                {
                    ManagerGrouping.managerGrouping.soM.ChangeSelected(selctedObject.GetComponent<Grid>().unit);    //Never write "CheckButtonState(selctedObject);", it makes infinity loof. because it's already there 
                }
            }
        }
        else if (selctedObject.tag == "Unit")
        {

            //if(cant merge)ChangeButtonState(5)
            ChangeButtonState(4);
            //else
            //ChangeButtonState(5)
        }
        else
        {
            ManagerGrouping.managerGrouping.soM.UnSelsectObject();
            ChangeButtonState(0);
        }
    }

    public void CheckButtonState()  //Its overloading. When selectedObj didnt change, it should be call
    {
        Debug.Log("Call CheckButtonState");
        GameObject selctedObject = ManagerGrouping.managerGrouping.soM.selectedObject;

        if (selctedObject == null)
        {
            ChangeButtonState(0);
        }
        else if (selctedObject.tag == "Grid")
        {
            bool castle = selctedObject.GetComponent<Grid>().castle;

            if (castle == false)
            {
                bool onBreak = ManagerGrouping.managerGrouping.stM.onBreak;
                Node selectedNode = ManagerGrouping.managerGrouping.ggM.nodeArray[selctedObject.GetComponent<Grid>().i_Row - 1, selctedObject.GetComponent<Grid>().i_Column - 1];
                bool checkPath = ManagerGrouping.managerGrouping.rtM.CheckPath(selectedNode);
                int playerMoney = ManagerGrouping.managerGrouping.plM.pim.playerMoney;
                int castleCost = ManagerGrouping.managerGrouping.plM.pim.castleCost;

                if (onBreak == false || checkPath == false || playerMoney < castleCost) ChangeButtonState(0);
                else ChangeButtonState(1);
            }
            else
            {
                bool summon = selctedObject.GetComponent<Grid>().summon;
                int playerMoney = ManagerGrouping.managerGrouping.plM.pim.playerMoney;
                int summonCost = ManagerGrouping.managerGrouping.plM.pim.summonCost;

                if (summon == false)
                {
                    if (playerMoney < summonCost) ChangeButtonState(2);
                    else ChangeButtonState(3);
                }
                else    //This scope can run well because selectedObject is ref value
                {
                    ManagerGrouping.managerGrouping.soM.ChangeSelected(selctedObject.GetComponent<Grid>().unit);    //Never write "CheckButtonState(selctedObject);" because it's already there 
                }
            }
        }
        else if (selctedObject.tag == "Unit")
        {

            //if(cant merge)ChangeButtonState(5)
            ChangeButtonState(4);
            //else
            //ChangeButtonState(5)
        }
        else
        {
            ManagerGrouping.managerGrouping.soM.UnSelsectObject();
            ChangeButtonState(0);
        }
    }

    private void ChangeButtonState(int i)
    {
        if (i == 0)
        {
            buttonState = 0;
            buttonText.text = "건설";
            button.interactable = false;
        }
        else if (i == 1)
        {
            buttonState = 1;
            buttonText.text = "건설";
            button.interactable = true;
        }
        else if (i == 2)
        {
            buttonState = 2;
            buttonText.text = "소환";
            button.interactable = false;
        }
        else if (i == 3)
        {
            buttonState = 3;
            buttonText.text = "소환";
            button.interactable = true;
        }
        else if (i == 4)
        {
            buttonState = 4;
            buttonText.text = "조합";
            button.interactable = false;
        }
        else if (i == 5)
        {
            buttonState = 5;
            buttonText.text = "조합";
            button.interactable = true;
        }
        else
        {
            i = 0;
            ChangeButtonState(i);
        }
    }
}
