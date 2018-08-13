using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiUnitMovementShip : UnitMovement {

    public GameObject[] containedIndicators;
    public GameObject[] prefabs;
    public Transform spawnPoint;
    public int contained = 5;

    void SpawnPrefab()
    {
        RaycastHit info;
        if (contained > 0 && Physics.Raycast(new Ray(spawnPoint.position, Vector3.down), out info) && (info.collider.tag == "Land" || info.collider.tag == "Unit"))
        {
            int i = Random.Range(0, prefabs.Length - 1);

            Instantiate(prefabs[i], spawnPoint.position, spawnPoint.rotation);
            --contained;
            ResetAim();
            UpdateContainedIndicator();
            if (contained == 0)
                enabled = false;
        }
    }

    new private void Start()
    {
        base.Start();
        UpdateContainedIndicator();
    }

    new void Update () {
        if (resetAimCd.isReadyRestart())
        {
            resetAimCd.cd = Random.Range(resetAimCdMin, resetAimCdMax);

            if ( !AutoTargetSelection() )
            {
                if (!randomMovement())
                    ResetAim();
            }
        }

        AtackUpdate();
    }

    new protected bool AutoTargetSelection()
    {
        if (perception)
            foreach (var it in perception.memory)
            {
                bool hasHealth = it.unit.health;
                bool validFraction = !it.unit.fraction ||
                    (it.unit.fraction.gameObject != gameObject &&
                    fraction.GetAttitude(it.unit.fraction.fractionName) == AiFraction.Attitude.enemy);

                bool canMove = false;// it.unit.water;
                canMove |= hasHealth && it.unit.land;
                canMove |= it.unit.port;

                if (canMove && hasHealth && validFraction)
                {
                    hpAim = it.unit.health;
                    aim = it.unit.transform.position;
                    aim.y = body.position.y;
                    movingToAim = true;
                    return true;
                }
            }
        return false;
    }

    new protected void AtackUpdate()
    {
        if (hpAim )
        {
            SpawnPrefab();
            return;
        }
    }

    void UpdateContainedIndicator()
    {
        int i = 0;
        for (; i < contained; ++i)
        {
            containedIndicators[i].SetActive(true);
        }
        for (; i < 5; ++i)
        {
            containedIndicators[i].SetActive(false);
        }
    }


}
