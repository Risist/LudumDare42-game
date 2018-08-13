using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHeal : MonoBehaviour {

    public float minHealth;
    public float bonusHealth;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthController>();
        var movement = other.GetComponent<UnitMovement>();
        if(health && movement && movement.useModule )//&& (health.actual/ health.max) < minHealth)
        {
            Debug.Log("heal");
            health.max += bonusHealth;
            health.actual = health.max;
            Destroy(gameObject);
        }
    }
}
