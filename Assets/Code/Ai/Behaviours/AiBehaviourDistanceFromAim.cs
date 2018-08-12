using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviourDistanceFromAim : AiBehaviourBase{

	public AiLocationBase aim;
	public float distanceMin = 0.0f;
	public float distanceMax = 1000000.0f;
	public bool inside = true;


	public override bool CanEnter()
	{
        Vector3 diff = transform.position - aim.GetLocation();
        diff.y = 0;
        float distanceSq = diff.sqrMagnitude;
		bool b = distanceSq < distanceMax * distanceMax && distanceSq > distanceMin * distanceMin;
		return inside ? b : !b;
	}
}
