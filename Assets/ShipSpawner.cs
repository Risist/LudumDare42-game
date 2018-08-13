using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {

    public float tMin;
    public float tMax;
    Timer timer = new Timer();
    public GameObject prefab;
    public float minRadius;
    public float maxRadius;

    // Use this for initialization
    void Start () {
        timer.cd = Random.Range(tMin, tMax);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer.isReadyRestart())
        {
            float raddi = Random.Range(minRadius, maxRadius);
            Vector3 position = Random.insideUnitCircle;
            position.z = position.y;
            position.y = 0;

            Instantiate(prefab, transform.position + position * raddi, Quaternion.Euler(0, Random.value * 360, 0));
        }
	}
}
