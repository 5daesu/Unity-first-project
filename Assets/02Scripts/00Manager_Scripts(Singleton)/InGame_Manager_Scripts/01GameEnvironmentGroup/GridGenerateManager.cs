using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerateManager : MonoBehaviour
{
    [SerializeField] private GameObject mapTable;
    [SerializeField] private GameObject gridPrefab;     //for prefabs
    public int row;         //god, work on inspector window
    public int column;      //fuf, work on inspector window
    private GameObject instanceGrid;                    //for instances
    public Node[,] nodeArray;                           //the array is only-one in the game

    void Awake()
    {
        nodeArray = new Node[row, column];      //make memory space to array
        float halfWidth = 1;        //Grid's halfWidth
        float halfHeight = 0.5f;    //Grid's halfHeight

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                nodeArray[i, j] = new Node();   //make memory space to array's data

                float x = 0 - (halfWidth * i) + (halfWidth * j);
                float y = 4f - (halfHeight * i) - (halfHeight * j);   
                Vector3 locFromParent = new Vector3(x, y, 0f);

                instanceGrid = Instantiate(gridPrefab, transform.position + locFromParent, Quaternion.identity, mapTable.transform);    //Instantiate by child of GGM
                //instanceGrid.transform.parent = gameObject.transform;   //make Grid go to parent's(GridGenerateManager) child object
                GameGrid grid = instanceGrid.GetComponent<GameGrid>();
                grid.i_Row = i + 1;        //for 1 ~ n ( not 0 ~ n-1 ) 
                grid.i_Column = j + 1;     //for 1 ~ n
                grid.RefreshSortingOrder();
                nodeArray[i, j].grid = grid;
            }
        }
    }
}
