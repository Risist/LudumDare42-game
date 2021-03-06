﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviourRotateToAim : AiBehaviourBase
{
	public bool update = true;
	public AiLocationBase aim;
    public float addictionalRotation;
	Rigidbody body;

	protected new void Start()
	{
		base.Start();
		body = GetComponentInParent<Rigidbody>();
	}

	public override bool PerformAction()
	{
		if (update)
		{
			Vector3 directionOfMove = aim.GetLocation() - body.position;
            directionOfMove.y = directionOfMove.z;
            directionOfMove.z = 0;
            body.rotation = Quaternion.Euler(0, -Vector2.Angle(Vector2.up, directionOfMove) * (directionOfMove.x > 0 ? -1 : 1) + addictionalRotation, 0);
		}
		return true;
	}

	public override void EnterAction()
	{
		base.EnterAction();
		Vector3 directionOfMove = aim.GetLocation() - body.position;
        directionOfMove.y = directionOfMove.z;
        directionOfMove.z = 0;
        body.rotation = Quaternion.Euler(0, -Vector2.Angle(Vector2.up, directionOfMove) * (directionOfMove.x > 0 ? -1 : 1) + addictionalRotation, 0);
	}
}
