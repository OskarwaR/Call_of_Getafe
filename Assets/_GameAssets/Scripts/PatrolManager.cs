using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolManager : MonoBehaviour
{
    public bool isPlayerDetected = false;

    //public Transform[] patrolPoints;
    //private List<Transform> patrolList = new List<Transform>();
    private NavMeshAgent nma;
    [SerializeField] GameObject lista;
    [SerializeField] GameObject pfExplosionCabeza;
    private int currentPoint = 0;
    private Animator agentAnimator;
    private int n,n2;
    private bool destino = false;

    private float distanceToPlayer;
    private GameObject Player;
    private Vector3 playerPosition;
    [SerializeField] float viewDistance=15;
    private float view;

    [SerializeField] float walkSpeed = 1.7f;
    [SerializeField] float detectSpeed = 2.5f;

    private bool attack=false;
    private bool ad=false;

    private AudioSource audioLoop;
    private AudioSource audioDetect;
    private AudioSource audioGolpe;
    private AudioSource audioHitZombie;

    private int afk = 0;

    private int zombieSalud;
    private string zombieImpacto;
    private Health salud;

    private bool alive=true;

    private bool alerta = false;

    [SerializeField] int ataqueGarra = 20;

    private Health playerSalud;
    private GameObject cabezaPath;


    private void OnEnable()
    {
        

        /*foreach (Transform child in Lista.transform)
        {
            patrolList.Add(child);
        }

        n = -1;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponentInParent<NavMeshAgent>();
        agentAnimator = GetComponentInChildren<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        view = viewDistance;
        //audioSource = GetComponent<AudioSource>();
        AudioSource[] audios = GetComponentsInParent<AudioSource>();
        audioLoop = audios[0];
        audioDetect = audios[1];
        audioGolpe = audios[2];
        audioHitZombie = audios[3];

        nma.avoidancePriority = Random.Range(0, 100);

        salud = GetComponentInParent<Health>();
        zombieSalud = salud.getSalud();
        zombieImpacto = salud.getZona();

        playerSalud = Player.GetComponent<Health>();
        //nma.SetDestination(patrolPoints[currentPoint].transform.position);
        float randomStartingTime = Random.Range(0, 240);
        audioLoop.time = randomStartingTime;
        audioLoop.pitch= Random.Range(1.8f, 2.2f);
        audioLoop.Play();

        if (!destino)
        {
            //print("primer destino");
            Invoke("Andar", Random.Range(2,10));
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

        Vida();
        if (zombieSalud <= 0) return;
        Vista();
        Destino();
    }
  
    //Comprobamos la vida del zombie
    private void Vida()
    {
        if (!alive) return;
        zombieSalud = salud.getSalud();
        zombieImpacto = salud.getZona();
        cabezaPath = salud.getPath();
        //print(zombieImpacto);
        if (zombieSalud <= 0)
        {
            agentAnimator.SetBool("Death", true);
            nma.ResetPath();
            nma.enabled = false;
            audioLoop.Stop();
            alive = false;
            GetComponentInParent<CapsuleCollider>().enabled = false;
            //GetComponentInChildren<CapsuleCollider>().enabled = false;
            //print(zombieImpacto);
            if (zombieImpacto=="Head")
            {
                
                string path = "/" + cabezaPath.name;
                while (cabezaPath.transform.parent != null)
                {
                    cabezaPath = cabezaPath.transform.parent.gameObject;
                    path = "/" + cabezaPath.name + path;
                }
                  
                GameObject cabeza = transform.Find(path).gameObject;
                GameObject gore = Instantiate(pfExplosionCabeza, cabeza.transform.position, cabeza.transform.rotation);
                gore.transform.localScale = new Vector3(3, 3, 3);
                cabeza.transform.localScale = new Vector3(0, 0, 0);
                //print(path);
            }
        }
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
            if (!ad)
            {
                audioDetect.pitch = Random.Range(0.8f, 1.2f);
                audioDetect.Play();
                ad = true;
                //print("sonido deteccion");
            }
            Vector3 temp = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            nma.SetDestination(playerPosition+temp);
            agentAnimator.SetBool("Detect", true);
            viewDistance = view+15;
            nma.speed = detectSpeed;
            if(distanceToPlayer <= 5f)
            {
                agentAnimator.SetBool("Attack", true);
                attack = true;
                //nma.SetDestination(transform.position);
                //nma.enabled = false;
                nma.ResetPath();
                var targetPosition = playerPosition;
                targetPosition.y = transform.position.y;
                transform.parent.gameObject.transform.LookAt(targetPosition);

                audioGolpe.pitch = Random.Range(0.8f, 1.2f);
                audioGolpe.Play();
            }
        }
        else
        {
            isPlayerDetected = false;
            agentAnimator.SetBool("Detect", false);
            viewDistance = view;
        }

        if(distanceToPlayer >= viewDistance)
        {
            ad = false;
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
                        nma.ResetPath();
                        agentAnimator.SetBool("Walk", false);

                        Invoke("Andar", Random.Range(2, 10));
                    }
                }
            }
        }
    }

    //Asignacion de destino
    private void Andar()
    {
        //print(this.name+": tiene nuevo destino");
        if (!alive) return;
        if (!attack)
        {
            if (!alerta) nma.SetDestination(RandomNavmeshLocation(50f));
            else nma.SetDestination(playerPosition);
            agentAnimator.SetBool("Walk", true);
            destino = true;
            nma.speed = walkSpeed;
        }
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

    public void attackImpact()
    {
        if(distanceToPlayer<=6)
        { 
            playerSalud.setSalud(-10);
        }
    }
    public void stopAttack()
    {
        attack = false;
        agentAnimator.SetBool("Attack", false);
        //nma.enabled = true;
    }

    public void afkCheck()
    {
        afk++;
        if(afk>=2)
        {
            afk = 0;
            agentAnimator.SetBool("Walk", false);
            agentAnimator.SetBool("Detect", false);
            agentAnimator.SetBool("Attack", false);
            attack = false;
            destino = false;
            isPlayerDetected = false;
            nma.ResetPath();
            Invoke("Andar", Random.Range(2, 10));
            Vista();
            Destino(); 
        }
    }

    public void Hit()
    {
        alerta = true;
        audioHitZombie.pitch = Random.Range(0.8f, 1.2f);
        audioHitZombie.PlayOneShot(audioHitZombie.clip);
        Andar();
    }
}
