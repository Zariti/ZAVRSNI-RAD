/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;

    }
}
*/
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRange = 10f; // Udaljenost na kojoj �e neprijatelj primijetiti igra�a
    public float viewAngle = 90f; // Kutni pogled u stupnjevima
    public List<Transform> idle_spots;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Provjera udaljenosti izme�u neprijatelja i igra�a
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Provjera da li je igra� unutar vidnog polja neprijatelja
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Ako je igra� unutar vidnog polja i unutar odre�ene udaljenosti
        if (angleToPlayer <= viewAngle / 2f && distanceToPlayer <= detectionRange)
        {
            agent.destination = player.position; // Neprijatelj prati igra�a
        }
        else
        {
            int randomNumber = Random.Range(0, 5);
            Vector3 randomPosition = idle_spots[randomNumber].position;
            agent.destination = randomPosition; // Neprijatelj stoji na mjestu
            //agent.destination = transform.position; // Neprijatelj stoji na mjestu
        }
    }
}
*/
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRange = 10f; // Udaljenost na kojoj �e neprijatelj primijetiti igra�a
    public float viewAngle = 90f; // Kutni pogled u stupnjevima
    public List<Transform> idle_spots;
    public AudioClip detected;

    private Vector3 currentDestination; // Trenutna odredi�na pozicija agenta


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        // Provjera udaljenosti izme�u neprijatelja i igra�a
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Provjera da li je igra� unutar vidnog polja neprijatelja
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Ako je igra� unutar vidnog polja i unutar odre�ene udaljenosti
        if (angleToPlayer <= viewAngle / 2f && distanceToPlayer <= detectionRange)
        {
            agent.destination = player.position; // Neprijatelj prati igra�a
            detected

        }
        else
        {
            // Ako je agent stigao do trenutne destinacije, postavljamo novu random destinaciju
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
        }
    }

    // Postavljanje nove random destinacije
    void SetNewRandomDestination()
    {
        int randomNumber = Random.Range(0, 4);
        //do
        //{
        //    randomNumber = Random.Range(0, 4);
        //}
        //while (idle_spots[randomNumber].position == currentDestination); // Ponovno generiraj random broj ako je isti kao trenutna destinacija

        currentDestination = idle_spots[randomNumber].position;
        agent.destination = currentDestination;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
public class NavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRange = 20; // Udaljenost na kojoj �e neprijatelj primijetiti igra�a
    public float viewAngle = 90f; // Kutni pogled u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource detectionSound; // Referenca na AudioSource komponentu koja �e reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    private Vector3 currentDestination; // Trenutna odredi�na pozicija agenta
    private int cnt = 0;

    public Animator characterAnimation;


    public GameObject gameOverScreenOBJ; // Referenca na GameOverScreen skriptu
    public GameOverScreen gameOverScreen; // Referenca na GameOverScreen skriptu
    public float gameOverDistance = 10f;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
        //gameOverScreen = FindObjectOfType<GameOverScreen>(); // Prona�i GameOverScreen skriptu u sceni
    }

    // Update is called once per frame
    void Update()
    {

        
        // Provjera udaljenosti izme�u neprijatelja i igra�a
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 2f)
        {
            //Debug.Log("Game Over triggered"); // Dodaj log
            //gameOverScreen.ShowGameOver(); // Prikaz Game Over ekrana
            
            gameOverScreen.ShowGameOver();
            
            
        }

        
        // Provjera da li je igra� unutar vidnog polja neprijatelja
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        //RaycastHit hit;

        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        //Debug.DrawLine(transform.position, player.position, obstacleBetween ? Color.red : Color.green);

        // Ako je igra� unutar vidnog polja i unutar odre�ene udaljenosti
        if (angleToPlayer <= viewAngle / 2f && distanceToPlayer <= detectionRange && !obstacleBetween)
        {
            
            agent.destination = player.position; // Neprijatelj prati igra�a
            agent.speed = 5.5f;
            Debug.Log("Distance to player: " + distanceToPlayer);
            characterAnimation.SetBool("Seen", true);  // ovo je animacija door koja poziva njenu setBool metodu
            characterAnimation.SetBool("Lost", false);   // ovo je animacija door koja poziva njenu setBool metodu

            if (!heartbeatSound.isPlaying)
            {
                heartbeatSound.Play();
            }
            // Reproduciraj zvuk detekcije
            if (!detectionSound.isPlaying && cnt == 0) // Provjeri je li zvuk ve� u reprodukciji kako bi izbjegli neprekidno ponavljanje
            {
                detectionSound.Play();
                cnt = 1;
                //detectionSound.enabled = false; // Onemogu�i AudioSource nakon �to se zvuk reproducira
            }
        }
        
        else
        {
            // Ako je agent stigao do trenutne destinacije, postavljamo novu random destinaciju

            characterAnimation.SetBool("Seen", false);  
            characterAnimation.SetBool("Lost", true);

            agent.speed = 2f;

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
                cnt = 0;
                //detectionSound.enabled = true; // Omogu�i AudioSource opet
                
            }
        }

        
    }


    // Postavljanje nove random destinacije
    void SetNewRandomDestination()
    {
        int randomNumber = Random.Range(0, 4);
        currentDestination = idle_spots[randomNumber].position;
        agent.destination = currentDestination;
    }

    // Provjera udaljenosti za Game Over
    
    
}
*/


