using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulidUIDetector : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public bool showBulidUI;

    private ChangeValueUI cValueUi;

    void Awake()
    {
        cValueUi = new ChangeValueUI();
        cValueUi.ChangeColorA(0, GetComponent<Image>());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
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
