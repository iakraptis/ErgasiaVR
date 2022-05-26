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
        // use fixed update for this? Answer below
        if (heldObj != null)
        {
           MoveObject();
        }
    }

    private void FixedUpdate()
    {
        // needs a refactor. Decouple the keypresses using a seperate function and a different function for the movement
        //if (heldObj != null)
        //{
        //    MoveObject();
        //}
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = holdParent.position - heldObj.transform.position;
            // no force is more stable
            // heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
            // rotate the object using the IKJLUO keys
            if (Input.GetKey(KeyCode.I))
            {
                heldObj.transform.Rotate(Vector3.up * Time.deltaTime * 100);
            }
            if (Input.GetKey(KeyCode.K))
            {
                heldObj.transform.Rotate(Vector3.down * Time.deltaTime * 100);
            }
            if (Input.GetKey(KeyCode.J))
            {
                heldObj.transform.Rotate(Vector3.left * Time.deltaTime * 100);
            }
            if (Input.GetKey(KeyCode.L))
            {
                heldObj.transform.Rotate(Vector3.right * Time.deltaTime * 100);
            }
            // rotate the object on the z axis
            if (Input.GetKey(KeyCode.U))
            {
                heldObj.transform.Rotate(Vector3.forward * Time.deltaTime * 100);
            }
            if (Input.GetKey(KeyCode.O))
            {
                heldObj.transform.Rotate(Vector3.back * Time.deltaTime * 100);
            }
            // move the object away from the camera using the mouse wheel
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                heldObj.transform.position += mainCamera.transform.forward * Time.deltaTime * 50;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                heldObj.transform.position -= mainCamera.transform.forward * Time.deltaTime * 50;
            }
            


        }
    }
    
    void PickupObject (GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
           Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity =  false;
            objRig.freezeRotation = true;
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
            heldRig.freezeRotation = false;
            heldRig.drag = 1;
            heldRig.transform.parent = null;
            heldObj = null;
        }
    }
}
