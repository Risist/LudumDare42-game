using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitMovement : MonoBehaviour
{

    public bool useModule = true;

    public float movementSpeed;
    public float minDist;
    public bool canMoveOnWater;
    public bool canMoveOnLand;
    public Timer atackCd;
    public float atackDistance;

    protected Animator animator;
    protected AiFraction fraction;
    protected AiPerception perception;
    protected Timer resetAimCd = new Timer();
    public float resetAimCdMin;
    public float resetAimCdMax;

    protected Rigidbody body;
    protected Vector3 aim;

    protected HealthController hpAim;
    protected bool movingToAim;
    public float heihtOffset;

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

    protected void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        fraction = GetComponent<AiFraction>();
        ResetAim();

        SetSelected(false);
        
        perception = GetComponent<AiPerception>();

    }

    protected void Update()
    {
        if (Input.GetButton("Fire2") && selected)
            UpdateAim();
        else if (resetAimCd.isReadyRestart(resetAimCd.cd))
        {
            resetAimCd.cd = Random.Range(resetAimCdMin, resetAimCdMax);
            AutoTargetSelection();
            //if (!AutoTargetSelection())
                //randomMovement();
        }       

        AtackUpdate();
    }
    protected bool AutoTargetSelection()
    {
        if(perception)
            foreach (var it in perception.memory)
        {
            bool canMove = canMoveOnLand && it.unit.land;
            canMove |= canMoveOnWater && it.unit.water;
            canMove |= it.unit.port;

            bool hasHealth = it.unit.health;
            bool validFraction = !it.unit.fraction ||
                (it.unit.fraction.gameObject != gameObject &&
                fraction.GetAttitude(it.unit.fraction.fractionName) == AiFraction.Attitude.enemy);

            /*Debug.Log("target selection:" +
                "\ncanMove = " + canMove +
                "\nhasHealth = " + hasHealth + 
                "\nvalidFraction = " + validFraction
                );*/
            if (canMove && hasHealth && validFraction)
            {

                //Debug.Log("found");

                hpAim = it.unit.health;
                aim = it.unit.transform.position;
                aim.y = body.position.y;
                movingToAim = true;
                return true;
            }
        }
        return false;
    }
    protected bool randomMovement(float randomRadiusMin = 3.0f, float randomRadiusMax = 7.0f)
    {
        Vector3 target = Random.insideUnitCircle * Random.Range(randomRadiusMin, randomRadiusMax);
        target.z = target.y; target.y = 0;

        Ray ray = new Ray(body.position + target + Vector3.up * 20, Vector3.down);
        RaycastHit info;
        bool b = true;
        if( Physics.Raycast(ray, out info) )
        {
            bool cantMove = info.collider.tag == "Water" && !canMoveOnWater;
            cantMove |= info.collider.tag == "Land" && !canMoveOnLand;

            if (cantMove)
                b = false;
        }
        if (b)
        {
            aim = body.position + target;
            movingToAim = true;
            return true;
        }
        return false;
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
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
        else
            movingToAim = false;


        float height = transform.position.y;

        RaycastHit info;
        if (Physics.Raycast(new Ray(body.position + Vector3.up * -1, Vector3.down), out info))
        {
            //Debug.Log("dsada " + info.collider.tag);
            if (info.collider.tag == "Water" || info.collider.tag == "Land" || info.collider.tag == "Unit")
                height += -info.distance + heihtOffset;
        }

        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

    bool UpdateAim()
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

                return true;
            }
            else if (animator)
            {
                HealthController hp = hit.collider.GetComponent<HealthController>();
                AiFraction aiFraction = hit.collider.GetComponent<AiFraction>();
                if (hp && (!aiFraction || fraction.GetAttitude(aiFraction.fractionName) != AiFraction.Attitude.friendly ) )
                {
                    hpAim = hp;
                    aim = hpAim.transform.position;
                    aim.y = body.position.y;
                    movingToAim = true;
                    return true;
                }
            }

            ResetAim();
        }
        return false;
    }


    public static void ResetSelection()
    {
        var objs = Object.FindObjectsOfType<UnitMovement>();
        foreach (var it in objs)
            it.SetSelected(false);
    }

    protected void AtackUpdate()
    {
        if (hpAim && atackCd.isReady())
        {
            Vector3 diff = hpAim.transform.position - transform.position;
            diff.y = 0;
            if (diff.sqrMagnitude < atackDistance * atackDistance)
            {
                atackCd.restart();
                ResetAim();
                animator.SetTrigger("Atack");
            }
            return;
        }
    }
}
