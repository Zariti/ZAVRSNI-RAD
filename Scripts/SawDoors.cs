using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDoors : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator door;  // animacija
    public GameObject openText;
    public AudioSource doorSound; // zvuk vrata

    public GameObject invKey;
    public GameObject missingKey;

    public bool inReach;
    public bool isOpen;

    public GameObject plank1;
    public GameObject plank2;
    public GameObject plank3;

    public AudioSource sawSound; // zvuk vrata




    void Start()
    {
        inReach = false;
    }

    void OnTriggerEnter(Collider other)    // ovo je da prikaze tekst "open" ako si u reach-u
    {
        if (isOpen && other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
        else if (other.gameObject.tag == "Reach")
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
        if (isOpen && inReach && Input.GetButtonDown("Interact") && invKey.activeInHierarchy)
        {
            //door.GetComponent<BoxCollider>().enabled = false;
            //door.GetComponent<SawDoors>().enabled = false;
            openText.SetActive(false);
            return;
        }

        else if (inReach && Input.GetButtonDown("Interact") && invKey.activeInHierarchy)
        {
            /*
            sawSound.Play();
            plank1.SetActive(false);
            StartCoroutine(OpenDoorsDelay()); // ceka jednu sec
            plank2.SetActive(false);
            StartCoroutine(OpenDoorsDelay()); // ceka jednu sec
            plank3.SetActive(false);
            StartCoroutine(OpenDoorsDelay()); // ceka jednu sec
            DoorOpens();   // funkcija koja otvara vrata?
            */
            StartCoroutine(OpenDoorsDelay());
            openText.SetActive(false);
            isOpen = true;
       

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
        if (isOpen)
        {
            door.GetComponent<BoxCollider>().enabled = false;
            //door.GetComponent<SawDoors>().enabled = false;
        }
        



    }

    public void DisableText()
    {
        openText.SetActive(false);
    }
    public void DoorOpens()
    {
        //Debug.Log("It Opens");
        door.SetBool("Open", true);  // ovo je animacija door koja poziva njenu setBool metodu
        door.SetBool("Closed", false);   // ovo je animacija door koja poziva njenu setBool metodu
        doorSound.Play();  // zvuk vrata

    }

    public void DoorCloses()
    {
        //Debug.Log("It Closes");
        door.SetBool("Open", false);   // ovo je animacija door koja poziva njenu setBool metodu
        door.SetBool("Closed", true);  // ovo je animacija door koja poziva njenu setBool metodu
    }

    private IEnumerator ShowMissingKeyText()
    {
        missingKey.SetActive(true); // Prikazivanje teksta
        yield return new WaitForSeconds(2f); // Èekanje dvije sekunde
        missingKey.SetActive(false); // Skrivanje teksta
    }

    private IEnumerator OpenDoorsDelay()
    {
        sawSound.Play();
        plank1.SetActive(false);
        yield return new WaitForSeconds(1f); // Èekanje 1 sec
        plank2.SetActive(false);
        yield return new WaitForSeconds(1f); // Èekanje 1 sec
        plank3.SetActive(false);
        yield return new WaitForSeconds(1f); // Èekanje 1 sec
        DoorOpens();   // funkcija koja otvara vrata?
        
        
    }
}
