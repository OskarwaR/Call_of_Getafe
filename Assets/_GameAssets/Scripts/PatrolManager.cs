using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolManager : MonoBehaviour
{
    public Transform[] patrolPoints;
    private List<Transform> patrolList = new List<Transform>();
    private NavMeshAgent nma;
    private GameObject lista;
    public int currentPoint = 0;
    private void Awake()
    {
        lista = GameObject.Find("RutaPatruya");
        nma = GetComponent<NavMeshAgent>();

        foreach (Transform child in lista.transform)
        {
            //print("Foreach loop: " + child + " " + patrolList.Count);
            patrolList.Add(child);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        nma.SetDestination(patrolPoints[currentPoint].transform.position);
        //Ñapa
        GetComponentInChildren<Animator>().SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (nma.remainingDistance <= nma.stoppingDistance)
        {
            currentPoint++;
            if (currentPoint == patrolPoints.Length) currentPoint = 0;
            //nma.SetDestination(patrolPoints[currentPoint].transform.position);
            //nma.SetDestination(patrolPoints[Random.Range(0,patrolPoints.Length)].transform.position);
            nma.SetDestination(patrolList[Random.Range(0, patrolList.Count)].transform.position);
        }
    }
}
