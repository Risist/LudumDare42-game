using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStateDisplayer : MonoBehaviour {

    public Renderer healthRenderer;
    HealthController health;

    Vector3 baseScale;
    Color colorBase;

    private void Start()
    {
        health = GetComponentInParent<HealthController>();
        baseScale = transform.localScale;
        colorBase = healthRenderer.material.color;
    }
    public void Update()
    {
        Color color = colorBase;
        float percent = health.actual / health.max;
        percent = Mathf.Sqrt(percent);
        percent = Mathf.Sqrt(percent);
        percent = Mathf.Sqrt(percent);
        percent = Mathf.Sqrt(percent);

        color.r = (float)color.r * percent;
        color.g = (float)color.g * percent;
        color.b = (float)color.b * percent;

        healthRenderer.material.color = color;
        transform.localScale = baseScale * (0.25f + (health.max / 100.0f)*0.75f);
    }
}
