using UnityEngine;
using UnityEngine.UI;

public class RandomNumberGenerator : MonoBehaviour
{
    public Text numberText;
    public Keypad keypadScript; // Referenca na skriptu za otvaranje sefa
    public Text passwordText;
    

    void Start()
    {
        GenerateRandomNumber();

        keypadScript.answer = numberText.text;
        passwordText.text = numberText.text;  // ovo je za UI spremit sifru da se pokaze na ekranu
    }

    void GenerateRandomNumber()
    {
        string randomNumber = "";

        // Generiraj èetiri random broja od 1 do 9
        for (int i = 0; i < 4; i++)
        {
            randomNumber += Random.Range(1, 10).ToString();
        }

        // Postavi generirani broj kao tekst u gameobjectu
        numberText.text = randomNumber;
    }
}

