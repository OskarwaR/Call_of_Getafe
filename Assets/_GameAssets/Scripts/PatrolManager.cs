using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolManager : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent nma;
    public int currentPoint = 0;
    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nma.SetDestination(patrolPoints[currentPoint].transform.position);
        //Ñapa
        GetComponentInChildren<Animator>().SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (nma.remainingDistance <= nma.stoppingDistance)
        {
            currentPoint++;
            if (currentPoint == patrolPoints.Length) currentPoint = 0;
            nma.SetDestination(patrolPoints[currentPoint].transform.position);
        }
    }
}
