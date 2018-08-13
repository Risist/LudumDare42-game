using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class FirePalce : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [System.Serializable]
    public struct SpawnData
    {
        public GameObject prefab;
        public int woodCost;
        public int goldCost;
    }
    public SpawnData[] spawnOptions;

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
            if (obj && obj.useModule)
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

    public void Recruit(int id)
    {
        bool b = PickContainer.istance.Wood >= spawnOptions[id].woodCost;
        b &= PickContainer.istance.Gold >= spawnOptions[id].goldCost;


        if (b)
        {
            Vector3 s = Random.onUnitSphere * 2.0f + Vector3.up * 50 + transform.position;
            Instantiate(spawnOptions[id].prefab, s, Quaternion.Euler(0, Random.value, 0));

            PickContainer.istance.Wood -= spawnOptions[id].woodCost;
            PickContainer.istance.Gold -= spawnOptions[id].goldCost;
        }
    }

    public void Recruit_1() { Recruit(0); }
    public void Recruit_2() { Recruit(1); }
    public void Recruit_3() { Recruit(2); }
    public void Recruit_4() { Recruit(3); }
    public void Recruit_5() { Recruit(4); }


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
