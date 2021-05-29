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
    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        agentAnimator = GetComponentInChildren<Animator>();

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

        if(!destino)
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
        Destino();
    }

    private void Destino()
    {
        if (destino)
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

    private void Andar()
    {
        //print(this.name+": tiene nuevo destino");
        nma.SetDestination(RandomNavmeshLocation(50f));
        agentAnimator.SetBool("Walk", true);
        destino = true;
        
    }


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
}
