using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeValueUI : MonoBehaviour
{

    public void ChangeColorA(float value)
    {
        Image[] images = GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            Color c = image.color;
            c.a = value;
            image.color = c;
        }
    }

    public void ChangeColorA(float value, Image targetImage)
    {
        Color c = targetImage.color;
        c.a = value;
        targetImage.color = c;
    }
}
