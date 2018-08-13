using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ArmyManager : MonoBehaviour {

    public float height;
    Vector3 startPoint;

	// Use this for initialization
	void Start () {
	}

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            startPoint = GetAim();
        }   
        else if (Input.GetButtonUp("Fire1"))
        {
            if(!Input.GetKey(KeyCode.LeftControl))
                UnitMovement.ResetSelection();

            Vector3 endPoint = GetAim();
            Vector3 diff = startPoint - endPoint;
            var c = Physics.OverlapBox((startPoint + endPoint) * 0.5f,
                new Vector3(
                    diff.x > 0.0f ? diff.x : -diff.x,
                    height,
                    diff.z > 0.0f ? diff.z : -diff.z
                ));

            foreach(var it in c)
            {
                UnitMovement movemet = it.GetComponent<UnitMovement>();
                if(movemet)
                {
                    movemet.SetSelected(true);
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    Vector3 GetAim()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return  hit.point;
    }
}
