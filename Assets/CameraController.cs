using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraMovementSpeed;

    private void Update()
    {
        Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") ) * cameraMovementSpeed;
        transform.position += offset * Time.deltaTime;
        
    }
}
