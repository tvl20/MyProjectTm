using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Module Module;
    public CrewMember crewMember;
    public int X;
    public int Y;

    void Start()
    {
        Module = GetComponent<Module>();
    }

    public override bool Equals(object other)
    {
        Cell otherCell = other as Cell;
        if (other == null)
        {
            return false;
        }
        else
        {
            return otherCell.X == this.X && otherCell.Y == this.Y;
        }
    }
}
