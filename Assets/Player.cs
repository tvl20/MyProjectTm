using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ship MyShip;

    public void StartTurn()
    {
        MyShip.ReduceWeaponCooldown();
    }
}
