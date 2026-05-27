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
    private const int AttackRangeSpriteSize = 512;
    private const float AttackRangeEdgeWidth = 3f;
    private const float IsometricRangeYScale = 0.5f;

    private GameObject attackRangeIndicator;
    private SpriteRenderer attackRangeRenderer;
    private List<GameObject> selectedUnits = new List<GameObject>();
    private Dictionary<GameObject, Color> selectedObjectOriginalColors = new Dictionary<GameObject, Color>();

    //bool selected = false;

    void Awake()
    {
        CreateAttackRangeIndicator();
    }

    void Update()
    {
        UpdateAttackRangeIndicatorPosition();
    }

    public void UnSelectObject()
    {
        if (SingletonTable.singletonTable.soM.selectedObject == null && selectedUnits.Count == 0)
        {
            HideAttackRange();
            InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
            return;
        }

        RestoreSelectedObjectColor(SingletonTable.singletonTable.soM.selectedObject);

        foreach (GameObject selectedUnit in selectedUnits)
        {
            RestoreSelectedObjectColor(selectedUnit);
        }

        selectedUnits.Clear();
        HideAttackRange();
        //selected = false;
        SingletonTable.singletonTable.soM.selectedObject = null;
        SingletonTable.singletonTable.rtM.RefreshRoutePreview();

        InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
    }

    public void ChangeSelected(GameObject tmp)  //will be used for singleton so it should be public
    {
        if (tmp == null)
        {
            UnSelectObject();
            return;
        }

        if (tmp.CompareTag("Unit"))
        {
            ToggleUnitSelected(tmp);
            return;
        }

        if (tmp == selectedObject)  //if user click same things it should be unselected
        {
            UnSelectObject();
        }
        else
        {
            UnSelectObject();
            selectedObject = tmp;
            DarkenSelectedObject(tmp);

            ShowAttackRange(selectedObject);
            SingletonTable.singletonTable.rtM.RefreshRoutePreview();

            InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
        }
    }

    public int SelectedUnitCount()
    {
        return selectedUnits.Count;
    }

    public List<GameObject> GetSelectedUnits()
    {
        return new List<GameObject>(selectedUnits);
    }

    public List<UnitData> GetSelectedUnitDataList()
    {
        List<UnitData> unitDataList = new List<UnitData>();

        foreach (GameObject selectedUnit in selectedUnits)
        {
            UnitStatus unitStatus = selectedUnit.GetComponent<UnitStatus>();
            if (unitStatus == null || unitStatus.unitData == null) continue;

            unitDataList.Add(unitStatus.unitData);
        }

        return unitDataList;
    }

    public bool HasMergeableSelection()
    {
        if (selectedUnits.Count < 1) return false;

        UnitData result;
        return SingletonTable.singletonTable.piM.TryFindMergeResult(GetSelectedUnitDataList(), out result);
    }

    private void ToggleUnitSelected(GameObject unit)
    {
        if (selectedUnits.Contains(unit))
        {
            selectedUnits.Remove(unit);
            RestoreSelectedObjectColor(unit);
        }
        else
        {
            if (selectedObject != null && selectedObject.CompareTag("Unit") == false)
            {
                RestoreSelectedObjectColor(selectedObject);
            }

            selectedUnits.Add(unit);
            DarkenSelectedObject(unit);
        }

        selectedObject = selectedUnits.Count > 0 ? selectedUnits[selectedUnits.Count - 1] : null;
        ShowAttackRange(selectedObject);
        SingletonTable.singletonTable.rtM.RefreshRoutePreview();
        InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
    }

    private void DarkenSelectedObject(GameObject target)
    {
        SpriteRenderer selectedSpriteRenderer = target.GetComponent<SpriteRenderer>();
        if (selectedSpriteRenderer == null) return;

        if (selectedObjectOriginalColors.ContainsKey(target) == false)
        {
            selectedObjectOriginalColors.Add(target, selectedSpriteRenderer.color);
        }

        selectedObjectOriginalColor = selectedSpriteRenderer.color;
        selectedSpriteRenderer.color = new Color(
            selectedSpriteRenderer.color.r * selectedDarkenAmount,
            selectedSpriteRenderer.color.g * selectedDarkenAmount,
            selectedSpriteRenderer.color.b * selectedDarkenAmount,
            selectedSpriteRenderer.color.a);
    }

    private void RestoreSelectedObjectColor(GameObject target)
    {
        if (target == null || selectedObjectOriginalColors.ContainsKey(target) == false) return;

        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.color = selectedObjectOriginalColors[target];

        selectedObjectOriginalColors.Remove(target);
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
        Texture2D texture = new Texture2D(AttackRangeSpriteSize, AttackRangeSpriteSize, TextureFormat.RGBA32, false);
        Color clear = new Color(1f, 1f, 1f, 0f);
        Color white = Color.white;
        Vector2 center = new Vector2((AttackRangeSpriteSize - 1) * 0.5f, (AttackRangeSpriteSize - 1) * 0.5f);
        float radius = (AttackRangeSpriteSize - 1) * 0.5f - AttackRangeEdgeWidth;

        for (int y = 0; y < AttackRangeSpriteSize; y++)
        {
            for (int x = 0; x < AttackRangeSpriteSize; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                float alpha = Mathf.Clamp01((radius + AttackRangeEdgeWidth - distance) / AttackRangeEdgeWidth);
                texture.SetPixel(x, y, alpha > 0f ? new Color(white.r, white.g, white.b, alpha) : clear);
            }
        }

        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, AttackRangeSpriteSize, AttackRangeSpriteSize), new Vector2(0.5f, 0.5f), AttackRangeSpriteSize);
    }

    private void ShowAttackRange(GameObject target)
    {
        if (target == null)
        {
            HideAttackRange();
            return;
        }

        UnitStatus unitStatus = target.GetComponent<UnitStatus>();
        if (unitStatus == null || unitStatus.attackRange <= 0f)
        {
            HideAttackRange();
            return;
        }

        attackRangeIndicator.transform.position = target.transform.position;
        float diameter = unitStatus.attackRange * 2f;
        attackRangeIndicator.transform.localScale = new Vector3(diameter, diameter * IsometricRangeYScale, 1f);
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
}
