using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropManager_DeckBuild : MonoBehaviour
{
    public GameObject blankObject;  //for contain copiedObject who is did

    Grid beforeGrid;
    Grid afterGrid;

    Vector3 mousePosition;
    RaycastHit2D hitCollider;

    bool onDrag;


    void Start()
    {
        onDrag = false;
    }

    void Update()
    {
        UpdateObjectPos();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(mousePosition - new Vector3(0, 0, -0.5f), new Vector3(0, 0, -1f));

            hitCollider = Physics2D.GetRayIntersection(ray);

            if (hitCollider.collider != null)
            {
                GameObject hitObject = hitCollider.transform.gameObject;

                if (hitObject.tag == "Grid")
                {
                    beforeGrid = hitObject.GetComponent<Grid>();

                    if (beforeGrid.castle == false) return;
                    else
                    {
                        if (beforeGrid.summon == false) return;
                        else
                        {
                            blankObject.GetComponent<SpriteRenderer>().sprite = beforeGrid.unit.GetComponent<SpriteRenderer>().sprite;
                            onDrag = true;
                        }
                    }
                }
            }
        }

        if (onDrag == true && Input.GetMouseButtonUp(0))
        {
            //Debug.Log(mousePosition.x + "+" + mousePosition.y + "+" + mousePosition.z);
            Ray ray = new Ray(mousePosition - new Vector3(0, 0, -0.5f), new Vector3(0, 0, -1f));
            //Debug.Log(ray.origin + "+" + ray.direction);

            hitCollider = Physics2D.GetRayIntersection(ray);

            if (hitCollider.collider != null)    //6 is Grid LayerMask
            {
                //Debug.Log("there is RaycastHit");
                GameObject hitObject = hitCollider.transform.gameObject;

                if (hitObject.tag == "Grid")
                {
                    afterGrid = hitObject.GetComponent<Grid>();
                    //Debug.Log(grid.i_Row + "+" + grid.i_Column);

                    if (afterGrid.castle == false)
                    {
                        Debug.Log("Wrong Grid");
                    }
                    else
                    {
                        if (afterGrid.summon == false)
                        {
                            beforeGrid.summon = false;
                            afterGrid.summon = true;
                            beforeGrid.unit.transform.parent = afterGrid.transform;
                            beforeGrid.unit.transform.localPosition = new Vector3(0, 0, 0);
                            afterGrid.unit = beforeGrid.unit;
                            beforeGrid.unit = null;
                        }
                        else
                        {
                            Debug.Log("Theres already unit");
                        }
                    }
                }
            }
            onDrag = false;
            blankObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }   //Update method scope

    void UpdateObjectPos()  //Update Object position by using mouse position
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        blankObject.transform.position = mousePosition;
    }
}
