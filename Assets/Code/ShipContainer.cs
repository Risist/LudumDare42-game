﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipContainer : MonoBehaviour {

    public int maxContained;
    List<UnitMovement> contained = new List<UnitMovement>();
    public GameObject[] containedIndicator;

    private void Start()
    {
        UpdateContainedIndicator();
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
        contained.Remove(obj);

        UpdateContainedIndicator();
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
    }

    
}