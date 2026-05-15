using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    const int SortingOrderScale = 10;
    const int UnitSortingOffset = 1;

    public bool castle = false;
    public bool summon = false;
    public GameObject unit;
    public int i_Row;           // instance's row       range : 1 ~ n ( not 0 ~ n-1 )
    public int i_Column;        // instance's column    range : 1 ~ n

    SpriteRenderer gridsprite;
    public Sprite castlesprite;     // work on inspector window
    //public Vector3 myPosition;      // object own self's position

    void Awake()
    {
        gridsprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public int GetBaseSortingOrder()
    {
        return (i_Row + i_Column) * SortingOrderScale;
    }

    public int GetObjectSortingOrder(int offset)
    {
        return GetBaseSortingOrder() + offset;
    }

    public void RefreshSortingOrder()
    {
        if (gridsprite == null) gridsprite = gameObject.GetComponent<SpriteRenderer>();

        gridsprite.sortingOrder = GetBaseSortingOrder();

        if (unit != null)
        {
            SpriteRenderer unitSprite = unit.GetComponent<SpriteRenderer>();
            if (unitSprite != null) unitSprite.sortingOrder = GetObjectSortingOrder(UnitSortingOffset);
        }
    }

    public void BuildCastle()  //construct castle in grid
    {
        castle = true;
        gridsprite.sprite = castlesprite;
        RefreshSortingOrder();
    }

    public void Summon()    //summon unit on castle
    {
        Debug.Log("½ÇÇàµÊ");
        int i = Random.Range(0, 5);
        summon = true;
        unit = Instantiate(SingletonTable.singletonTable.piM.playerDeck.Summon(), transform.position, Quaternion.identity, transform);
        RefreshSortingOrder();
    }
    
    private void OnMouseOver()  //run when mouse is over object's collider
    {
        
    }

    private void OnMouseDown()  //run when mouse click object's collider
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (summon == false) SingletonTable.singletonTable.soM.ChangeSelected(gameObject);
                else SingletonTable.singletonTable.soM.ChangeSelected(unit);
                Debug.Log(i_Row + "Çà " + i_Column + "·Ä");
            }
        }
    }
}
