using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    
    public float movementSpeed;
    public float minDist;
    public bool canMoveOnWater;
    public bool canMoveOnLand;
    public Timer atackCd;
    public float atackDistance;

    Animator animator;
    AiFraction fraction;

    Rigidbody body;
    Vector3 aim;

    HealthController hpAim;
    bool movingToAim;

    public void ResetAim() { aim = body.position;
        movingToAim = false;
    }

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
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        fraction = GetComponent<AiFraction>();
        ResetAim();

        SetSelected(false);
    }

    private void Update()
    {
        if (Input.GetButton("Fire2") && selected)
            UpdateAim();

        AtackUpdate();
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    void PerformMovement()
    {
        Vector3 diff = body.position - aim;
        if (movingToAim && diff.sqrMagnitude > minDist * minDist)
        {
            body.AddForce(-diff.normalized * movementSpeed);

            float rotation = Vector3.SignedAngle(Vector3.left, diff, Vector3.up);
            body.rotation = Quaternion.Euler(0, rotation, 0);
        }
        else
            movingToAim = false;

        
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
            if ((water && canMoveOnWater) || (land && canMoveOnLand) || (port))
            {
                aim = hit.point;
                aim.y = body.position.y;
                hpAim = null;
                movingToAim = true;

                return;
            }
            else if (animator)
            {
                HealthController hp = hit.collider.GetComponent<HealthController>();
                AiFraction aiFraction = hit.collider.GetComponent<AiFraction>();
                if (hp)
                {
                    hpAim = hp;
                    aim = hpAim.transform.position;
                    aim.y = body.position.y;
                    movingToAim = true;
                    return;
                }
            }

            ResetAim();


        }
    }
    public static void ResetSelection()
    {
        var objs = Object.FindObjectsOfType<UnitMovement>();
        foreach (var it in objs)
            it.SetSelected(false);
    }

    void AtackUpdate()
    {
        if (hpAim && atackCd.isReady()) //&& (!fraction || ( aiFraction && fraction.GetAttitude(aiFraction.fractionName) != AiFraction.Attitude.friendly)))
        {
            Vector3 diff = hpAim.transform.position - transform.position;
            diff.y = 0;
            if (diff.sqrMagnitude < atackDistance * atackDistance)
            {
                atackCd.restart();
                ResetAim();
                animator.SetTrigger("Atack");
            }


            float rotation = Vector3.SignedAngle(Vector3.left, diff, Vector3.up);
            body.rotation = Quaternion.Euler(0, -rotation, 0);
            return;
        }
    }
}
