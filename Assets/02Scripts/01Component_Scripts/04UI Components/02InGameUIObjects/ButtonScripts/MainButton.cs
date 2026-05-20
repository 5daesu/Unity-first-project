using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //when script is for ui, need this namespace 

public class MainButton : MonoBehaviour
{
    Button button;

    public Text buttonText;
    public int buttonState { get; private set; }

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void CheckButtonState(GameObject selctedObject)
    {
        Debug.Log("Call CheckButtonState");
        if (SingletonTable.singletonTable.soM.SelectedUnitCount() > 0)
        {
            if (SingletonTable.singletonTable.soM.HasMergeableSelection()) ChangeButtonState(5);
            else ChangeButtonState(4);
        }
        else if (selctedObject == null)
        {
            ChangeButtonState(0);
        }
        else if (selctedObject.tag == "Grid")
        {
            bool castle = selctedObject.GetComponent<GameGrid>().castle;

            if (castle == false)
            {
                bool onBreak = SingletonTable.singletonTable.gpM.onBreak;
                Node selectedNode = SingletonTable.singletonTable.ggM.nodeArray[selctedObject.GetComponent<GameGrid>().i_Row - 1, selctedObject.GetComponent<GameGrid>().i_Column - 1];
                bool checkPath = SingletonTable.singletonTable.rtM.CheckPath(selectedNode);
                int playerMoney = SingletonTable.singletonTable.piM.playerMoney;
                int castleCost = SingletonTable.singletonTable.piM.castleCost;

                if (onBreak == false || checkPath == false || playerMoney < castleCost) ChangeButtonState(0);
                else ChangeButtonState(1);
            }
            else
            {
                bool summon = selctedObject.GetComponent<GameGrid>().summon;
                int playerMoney = SingletonTable.singletonTable.piM.playerMoney;
                int summonCost = SingletonTable.singletonTable.piM.summonCost;

                if (summon == false)
                {
                    if (playerMoney < summonCost) ChangeButtonState(2);
                    else ChangeButtonState(3);
                }
                else    //This scope can run well because selectedObject is ref value
                {
                    SingletonTable.singletonTable.soM.ChangeSelected(selctedObject.GetComponent<GameGrid>().unit);    //Never write "CheckButtonState(selctedObject);", it makes infinity loof. because it's already there
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
            SingletonTable.singletonTable.soM.UnSelsectObject();
            ChangeButtonState(0);
        }
    }

    public void CheckButtonState()  //Its overloading. When selectedObj didnt change, it should be call
    {
        Debug.Log("Call CheckButtonState");
        GameObject selctedObject = SingletonTable.singletonTable.soM.selectedObject;

        if (SingletonTable.singletonTable.soM.SelectedUnitCount() > 0)
        {
            if (SingletonTable.singletonTable.soM.HasMergeableSelection()) ChangeButtonState(5);
            else ChangeButtonState(4);
        }
        else if (selctedObject == null)
        {
            ChangeButtonState(0);
        }
        else if (selctedObject.tag == "Grid")
        {
            bool castle = selctedObject.GetComponent<GameGrid>().castle;

            if (castle == false)
            {
                bool onBreak = SingletonTable.singletonTable.gpM.onBreak;
                Node selectedNode = SingletonTable.singletonTable.ggM.nodeArray[selctedObject.GetComponent<GameGrid>().i_Row - 1, selctedObject.GetComponent<GameGrid>().i_Column - 1];
                bool checkPath = SingletonTable.singletonTable.rtM.CheckPath(selectedNode);
                int playerMoney = SingletonTable.singletonTable.piM.playerMoney;
                int castleCost = SingletonTable.singletonTable.piM.castleCost;

                if (onBreak == false || checkPath == false || playerMoney < castleCost) ChangeButtonState(0);
                else ChangeButtonState(1);
            }
            else
            {
                bool summon = selctedObject.GetComponent<GameGrid>().summon;
                int playerMoney = SingletonTable.singletonTable.piM.playerMoney;
                int summonCost = SingletonTable.singletonTable.piM.summonCost;

                if (summon == false)
                {
                    if (playerMoney < summonCost) ChangeButtonState(2);
                    else ChangeButtonState(3);
                }
                else    //This scope can run well because selectedObject is ref value
                {
                    SingletonTable.singletonTable.soM.ChangeSelected(selctedObject.GetComponent<GameGrid>().unit);    //Never write "CheckButtonState(selctedObject);" because it's already there
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
            SingletonTable.singletonTable.soM.UnSelsectObject();
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
