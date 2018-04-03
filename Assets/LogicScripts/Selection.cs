using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public Cell selectedCell = null;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // if (selectedCell != null) selectedCell.onDeselect();
            selectedCell = getClickedCell();
            // if (selectedCell != null) selectedCell.onSelect();
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cell moveCell = getClickedCell();
            if (selectedCell == null || moveCell == null || selectedCell.crewMember == null)
            {
                return;
            }

            selectedCell.crewMember.MoveTo(moveCell);

            // selectedCell.onDeselect();
            selectedCell = moveCell;
            // selectedCell.onSelect();
        }
    }

    private Cell getClickedCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;
        Cell hitCell = null;
        if (Physics.Raycast(ray, out hitObject))
        {
            hitCell = hitObject.collider.GetComponentInParent<Cell>();
        }
        return hitCell;
    }
}
