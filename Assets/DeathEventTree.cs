using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventTree : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject pickupPrefab;
    public Timer destroyTimer;
    bool agony = false;

    private void Update()
    {
        if (agony && destroyTimer.isReady())
        {
            Destroy(objectToDestroy);
        }
    }

    public void OnDeath(HealthController.DamageData data)
    {
        if (!agony)
        {
            GetComponentInParent<Animator>().SetTrigger("Fall");
            agony = true;
            destroyTimer.restart();

            if (pickupPrefab)
                Instantiate(pickupPrefab, objectToDestroy.transform.position, objectToDestroy.transform.rotation);
        }
    }
}