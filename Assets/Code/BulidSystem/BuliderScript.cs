using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuliderScript : MonoBehaviour
{
    public GameObject balist;
    public GameObject port;
    public GameObject palisade;


    public BulidUIDetector BulidUiDetector;

    public GameObject BulidUI;
    public ChangeValueUI changeValueUi;

    public GameObject IstancedGameObject;
    private UnitMovement cont;
    private Transform savedPosTransform;

    private void Start()
    {
        //changeValueUi = BulidUI.GetComponent<ChangeValueUI>();
      //  changeValueUi.ChangeColorA(0.4f);
        cont = transform.parent.GetComponent<UnitMovement>();
    }

    void Update()
    {
        BulidUI.SetActive(BulidUiDetector.showBulidUI);   
        BulidUI.transform.rotation = Quaternion.Euler(138.206f, 167.253f, 170.2f);
        BulidUI.transform.position = transform.parent.position + new Vector3(0,1,-1);

        if (IstancedGameObject != null)
        {
            float distacne = 1;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {


                var c = IstancedGameObject.GetComponentsInChildren<Collider>();
                foreach (var coll in c)
                {
                    if (coll.enabled)
                    {
                        coll.enabled = false;
                    }
                }

                IstancedGameObject.transform.position =hit.point;

                distacne = Vector3.Distance(cont.transform.position, IstancedGameObject.transform.position);

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    savedPosTransform = hit.transform;
                    cont.SetAim(IstancedGameObject.transform.position);
                    print(savedPosTransform.position);

                }

             //   print(distacne);
                if (distacne < 3f && savedPosTransform != null)
                {
                    print("Readt");
                    foreach (var coll in c)
                    {
                        if (coll.enabled == false)
                        {
                            coll.enabled = true;
                        }
                    }
                  //  print(savedPosTransform.position);
                   // IstancedGameObject.transform.position = savedPosTransform.position;

                    IstancedGameObject = null;
                    savedPosTransform = null;
                }
            }
        }

    }


    //Ballist
    public void BulidOne()
   {
       if (IstancedGameObject != null) return;
        // IstancedGameObject = null;
        IstancedGameObject = Instantiate(balist);
   }

    //Palsiade
    public void BulidTwo()
    {
        if(IstancedGameObject != null) return;
        //IstancedGameObject = null;
        IstancedGameObject = Instantiate(palisade);
    }

    //Port
    public void BulidThree()
    {
        if (IstancedGameObject != null) return;
        //IstancedGameObject = null;
        IstancedGameObject = Instantiate(port);
    }

}
