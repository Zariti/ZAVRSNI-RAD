using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Ovaj namespace omoguæava restartiranje scene

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverUI; // Referenca na GameOver UI Canvas
    public GameObject player;

    void Start()
    {
        gameOverUI.SetActive(false); // Sakrij Game Over ekran na poèetku igre
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true); // Prikaži Game Over ekran
        Time.timeScale = 0f; // Zaustavi igru
        //player.GetComponent<FPSController>().enabled = false;
        //StartCoroutine(ShowGameOverWithDelay());
        RestartGame();
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        //StartCoroutine(ShowGameOverWithDelay());


        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void RestartGame()
    {
        //gameOverUI.SetActive(true);
        Time.timeScale = 1f; // Nastavi igru
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restartaj trenutnu scenu
    }

    IEnumerator ShowGameOverWithDelay()
    {
        // Prièekaj waitTime sekundi prije prikaza Game Over ekrana
        yield return new WaitForSeconds(3f);

        
        
    }
}

