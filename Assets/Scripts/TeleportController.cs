using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{

    Rigidbody rb;
    // attach the main camera
    public Camera mainCamera;
    // set the ground
    public LayerMask whatIsGround;
    bool isGrounded;

    // Laser Pointer variables
    public GameObject laserPointer;
    bool laserPointerActive = false;

    // set time boundaries to teleport
    bool readyToTeleport = true;
    public float teleportCooldown = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // set the laser pointer to inactive
        laserPointer.SetActive(false);
    }

    
    void Update()
    {
        TeleportAction();
        LaserPointerActive();
    }
    
    private void TeleportMovement()
    {
        // Teleport to the position of the mouse click
        // we use a raycast originating from the player camera, and check if it hits the ground
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 11f, whatIsGround))
        {
            
                //print the point where the ray hits the ground
                Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
                Debug.Log("Teleport Movement Function:" + newPosition);
                rb.transform.position = newPosition;
        }
        else
        {
                Debug.Log("Not Ground");
            
        }


    }
    private void TeleportAction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && readyToTeleport && laserPointerActive == true )
        {
            readyToTeleport = false;
            Debug.Log("Right Mouse Pressed");
            TeleportMovement();
            Invoke(nameof(ResetTeleport), teleportCooldown);
        }
    }
    private void ResetTeleport()
    {
        readyToTeleport = true;
    }
    // laser pointer activation function
    private void LaserPointerActive()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (laserPointerActive == false)
            {
                laserPointer.SetActive(true);
                laserPointerActive = true;
            }
            else
            {
                laserPointer.SetActive(false);
                laserPointerActive = false;
            }
        }
        
    }
}
