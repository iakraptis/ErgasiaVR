using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotificationTriggerEvent : MonoBehaviour
{
    [Header("UI Content")]
    [SerializeField] private GameObject notificationUI;
    [SerializeField] private Text notificationTextUI;    
    [SerializeField] private RawImage notificationImageUI;
    
    
    
    [Header("Message Customisation")]
    
    [SerializeField][TextArea] private string notificationMessage;
    [SerializeField] private Texture notificationImage;


    [Header("Notification Removal")]
    [SerializeField] private bool removeAfterExit = false;
    [SerializeField] private bool disableAfterTimer = false;
    [SerializeField] private float disableTimer = 1.5f;

    public Collider objectCollider;
    private bool inRange = false;

    private void Awake()
    {
        objectCollider = gameObject.GetComponent<Collider>();
        notificationUI.SetActive(false);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger");
            inRange = true;
            StartCoroutine(EnableNotification());
            removeAfterExit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && removeAfterExit)
        {
            RemoveNotification();
            notificationUI.SetActive(false);
            inRange = false;
        }
      
    }

    IEnumerator EnableNotification()
    {
        
        
      
        notificationUI.SetActive(true);
        notificationTextUI.text = notificationMessage;
        notificationImageUI.texture = notificationImage;
        if (disableAfterTimer)
        {
            yield return new WaitForSeconds(disableTimer);
            RemoveNotification();
        }
    }

    void RemoveNotification()
    {
        
        // disable the text notification
        notificationTextUI.text = "";
        notificationUI.SetActive(false);
        

    }
}
