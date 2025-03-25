using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;

    public AudioSource turnOn;
    public AudioSource turnOff;

    public bool on;
    public bool off;


    void Start()
    {
        off = true;
        flashlight.SetActive(false); // deaktivira nas gameobjekt po defaultu
    }

    // Update is called once per frame
    void Update()
    {
        if (off && Input.GetButtonDown("F")) //ako je svica ugasena i stisnemo F
        {
            flashlight.SetActive(true); // aktivira se nas flashlight gameobject (pali svjetiljku)
            turnOn.Play();  // i playa se zvuk svjetiljke
            off = false;
            on = true;
        }
        else if (on && Input.GetButtonDown("F")) // ako je svica upaljena a stisnemo F
        {
            flashlight.SetActive(false); // onda cemo deaktivirat flashlight gameobjekt (ugasit svjetiljku)
            turnOff.Play();
            off = true;
            on = false;
        }
    }
}
