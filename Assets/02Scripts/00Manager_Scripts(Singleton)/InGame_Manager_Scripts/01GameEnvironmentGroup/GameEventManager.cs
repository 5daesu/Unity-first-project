using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public GameEvent curStageGameEvent { get; set; }

    [SerializeField] private NoticeGameEvent[] noticeGameEvents;

    private List<GameEvent> availableGameEvents;    //current selectable

    void Awake()
    {
        availableGameEvents = new List<GameEvent>();
    }

    void Start()
    {
        RefreshAvailableGameEvents();
    }

    private void CollectAvailableGameEvents(GameEvent[] gameEvents)
    {
        foreach (GameEvent gameEvent in gameEvents)
        {
            if (gameEvent == null) continue;
            if (!gameEvent.IsAvailable()) continue;
            if (gameEvent.GetWeight() <= 0f) continue;

            availableGameEvents.Add(gameEvent);
        }
    }

    public void DrawGameEvent()    //Draw is like a roulette, bbopkki
    {
        Debug.Log("Draw GameEvent!");
        RefreshAvailableGameEvents();

        if (availableGameEvents.Count == 0)
        {
            curStageGameEvent = null;
            Debug.LogWarning("There is no available GameEvent.");
            return;
        }

        curStageGameEvent = DrawWeightedGameEvent(availableGameEvents);
    }

    private void RefreshAvailableGameEvents()
    {
        availableGameEvents.Clear();

        CollectAvailableGameEvents(noticeGameEvents);
    }

    private GameEvent DrawWeightedGameEvent(List<GameEvent> gameEvents)
    {
        float totalWeight = 0f;

        foreach (GameEvent gameEvent in gameEvents)
        {
            totalWeight += gameEvent.GetWeight();
        }

        if (totalWeight <= 0f) return null;

        float randomWeight = Random.Range(0f, totalWeight);

        foreach (GameEvent gameEvent in gameEvents)
        {
            randomWeight -= gameEvent.GetWeight();
            if (randomWeight <= 0f) return gameEvent;
        }

        return gameEvents[gameEvents.Count - 1];
    }
}
