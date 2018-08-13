using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuliderScript : MonoBehaviour
{
    public GameObject balist;
    public GameObject port;
    public GameObject palisade;

    public int costBalista;
    public int costPort;
    public int costPalisade;


    public BulidUIDetector BulidUiDetector;

    public GameObject BulidUI;
    public ChangeValueUI changeValueUi;

    public GameObject IstancedGameObject;
    private UnitMovement cont;
    public Vector3 offset = new Vector3(-1, 1, -1);
    
    bool arriving;
    int sds = 0;

    int cost;

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
        BulidUI.transform.position = transform.parent.position + offset;

        if (IstancedGameObject )
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {


                if (arriving)
                {
                    float distacneSq = (cont.transform.position - IstancedGameObject.transform.position).sqrMagnitude; //Vector3.Distance(cont.transform.position, IstancedGameObject.transform.position);
                    const float distance_build = 3.0f * 3.0f;

                    Debug.Log(distacneSq);
                    if (distacneSq < distance_build )
                    {
                        IstancedGameObject.SetActive(true);

                        var c = IstancedGameObject.GetComponentsInChildren<Collider>();
                        foreach (var coll in c)
                        {
                            if (coll.enabled)
                            {
                                coll.enabled = true;
                            }
                        }


                        IstancedGameObject = null;
                        arriving = false;
                        cont.ResetAim();
                        sds = 0;
                    }
                }
                else
                {
                    if(sds == 0)
                        IstancedGameObject.transform.position = hit.point;
                    else if(sds == 2)
                    {
                        float rotation = -Mathf.Atan2(
                                hit.point.z - IstancedGameObject.transform.position.z,
                                hit.point.x - IstancedGameObject.transform.position.x
                                ) * Mathf.Rad2Deg;
                        IstancedGameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
                    }

                    if(Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        Destroy(IstancedGameObject);
                        arriving = false;
                        sds = 0;
                    }
                    else
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (sds == 1 && hit.collider.tag == "Land")
                        {
                            //

                            IstancedGameObject.transform.position = hit.point;
                            

                            sds = 2;

                        }
                        else if (sds == 2)
                        {
                            IstancedGameObject.SetActive(false);
                            arriving = true;
                            cont.SetAim(IstancedGameObject.transform.position);

                            PickContainer.istance.Wood -= cost;
                        }
                    }
                }
            }
        }

    }


    //Ballist
    public void BulidOne()
   {
       if (IstancedGameObject != null) return;
        if (PickContainer.istance.Wood < costBalista) return;
        // IstancedGameObject = null;
        IstancedGameObject = Instantiate(balist);
        var c = IstancedGameObject.GetComponentsInChildren<Collider>();
        foreach (var coll in c)
        {
            if (coll.enabled)
            {
                coll.enabled = false;
            }
        }
        sds = 1;

         cost = costBalista;
    }

    //Palsiade
    public void BulidTwo()
    {
        if(IstancedGameObject != null) return;
        if (PickContainer.istance.Wood < costPalisade) return;
        //IstancedGameObject = null;
        IstancedGameObject = Instantiate(palisade);

        var c = IstancedGameObject.GetComponentsInChildren<Collider>();
        foreach (var coll in c)
        {
            if (coll.enabled)
            {
                coll.enabled = false;
            }
        }
        sds = 1;
        cost = costPalisade;
    }

    //Port
    public void BulidThree()
    {
        if (IstancedGameObject != null) return;
        if (PickContainer.istance.Wood < costPort) return;
        //IstancedGameObject = null;
        IstancedGameObject = Instantiate(port);

        var c = IstancedGameObject.GetComponentsInChildren<Collider>();
        foreach (var coll in c)
        {
            if (coll.enabled)
            {
                coll.enabled = false;
            }
        }
        sds = 1;

        cost =  costPort;
    }

}
