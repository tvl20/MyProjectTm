using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int Health = 25;
    public Weapon Weapon;
    public ShipLayout shipLayout;

    public void ReduceWeaponCooldown()
    {
        Weapon.LowerCooldown();
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        Debug.Log("Shit hit, hp left: " + Health);
    }
}
