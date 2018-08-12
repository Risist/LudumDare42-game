using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonGameObject : MonoBehaviour
{
    
    private Port port;
    private Renderer indicator;
    private GameObject canvas;

    private void Start()
    {
        port = GetComponentInParent<Port>();
        indicator = GetComponent<Renderer>();
        canvas = transform.Find("Canvas").gameObject;
    }
    private void Update()
    {
        indicator.enabled = port.ship;
        canvas.SetActive(port.ship && !port.ship.IsEmpty());
    }

    
    
}
