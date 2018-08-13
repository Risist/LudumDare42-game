using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickContainer : MonoBehaviour
{
    public static PickContainer istance;

    public int Wood;
    public int Gold;

    private void OnEnable()
    {
        istance = this;
    }

    public  void AddWood(int value)
    {
        Wood += value;
    }

    public void RemoveWood(int value)
    {
        Wood -= value;
    }
}
