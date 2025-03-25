using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OpenBoxNoKey : MonoBehaviour
{
    public Animator boxOB;

    public GameObject openText;
    
    public AudioSource openSound;

    public GameObject key_drop;  // ovo je kljuc koji je predefiniran (uvijek je isti za ovu instancu, ali za drugu instancu je razlicit) 
    
  

    public bool inReach;
    public bool isOpen;

    public int randomNumber;

    //private static List<GameObject> dodijeljeniKljucevi = new List<GameObject>();

    void Start()
    {

        inReach = false;
        openText.SetActive(false);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
            //keyMissingText.SetActive(false);
        }
    }


    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
         
            openSound.Play();
            boxOB.SetBool("open", true);
            openText.SetActive(false);
            
            isOpen = true;


            key_drop.SetActive(true); // kad se kutija otvori predefinirani kljuc se stvori
            
        }
        

        /*
        else if (keyOBNeeded.activeInHierarchy == false && inReach && Input.GetButtonDown("Interact"))
        {
            openText.SetActive(false);
            keyMissingText.SetActive(true);
        }
        */

        if (isOpen)
        {
            boxOB.GetComponent<BoxCollider>().enabled = false;
            boxOB.GetComponent<OpenBoxNoKey>().enabled = false;
        }
    }

   
}


/*
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxNoKey : MonoBehaviour
{
    public Animator boxOB;
    public GameObject openText;
    public AudioSource openSound;

    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public GameObject drop4;
  
    public bool inReach;
    public bool isOpen;

    private static List<GameObject> assignedKeys = new List<GameObject>();

    void Start()
    {
        inReach = false;
        openText.SetActive(false);

        List<GameObject> availableKeys = new List<GameObject> { drop1, drop2, drop3, drop4 };

        // Remove already assigned keys from availableKeys
        foreach (var assignedKey in assignedKeys)
        {
            availableKeys.Remove(assignedKey);
        }


        Debug.Log("Available keys at the beginning:");
        foreach (var key in availableKeys)
        {
            Debug.Log(key.name);
        }

        int randomNumber = Random.Range(0, availableKeys.Count);

        while (assignedKeys.Contains(availableKeys[randomNumber]))
        {
            randomNumber = Random.Range(0, availableKeys.Count);
        }

        availableKeys[randomNumber].SetActive(true);
        Debug.Log("Assigned key: " + availableKeys[randomNumber].name);

        assignedKeys.Add(availableKeys[randomNumber]);
        Debug.Log("Assigned keys after adding: ");
        foreach (var key in assignedKeys)
        {
            Debug.Log(key.name);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }


    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            openSound.Play();
            boxOB.SetBool("open", true);
            openText.SetActive(false);
            isOpen = true;
            
            // Activate only the drop if the box is opened
            ActivateDrop();
        }

        if (isOpen)
        {
            boxOB.GetComponent<BoxCollider>().enabled = false;
            boxOB.GetComponent<OpenBoxNoKey>().enabled = false;
        }
    }

    void ActivateDrop()
    {
        foreach (var drop in assignedKeys)
        {
            drop.SetActive(true);
        }
    }
}
*/