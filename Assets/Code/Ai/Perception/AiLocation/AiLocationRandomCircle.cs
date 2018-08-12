using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLocationRandomCircle : AiLocationBase {

	public float minRadius = 0.0f;
	public float maxRadius = 10.0f;

	public float recalculateTimeMin = 1.0f;
	public float recalculateTimeMax = 1.0f;
	Timer recalculateTimer = new Timer(0);

	Vector3 location = Vector3.zero;

	public override Vector3 GetLocation()
	{
		if(recalculateTimer.isReadyRestart() )
		{
			recalculateTimer.cd = Random.Range(recalculateTimeMin, recalculateTimeMax);
			location = Random.insideUnitCircle * Random.Range(minRadius, maxRadius);
            location.z = location.y;
            location.y = 0;
		}

		return location + base.GetLocation();
	}


}
