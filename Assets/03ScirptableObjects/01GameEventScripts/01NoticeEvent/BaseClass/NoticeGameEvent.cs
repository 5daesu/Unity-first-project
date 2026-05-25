using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoticeGameEvent", menuName = "ScriptableObjects/GameEvents/NoticeGameEvent", order = 0)]
public class NoticeGameEvent : GameEvent
{
    // false면 이 이벤트는 추첨 후보에 들어가지 않는다.
    [SerializeField] private bool isEnabled = true;

    // 이 목록의 모든 조건을 만족해야 이벤트가 추첨 후보에 들어간다.
    [SerializeField] private GameEventStatCondition[] availabilityConditions;

    // 스탯 보정이 더해지기 전의 기본 추첨 가중치.
    [SerializeField, Min(0f)] private float baseWeight = 100f;

    // 추첨 시 baseWeight에 더해지는 선택적 스탯 기반 보정값들.
    [SerializeField] private GameEventStatWeightModifier[] weightModifiers;

    public override bool IsAvailable()
    {
        if (!isEnabled) return false;
        if (availabilityConditions == null) return true;

        // 조건은 AND 방식으로 처리한다. 하나라도 실패하면 후보에서 제외된다.
        foreach (GameEventStatCondition condition in availabilityConditions)
        {
            if (condition == null) continue;
            if (!condition.IsSatisfied()) return false;
        }

        return true;
    }

    public override float GetWeight()
    {
        float weight = baseWeight;

        // 최종 가중치 = 기본 가중치 + 활성화된 모든 스탯 보정 가중치.
        if (weightModifiers != null)
        {
            foreach (GameEventStatWeightModifier modifier in weightModifiers)
            {
                if (modifier == null) continue;

                weight += modifier.GetWeightBonus();
            }
        }

        // 음수 가중치는 가중치 랜덤 선택을 망가뜨리므로 0 이상으로 제한한다.
        return Mathf.Max(0f, weight);
    }
}

public enum GameEventStatComparison
{
    LessOrEqual,
    GreaterOrEqual,
    Equal
}

public enum GameEventStatWeightMode
{
    LowerStatIncreases,
    HigherStatIncreases
}

[System.Serializable]
public class GameEventStatCondition
{
    // 예: Diplomacy <= 30이면 외교가 낮을 때만 이 이벤트가 등장한다.
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private GameEventStatComparison comparison;
    [SerializeField] private int value;

    public bool IsSatisfied()
    {
        int currentValue = SingletonTable.singletonTable.piM.GetPlayerStat(playerStat);

        switch (comparison)
        {
            case GameEventStatComparison.LessOrEqual:
                return currentValue <= value;
            case GameEventStatComparison.GreaterOrEqual:
                return currentValue >= value;
            case GameEventStatComparison.Equal:
                return currentValue == value;
            default:
                return false;
        }
    }
}

[System.Serializable]
public class GameEventStatWeightModifier
{
    // 예: Diplomacy + LowerStatIncreases 조합이면 외교가 낮을수록 가중치가 증가한다.
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private GameEventStatWeightMode mode;

    // 보정 가중치가 더해지기 시작하는 기준 스탯 값.
    [SerializeField] private int referenceValue = 50;

    // 기준값을 벗어난 스탯 1포인트마다 더할 보정 가중치.
    [SerializeField, Min(0f)] private float weightPerPoint = 1f;

    // 하나의 보정값이 전체 추첨 테이블을 과하게 지배하지 않도록 제한하는 최대 보정치.
    [SerializeField, Min(0f)] private float maxBonusWeight = 50f;

    public float GetWeightBonus()
    {
        int currentValue = SingletonTable.singletonTable.piM.GetPlayerStat(playerStat);
        int difference = 0;

        // 선택한 모드에 따라 기준값에서 얼마나 벗어났는지를 양수 값으로 계산한다.
        switch (mode)
        {
            case GameEventStatWeightMode.LowerStatIncreases:
                difference = referenceValue - currentValue;
                break;
            case GameEventStatWeightMode.HigherStatIncreases:
                difference = currentValue - referenceValue;
                break;
        }

        if (difference <= 0) return 0f;

        // 예: 외교가 기준보다 20 낮고 weightPerPoint가 1이면 +20 가중치. 단, maxBonusWeight를 넘지 않는다.
        return Mathf.Min(difference * weightPerPoint, maxBonusWeight);
    }
}
