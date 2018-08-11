using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipContainer : MonoBehaviour {

    public int maxContained;
    List<GameObject> contained;

    public bool Insert(GameObject obj)
    {
        if(contained.Count < maxContained)
        {
            contained.Add(obj);
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
        contained.Remove(obj);
    }
	

    
}
