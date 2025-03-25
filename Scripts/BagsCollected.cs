using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagsCollected : MonoBehaviour
{
    public int cnt;
    //public int totalItems = 5;
    void Start()
    {
        cnt = 0;
        Debug.Log("BagsCollected Start: cnt = " + cnt); // Dodajemo debug log
    }
    public void IncrementCount()
    {
        cnt++;
        Debug.Log("IncrementCount: cnt = " + cnt); // Dodajemo debug log
        /*
        if (cnt >= totalItems)
        {
            // Ovdje možete dodati logiku za otkljuèavanje vrata ili omoguæavanje izlaza
            Debug.Log("All items collected!");
        }
        */
    }
}
