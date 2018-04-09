using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string PlayerName;
    public Canvas UI;
    public Selection SelectionScript;
    public Ship MyShip;

    void Start()
    {
        GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>().NewTurnEvent.AddListener(NewTurn);
    }

    public void NewTurn(string playerName)
    {
        if (playerName != this.PlayerName)
        {
            return;
        }

        StartTurn();
    }

    public void StartTurn()
    {
        UI.gameObject.SetActive(true);
        ModuleMonitor[] Monitors = UI.GetComponentsInChildren<ModuleMonitor>();
        foreach (ModuleMonitor monitor in Monitors)
        {
            monitor.updateUI();
        }

        SelectionScript.gameObject.SetActive(true); 

        MyShip.ReduceWeaponCooldown();
        MyShip.RestoreActionPoints();
    }

    public void EndTurn()
    {
        UI.gameObject.SetActive(false);
        SelectionScript.gameObject.SetActive(false);
    }
}
