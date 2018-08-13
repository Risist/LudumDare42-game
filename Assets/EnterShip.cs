using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShip : MonoBehaviour {

    public float sphereRadius;
    public Timer minCd = new Timer(0.3f);
    ShipContainer ship;

    private void Start()
    {
        ship = GetComponentInParent<ShipContainer>();
    }

    private void FixedUpdate()
    {
        if (!minCd.isReadyRestart())
            return;

        var colliders = Physics.OverlapSphere(transform.position, sphereRadius);
        foreach (var it in colliders)
            if(it.gameObject != gameObject)
        {
            var unit = it.GetComponent<UnitMovement>();
            if(unit && unit.useModule)
            {
                ship.Insert(unit);
                break;
            }
        }
    }

}
