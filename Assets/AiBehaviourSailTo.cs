using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviourSailTo : AiBehaviourBase {

    public float movementSpeed;
    public AiLocationBase aim;
    public float minDistance;


    public float addictionalRotation;
    Rigidbody body;

    bool shouldMove = false;

    new private void Start()
    {
        base.Start();
        body = GetComponentInParent<Rigidbody>();
    }

    public override bool PerformAction()
    {
        
        Vector3 directionOfMove = aim.GetLocation() - body.position;
        directionOfMove.y = directionOfMove.z;
        directionOfMove.z = 0;
        body.rotation = Quaternion.Euler(0, -Vector2.Angle(Vector2.up, directionOfMove) * (directionOfMove.x > 0 ? -1 : 1) + addictionalRotation, 0);
        if (directionOfMove.sqrMagnitude > minDistance * minDistance)
            shouldMove = true;

        return true;
    }

    private void FixedUpdate()
    {
        if(shouldMove)
        {
            shouldMove = false;
            body.AddForce(body.transform.forward * movementSpeed);
        }
    }
}
