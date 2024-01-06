using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node   // for A* algorithm
{
    public Grid grid;
    public int value_G, value_H;    // moved distance, rest of distance
    public Node parentNode;

    public int value_F { get { return value_G + value_H; } }
}