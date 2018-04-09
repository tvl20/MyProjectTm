using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour 
{
    public Ship ObservedShip;
    public Text MaxHPText;
    public Text CurrentHPText;

    void Start()
    {
        MaxHPText.text = ObservedShip.DamageAble.MaxHP.ToString();
        CurrentHPText.text = ObservedShip.DamageAble.HP.ToString();

        ObservedShip.DamageAble.OnDamageTaken.AddListener(updateUI);
        ObservedShip.DamageAble.OnHealthRestored.AddListener(updateUI);
        updateUI();
    }

    private void updateUI()
    {
        CurrentHPText.text = ObservedShip.DamageAble.HP.ToString();
    }
}
