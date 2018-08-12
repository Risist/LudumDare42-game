using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStateDisplayer : MonoBehaviour {

    public Renderer healthRenderer;
    HealthController health;

    private void Start()
    {
        health = GetComponentInParent<HealthController>();
    }
    public void Update()
    {
        Color color = healthRenderer.material.color;
        float percent = health.actual / health.max;
        percent = Mathf.Sqrt(percent);
        percent = Mathf.Sqrt(percent);
        percent = Mathf.Sqrt(percent);
        percent = Mathf.Sqrt(percent);

        color.r = (float)color.r * percent;
        color.g = (float)color.g * percent;
        color.b = (float)color.b * percent;

        healthRenderer.material.color = color;
    }
}
