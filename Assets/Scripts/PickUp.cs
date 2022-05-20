using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float pickUpRange = 10f;
    private GameObject heldObj;
    public Transform holdParent;
    public float moveForce = 250;
    // attach the main camera
    public Camera mainCamera;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            if (heldObj == null)
            {
                // maybe needs our own system as an origin? Use the camera?
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, pickUpRange))
                {
                    Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
                    Debug.Log("Pickup hit at:" + newPosition);
                    PickupObject(hit.transform.gameObject);
                }

            }
            else
            {
                DropObject();
            }    
        }
        // use fixed update for this?
        if (heldObj != null)
        {
            MoveObject();
        }
    }
    
    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = holdParent.position - heldObj.transform.position;
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }
    
    void PickupObject (GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
           Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity =  false;
            objRig.drag = 10;

            objRig.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }

    void DropObject()
    {
        if (heldObj != null)
        {
            Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
            heldRig.useGravity = true;
            heldRig.drag = 1;
            heldRig.transform.parent = null;
            heldObj = null;
        }
    }
}
