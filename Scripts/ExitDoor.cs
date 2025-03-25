using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public Animator door;  // animacija
    public GameObject openText;
    public AudioSource doorSound; // zvuk vrata

    public GameObject notEnoughBagsText;
    

    public bool inReach;
    public bool canEscape;




    void Start()
    {
        inReach = false;
        canEscape = false; // na pocetku nece moc pobic
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
            notEnoughBagsText.SetActive(false);
        }
    }





    void Update()
    {

        if (inReach && Input.GetButtonDown("Interact") && canEscape == true)  // ako imamo potreban(plavi) kljuc
        {
            DoorOpens();   // funkcija koja otvara vrata?
            notEnoughBagsText.SetActive(false);
            Debug.Log("It Opens");

        }

        else if (inReach && Input.GetButtonDown("Interact") && canEscape == false)  // ako imamo potreban(plavi) kljuc
        {

            notEnoughBagsText.SetActive(true);
            openText.SetActive(false);
            DoorCloses();
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
    }
}
