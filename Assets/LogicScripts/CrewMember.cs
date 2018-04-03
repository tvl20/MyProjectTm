using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    public DamageAble DamageAble;
    public int ActionPoints;
    public int MaxActionPoints;

    void Start()
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

    public void MoveTo(Cell target)
    {
        if (ActionPoints > 0)
        {
            Cell currentCell = this.gameObject.GetComponentInParent<Cell>();

            if (currentCell.Equals(target))
            {
                if (target.Module != null)
                {
                    interactModule(target.Module);
                }
                return;
            }

            int tileDistance = getDistance(currentCell, target);
            if (tileDistance > ActionPoints || target.crewMember != null)
            {
                Debug.Log("Invalid Move!");
                return;
            }

            ActionPoints -= tileDistance;

            if (currentCell.Module != null)
            {
                currentCell.Module.MannedCrewMember = null;
            }

            currentCell.crewMember = null;
            target.crewMember = this;

            this.gameObject.transform.position = target.transform.position;
            this.transform.parent = target.transform;
        }
    }

    private void interactModule(Module module)
    {
        if (module.DamageLevel != DamageLevel.OK)
        {
            module.DamageAble.HealDamage(1);
        }
        else if (module.MannedCrewMember == null)
        {
            module.MannedCrewMember = this;
        }
        else
        {
            return;
        }

        ActionPoints--;
    }

    private int getDistance(Cell original, Cell target)
    {
        int xDistance = Mathf.Abs(target.X - original.X);
        int yDistance = Mathf.Abs(target.Y - original.Y);
        int totalDistance = xDistance + yDistance;
        return totalDistance;
    }
}
