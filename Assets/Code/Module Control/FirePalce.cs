using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class FirePalce : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Transform SpawnTransform;
    public GameObject spawnObj;

    public GameObject UIFirePlace;
    public ChangeValueUI ValueUi;

    private bool used;

    private void Awake()
    {
        ValueUi.ChangeColorA(0.1f);
    }

    private void OnEnable()
    {
        if (!ValueUi)
        {
           Debug.LogError("the ValueUi parameter is empty");
        }
        
    }

    private void Update()
    {
        UIFirePlace.SetActive(used);
        //UIFirePlace.transform.LookAt(Camera.main.transform);

        var colli = Physics.OverlapSphere(transform.position, 2f);

        foreach (var collider in colli)
        {
            var obj = collider.gameObject.GetComponent<UnitMovement>();
            if (obj)
            {
                used = true;
                break;
            }
            else
            {
                used = false;
            }
        }
    }

    public void Recruit(int cost,PickItems pickItems)
    {
        int currentValue = 0;

        switch (pickItems)
        {
            case PickItems.Wood:
                currentValue = PickContainer.istance.Wood;
                if (currentValue >= cost)
                {
                    Instantiate(spawnObj, SpawnTransform);
                    PickContainer.istance.Wood -= cost;

                }
                else
                {
                    ShowMessageInUI("you do not have enough Wood");
                }
                break;
        }
    }

    private void ShowMessageInUI(string msg)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            ValueUi.ChangeColorA(1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            ValueUi.ChangeColorA(0.1f);
        }
    }
}
