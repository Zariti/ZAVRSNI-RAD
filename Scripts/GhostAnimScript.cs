using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimScript : MonoBehaviour
{
    public GameObject[] frames;
    public float frameRate = 0.1f; // vreme izmeðu frame-ova
    private int currentFrame;
    private float timer;
    private bool goingForward = true; // Indikator smera animacije

    void Start()
    {
        currentFrame = 0;
        timer = 0f;

        // Iskljuèite sve frame-ove osim prvog
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i].SetActive(i == currentFrame);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            // Iskljuèite trenutni frame
            frames[currentFrame].SetActive(false);

            // Odluèite sledeæi frame
            if (goingForward)
            {
                currentFrame++;
                if (currentFrame >= frames.Length)
                {
                    currentFrame = frames.Length - 2; // Prebacite se na predzadnji frame
                    goingForward = false; // Promenite smer
                }
            }
            else
            {
                currentFrame--;
                if (currentFrame < 0)
                {
                    currentFrame = 1; // Prebacite se na drugi frame
                    goingForward = true; // Promenite smer
                }
            }

            // Ukljuèite sledeæi frame
            frames[currentFrame].SetActive(true);

            timer = 0f; // Resetujte tajmer
        }
    }
}
