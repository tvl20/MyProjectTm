using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StartTurnEvent : UnityEvent<string> { }

public class TurnManager : MonoBehaviour
{
    public StartTurnEvent NewTurnEvent = new StartTurnEvent();
    public List<Player> AllPlayers;
    public int CurrentPlayerIndex = 0;
    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = Camera.main.transform.position - AllPlayers[CurrentPlayerIndex].transform.position;

        foreach(Player player in AllPlayers)
        {
            player.EndTurn();
        }

        StartTurn();
    }

    public void StartTurn()
    {
        NewTurnEvent.Invoke(AllPlayers[CurrentPlayerIndex].PlayerName);
    }

    public void EndTurn()
    {
        if (CurrentPlayerIndex >= 0)
        {
            AllPlayers[CurrentPlayerIndex].EndTurn();
        }

        changeActivePlayer();
        StartTurn();
    }

    [System.Obsolete("This function only works if there are only 2 players in the list of all the players", false)]
    public Player GetInactivePlayer()
    {
        switch (CurrentPlayerIndex)
        {
            case 0:
                return AllPlayers[1];
            case 1:
                return AllPlayers[0];
        }
        return null;
    }

    public Player GetActivePlayer()
    {
        return AllPlayers[CurrentPlayerIndex];
    }

    private void changeActivePlayer()
    {
        CurrentPlayerIndex++;
        if (CurrentPlayerIndex >= AllPlayers.Count)
        {
            CurrentPlayerIndex = 0;
        }

        Camera.main.transform.position = AllPlayers[CurrentPlayerIndex].transform.position + cameraOffset;
    }
}
