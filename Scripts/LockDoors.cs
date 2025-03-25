using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LockDoors : MonoBehaviour
{
    // Start is called before the first frame update

    //public Animator door;  // animacija
    //public AudioSource doorSound; // zvuk vrata

    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onTriggerEnter.Invoke();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onTriggerExit.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
