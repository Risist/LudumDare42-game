using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviourRandomMovement : AiBehaviourMotor
{

	Timer cd = new Timer();
	public float minCd = 1.0f;
	public float maxCd = 1.0f;
	protected void RestartCd() { cd.restart(); cd.cd = Random.Range(minCd, maxCd); }

	public Transform center;
	public float radiusMin = 3.0f;
	public float radiusMax = 3.0f;

	public float stopDistance = 1.0f;

	Vector3 destination = Vector3.zero;
	void RandDestination()
	{
        Vector2 v = Random.insideUnitCircle * Random.Range(radiusMin, radiusMax);
        destination = center.position + new Vector3(v.x, 0, v.y);
	}

	// Use this for initialization
	new void Start()
	{
		base.Start();
		if (!center)
			center = transform;

	}

	public override void EnterAction()
	{
		RestartCd();
		RandDestination();
		base.EnterAction();
	}
	public override bool PerformAction()
	{
		if (cd.isReady())
		{
			RestartCd();
			RandDestination();
		}

		Vector3 directionOfMove = destination - body.position;
        directionOfMove.y = directionOfMove.z;
        directionOfMove.z = 0;
		if (directionOfMove.sqrMagnitude > stopDistance * stopDistance)
		{
			body.rotation = Quaternion.Euler(0, Vector2.Angle(Vector2.up, directionOfMove) * (directionOfMove.x > 0 ? -1 : 1), 0);
			bShouldMove = true;
		}

		return true;
	}
}