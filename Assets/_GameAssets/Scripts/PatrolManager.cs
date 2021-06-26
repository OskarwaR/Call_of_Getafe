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
    [SerializeField] GameObject huesoCabesa;
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

    //Ragdoll
    Collider[] colliders;
    Rigidbody[] rigidbodys;
    Rigidbody rg;
    Vector3 direction;
    RaycastHit rdHit;


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

        //ragdoll
        colliders = GetComponentsInChildren<Collider>();
        rigidbodys = GetComponentsInChildren<Rigidbody>();

        if (!destino)
        {
            //print("primer destino");
            Invoke("Andar", Random.Range(2,10));
        }

    }

    void Update()
    {
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
            //agentAnimator.SetBool("Death", true);
            //nma.ResetPath();
            agentAnimator.enabled = false;
            nma.enabled = false;
            audioLoop.Stop();
            alive = false;

            //print(zombieImpacto);
            if (zombieImpacto=="Head")
            { 
                GameObject gore = Instantiate(pfExplosionCabeza, huesoCabesa.transform.position, huesoCabesa.transform.rotation);
                gore.transform.localScale = new Vector3(3, 3, 3);
                huesoCabesa.transform.localScale = new Vector3(0, 0, 0);
            }
            
            foreach (Rigidbody rig in rigidbodys) rig.isKinematic = false;
            rg.AddForceAtPosition(direction.normalized * 3000, rdHit.point);
            StartCoroutine(DisableZombie());
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
            if(distanceToPlayer <= 6f)
            {
                agentAnimator.SetBool("Attack", true);
                attack = true;                
                nma.ResetPath();
                //nma.enabled = false;
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
            if(nma) nma.ResetPath();
            Invoke("Andar", Random.Range(2, 10));
            Vista();
            Destino(); 
        }
    }

    public void Hit(Rigidbody tRig, Vector3 tDirection, RaycastHit tHit)
    {
        alerta = true;
        audioHitZombie.pitch = Random.Range(0.8f, 1.2f);
        //audioHitZombie.PlayOneShot(audioHitZombie.clip);
        rg = tRig;
        direction = tDirection;
        rdHit= tHit;
        if (zombieSalud <= 0)
            rg.AddForceAtPosition(direction.normalized * 1000, rdHit.point);
        Andar();
    }

    IEnumerator DisableZombie()
    {
        yield return new WaitForSeconds(15);
        foreach (Collider collider in GetComponentsInChildren<Collider>()) collider.isTrigger = true;
        foreach (Rigidbody rig in rigidbodys) rig.isKinematic = true;
        yield return new WaitForSeconds(60);
        foreach (Rigidbody rig in rigidbodys) rig.isKinematic = false;
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
