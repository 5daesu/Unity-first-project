using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObjectManager : MonoBehaviour
{
    public GameObject selectedObject { get; set; }
    [SerializeField] private Color attackRangeColor = new Color(0.45f, 0.45f, 0.45f, 0.25f);
    [SerializeField] private int attackRangeSortingOrder = 9999;

    private Color selectedObjectOriginalColor;
    private const float selectedDarkenAmount = 0.65f;
    private const int AttackRangeSpriteSize = 128;

    private GameObject attackRangeIndicator;
    private SpriteRenderer attackRangeRenderer;

    //bool selected = false;

    void Awake()
    {
        CreateAttackRangeIndicator();
    }

    void Update()
    {
        UpdateAttackRangeIndicatorPosition();
    }

    public void UnSelsectObject()
    {
        if (SingletonTable.singletonTable.soM.selectedObject == null)   //write for -> if there's no ref for selectedObject this method will make error  
        {
            HideAttackRange();
            InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
            return;
        }

        SpriteRenderer spriteRenderer = SingletonTable.singletonTable.soM.selectedObject.GetComponent<SpriteRenderer>(); //for visual effects
        if (spriteRenderer != null) spriteRenderer.color = selectedObjectOriginalColor;
        HideAttackRange();
        //selected = false;
        SingletonTable.singletonTable.soM.selectedObject = null;

        InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
    }

    public void ChangeSelected(GameObject tmp)  //will be used for singleton so it should be public
    {
        if (tmp == null)
        {
            UnSelsectObject();
            return;
        }

        if (tmp == selectedObject)  //if user click same things it should be unselected
        {
            UnSelsectObject();
        }
        else
        {
            UnSelsectObject();
            selectedObject = tmp;
            SpriteRenderer selectedSpriteRenderer = tmp.GetComponent<SpriteRenderer>();    //for visual effects
            if (selectedSpriteRenderer != null)
            {
                selectedObjectOriginalColor = selectedSpriteRenderer.color;
                selectedSpriteRenderer.color = new Color(
                    selectedObjectOriginalColor.r * selectedDarkenAmount,
                    selectedObjectOriginalColor.g * selectedDarkenAmount,
                    selectedObjectOriginalColor.b * selectedDarkenAmount,
                    selectedObjectOriginalColor.a);
            }

            ShowAttackRange(selectedObject);
            //selected = true;
            //StartCoroutine(SelectVFX(selectedSpriteRenderer));
            Debug.Log("New object is Selected");

            InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
        }
    }

    private void CreateAttackRangeIndicator()
    {
        attackRangeIndicator = new GameObject("AttackRangeIndicator");
        attackRangeIndicator.transform.SetParent(transform);
        attackRangeRenderer = attackRangeIndicator.AddComponent<SpriteRenderer>();
        attackRangeRenderer.sprite = CreateCircleSprite();
        attackRangeRenderer.color = attackRangeColor;
        attackRangeRenderer.sortingOrder = attackRangeSortingOrder;
        attackRangeIndicator.SetActive(false);
    }

    private Sprite CreateCircleSprite()
    {
        Texture2D texture = new Texture2D(AttackRangeSpriteSize, AttackRangeSpriteSize);
        Color clear = new Color(1f, 1f, 1f, 0f);
        Color white = Color.white;
        Vector2 center = new Vector2((AttackRangeSpriteSize - 1) * 0.5f, (AttackRangeSpriteSize - 1) * 0.5f);
        float radius = AttackRangeSpriteSize * 0.5f;

        for (int y = 0; y < AttackRangeSpriteSize; y++)
        {
            for (int x = 0; x < AttackRangeSpriteSize; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                texture.SetPixel(x, y, distance <= radius ? white : clear);
            }
        }

        texture.Apply();
        texture.filterMode = FilterMode.Bilinear;
        return Sprite.Create(texture, new Rect(0, 0, AttackRangeSpriteSize, AttackRangeSpriteSize), new Vector2(0.5f, 0.5f), AttackRangeSpriteSize);
    }

    private void ShowAttackRange(GameObject target)
    {
        UnitStatus unitStatus = target.GetComponent<UnitStatus>();
        if (unitStatus == null || unitStatus.attackRange <= 0f)
        {
            HideAttackRange();
            return;
        }

        attackRangeIndicator.transform.position = target.transform.position;
        float diameter = unitStatus.attackRange * 2f;
        attackRangeIndicator.transform.localScale = new Vector3(diameter, diameter, 1f);
        attackRangeIndicator.SetActive(true);
    }

    private void HideAttackRange()
    {
        if (attackRangeIndicator != null) attackRangeIndicator.SetActive(false);
    }

    private void UpdateAttackRangeIndicatorPosition()
    {
        if (selectedObject == null || attackRangeIndicator == null || attackRangeIndicator.activeSelf == false) return;

        attackRangeIndicator.transform.position = selectedObject.transform.position;
    }

    
    /*
    IEnumerator SelectVFX(SpriteRenderer spriteRenderer)
    {
        bool alphaDown = false;
        spriteRenderer.color = new Color(1, 1, 1, 0.25f);

        while (selected)
        {
            if (spriteRenderer.color.a <= 0.5f) alphaDown = false;
            else if (spriteRenderer.color.a >= 1) alphaDown = true;

            if (alphaDown == true) spriteRenderer.color -= new Color(1, 1, 1, 0.6f * Time.deltaTime);
            else spriteRenderer.color += new Color(1, 1, 1, 0.6f * Time.deltaTime);

            yield return null;  // new WaitForSeconds(0.1f);
            if(!selected)
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield break;
            }
        }
    }
    */
}
