﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour 
{
	public Ship ParentShip;
	public string Name;
	public int BaseBuff;
	public int MannedBuff;
	public DamageAble DamageAble;
	public DamageLevel DamageLevel;
	
	// TODO: FIX MANNING A SYSTEM
	public bool Manned = false;

	void Awake()
	{
		ParentShip = GetComponentInParent<Ship>();
		DamageAble = GetComponent<DamageAble>();
	}

	void Start () 
	{
		DamageAble.OnHealthRestored.AddListener(healthRestored);
		DamageAble.OnDamageTaken.AddListener(damageTaken);
	}

	public int getBuff()
	{
		int currentBuff = 0;
		if(Manned)
		{
			currentBuff = MannedBuff;
		}
		else
		{
			currentBuff = BaseBuff;
		}

		switch (DamageLevel)
		{
			case DamageLevel.DAMAGED:
				currentBuff /= 2;
				break;
			case DamageLevel.BROKEN:
				currentBuff = 0;
				break;
		}
		
		return currentBuff;
	}

	private void healthChanged()
	{
		if (DamageAble.HP <= 2)
		{
			DamageLevel = DamageLevel.BROKEN;
		}
		else if (DamageAble.HP <= 6)
		{
			DamageLevel = DamageLevel.DAMAGED;
		}
		else
		{
			DamageLevel = DamageLevel.OK;
		}

		// TODO: FIX MANNING A SYSTEM
		if (DamageLevel != DamageLevel.OK)
		{
			Manned = false;
		}
	}

	private void healthRestored()
	{
		healthChanged();
	}

	private void damageTaken()
	{
		ParentShip.TakeDamage(DamageAble.DamageFromLastHit);

		healthChanged();
	}
}