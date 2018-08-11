using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{

    ShipContainer ship;
    public Transform exitPoint;

    public void ExitUnit()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ShipContainer shipContainer = other.GetComponent<ShipContainer>();
        if (shipContainer)
            ship = shipContainer;

        UnitMovement unit = other.GetComponent<UnitMovement>();
        if(unit)
        {
            ship.Insert(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ship.gameObject == other.gameObject)
        {
            ship = null;
        }
    }
}
