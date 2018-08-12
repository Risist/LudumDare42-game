using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventSpawn : MonoBehaviour {

	public GameObject prefab;

	void OnDeath(HealthController.DamageData data)
	{
        if(prefab)
		    Instantiate(prefab, transform.position, transform.rotation );
        prefab = null;
	}
}
