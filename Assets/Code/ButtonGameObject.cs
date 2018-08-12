using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonGameObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private Port port;
    private Renderer indicator;
    private GameObject canvas;
    public ChangeValueUI ValueUi;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            ValueUi.ChangeColorA(180.0f/255.0f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            ValueUi.ChangeColorA(0.35f);
        }
    }

}
