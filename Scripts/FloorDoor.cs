using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    public Animator floorDoor;
    public GameObject openText;
    public AudioSource floorDoorSound;

    public bool inReach;

    void Start()
    {
        inReach = false;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            FloorDoorOpens();   // funkcija koja otvara vrata?
        }
        else
        {
            FloorDoorCloses();
        }
    }

    void FloorDoorOpens()
    {
        floorDoor.SetBool("open", true);
        floorDoor.SetBool("closed", false);
        floorDoorSound.Play();
    }

    void FloorDoorCloses()
    {
        floorDoor.SetBool("open", false);
        floorDoor.SetBool("closed", true);
        floorDoorSound.Play();
    }
}
