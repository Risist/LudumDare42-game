using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeValueUI : MonoBehaviour
{

    public void ChangeColorA(float targetValue)
    {
        Image[] images = GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            Color c = image.color;
            c.a = targetValue;
            image.color = c;
        }
    }
}
