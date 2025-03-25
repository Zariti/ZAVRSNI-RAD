/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatueScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRange = 10f; // Udaljenost na kojoj æe neprijatelj primijetiti igraèa
    public float viewAngle = 360f; // Kutni pogled u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource detectionSound; // Referenca na AudioSource komponentu koja æe reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    private Vector3 currentDestination; // Trenutna odredišna pozicija agenta
    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {

        // Provjera udaljenosti izmeðu neprijatelja i igraèa
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Provjera da li je igraè unutar vidnog polja neprijatelja
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        //RaycastHit hit;

        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        //Debug.DrawLine(transform.position, player.position, obstacleBetween ? Color.red : Color.green);

        // Ako je igraè unutar vidnog polja i unutar odreðene udaljenosti
        if (angleToPlayer <= viewAngle / 2f && distanceToPlayer <= detectionRange && !obstacleBetween)
        {
            agent.destination = player.position; // Neprijatelj prati igraèa
            if (!heartbeatSound.isPlaying)
            {
                heartbeatSound.Play();
            }
            // Reproduciraj zvuk detekcije
            if (!detectionSound.isPlaying && cnt == 0) // Provjeri je li zvuk veæ u reprodukciji kako bi izbjegli neprekidno ponavljanje
            {
                detectionSound.Play();
                cnt = 1;
                //detectionSound.enabled = false; // Onemoguæi AudioSource nakon što se zvuk reproducira
            }
        }
        else
        {
            // Ako je agent stigao do trenutne destinacije, postavljamo novu random destinaciju
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
                cnt = 0;
                //detectionSound.enabled = true; // Omoguæi AudioSource opet

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
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatueScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float playerViewAngle = 90f; // Kutni pogled igraèa u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource detectionSound; // Referenca na AudioSource komponentu koja æe reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    private Vector3 currentDestination; // Trenutna odredišna pozicija agenta
    private int cnt = 0;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
    }

    void Update()
    {
        // Provjera da li igraè gleda neprijatelja
        if (!IsPlayerLookingAtEnemy())
        {
            // Ako igraè ne gleda neprijatelja, neprijatelj se može kretati
            agent.isStopped = false;
            agent.destination = player.position;
            if (!isMoving)
            {
                SetNewRandomDestination();
                isMoving = true;
            }

            // Ako agent stiže na trenutnu destinaciju, postavi novu nasumiènu destinaciju
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
        }
        else
        {
            // Ako igraè gleda neprijatelja, neprijatelj stoji
            agent.isStopped = true;
            isMoving = false;
        }
    }

    // Postavljanje nove random destinacije
    void SetNewRandomDestination()
    {
        int randomNumber = Random.Range(0, idle_spots.Count);
        currentDestination = idle_spots[randomNumber].position;
        agent.destination = currentDestination;
    }

    // Provjera da li igraè gleda neprijatelja
    bool IsPlayerLookingAtEnemy()
    {
        Vector3 directionToEnemy = transform.position - player.position;
        float angle = Vector3.Angle(player.forward, directionToEnemy);
        return angle < playerViewAngle / 2f;
    }

    void StopAgentImmediately()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
*/
/* BEST FOR NOW
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatueScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float playerViewAngle = 150f; // Kutni pogled igraèa u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource detectionSound; // Referenca na AudioSource komponentu koja æe reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    public float chaseRange = 30f;


    private Vector3 currentDestination; // Trenutna odredišna pozicija agenta
    private int cnt = 0;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f; // Osigurava trenutno zaustavljanje na destinaciji
        SetNewRandomDestination();
    }

    void Update()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        // Provjera da li igraè gleda neprijatelja
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!IsPlayerLookingAtEnemy())
        {
            // Ako igraè ne gleda neprijatelja, neprijatelj se može kretati
            agent.isStopped = false;
            if (obstacleBetween && distanceToPlayer <= chaseRange)
            {
                //agent.destination = player.position;
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                // Dodavanje offseta u suprotnom smeru od igraèa
                Vector3 destination = player.position - directionToPlayer * 1.5f; // 1.5f je offset udaljenost, možete je prilagoditi

                // Postavljanje destinacije agenta
                agent.destination = destination;
            }

            else if (!obstacleBetween && distanceToPlayer <= chaseRange)
            {
                //agent.destination = player.position;
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                // Dodavanje offseta u suprotnom smeru od igraèa
                Vector3 destination = player.position - directionToPlayer * 1.5f; // 1.5f je offset udaljenost, možete je prilagoditi

                // Postavljanje destinacije agenta
                agent.destination = destination;
            }

            else if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
            
        }
        else
        {
            // Ako igraè gleda neprijatelja, neprijatelj stoji
            StopAgentImmediately();
            isMoving = false;
        }
    }

    // Postavljanje nove random destinacije
    void SetNewRandomDestination()
    {
        int randomNumber = Random.Range(0, idle_spots.Count);
        currentDestination = idle_spots[randomNumber].position;
        agent.SetDestination(currentDestination);
    }

    // Provjera da li igraè gleda neprijatelja
    bool IsPlayerLookingAtEnemy()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        if (!obstacleBetween)
        {
            Vector3 directionToEnemy = transform.position - player.position;
            float angle = Vector3.Angle(player.forward, directionToEnemy);
            return angle < playerViewAngle / 2f;
        }
        return false;
    }

    // Funkcija za trenutno zaustavljanje agenta
    void StopAgentImmediately()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
*/
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatueScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float playerViewAngle = 150f; // Kutni pogled igraèa u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource detectionSound; // Referenca na AudioSource komponentu koja æe reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    public float chaseRange = 30f;

    private Vector3 currentDestination; // Trenutna odredišna pozicija agenta
    private int cnt = 0;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f; // Osigurava trenutno zaustavljanje na destinaciji
        SetNewRandomDestination();
    }

    void Update()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        // Provjera da li igraè gleda neprijatelja
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!IsPlayerLookingAtEnemy())
        {
            // Ako igraè ne gleda neprijatelja, neprijatelj se može kretati
            agent.isStopped = false;
            if (obstacleBetween && distanceToPlayer <= chaseRange)
            {
                //agent.destination = player.position;
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                // Dodavanje offseta u suprotnom smeru od igraèa
                Vector3 destination = player.position - directionToPlayer * 1.5f; // 1.5f je offset udaljenost, možete je prilagoditi

                // Postavljanje destinacije agenta
                agent.destination = destination;
            }
            else if (!obstacleBetween && distanceToPlayer <= chaseRange)
            {
                //agent.destination = player.position;
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                // Dodavanje offseta u suprotnom smeru od igraèa
                Vector3 destination = player.position - directionToPlayer * 1.5f; // 1.5f je offset udaljenost, možete je prilagoditi

                // Postavljanje destinacije agenta
                agent.destination = destination;
            }
            else if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
        }
        else
        {
            // Ako igraè gleda neprijatelja, neprijatelj stoji
            StopAgentImmediately();
            isMoving = false;
        }
    }

    // Postavljanje nove random destinacije
    void SetNewRandomDestination()
    {
        int randomNumber = Random.Range(0, idle_spots.Count);
        currentDestination = idle_spots[randomNumber].position;
        agent.SetDestination(currentDestination);
    }

    // Provjera da li igraè gleda neprijatelja
    bool IsPlayerLookingAtEnemy()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        if (!obstacleBetween)
        {
            Vector3 directionToEnemy = transform.position - player.position;
            float angle = Vector3.Angle(player.forward, directionToEnemy);

            if (angle < playerViewAngle / 2f)
            {
                return true;
            }

            // Provjera da li je neprijatelj na rubu ekrana
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
            {
                // Ako je neprijatelj unutar vidljivog dela kamere
                return true;
            }
        }
        return false;
    }

    // Funkcija za trenutno zaustavljanje agenta
    void StopAgentImmediately()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatueScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float playerViewAngle = 150f; // Kutni pogled igraèa u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource detectionSound; // Referenca na AudioSource komponentu koja æe reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    public float chaseRange = 30f;

    private Vector3 currentDestination; // Trenutna odredišna pozicija agenta
    private int cnt = 0;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f; // Osigurava trenutno zaustavljanje na destinaciji
        SetNewRandomDestination();
    }

    void Update()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        // Provjera da li igraè gleda neprijatelja
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!IsPlayerLookingAtEnemy() && !IsEnemyInView())
        {
            // Ako igraè ne gleda neprijatelja, neprijatelj se može kretati
            agent.isStopped = false;
            if (obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (!obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
        }
        else if (!IsPlayerLookingAtEnemy() && IsEnemyInView() && obstacleBetween)
        {
            // Ako igraè ne gleda neprijatelja, neprijatelj se može kretati
            agent.isStopped = false;
            if (obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (!obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
        }
        else
        {
            // Ako igraè gleda neprijatelja ili je neprijatelj u vidnom polju kamere, neprijatelj stoji
            StopAgentImmediately();
            isMoving = false;
        }
    }

    // Postavljanje nove random destinacije [ZASAD ISKLJUCENO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!]
    void SetNewRandomDestination()
    {
        //iskomentirat odavde
        int randomNumber = Random.Range(0, idle_spots.Count);
        currentDestination = idle_spots[randomNumber].position;
        agent.SetDestination(currentDestination);
        // do ovde
    }

    // Provjera da li igraè gleda neprijatelja
    bool IsPlayerLookingAtEnemy()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        if (!obstacleBetween)
        {
            Vector3 directionToEnemy = transform.position - player.position;
            float angle = Vector3.Angle(player.forward, directionToEnemy);
            return angle < playerViewAngle / 2f;
        }
        return false;
    }

    // Provjera da li je neprijatelj unutar vidnog polja kamere
    bool IsEnemyInView()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }

    // Funkcija za trenutno zaustavljanje agenta
    void StopAgentImmediately()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    // Pomicanje prema igraèu s offsetom
    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 destination = player.position - directionToPlayer * 1.5f; // 1.5f je offset udaljenost, možete je prilagoditi
        agent.destination = destination;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatueScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float playerViewAngle = 150f; // Kutni pogled igraèa u stupnjevima
    public List<Transform> idle_spots;
    public AudioSource scareSound; // Referenca na AudioSource komponentu koja æe reproducirati zvuk detekcije
    public AudioSource heartbeatSound; // <3
    public LayerMask obstacleLayer;

    public float chaseRange = 30f;

    private Vector3 currentDestination; // Trenutna odredišna pozicija agenta
    private int cnt = 0; // za prvi jumpscare
    private bool isMoving = false;

    public GameOverScreen gameOverScreen; // Referenca na GameOverScreen skriptu

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f; // Osigurava trenutno zaustavljanje na destinaciji
        SetNewRandomDestination();
    }

    void Update()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        // Provjera da li igraè gleda neprijatelja
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 1.7f)
        {
            //Debug.Log("Game Over triggered"); // Dodaj log
            //gameOverScreen.ShowGameOver(); // Prikaz Game Over ekrana

            gameOverScreen.ShowGameOver();


        }

        if (distanceToPlayer <= 3f && IsPlayerLookingAtEnemy() && cnt == 0)
        {
            //Debug.Log("Game Over triggered"); // Dodaj log
            //gameOverScreen.ShowGameOver(); // Prikaz Game Over ekrana

            //gameOverScreen.ShowGameOver();
            scareSound.Play();
            cnt++;


        }

        if (!IsPlayerLookingAtEnemy() && !IsEnemyInView())
        {
            // Ako igraè ne gleda neprijatelja, neprijatelj se može kretati
            agent.isStopped = false;
            if (obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (!obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
        }
        else if (!IsPlayerLookingAtEnemy() && IsEnemyInView() && obstacleBetween)
        {
            // Ako igraè ne gleda neprijatelja, neprijatelj se može kretati
            agent.isStopped = false;
            if (obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (!obstacleBetween && distanceToPlayer <= chaseRange)
            {
                MoveTowardsPlayer();
            }
            else if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetNewRandomDestination();
            }
        }
        else
        {
            // Ako igraè gleda neprijatelja ili je neprijatelj u vidnom polju kamere, neprijatelj stoji
            StopAgentImmediately();
            isMoving = false;
        }
    }

    // Postavljanje nove random destinacije [ZASAD ISKLJUCENO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!]
    void SetNewRandomDestination()
    {
        /*
        int randomNumber = Random.Range(0, idle_spots.Count);
        currentDestination = idle_spots[randomNumber].position;
        agent.SetDestination(currentDestination);
        */
    }

    // Provjera da li igraè gleda neprijatelja
    bool IsPlayerLookingAtEnemy()
    {
        bool obstacleBetween = Physics.Linecast(transform.position, player.position, obstacleLayer);
        if (!obstacleBetween)
        {
            Vector3 directionToEnemy = transform.position - player.position;
            float angle = Vector3.Angle(player.forward, directionToEnemy);
            return angle < playerViewAngle / 2f;
        }
        return false;
    }

    // Provjera da li je neprijatelj unutar vidnog polja kamere
    bool IsEnemyInView()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }

    // Funkcija za trenutno zaustavljanje agenta
    void StopAgentImmediately()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    // Pomicanje prema igraèu s offsetom
    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 destination = player.position - directionToPlayer * 1.5f; // 1.5f je offset udaljenost, možete je prilagoditi
        agent.destination = destination;
    }
}
