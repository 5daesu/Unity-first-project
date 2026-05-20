using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitDragManager : MonoBehaviour        //Action about Moving Unit by Drag and Drop
{
    public GameObject blankObject;  //for contain copiedObject who is did

    GameGrid beforeGrid;
    GameGrid afterGrid;

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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = new Ray(mousePosition - new Vector3(0, 0, -0.5f), new Vector3(0, 0, -1f));

                hitCollider = Physics2D.GetRayIntersection(ray);

                if (hitCollider.collider != null)
                {
                    GameObject hitObject = hitCollider.transform.gameObject;

                    if (hitObject.tag == "Grid")
                    {
                        beforeGrid = hitObject.GetComponent<GameGrid>();

                        if (beforeGrid.castle == false) return;
                        else
                        {
                            if (beforeGrid.summon == false) return;
                            else
                            {
                                SpriteRenderer blankSprite = blankObject.GetComponent<SpriteRenderer>();
                                blankSprite.sprite = beforeGrid.unit.GetComponent<SpriteRenderer>().sprite;
                                blankSprite.sortingOrder = 10000;
                                onDrag = true;
                            }
                        }
                    }
                }
            }
        }

        //if (onDrag) UpdateObjectPos();

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
                    afterGrid = hitObject.GetComponent<GameGrid>();
                    //Debug.Log(grid.i_Row + "+" + grid.i_Column);

                    if (afterGrid.castle == false)
                    {
                        Debug.Log("Wrong Grid");
                    }
                    else
                    {
                        if (afterGrid.summon == false)
                        {
                            MoveUnitToEmptyGrid();
                        }
                        else
                        {
                            SwapUnits();
                        }
                    }
                }
            }
            onDrag = false;
            SpriteRenderer blankSprite = blankObject.GetComponent<SpriteRenderer>();
            blankSprite.sprite = null;
            blankSprite.sortingOrder = 0;
        }
    }   //Update method scope

    void UpdateObjectPos()  //Update Object position by using mouse position
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        blankObject.transform.position = mousePosition;
    }

    void MoveUnitToEmptyGrid()
    {
        beforeGrid.summon = false;
        afterGrid.summon = true;
        beforeGrid.unit.transform.parent = afterGrid.transform;
        beforeGrid.unit.transform.localPosition = new Vector3(0, 0, 0);
        afterGrid.unit = beforeGrid.unit;
        beforeGrid.unit = null;
        beforeGrid.RefreshSortingOrder();
        afterGrid.RefreshSortingOrder();
    }

    void SwapUnits()
    {
        if (beforeGrid == afterGrid) return;

        GameObject beforeUnit = beforeGrid.unit;
        GameObject afterUnit = afterGrid.unit;

        beforeGrid.unit = afterUnit;
        afterGrid.unit = beforeUnit;

        beforeUnit.transform.parent = afterGrid.transform;
        beforeUnit.transform.localPosition = new Vector3(0, 0, 0);

        afterUnit.transform.parent = beforeGrid.transform;
        afterUnit.transform.localPosition = new Vector3(0, 0, 0);

        beforeGrid.RefreshSortingOrder();
        afterGrid.RefreshSortingOrder();
    }
}
