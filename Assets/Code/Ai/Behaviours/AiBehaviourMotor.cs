using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviourMotor : AiBehaviourBase
{

	public float movementSpeedMin = 1000.0f;
	public float movementSpeedMax = 1000.0f;
	protected float movementSpeed;
	protected Rigidbody body;

	protected bool bShouldMove;
	public new void Start()
	{
		base.Start();
		body = GetComponentInParent<Rigidbody>();
	}
	public override bool PerformAction()
	{
		bShouldMove = true;
		return true;
	}

	public override void EnterAction()
	{
		base.EnterAction();
		movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);
	}

	public void FixedUpdate()
	{
		if (bShouldMove)
			body.AddForce(body.transform.forward * movementSpeed);
		bShouldMove = false;
	}

}