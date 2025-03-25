using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animator door;  // animacija
    public GameObject openText;
    public AudioSource doorSound; // zvuk vrata

    public GameObject invKey;
    public GameObject missingKey;

    public bool inReach;




    void Start()
    {
        inReach = false;
    }

    void OnTriggerEnter(Collider other)    // ovo je da prikaze tekst "open" ako si u reach-u
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)  // ovo je da makne tekst "open" ako vise nisi u reachu
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }





    void Update()
    {

        if (inReach && Input.GetButtonDown("Interact") && invKey.activeInHierarchy)
        {
            DoorOpens();   // funkcija koja otvara vrata?
        }
        else if (inReach && Input.GetButtonDown("Interact") && !invKey.activeInHierarchy)
        {
            //DoorOpens();   // funkcija koja otvara vrata?
          
            StartCoroutine(ShowMissingKeyText());
           
        }
        else 
        {
            DoorCloses();  // funkcija koja zatvara vrata?
            
        }




    }
    void DoorOpens()
    {
        //Debug.Log("It Opens");
        door.SetBool("Open", true);  // ovo je animacija door koja poziva njenu setBool metodu
        door.SetBool("Closed", false);   // ovo je animacija door koja poziva njenu setBool metodu
        doorSound.Play();  // zvuk vrata

    }

    void DoorCloses()
    {
        //Debug.Log("It Closes");
        door.SetBool("Open", false);   // ovo je animacija door koja poziva njenu setBool metodu
        door.SetBool("Closed", true);  // ovo je animacija door koja poziva njenu setBool metodu
        doorSound.Play();
    }

    private IEnumerator ShowMissingKeyText()
    {
        missingKey.SetActive(true); // Prikazivanje teksta
        yield return new WaitForSeconds(2f); // Èekanje dvije sekunde
        missingKey.SetActive(false); // Skrivanje teksta
    }


}
