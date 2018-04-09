using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerDisplay : MonoBehaviour 
{
	public Text PlayerNameText;

	void Start()
	{
		GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>().NewTurnEvent.AddListener(UpdateCurrentPlayer);
	}

	public void UpdateCurrentPlayer(string playerName)
	{
		PlayerNameText.text = playerName;
	}
}
