using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public GameEvent curStageGameEvent { get; set; }

    [SerializeField] private NoticeGameEvent[] noticeGameEvents;

    private List<GameEvent> possibleGameEvents;     //when GameEventManager choose GameEvent, there's no possiblity of choosing whitin this List, but later it can be changed.
    private List<GameEvent> availableGameEvents;    //current selectable

    void Awake()
    {
        possibleGameEvents = new List<GameEvent>();
        availableGameEvents = new List<GameEvent>();
    }

    void Start()
    {
        CheckPrimaryCondition(noticeGameEvents);
        CheckSecondCondition(possibleGameEvents);
    }

    private void CheckPrimaryCondition(GameEvent[] gameEvents)
    {
        foreach (GameEvent gameEvent in gameEvents)
        {
            if (gameEvent.PrimaryCondition() == true)     //it will be modified like this "if (gameEvent.FirstCondition() == true)"
            {
                possibleGameEvents.Add(gameEvent);
            }
        }
    }

    private void CheckSecondCondition(List<GameEvent> gameEvents)   //Check Second Condition, Second Condition returns 0 ~ 1 float number, that is probability, it shoulb be called when player turn ends
    {
        for (int i = 0; i < gameEvents.Count; i++)
        {
            if (gameEvents[i].SecondaryCondition() > 0)
            {
                availableGameEvents.Add(gameEvents[i]);
                possibleGameEvents.RemoveAt(i);
                i--;    //because it is deleted once, so index should be updated
            }
        }
    }

    public void DrawGameEvent()    //Draw is like a roulette, bbopkki
    {
        Debug.Log("Draw GameEvent!");

        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("AvailableGameEvent's count is " + availableGameEvents.Count);
            int random = Random.Range(0, availableGameEvents.Count);
            //Debug.Log("EventRandomNumber is " + random);

            if (availableGameEvents[random].SecondaryCondition() > Random.Range(0f, 1f))
            {
                curStageGameEvent = availableGameEvents[Random.Range(0, availableGameEvents.Count)];
            }
        }

        curStageGameEvent = availableGameEvents[Random.Range(0, availableGameEvents.Count)];
    }
}