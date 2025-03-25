
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
public class PauseGame : MonoBehaviour
{
    public GameObject menu;
    private bool isPaused = false;
    private bool cursorWasVisible;

    void Start()
    {
        menu.SetActive(false);
        Cursor.visible = false; // Sakrij kursor prilikom pokretanja igre
        Cursor.lockState = CursorLockMode.Locked; // Zakljuèaj kursor
    }

    void Update()
    {
        if (!isPaused && Input.GetButtonDown("pause"))
        {
            Pause();
        }
        else if (isPaused && Input.GetButtonDown("pause"))
        {
            Resume();
        }

        // Provjeri je li igra pauzirana i je li pritisnuta tipka za izlazak iz pauze
        if (isPaused && !Cursor.visible)
        {
            // Klikni lijevim gumbom miša
            Debug.Log("Kliknuto lijevim gumbom miša nakon izlaska iz pauze.");
            OnLeftMouseClick();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        cursorWasVisible = Cursor.visible;
        Cursor.visible = true; // Prikaži kursor kad je igra pauzirana
        Cursor.lockState = CursorLockMode.None; // Omoguæi slobodno kretanje kursora
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        Cursor.visible = cursorWasVisible; // Vrati prethodno stanje kursora
        Cursor.lockState = CursorLockMode.Locked; // Zakljuèaj kursor kad je igra aktvina
        isPaused = false;
    }

    // Funkcija koja æe se izvršiti kada se klikne lijevim gumbom miša
    private void OnLeftMouseClick()
    {
        // Simulacija klika lijevim gumbom miša
        Debug.Log("Simuliran klik lijevim gumbom miša.");
        // Ovdje možeš dodati akcije koje se trebaju izvršiti nakon klika
    }
}
*/

public class PauseGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject resume;
    public GameObject quit;

    public GameObject player;

    public bool isOpen = false;
    //public bool on;
    //public bool off;

    private bool cursorWasVisible;



    void Start()
    {
        menu.SetActive(false);
        isOpen = false;
    }




    void Update()
    {
        if (menu.activeInHierarchy)
        {
            //hud.SetActive(false);
            //inv.SetActive(false);
            player.GetComponent<FPSController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            player.GetComponent<FPSController>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        if (isOpen == false && Input.GetButtonDown("pause"))
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            //off = false;
            isOpen = true;
            //on = true;
            //GetComponent<FPSController>().enabled = false;     ovo ne radi


            //player.GetComponent<FPSController>().enabled = true;

            //cursorWasVisible = Cursor.visible;
            //Cursor.visible = true; // Sakrij kursor kad je igra pauzirana
            //Cursor.lockState = CursorLockMode.None; // Omoguæi slobodno kretanje kursora
        }

        else if (isOpen == true && Input.GetButtonDown("pause"))
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            //off = true;
            //on = false;
            isOpen = false;
            //GetComponent<FPSController>().enabled = true;
            //player.GetComponent<FPSController>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = cursorWasVisible; // Vrati prethodno stanje kursora
            //Cursor.lockState = CursorLockMode.Locked; // Zakljuèaj kursor kad je igra aktvina
        }
        

    }

    public void Resume()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        isOpen = false;
        //off = true;
        //on = false;

    }

    public void MainMenu()
    {
        // Uèitajte scenu pod nazivom "Title"
        //Time.timeScale = 0;
        SceneManager.LoadScene("Title");
    }
}
