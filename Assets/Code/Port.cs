using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    public Timer UnitExitCd;
    public ShipContainer ship;
    public Transform exitPoint;

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && ship && UnitExitCd.isReadyRestart() )
            ship.ExitUnit(exitPoint);
    }

    public void LeaveShip()
    {
        if (ship)
            ship.ExitUnit(exitPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        ShipContainer shipContainer = other.GetComponent<ShipContainer>();
        if (shipContainer)
            ship = shipContainer;

        UnitMovement unit = other.GetComponent<UnitMovement>();
        if(unit && ship && other.gameObject != ship.gameObject)
        {
            ship.Insert(unit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ship && ship.gameObject == other.gameObject)
        {
            ship = null;
        }
    }
}
