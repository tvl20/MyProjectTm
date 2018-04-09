using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Module : MonoBehaviour
{
    public Ship ParentShip;
    public string Name;
    public int BaseBuff;
    public int MannedBuff;
    public DamageAble DamageAble;
    public DamageLevel DamageLevel;
    public UnityEvent OnMannedCrewMemberChanged = new UnityEvent();
    public CrewMember MannedCrewMember;

    void Awake()
    {
        ParentShip = GetComponentInParent<Ship>();
        DamageAble = GetComponent<DamageAble>();
    }

    void Start()
    {
        DamageAble.OnHealthRestored.AddListener(healthRestored);
        DamageAble.OnDamageTaken.AddListener(damageTaken);
    }

    public int getBuff()
    {
        int currentBuff = 0;
        if (MannedCrewMember != null)
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

    public void MountModule(CrewMember crewMember)
    {
        this.MannedCrewMember = crewMember;
        OnMannedCrewMemberChanged.Invoke();
    }

    public void UnMountModule()
    {
        this.MannedCrewMember = null;
        OnMannedCrewMemberChanged.Invoke();
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

        if (DamageLevel != DamageLevel.OK)
        {
            UnMountModule();
        }
    }

    private void healthRestored()
    {
        healthChanged();
    }

    private void damageTaken()
    {
        ParentShip.DamageAble.TakeDamage(DamageAble.DamageFromLastHit);

        healthChanged();
    }
}