public class NavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRange = 20; // Udaljenost na kojoj �e neprijatelj primijetiti igra�a
    public float viewAngle = 90f; // Kutni pogled u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource detectionSound; // Referenca na AudioSource komponentu koja �e reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    private Vector3 currentDestination; // Trenutna odredi�na pozicija agenta
    private int cnt = 0;

    public Animator characterAnimation;


    public GameObject gameOverScreenOBJ; // Referenca na GameOverScreen skriptu
    public GameOverScreen gameOverScreen; // Referenca na GameOverScreen skriptu
    public float gameOverDistance = 10f;


    ////////////////////
    private float timeWatchingPlayer = 0f;  // Vreme koliko neprijatelj gleda igra�a
    public float watchTime = 5f;  // Minimalno vreme gledanja (5 sekundi)



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
        //gameOverScreen = FindObjectOfType<GameOverScreen>(); // Prona�i GameOverScreen skriptu u sceni
    }

    // Update is called once per frame
    void Update()
    {
        // Provjera udaljenosti izme�u neprijatelja i igra�a
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 2f)
        {
            //Debug.Log("Game Over triggered"); // Dodaj log
            //gameOverScreen.ShowGameOver(); // Prikaz Game Over ekrana

            gameOverScreen.ShowGameOver();


        }


        // Provjera da li je igra� unutar vidnog polja neprijatelja
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        //RaycastHit hit;

        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        //Debug.DrawLine(transform.position, player.position, obstacleBetween ? Color.red : Color.green);

        // Ako je igra� unutar vidnog polja i unutar odre�ene udaljenosti
        if (angleToPlayer <= viewAngle / 2f && distanceToPlayer <= detectionRange && !obstacleBetween)
        {
            timeWatchingPlayer += Time.deltaTime;  // Po�ni merenje vremena gledanja igra�a
            

            agent.isStopped = true;  // Zaustavi agenta
            characterAnimation.SetBool("FirstSeen", true);
            characterAnimation.SetBool("Lost", false);
            characterAnimation.SetBool("Seen", false);
            //agent.destination = player.position;
            // Rotiraj neprijatelja da gleda prema igra�u
            Vector3 lookDirection = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);

            if (timeWatchingPlayer >= watchTime)// ako ga gleda dulje od 5 sekundi
            {
                if (distanceToPlayer <= detectionRange / 2f)  // Ako je igra� blizu, neprijatelj po�inje da juri
                {
                    agent.isStopped = false;  // oslobodi agenta
                    agent.destination = player.position;
                    agent.speed = 5.5f;
                    //isChasing = true;
                    characterAnimation.SetBool("Seen", true);
                    characterAnimation.SetBool("Lost", false);
                    characterAnimation.SetBool("FirstSeen", false);

                    if (!heartbeatSound.isPlaying)
                    {
                        heartbeatSound.Play();
                    }

                    if (!detectionSound.isPlaying && cnt == 0)
                    {
                        detectionSound.Play();
                        cnt = 1;
                    }
                }
                
            }
            else
            {
                // Ako je agent stigao do trenutne destinacije, postavljamo novu random destinaciju
                characterAnimation.SetBool("FirstSeen", false);
                characterAnimation.SetBool("Seen", false);
                characterAnimation.SetBool("Lost", true);

                agent.speed = 2f;

                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    SetNewRandomDestination();
                    cnt = 0;
                    //detectionSound.enabled = true; // Omogu�i AudioSource opet
                }
            }

        }

        else
        {
            // Ako je agent stigao do trenutne destinacije, postavljamo novu random destinaciju
            agent.isStopped = false;
            characterAnimation.SetBool("FirstSeen", false);
            characterAnimation.SetBool("Seen", false);
            characterAnimation.SetBool("Lost", true);

            agent.speed = 2f;

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
                cnt = 0;
                //detectionSound.enabled = true; // Omogu�i AudioSource opet
            }
        }
    }

    // Postavljanje nove random destinacije
    void SetNewRandomDestination()
    {
        int randomNumber = Random.Range(0, 4);
        currentDestination = idle_spots[randomNumber].position;
        agent.destination = currentDestination;
    }
    // Provjera udaljenosti za Game Over
}


