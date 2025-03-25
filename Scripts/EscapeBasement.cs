using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBasement : MonoBehaviour
{
    // Start is called before the first frame update
    public BagsCollected bagsCollected;
    public GameObject openText;
    public bool inReach;
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
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            
        }
    }
}
