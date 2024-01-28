using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
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

    public void BuildCastle()  //construct castle in grid
    {
        castle = true;
        gridsprite.sprite = castlesprite;
    }

    public void Summon()    //summon unit on castle
    {
        Debug.Log("½ÇÇàµÊ");
        int i = Random.Range(0, 5);
        summon = true;
        unit = Instantiate(Deck.playerDeck.deckList[i], transform.position, Quaternion.identity, transform);
    }
    
    private void OnMouseOver()  //run when mouse is over object's collider
    {
        
    }

    private void OnMouseDown()  //run when mouse click object's collider
    {
        if (summon == false)
        {
            ManagerGrouping.managerGrouping.soM.ChangeSelected(gameObject);
        }
        else
        {
            ManagerGrouping.managerGrouping.soM.ChangeSelected(unit);
        }
        Debug.Log(i_Row + "Çà " + i_Column + "·Ä");
    }
}
