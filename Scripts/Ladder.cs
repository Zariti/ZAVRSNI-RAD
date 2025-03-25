using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform playerController; // vec ugradena klasa koja daje info o poziciji, rotaciji i skali objekta(playera)
    bool inside = false;
    public float speed = 3f;
    public FPSController player; // moja klasa/skripta u kojoj je kod za gibanje player-a
    public AudioSource sound;  // vec ugradena klasa u unity za manipulaciju zvukom


    public float ladderSlopeLimit = 90f;
    private float defaultSlopeLimit;
    public float ladderSpeedMultiplier = 3f; // Množitelj brzine kretanja uz ljestve

    void Start()
    {
        player = GetComponent<FPSController>();
        inside = false;
        defaultSlopeLimit = player.GetComponent<CharacterController>().slopeLimit;  // stavljeno mi je po default 45
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            Debug.Log("TouchingLadderTrue");
            //player.enabled = false;
            inside = !inside;

            // Postavite slopeLimit na vrijednost za ljestve kada igraè dodirne ljestve
            player.GetComponent<CharacterController>().slopeLimit = ladderSlopeLimit;

            player.GetComponent<FPSController>().walkSpeed *= ladderSpeedMultiplier;
            player.GetComponent<FPSController>().runSpeed *= ladderSpeedMultiplier;
        }
        else if (col.gameObject.tag == "Ladder2")
        {
            Debug.Log("TouchingLadderTrue");
            //player.enabled = false;
            inside = !inside;

            // Postavite slopeLimit na vrijednost za ljestve kada igraè dodirne ljestve
            player.GetComponent<CharacterController>().slopeLimit = ladderSlopeLimit;

            player.GetComponent<FPSController>().walkSpeed *= ladderSpeedMultiplier - 0.2f;
            player.GetComponent<FPSController>().runSpeed *= ladderSpeedMultiplier - 0.2f;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            Debug.Log("TouchingLadderFalse");
            //player.enabled = true;
            inside = !inside;

            // Vratite slopeLimit na njegovu standardnu vrijednost kada igraè napusti ljestve
            player.GetComponent<CharacterController>().slopeLimit = defaultSlopeLimit;

            player.GetComponent<FPSController>().walkSpeed /= ladderSpeedMultiplier;
            player.GetComponent<FPSController>().runSpeed /= ladderSpeedMultiplier;
        }
    }



    void Update()
    {
        if (inside == true && Input.GetKey("w"))
        {
            sound.enabled = true;
            sound.loop = true;
            Debug.Log("PRIE");
            /*
            Vector3 moveDirection = player.transform.forward;
            Debug.Log("PRIE");

            // Provjeravamo je li igraè okrenut prema dolje
            if (Vector3.Dot(moveDirection, Vector3.down) > 0.5f)
            {
                Debug.Log("Triba bi ic doli");

                // Ako je igraè okrenut prema dolje, pomièemo ga prema dolje niz ljestve
                player.transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            */
            /*
            Vector3 ladderDirection = (transform.position - player.transform.position).normalized;
            player.transform.position -= ladderDirection * speed * Time.deltaTime;
            Debug.Log(player.transform.position);

            Debug.Log("ISPOD SMJER LJESTVI:");
            Debug.Log(ladderDirection);
            */


            if ((player.playerCamera.transform.rotation.eulerAngles.x > 50f) && (player.playerCamera.transform.rotation.eulerAngles.x < 80.003f)) // ako igrac drzi w i gleda dolje da ide prema dolje niz skale
            {
                Debug.Log(player.playerCamera.transform.rotation.eulerAngles.x);
                Debug.Log("Triba bi ic doli");
                Vector3 ladderDirection = (transform.position - player.transform.position).normalized;
                player.transform.position -= ladderDirection * speed * Time.deltaTime;

            }
            

        }

        else if (inside == true && Input.GetKey("s"))
        {
            Vector3 ladderDirection = (transform.position - player.transform.position).normalized;
            player.transform.position -= ladderDirection * speed * Time.deltaTime;
            sound.enabled = true;
            sound.loop = true;
        }

        else
        {
            sound.enabled = false;
            sound.loop = false;
        }

    }

}
    /*
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<FPSController>();
        inside = false;
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Ladder")
        {
            Debug.Log("TouchingLadderTrue");
            player.enabled = false;
            inside = !inside;  // ako taknemo ladder inside ce postat true jer je bio false na pocetku
        }


    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Ladder")
        {
            Debug.Log("TouchingLadderFalse");
            player.enabled = true;
            inside = !inside;  // ako vise ne diramo ladder inside ce postat false jer je bio true maloprije
        }


    }
    void Update()
    {
        if (inside)
        {
            Vector3 ladderDirection = (transform.position - player.transform.position).normalized;

            if (Input.GetKey("w"))
            {
                player.transform.position += ladderDirection * speed * Time.deltaTime;
                sound.enabled = true;
                sound.loop = true;
            }

            if (Input.GetKey("s"))
            {
                player.transform.position -= ladderDirection * speed * Time.deltaTime;
                sound.enabled = true;
                sound.loop = true;
            }

            // Ovdje dodajte logiku za reprodukciju zvuka, ako je potrebno
        }
        else
        {
            sound.enabled = false;
            sound.loop = false;
        }
    }


    
    void Update()
    {
        if (inside == true && Input.GetKey("w"))
        {
            player.transform.position += Vector3.up /
            speed * Time.deltaTime;
        }

        if (inside == true && Input.GetKey("s"))
        {
            player.transform.position += Vector3.down /
            speed * Time.deltaTime;
        }

        if (inside == true && Input.GetKey("w"))
        {
            sound.enabled = true;
            sound.loop = true;
        }

        else if (inside == true && Input.GetKey("s"))
        {
            sound.enabled = true;
            sound.loop = true;
        }

        else
        {
            sound.enabled = false;
            sound.loop = false;
        }

    }
    
}
*/