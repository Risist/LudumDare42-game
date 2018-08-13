using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonGameObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    ShipContainer ship;
    //private Renderer indicator;
    private GameObject canvas;
    public ChangeValueUI ValueUi;

    Quaternion rotation;
    Vector3 positionOffset;

    private void Start()
    {
        ship = GetComponentInParent<ShipContainer>();
        canvas = transform.Find("Canvas").gameObject;

        rotation = transform.rotation;
        positionOffset = transform.position - transform.parent.position;

    }
    private void Update()
    {
        //indicator.enabled = port.ship;
        canvas.SetActive(!ship.IsEmpty() && ship.CanExit() );

        transform.rotation = rotation;
        transform.position = transform.parent.position + positionOffset;
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
