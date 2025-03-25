using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ExitDoor exitDoor;
    public GameObject escapeText;
    public GameObject cantEscText;
    public GameObject endgameScreen;
    public bool inReach;

    void Start()
    {
        inReach = false;
        cantEscText.SetActive(false);
        escapeText.SetActive(false);
        endgameScreen.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            escapeText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)  // ovo je da makne tekst "open" ako vise nisi u reachu
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            escapeText.SetActive(false);
            cantEscText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact") && exitDoor.canEscape == true)
        {
            endgameScreen.SetActive(true);

            Debug.Log("YOU ESCAPED!!!");
        }
        else if (inReach && Input.GetButtonDown("Interact") && exitDoor.canEscape == false)
        {
            cantEscText.SetActive(true);
        }
    }
}
