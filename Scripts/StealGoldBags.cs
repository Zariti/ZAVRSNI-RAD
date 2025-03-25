using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealGoldBags : MonoBehaviour
{
    public GameObject goldBagOB;
    //public GameObject invOB;
    public GameObject pickUpText;
    public AudioSource TakeGoldSound;
    public Text stolenGoldBagsText; // Dodajte referencu na UI Text element
    public BagsCollected bagsCollected; // Referenca na BagsCollected skriptu
    

    public bool inReach;
    public int cntGUI;


    public ExitDoor exit;
    //public bool isTaken;

    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        //invOB.SetActive(false);
        //cnt = 0; // brojac ukradenih gold bags
        //isTaken = false;
        UpdateStolenGoldBagsText(); // A�urirajte tekst na po�etku
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
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            transform.position += new Vector3(-14f, -10f, -23f); // Pomi�e objekt
            //TakeGoldSound.Play();    ///ZVUK AKO JE NA NULL ZNA STVARAT PROBLEME JER SE SVE ISPOD NJEGA NECE IZVRSIT, ZATO MI CNT NIJE RADIO!!!
            pickUpText.SetActive(false);
            Debug.Log("Interacted: Calling IncrementCount"); // Dodajemo debug log
            bagsCollected.IncrementCount(); // Pove�ava count u BagsCollected
            cntGUI = bagsCollected.cnt;
            
            //cnt = bagsCollected.Cnt; // A�urira lokalni count
            UpdateStolenGoldBagsText(); // A�urirajte tekst kad ukradete zlatnu vre�icu
        }

        if (cntGUI > 5)
        {
            Debug.Log("You can escape now");
            exit.canEscape = true;
        }
    }
    
    void UpdateStolenGoldBagsText()
    {
        stolenGoldBagsText.text = "Stolen Gold Bags: " + cntGUI.ToString() + "/6"; // A�urirajte tekst na UI Text elementu
    }
    
}


/*
public class StealGoldBags : MonoBehaviour
{
    public GameObject goldBagOB;
    public GameObject invOB;
    public GameObject pickUpText;
    public AudioSource TakeGoldSound;
    public Text stolenGoldBagsText; // Dodajte referencu na UI Text element

    public bool inReach;
    public int cnt;
    public bool isTaken;

    public BagsCollected bagsCollected; // Referenca na BagsCollected skriptu

    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        invOB.SetActive(false);
        cnt = 0; // brojac ukradenih gold bags
        isTaken = false;
        UpdateStolenGoldBagsText(); // A�urirajte tekst na po�etku
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

        }
    }


    void Update()
    {
        
        if (inReach && Input.GetButtonDown("Interact"))
        {

            //goldBagOB.SetActive(false);
            transform.position += new Vector3(-14f, -10f, -23f);// * Time.deltaTime;
            TakeGoldSound.Play();;
            //invOB.SetActive(true);
            pickUpText.SetActive(false);
            
            //cnt++;
            //isTaken = true;
            //UpdateStolenGoldBagsText(); // A�urirajte tekst kad ukradete zlatnu vre�icu
            
        }
        


    }

    void UpdateStolenGoldBagsText()
    {
        stolenGoldBagsText.text = "Stolen Gold Bags: " + cnt.ToString() + "/10"; // A�urirajte tekst na UI Text elementu
    }
}

*/