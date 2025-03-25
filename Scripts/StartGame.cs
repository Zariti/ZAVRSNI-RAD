using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string levelName;  // za scenu koju ocemo da se otvori kad stisnemo tipku (kod mene je uvik ista)
    public void loadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
