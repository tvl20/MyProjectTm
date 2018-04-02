using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : MonoBehaviour 
{
	public DamageAble DamageAble;
	public int ActionPoints;
	public int MaxActionPoints;

	void Start () 
	{
		DamageAble = GetComponent<DamageAble>();
		DamageAble.OnDamageTaken.AddListener(damageTaken);
	}

	public void RefreshActionPoints()
	{
		ActionPoints = MaxActionPoints;
	}

	private void damageTaken()
	{
		if (DamageAble.HP == 0)
		{
			Destroy(this.gameObject);
		}
	}
}
