using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonGameObject : MonoBehaviour,IPointerClickHandler
{
    public ShipContainer ShipContainer;
    private MeshRenderer filter;

    private void Awake()
    {
        filter = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (ShipContainer.currentContained > 0  && ShipContainer.inPort)
        {
            filter.enabled = true;
        }
        else
        {
            filter.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.clickCount == 1)
        {
            Port[] ports = FindObjectsOfType<Port>();

            if (ports == null )
            {
                return;
            }

            foreach (var port in ports)
            {
                if (port.ship != null && port.ship == ShipContainer && ShipContainer.currentContained > 0)
                {
                    ShipContainer.ExitUnit(port.exitPoint);
                    break;
                } 
            }
            
        }
    }

}
