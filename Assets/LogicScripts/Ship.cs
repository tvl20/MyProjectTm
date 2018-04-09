using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public DamageAble DamageAble;
    public Weapon Weapon;
    public ShipLayout shipLayout;

    void Start()
    {
        DamageAble.OnDamageTaken.AddListener(shipDamaged);
    }

    public void ReduceWeaponCooldown()
    {
        Weapon.LowerCooldown();
    }

    public void RestoreActionPoints()
    {
        foreach (CrewMember member in shipLayout.Crew)
        {
            member.RefreshActionPoints();
        }
    }

    private void shipDamaged()
    {
        if (DamageAble.HP == 0)
        {
            Debug.Log(this + " has 0 hp remaining, so lost.");
        }
    }
}
