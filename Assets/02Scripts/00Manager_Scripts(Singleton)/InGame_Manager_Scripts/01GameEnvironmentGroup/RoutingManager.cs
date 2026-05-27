using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutingManager : MonoBehaviour
{
    public List<Node> finalNodeList { get; set; }   //save determined route

    public Node startNode { get; set; }
    public Node targetNode { get; set; }

    private Node curNode;                       //current Node
    private List<Node> openList, closeList;

    [Header("Route Preview")]
    [SerializeField] private bool showRoutePreview = true;
    [SerializeField] private Color actualRouteColor = new Color(0.1f, 0.85f, 1f, 0.95f);
    [SerializeField] private Color supposedRouteColor = new Color(1f, 0.85f, 0.1f, 0.35f);
    [SerializeField] private float routeLineWidth = 0.08f;
    [SerializeField] private int routeSortingOrder = 10000;
    [SerializeField] private float routeZOffset = -0.05f;

    private LineRenderer actualRouteLine;
    private LineRenderer supposedRouteLine;
    private Material routeLineMaterial;

    void Awake()
    {
        CreateRouteLines();
    }

    void Start()
    {
        RefreshRoutePreview();
    }

    public bool CheckPath(Node selectedNode)    //true = can go, false = cant go
    {
        return FindPath(selectedNode).Count > 0;
    }

    public void PathFinding()   //by A* algorithm
    {
        finalNodeList = FindPath(null);
        RefreshRoutePreview();
    }

    public void SupposedPathFinding()   //for build castle, check something block to targetNode by supposing
    {
        RefreshRoutePreview();
    }

    public void RefreshRoutePreview()
    {
        if (showRoutePreview == false)
        {
            HideRouteLine(actualRouteLine);
            HideRouteLine(supposedRouteLine);
            return;
        }

        List<Node> actualPath = finalNodeList;
        if (SingletonTable.singletonTable != null && SingletonTable.singletonTable.gpM != null && SingletonTable.singletonTable.gpM.onBreak)
        {
            actualPath = FindPath(null);
        }

        DrawRouteLine(actualRouteLine, actualPath);
        DrawSupposedRoutePreview();
    }

    private List<Node> FindPath(Node blockedNode)
    {
        startNode = SingletonTable.singletonTable.ggM.nodeArray[0, 0];
        targetNode = SingletonTable.singletonTable.ggM.nodeArray[SingletonTable.singletonTable.ggM.row - 1, SingletonTable.singletonTable.ggM.column - 1];

        List<Node> path = new List<Node>();
        if (startNode == blockedNode || startNode.grid.castle == true) return path;  //ban building in startNode
        if (targetNode == blockedNode || targetNode.grid.castle == true) return path;

        ResetNodes();
        startNode.value_G = 0;

        openList = new List<Node>() { startNode };
        closeList = new List<Node>();

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
                    path.Add(targetCurNode);
                    targetCurNode = targetCurNode.parentNode;
                }
                path.Add(startNode);
                path.Reverse();

                return path;
            }

            OpenListAdd(curNode.grid.i_Row + 1, curNode.grid.i_Column, blockedNode);
            OpenListAdd(curNode.grid.i_Row, curNode.grid.i_Column + 1, blockedNode);
            OpenListAdd(curNode.grid.i_Row - 1, curNode.grid.i_Column, blockedNode);
            OpenListAdd(curNode.grid.i_Row, curNode.grid.i_Column - 1, blockedNode);
        }

        return path;
    }

    void OpenListAdd(int checkRow, int checkColumn, Node blockedNode)
    {
        Node neighborNode;
        int moveCost;

        if (!(checkRow < 1) && !(checkColumn < 1) && !(checkRow > SingletonTable.singletonTable.ggM.row) && !(checkColumn > SingletonTable.singletonTable.ggM.column) && SingletonTable.singletonTable.ggM.nodeArray[checkRow - 1, checkColumn - 1].grid.castle == false && !closeList.Contains(SingletonTable.singletonTable.ggM.nodeArray[checkRow - 1, checkColumn - 1]))
        {// checking Point1. is it in array's size   Point2. is there no castle   Point3. is it not in closeList
            neighborNode = SingletonTable.singletonTable.ggM.nodeArray[checkRow - 1, checkColumn - 1];
            if (neighborNode == blockedNode) return;

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

    private void ResetNodes()
    {
        for (int i = 0; i < SingletonTable.singletonTable.ggM.row; i++)
        {
            for (int j = 0; j < SingletonTable.singletonTable.ggM.column; j++)
            {
                Node node = SingletonTable.singletonTable.ggM.nodeArray[i, j];
                node.value_G = int.MaxValue;
                node.value_H = 0;
                node.parentNode = null;
            }
        }
    }

    private void DrawSupposedRoutePreview()
    {
        if (SingletonTable.singletonTable == null || SingletonTable.singletonTable.soM == null)
        {
            HideRouteLine(supposedRouteLine);
            return;
        }

        GameObject selectedObject = SingletonTable.singletonTable.soM.selectedObject;
        if (selectedObject == null || selectedObject.CompareTag("Grid") == false)
        {
            HideRouteLine(supposedRouteLine);
            return;
        }

        GameGrid selectedGrid = selectedObject.GetComponent<GameGrid>();
        if (selectedGrid == null || selectedGrid.castle == true)
        {
            HideRouteLine(supposedRouteLine);
            return;
        }

        Node selectedNode = SingletonTable.singletonTable.ggM.nodeArray[selectedGrid.i_Row - 1, selectedGrid.i_Column - 1];
        DrawRouteLine(supposedRouteLine, FindPath(selectedNode));
    }

    private void CreateRouteLines()
    {
        routeLineMaterial = new Material(Shader.Find("Sprites/Default"));
        actualRouteLine = CreateRouteLine("ActualRouteLine", actualRouteColor);
        supposedRouteLine = CreateRouteLine("SupposedRouteLine", supposedRouteColor);
    }

    private LineRenderer CreateRouteLine(string lineName, Color lineColor)
    {
        GameObject routeLineObject = new GameObject(lineName);
        routeLineObject.transform.SetParent(transform);

        LineRenderer lineRenderer = routeLineObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = routeLineMaterial;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = routeLineWidth;
        lineRenderer.endWidth = routeLineWidth;
        lineRenderer.numCapVertices = 4;
        lineRenderer.numCornerVertices = 4;
        lineRenderer.sortingOrder = routeSortingOrder;
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;

        return lineRenderer;
    }

    private void DrawRouteLine(LineRenderer lineRenderer, List<Node> path)
    {
        if (lineRenderer == null || path == null || path.Count < 2)
        {
            HideRouteLine(lineRenderer);
            return;
        }

        lineRenderer.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 position = path[i].grid.transform.position;
            position.z += routeZOffset;
            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.enabled = true;
    }

    private void HideRouteLine(LineRenderer lineRenderer)
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
    }
}
