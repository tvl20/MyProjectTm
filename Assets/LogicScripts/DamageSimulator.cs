using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSimulator : MonoBehaviour
{
    public TurnManager TurnManager;
    public Dropdown targetDropDown;

    public void FireWeapons()
    {
        // TODO: fix not dealing damage to the enemy on the first turn;

        Weapon firingWeapon = TurnManager.GetActivePlayer().MyShip.Weapon;
        if (firingWeapon.CooldownCharge < firingWeapon.Cooldown)
        {
            return;
        }

        // Target index is the index of the system in the target drop down
        int targetindex = targetDropDown.value;

        if (hitTarget())
        {
            Debug.Log("Target hit!");
            
            int damageCaused = getDamageCaused();
            ShipLayout targetLayout = TurnManager.GetInactivePlayer().MyShip.shipLayout;

            switch (targetindex)
            {
                case 0:
                    targetLayout.GetInstalledModule("Weapons").DamageAble.TakeDamage(damageCaused);
                    break;
                case 1:
                    targetLayout.GetInstalledModule("Engines").DamageAble.TakeDamage(damageCaused);
                    break;
                case 2:
                    targetLayout.GetInstalledModule("Sensors").DamageAble.TakeDamage(damageCaused);
                    break;
                case 3:
                    targetLayout.GetInstalledModule("Shields").DamageAble.TakeDamage(damageCaused);
                    break;
            }
        }

        firingWeapon.ResetCooldown();
    }

    private bool hitTarget()
    {
        int weaponAccuracy = TurnManager.GetActivePlayer().MyShip.Weapon.Accuracy;
        int accuracyBuff = TurnManager.GetActivePlayer().MyShip.shipLayout.GetInstalledModule("Sensors").getBuff();
        int engineEvade = TurnManager.GetInactivePlayer().MyShip.shipLayout.GetInstalledModule("Engines").getBuff();

        int chanceToHit = weaponAccuracy + accuracyBuff - engineEvade;
        return Random.Range(0, 101) <= chanceToHit;
    }

    private int getDamageCaused()
    {
        int weaponDamage = TurnManager.GetActivePlayer().MyShip.Weapon.Damage;
        int damageBuff = TurnManager.GetActivePlayer().MyShip.shipLayout.GetInstalledModule("Weapons").getBuff();
        int shieldPortection = TurnManager.GetInactivePlayer().MyShip.shipLayout.GetInstalledModule("Shields").getBuff();

        int totalDamageCaused = weaponDamage + damageBuff - shieldPortection;
        return totalDamageCaused;
    }
}
