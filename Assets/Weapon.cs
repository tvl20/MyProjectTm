using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour 
{
	public int Damage;
	public int Accuracy;
	public int Cooldown;
	public int CooldownCharge = 0;
	public UnityEvent OnCooldownChanged = new UnityEvent();

	public void LowerCooldown()
	{
		if (CooldownCharge != Cooldown)
		{
			CooldownCharge++;
		}

		OnCooldownChanged.Invoke();
	}

	public void ResetCooldown()
	{
		CooldownCharge = 0;
		OnCooldownChanged.Invoke();
	}
}
