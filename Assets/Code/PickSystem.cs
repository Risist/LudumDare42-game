using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSystem : MonoBehaviour
{
    private void Update()
    {
        var coli = Physics.OverlapSphere(transform.position, 3f);
       
        foreach (var collider in coli)
        {
            var toPick = collider.gameObject.GetComponent<ToPick>();

            if (toPick && toPick.isGet == false)
            {
                toPick.isGet = true;
                TakeIn(toPick);
                
                break;
            }

        }
    }

    void TakeIn(ToPick pick)
    {
        switch (pick.pickItems)
        {
            case PickItems.Wood:
                PickContainer.istance.AddWood(pick.addonValue);
                break;
        }
        Destroy(pick.gameObject);

    }
}
