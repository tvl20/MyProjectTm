using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleMonitor : MonoBehaviour 
{
	public Image ModuleTypeImage;
	public Text ModuleNameText;
	public Text DamageLevelText;
	public Text BuffProvidedText;
	public Toggle MannedToggle;
	public Image ModuleStatusImage;		
	public Module ObservedModule;

	void Start () 
	{
		ObservedModule.DamageAble.OnDamageTaken.AddListener(updateUI);
		ObservedModule.DamageAble.OnHealthRestored.AddListener(updateUI);
		ObservedModule.OnMannedCrewMemberChanged.AddListener(updateUI);
		
		ModuleNameText.text = ObservedModule.name;
		ModuleTypeImage.color = ObservedModule.gameObject.GetComponentInChildren<Renderer>().material.color;

		updateUI();
	}
	
	// TODO: when a system is fully repaired color stays yellow indicating damaged,
	// 		 if updateUI() is run again the color is changed.
	//       fix this bug in a later version/iteration.
	public void updateUI()
	{
		DamageLevelText.text = (7 - ObservedModule.DamageAble.HP).ToString();
		BuffProvidedText.text = ObservedModule.getBuff().ToString();
		MannedToggle.isOn = ObservedModule.MannedCrewMember != null;

		switch (ObservedModule.DamageLevel)
		{
			case DamageLevel.OK:
				ModuleStatusImage.color = Color.green;
				break;
			case DamageLevel.DAMAGED:
				ModuleStatusImage.color = Color.yellow;
				break;
			case DamageLevel.BROKEN:
				ModuleStatusImage.color = Color.red;
				break;
			default:
				ModuleStatusImage.color = Color.grey;
				break;
		}
	}
}
