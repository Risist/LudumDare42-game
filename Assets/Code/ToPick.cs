using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPick : MonoBehaviour
{
    public PickItems pickItems;
    public int addonValue;

    [HideInInspector]
    public bool isGet;
}

public enum PickItems
{
    Wood,
    Gold,
}