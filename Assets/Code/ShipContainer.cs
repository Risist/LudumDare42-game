using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShipContainer : MonoBehaviour
{

    public int maxContained;
    public int currentContained;

    public List<UnitMovement> contained = new List<UnitMovement>();
    public GameObject[] containedIndicator;

    public Transform[] exitTransforms;

    UnitMovement unit;

    public bool IsEmpty()
    {
        return contained.Count == 0;
    }

    private void Start()
    {
        unit = GetComponent<UnitMovement>();
        foreach(var it in contained)
        {
            it.gameObject.SetActive(false);
            //  it.ResetAim();
        }
        UpdateContainedIndicator();
    }

    private void Update()
    {
        //unit.enabled = !IsEmpty();
    }

    public bool Insert(UnitMovement obj)
    {
        if(contained.Count < maxContained)
        {
            contained.Add(obj);
            obj.gameObject.SetActive(false);
            obj.ResetAim();

            UpdateContainedIndicator();
            return true;
        }
        return false;
    }

    public void ExitUnit(Transform exitPoint)
    {
        if (contained.Count == 0)
            return;

        var obj = contained[contained.Count - 1];
        obj.transform.position = exitPoint.position;
        obj.transform.rotation = exitPoint.rotation;

        obj.gameObject. SetActive(true);
        obj.ResetAim();
        obj.SetSelected(false);
        contained.Remove(obj);

        UpdateContainedIndicator();
    }

    public void ExitUnit()
    {
        foreach (var it in exitTransforms)
        {
            RaycastHit info;
            if (Physics.Raycast(new Ray(it.position, Vector3.down), out info) && (info.collider.tag == "Land" || info.collider.tag == "Unit"))
            {
                ExitUnit(it);
                return;
            }
        }
    }
    public bool CanExit()
    {
        foreach (var it in exitTransforms)
        {
            RaycastHit info;
            if (Physics.Raycast(new Ray(it.position, Vector3.down), out info) && (info.collider.tag == "Land" || info.collider.tag == "Unit") )
            {
                return true;
            }
        }
        return false;
    }

    void UpdateContainedIndicator()
    {
        int i = 0;
        for (; i < contained.Count; ++i)
        {
            containedIndicator[i].SetActive(true);
        }
        for (; i < containedIndicator.Length; ++i)
        {
            containedIndicator[i].SetActive(false);
        }
        currentContained = contained.Count;
    }
}
