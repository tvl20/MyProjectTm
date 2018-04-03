using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCooldownUI : MonoBehaviour
{
    public Weapon ObservedWeapon;
    public Text MaxCooldownText;
    public Text CurrentCooldownText;

    void Start()
    {
        ObservedWeapon.OnCooldownChanged.AddListener(updateUI);
        MaxCooldownText.text = ObservedWeapon.Cooldown.ToString();
        updateUI();
    }

    private void updateUI()
    {
        CurrentCooldownText.text = ObservedWeapon.CooldownCharge.ToString();
    }
}
