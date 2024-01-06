using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float movingSpeed = 1;

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    /*
    private void OnMouseDown()  //run when mouse click object's collider
    {

    }
    */

    public void MovePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, ManagerGrouping.managerGrouping.rtM.finalNodeList[i + 1].grid.myPosition, movingSpeed * Time.deltaTime);

        if (transform.position == ManagerGrouping.managerGrouping.rtM.finalNodeList[i + 1].grid.myPosition) i++;

        if (transform.position == ManagerGrouping.managerGrouping.rtM.targetNode.grid.myPosition) gameObject.SetActive(false);
    }
}
