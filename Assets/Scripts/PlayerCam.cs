using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        // lock cursor and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 65;
    }
     void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);



        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

     void FixedUpdate ()
    {
        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
    }

}
