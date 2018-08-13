using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulidUIDetector : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public bool showBulidUI;

    private ChangeValueUI cValueUi;
    UnitMovement unit;

    void Awake()
    {
        cValueUi = new ChangeValueUI();
        cValueUi.ChangeColorA(0, GetComponent<Image>());

        unit = GetComponentInParent<UnitMovement>();
    }
    bool wasSelected = false;
    private void Update()
    {
        if (unit.IsSelected() != wasSelected)
            showBulidUI = unit.IsSelected();

        wasSelected = unit.IsSelected();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null && unit.IsSelected())
        {
            showBulidUI = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            showBulidUI = false;
        }
    }
}
