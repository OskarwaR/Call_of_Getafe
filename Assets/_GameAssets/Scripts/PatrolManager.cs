using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolManager : MonoBehaviour
{
    public bool isPlayerDetected = false;

    //public Transform[] patrolPoints;
    private List<Transform> patrolList = new List<Transform>();
    private NavMeshAgent nma;
    public GameObject Lista;
    private int currentPoint = 0;
    private Animator agentAnimator;
    private int n,n2;
    private bool destino = false;

    private float distanceToPlayer;
    private GameObject Player;
    private Vector3 playerPosition;
    public float viewDistance=15;
    private float view;

    public float walkSpeed = 1.7f;
    public float detectSpeed = 2.5f;

    private bool attack=false;

    private AudioSource audioSource;

    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        agentAnimator = GetComponentInChildren<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        view = viewDistance;
        audioSource = GetComponent<AudioSource>();

        /*foreach (Transform child in Lista.transform)
        {
            patrolList.Add(child);
        }

        n = -1;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        //nma.SetDestination(patrolPoints[currentPoint].transform.position);
        float randomStartingTime = Random.Range(0, 240);
        audioSource.time = randomStartingTime;
        audioSource.pitch= Random.Range(1.8f, 2.2f);
        audioSource.Play();

        if (!destino)
        {
            //print("primer destino");
            Invoke("Andar", Random.Range(0,10));
        }

    }

 
    // Update is called once per frame
    void Update()
    {
        /*if (nma.remainingDistance <= nma.stoppingDistance)
        {
            //currentPoint++;
            //if (currentPoint == patrolList.Count) currentPoint = 0;
            //nma.SetDestination(patrolPoints[currentPoint].transform.position);
            //nma.SetDestination(patrolPoints[Random.Range(0,patrolPoints.Length)].transform.position);
            if(n!=-1)
            {
                n2 = n;
            }
            n = Random.Range(0, patrolList.Count);
            if (n==n2) Random.Range(0, patrolList.Count);

            nma.SetDestination(patrolList[n].transform.position);
            print(n);
            
        }*/

        Vista();
        Destino();
    }

    //Deteccion del jugador
    private void Vista()
    {
        playerPosition = Player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceToPlayer <= viewDistance && !attack)
        {
            //print("player detectado");
            //print(distanceToPlayer);
            isPlayerDetected = true;
            nma.SetDestination(playerPosition);
            agentAnimator.SetBool("Detect", true);
            viewDistance = view+15;
            nma.speed = detectSpeed;
            if(distanceToPlayer <= 5f)
            {
                agentAnimator.SetBool("Attack", true);
                attack = true;
                nma.SetDestination(transform.position);
                nma.enabled = false;
                var targetPosition = playerPosition;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);
            }
        }
        else
        {
            isPlayerDetected = false;
            agentAnimator.SetBool("Detect", false);
            viewDistance = view;
        }
    }

    //Control de destinos
    private void Destino()
    {
        if (destino && !isPlayerDetected && !attack)
        {
            if (!nma.pathPending)
            {
                if (nma.remainingDistance <= nma.stoppingDistance)
                {
                    if (!nma.hasPath || nma.velocity.sqrMagnitude == 0f)
                    {
                        destino = false;
                        agentAnimator.SetBool("Walk", false);
                        Invoke("Andar", Random.Range(0, 10));
                    }
                }
            }
        }
    }

    //Asignacion de destino
    private void Andar()
    {
        //print(this.name+": tiene nuevo destino");
        nma.SetDestination(RandomNavmeshLocation(50f));
        agentAnimator.SetBool("Walk", true);
        destino = true;
        nma.speed = walkSpeed;
    }

    //Funcion que genera destinos aleatorios
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void stopAttack()
    {
        attack = false;
        agentAnimator.SetBool("Attack", false);
        nma.enabled = true;
    }
}
