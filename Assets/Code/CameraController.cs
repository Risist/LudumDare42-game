using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController istance;
    public float cameraMovementSpeed;
    public float cameraMouseSpeed;
    public float scrollSpeed;
    public int ignoredSize = 1;

    public Vector2 rangeOfZoom = new Vector2(20f, 70f);
    
    private Vector3 mousePos;
    private Vector2 sizeOfScreen;
    private Vector2 force;

    public UnitMovement[] all;
    public int currentCount = 0;
    public Vector2 off;

    private void OnEnable()
    {
        istance = this;
    }

    private void Update()
    {

        int i = 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            all = FindObjectsOfType<UnitMovement>();
            while (i++ < 1000)
            {
                if (all[currentCount].useModule)
                {
                    Vector3 pos;
                    pos = all[currentCount].transform.position;
                    pos.y = transform.position.y;

                    UnitMovement.ResetSelection();
                    all[currentCount].SetSelected(true);

                    pos.x += off.x;
                    pos.z += off.y;

                    transform.position = pos;
                    currentCount = (currentCount + 1) % all.Length;
                    break;
                }
                else
                {
                    currentCount = (currentCount + 1) % all.Length;
                }
            }
        }

        Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") ) * cameraMovementSpeed;
        
        mousePos = Input.mousePosition;
        sizeOfScreen.x = Screen.width;
        sizeOfScreen.y = Screen.height;
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            Camera.main.fieldOfView += (Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed);
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, rangeOfZoom.x, rangeOfZoom.y);
        }

        if (mousePos.x <= 0 + ignoredSize)
        {
            force += new Vector2(-1,0);
        }
        else if (mousePos.x >= sizeOfScreen.x - ignoredSize)
        {
            force += new Vector2(1, 0);
        }

        if (mousePos.y <= 0 + ignoredSize)
        {
            force += new Vector2(0,-1);
        }
        else if(mousePos.y >= sizeOfScreen.y - ignoredSize)
        {
            force += new Vector2(0, 1);
        }

        force = force * cameraMouseSpeed;
        offset.x += force.x;
        offset.z += force.y;
        transform.position += offset * Time.deltaTime;

        force = new Vector2();
    }
}
