using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Player> AllPlayers;
    public int CurrentPlayerIndex = -1;

    void Start()
    {
        foreach (Player player in AllPlayers)
        {
            player.gameObject.SetActive(false);
        }

        StartTurn();
    }

    public void StartTurn()
    {
        changeActivePlayer();
        AllPlayers[CurrentPlayerIndex].StartTurn();
    }

    public void EndTurn()
    {
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
        if (CurrentPlayerIndex >= 0)
        {
            AllPlayers[CurrentPlayerIndex].gameObject.SetActive(false);
        }

        CurrentPlayerIndex++;
        if (CurrentPlayerIndex >= AllPlayers.Count)
        {
            CurrentPlayerIndex = 0;
        }
        AllPlayers[CurrentPlayerIndex].gameObject.SetActive(true);
    }
}
