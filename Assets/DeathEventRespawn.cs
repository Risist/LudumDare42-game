using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventRespawn : MonoBehaviour
{
    public float dmgTogainGold;
    public float current = 0;
    public GameObject prefab;

    private void Start()
    {
        current = dmgTogainGold;
    }

    public void OnReceiveDamage(HealthController.DamageData data)
    {
        current += data.damage.toFloat();
        if(current <= 0)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            current += dmgTogainGold;
        }
    }
}
