using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    
    public float movementSpeed;
    public float minDist;
    public bool canMoveOnWater;
    public bool canMoveOnLand;



    Rigidbody body;
    Vector3 aim;
    public void ResetAim() { aim = body.position;  }

    private bool selected;
    public GameObject selectedIndicator;
    public void SetSelected(bool s)
    {
        if (selectedIndicator)
            selectedIndicator.SetActive(s);
        selected = s;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        ResetAim();

        SetSelected(false);
    }

    private void Update()
    {
        if (Input.GetButton("Fire2") && selected)
            UpdateAim();
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    void PerformMovement()
    {
        Vector3 diff = body.position - aim;
        if (diff.sqrMagnitude > minDist * minDist)
        {
            body.AddForce(-diff.normalized * movementSpeed);     
        }

        float rotation = Vector3.SignedAngle(Vector3.left, diff, Vector3.up);
        body.rotation = Quaternion.Euler(0, rotation, 0);
    }

    void UpdateAim()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            bool water = hit.collider.tag == "Water";
            bool land = hit.collider.tag == "Land";
            bool port = hit.collider.tag == "Port";
            if ((water && canMoveOnWater) || (land && canMoveOnLand) || (port) )
            {
                aim = hit.point;
                aim.y = body.position.y;
            }
            else ResetAim();
        }
    }
    public static void ResetSelection()
    {
        var objs = Object.FindObjectsOfType<UnitMovement>();
        foreach (var it in objs)
            it.SetSelected(false);
    }

}
