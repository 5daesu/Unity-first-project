using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutingManager : MonoBehaviour
{
    public List<Node> finalNodeList;    //save determined route
    public Node startNode, targetNode;
    Node curNode;   //current Node
    List<Node> openList, closeList;

    public bool CheckPath(Node selectedNode)    //true = can go, false = cant go
    {
        selectedNode.grid.castle = true;    //if the grid has caslte

        PathFinding();

        if (finalNodeList.Count == 0)   //there's no path
        {
            selectedNode.grid.castle = false;   //undo supposition
            return false;
        }
        else
        {
            selectedNode.grid.castle = false;
            return true;
        }
    }

    public void PathFinding()   //by A* algorithm
    {
        startNode = ManagerGrouping.managerGrouping.ggM.nodeArray[0, 0];
        targetNode = ManagerGrouping.managerGrouping.ggM.nodeArray[ManagerGrouping.managerGrouping.ggM.row - 1, ManagerGrouping.managerGrouping.ggM.column - 1];

        openList = new List<Node>() { startNode };
        closeList = new List<Node>();
        finalNodeList = new List<Node>();

        while (openList.Count > 0)
        {
            curNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].value_F <= curNode.value_F && openList[i].value_H < curNode.value_H) curNode = openList[i];
            }
            openList.Remove(curNode);
            closeList.Add(curNode);

            if (curNode == targetNode)
            {
                Node targetCurNode = targetNode;
                while (targetCurNode != startNode)
                {
                    finalNodeList.Add(targetCurNode);
                    targetCurNode = targetCurNode.parentNode;
                }
                finalNodeList.Add(startNode);
                finalNodeList.Reverse();

                //for (int i = 0; i < finalNodeList.Count; i++) Debug.Log(i + "번째는" + finalNodeList[i].grid.i_Row + "행" + finalNodeList[i].grid.i_Column + "렬입니다.");
                return;
            }

            OpenListAdd(curNode.grid.i_Row + 1, curNode.grid.i_Column);
            OpenListAdd(curNode.grid.i_Row, curNode.grid.i_Column + 1);
            OpenListAdd(curNode.grid.i_Row - 1, curNode.grid.i_Column);
            OpenListAdd(curNode.grid.i_Row, curNode.grid.i_Column - 1);
        }
    }

    void OpenListAdd(int checkRow, int checkColumn)
    {
        Node neighborNode;
        int moveCost;

        if (!(checkRow < 1) && !(checkColumn < 1) && !(checkRow > ManagerGrouping.managerGrouping.ggM.row) && !(checkColumn > ManagerGrouping.managerGrouping.ggM.column) && ManagerGrouping.managerGrouping.ggM.nodeArray[checkRow - 1, checkColumn - 1].grid.castle == false && !closeList.Contains(ManagerGrouping.managerGrouping.ggM.nodeArray[checkRow - 1, checkColumn - 1]))
        {// checking Point1. is it in array's size   Point2. is there no castle   Point3. is it not in closeList
            neighborNode = ManagerGrouping.managerGrouping.ggM.nodeArray[checkRow - 1, checkColumn - 1];
            moveCost = curNode.value_G + 1;

            if (moveCost < neighborNode.value_G || !openList.Contains(neighborNode))
            {
                neighborNode.value_G = moveCost;
                neighborNode.value_H = Mathf.Abs(neighborNode.grid.i_Row - targetNode.grid.i_Row) + Mathf.Abs(neighborNode.grid.i_Column - targetNode.grid.i_Column);
                neighborNode.parentNode = curNode;

                openList.Add(neighborNode);
            }
        }
    }
}