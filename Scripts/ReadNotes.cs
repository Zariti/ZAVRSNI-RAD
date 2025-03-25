using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadNotes : MonoBehaviour
{
    public GameObject paperObj;
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject inv;

    public GameObject pickUpText;
    public GameObject guideToNote;

    public AudioSource pickUpSound;

    //public Keypad keypad;
    public GameObject passwordText;

    public bool inReach;
    public bool seen;
    public bool isOpen; // provjera je li gledamo u note



    void Start()
    {
        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        pickUpText.SetActive(false);

        inReach = false;
        seen = false;
        paperObj.SetActive(true);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
            noteUI.SetActive(false);
        }
    }

    IEnumerator DelayedExecution()
    {
        guideToNote.SetActive(true);
        yield return new WaitForSeconds(3); // Ovdje postavljamo odgodu od 2 sekunde
        guideToNote.SetActive(false);
        // Ovdje stavite kod koji želite izvršiti nakon odgode
        // Na primjer, linije koda koje želite izvršiti nakon odgode:
        // keypadHUD.SetActive(false);
        // safe.GetComponent<BoxCollider>().enabled = false;
        // safe.GetComponent<Keypad>().enabled = false;
        //noteUI.SetActive(false);
    }


    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach)
        {
            seen = true;
            //noteUI.SetActive(true);
            //pickUpSound.Play();
            //paperObj.SetActive(false); // kao uzima papir objekt i on nestaje

            Vector3 newPosition = new Vector3(10f, -5f, 5f); // Postavite nove koordi
            paperObj.transform.position = newPosition;

            //passwordText.SetActive(true);
            //hud.SetActive(false);
            //inv.SetActive(false);
            //player.GetComponent<FPSController>().enabled = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            StartCoroutine(DelayedExecution());


        }

        if (seen && Input.GetButtonDown("Note") && !isOpen)
        {
            Vector3 newPosition = new Vector3(10f, -5f, 5f); // Postavite nove koordi
            paperObj.transform.position = newPosition;
            //passwordText.SetActive(true);
            noteUI.SetActive(true);
            isOpen = true;
        }
        else if (seen && Input.GetButtonDown("Note") && isOpen)
        {
            noteUI.SetActive(false);
            isOpen = false;
        }
        

    }


    public void ExitButton()
    {

        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        player.GetComponent<FPSController>().enabled = true;

    }
}