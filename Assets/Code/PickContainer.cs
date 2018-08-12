using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickContainer : MonoBehaviour
{
    public static PickContainer istance;

    public int Wood;

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
