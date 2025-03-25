using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityStandardAssets.Characters.FirstPerson;

/*
public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadOB;
    public GameObject hud;
    public GameObject inv;


    public GameObject animateOB;
    public Animator ANI;


    public Text textOB;
    public string answer = "12345";

    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;

    public bool animate;


    void Start()
    {
        keypadOB.SetActive(false);

    }


    public void Number(int number)
    {
        textOB.text += number.ToString();
        button.Play();
    }

    public void Execute()
    {
        if (textOB.text == answer)
        {
            correct.Play();
            textOB.text = "Right";

        }
        else
        {
            wrong.Play();
            textOB.text = "Wrong";
        }


    }

    public void Clear()
    {
        {
            textOB.text = "";
            button.Play();
        }
    }

    public void Exit()
    {
        keypadOB.SetActive(false);
        inv.SetActive(true);
        hud.SetActive(true);
        //player.GetComponent<FirstPersonController>().enabled = true;
    }

    public void Update()
    {
        if (textOB.text == "Right" && animate)
        {
            ANI.SetBool("animate", true);
            Debug.Log("its open");
        }


        if (keypadOB.activeInHierarchy)
        {
            hud.SetActive(false);
            inv.SetActive(false);
            //player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }


}
*/
/*

public class Keypad : MonoBehaviour
{
    [SerializeField] private Text Ans;
    [SerializeField] private Animator safe;


    public string answer = "1234";


    public void Number(int number)
    {
        Ans.text += number.ToString();

    }

    public void Execute()
    {
        if (Ans.text == answer)
        {
            Ans.text = "SUCCESS";
            safe.SetBool("Open", true);
           
        }
        else
        {
            Ans.text = "WRONG CODE";
        }
    }

}
*/

public class Keypad : MonoBehaviour
{
    [SerializeField] private Text Ans;
    [SerializeField] private Animator safe;

    public GameObject openText;
    public string answer = "1234";
    public bool inReach; // Da biste pratili je li igraè u reach-u od sefa
    public GameObject keypadHUD;
    public GameObject houseKey;
    public GameObject player;

    public bool isOpen = false;
    IEnumerator DelayedExecution()
    {
        yield return new WaitForSeconds(2); // Ovdje postavljamo odgodu od 2 sekunde
        // Ovdje stavite kod koji želite izvršiti nakon odgode
        // Na primjer, linije koda koje želite izvršiti nakon odgode:
        // keypadHUD.SetActive(false);
        // safe.GetComponent<BoxCollider>().enabled = false;
        // safe.GetComponent<Keypad>().enabled = false;
    }
    void Start()
    {
        houseKey.SetActive(false);
        keypadHUD.SetActive(false);
        inReach = false;
        Ans.text = ""; // Postavljanje poèetne vrijednosti Ans.text na prazan strin
        isOpen = false;
    }
    void Update()
    {
        // Provjerava je li igraè u reach-u i pritisnuo "Interact" tipku
        

        if (inReach && Input.GetButtonDown("Interact") && !isOpen)
        {
            keypadHUD.SetActive(true);
            Execute();
            //houseKey.SetActive(true);
            //isOpen = true;
            //player.GetComponent<FPSController>().enabled = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        
        if (keypadHUD.activeInHierarchy)
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

        if (isOpen && !keypadHUD.activeInHierarchy)
        {
            return;
        }
        
        /*
        else
        {
            // Ako keypadHUD nije aktivan, izvrši sljedeæe akcije
            //hud.SetActive(true);
            //inv.SetActive(true);
            player.GetComponent<FPSController>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        */
        /*
        if (isOpen)
        {
            //StartCoroutine(DelayedExecution());
            //keypadHUD.SetActive(false);
            //isOpen = true; // Resetiramo isOpen nakon što se izvrši DelayedExecution
            //safe.GetComponent<BoxCollider>().enabled = false;
            //safe.GetComponent<Keypad>().enabled = false;
            
        }
        */
    }

    // Postavlja inReach na true kada igraè uðe u trigger zone
    void OnTriggerEnter(Collider other)
    {

        if (keypadHUD.activeInHierarchy)
        {
            keypadHUD.SetActive(false);
            openText.SetActive(false);

        }

        if (other.gameObject.tag == "Reach" && !isOpen)
        {
            inReach = true;
            openText.SetActive(true);
        }
        else if (other.gameObject.tag == "Reach" && isOpen == true)
        {
            inReach = false;
            openText.SetActive(false);
        }
        /*
        else if (other.gameObject.tag == "Reach" && isOpen == true)
        {
            //openText.SetActive(false);
        }
        */
    }

    // Postavlja inReach na false kada igraè izaðe iz trigger zone
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
            keypadHUD.SetActive(false);
        }
    }

    // Metoda za unos broja na tipkovnici
    public void Number(int number)
    {
        Ans.text += number.ToString();
    }

    // Metoda za izvršavanje provjere unesene lozinke i otvaranja sefa
    public void Execute()
    {
        if (Ans.text == "")
        {
            // Ako je prazan string, nemoj postaviti "WRONG CODE"
            return;
        }
        if (Ans.text == answer && isOpen == true)
        {


            //enabled = false;
            Ans.text = "ALREADY OPEN";
            return;
            //keypadHUD.SetActive(false);
        }
        else if (Ans.text == answer)
        {
            Ans.text = "SUCCESS";
            safe.SetBool("Open", true);
            houseKey.SetActive(true);
            openText.SetActive(false);
            isOpen = true;  // otvorit ce se vrata sefa
            
            //keypadHUD.SetActive(false);
        }
        else
        {
            Ans.text = "WRONG CODE";
            openText.SetActive(true);
            //keypadHUD.SetActive(false);
        }
    }

    public void Exit()
    {
        keypadHUD.SetActive(false);
        player.GetComponent<FPSController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Clear()
    {
        Ans.text = ""; // Postavlja tekst na prazan string
    }

}