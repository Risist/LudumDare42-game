using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraMovementSpeed;
    public float cameraMouseSpeed;
    public int ignoredSize = 1;

    public Vector2 rangeOfZoom = new Vector2(20f, 70f);
    
    private Vector3 mousePos;
    private Vector2 sizeOfScreen;
    private Vector2 force;

    private void Update()
    {
        Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") ) * cameraMovementSpeed;
        
        mousePos = Input.mousePosition;
        sizeOfScreen.x = Screen.width;
        sizeOfScreen.y = Screen.height;
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            Camera.main.fieldOfView += (Input.GetAxis("Mouse ScrollWheel") * -15);
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

        force = force * cameraMouseSpeed * Time.deltaTime;
        offset.x += force.x;
        offset.z += force.y;
        transform.position += offset * Time.deltaTime;

        force = new Vector2();
    }
}
