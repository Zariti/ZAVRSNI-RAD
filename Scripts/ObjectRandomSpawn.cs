using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomSpawn : MonoBehaviour
{
    public Transform oB;

    public Transform[] spawnPoints;



    void Start()
    {
        int indexNumber = Random.Range(0, spawnPoints.Length);
        oB.position = spawnPoints[indexNumber].position;
        oB.rotation = spawnPoints[indexNumber].rotation;
    }

    void Update()
    {

    }
}
