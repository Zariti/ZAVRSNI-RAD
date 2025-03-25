using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlindManScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    //public float detectionRange = 10f; // Udaljenost na kojoj æe neprijatelj primijetiti igraèa
    //public float viewAngle = 90f; // Kutni pogled u stupnjevima
    public List<Transform> idle_spots;
    //public AudioSource detectionSound; // Referenca na AudioSource komponentu koja æe reproducirati zvuk detekcije
    //public AudioSource heartbeatSound; // <3
    //public LayerMask obstacleLayer;
    public AudioSource memeSound;
    private Vector3 currentDestination; // Trenutna odredišna pozicija agenta
    //public GameObject gameOverScreenOBJ;
    public GameOverScreen gameOverScreen;
    //private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 1.5f)
        {
            //Debug.Log("Game Over triggered"); // Dodaj log
            //gameOverScreen.ShowGameOver(); // Prikaz Game Over ekrana
            //gameOverScreen.SetActive(true);
            gameOverScreen.ShowGameOver();


        }
        if (!memeSound.isPlaying)
        {
            //memeSound.Play();
        }
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            //SetNewRandomDestination();
            StartCoroutine(WaitAndSetNewDestination(1f));
        }
        

    }

    // Postavljanje nove random destinacije
    void SetNewRandomDestination()
    {
        int randomNumber = Random.Range(0, 4);
        currentDestination = idle_spots[randomNumber].position;
        agent.destination = currentDestination;
    }

    IEnumerator WaitAndSetNewDestination(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetNewRandomDestination();
    }
}




