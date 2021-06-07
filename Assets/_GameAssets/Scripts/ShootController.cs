using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private GameObject Target;

    private int arma;

    [SerializeField] int damagePistola = 35;
    [SerializeField] int damageEscopeta = 50;
    [SerializeField] int damageCuchillo = 15;
    [SerializeField] int damageM16 = 25;
    private int damage;

    [SerializeField] float distanciaPistola = 50;
    [SerializeField] float distanciaEscopeta = 25;
    [SerializeField] float distanciaCuchillo = 1;
    [SerializeField] float distanciaM16 = 75;
    private float distancia;

    [SerializeField] GameObject sangreImpacto;
    [SerializeField] float sangreImpactoSize;

    [SerializeField] GameObject player;
    private GameObject brazos;

    private GameObject explosionPoint;
    [SerializeField] GameObject explosionPointEscopeta;
    [SerializeField] GameObject explosionPointM16;
    [SerializeField] GameObject explosionPointPistola;
    private GameObject explosionPointPF;
    [SerializeField] GameObject explosionPointPFEscopeta;
    [SerializeField] GameObject explosionPointPFM16;
    [SerializeField] GameObject explosionPointPFPistola;

    [SerializeField] LayerMask layerMask;
    [SerializeField] SoundManager soundManager;

    private bool disparando = false;
    private bool gatillo = false;
    private Inventario inventario;

    void Start()
    {
        //Inicializamos el rayo
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        inventario = GetComponentInParent<Inventario>();
        
        //Comprobamos el arma actual
        arma = inventario.getArmaActual();
        
    }

    void Update()
    {
        Disparo();
        Recarga();
    }

    private void Recarga()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            inventario.Recargar();
        }
    }

    private void Disparo()
    {
        if (inventario.getArmaActual() <= 0) return; //No se puede disparar sin arma

        if (Input.GetMouseButtonDown(0) || gatillo)
        {
            if (!disparando && inventario.getMunicion() > 0)
            {
                arma = inventario.getArmaActual();
                Debug.Log("MouseDown: " + arma);
                switch (arma)
                {
                    case 1:
                        distancia = distanciaCuchillo;
                        break;
                    case 2:
                        distancia = distanciaPistola;
                        break;
                    case 3:
                        distancia = distanciaEscopeta;
                        break;
                    case 4:
                        distancia = distanciaM16;
                        break;
                }

                // Reset ray with new mouse position
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, distancia, layerMask))
                {
                    Target = hit.collider.gameObject;
                    Debug.Log("Hit: " + Target.tag + " (" + Target.name + ") - " + hit.distance + " metros");
                    if (hit.collider.gameObject.tag == "Enemigo" || hit.collider.gameObject.tag == "Head")
                    {
                        Health enemigo = Target.GetComponentInParent<Health>();
                        if (hit.collider.gameObject.tag == "Head")
                        {
                            enemigo.setSalud(-damage * 2, Target.name);
                            print("daño: "+damage * 2);
                        }
                        else
                        {
                            enemigo.setSalud(-damage, Target.name);
                            print("daño: "+damage);
                        }

                        Target.GetComponentInParent<PatrolManager>().Hit();

                        GameObject sangre = Instantiate(sangreImpacto, hit.point, Quaternion.LookRotation(hit.normal));
                        sangre.transform.localScale = new Vector3(sangreImpactoSize, sangreImpactoSize, sangreImpactoSize);
                        //sangre.transform.SetParent(Target.transform);
                    }

                }


                //Comprobamos el arma equipada
                brazos = GameObject.FindGameObjectWithTag("Brazos");
                brazos.GetComponent<Animator>().SetTrigger("Disparo");

                switch (arma)
                {
                    //cuchillo
                    case 1:
                        soundManager.PlaySound(3);
                        player.transform.Rotate(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                        explosionPoint = null;
                        explosionPointPF = null;
                        damage = damageCuchillo;
                        break;
                    //pistola
                    case 2:
                        soundManager.PlaySound(2, true, 0.7f, 1.1f);
                        player.transform.Rotate(Random.Range(-6f, 6f), Random.Range(-6f, 6f), 0);
                        explosionPoint = explosionPointPistola;
                        explosionPointPF = explosionPointPFPistola;
                        damage = damageM16;
                        break;
                    //escopeta
                    case 3:
                        soundManager.PlaySound(0);
                        player.transform.Rotate(Random.Range(-11f, -9f), Random.Range(-2f, 2f), 0);
                        explosionPoint = explosionPointEscopeta;
                        explosionPointPF = explosionPointPFEscopeta;
                        damage = damageEscopeta;
                        break;
                    //m16
                    case 4:
                        soundManager.PlaySound(1);
                        player.transform.Rotate(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
                        explosionPoint = explosionPointM16;
                        explosionPointPF = explosionPointPFM16;
                        damage = damageM16;
                        break;
                }

                if(arma>1) inventario.setMunicion();

                if (explosionPoint)
                {
                    GameObject tempExplosion = Instantiate(explosionPointPF, explosionPoint.transform.position, explosionPoint.transform.rotation);
                    tempExplosion.transform.parent = explosionPoint.transform;
                }

                if (arma != 0) disparando = true;

                if (arma == 4)
                {
                    gatillo = true;
                }
                else
                {
                    gatillo = false;
                }


                //multiples hits
                /*
                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.tag == "Target")
                    {
                        Target = hit.collider.gameObject;
                        Debug.Log("Hit");
                    }
                }*/
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            gatillo = false;
        }
    }

    public void setDisparando(bool estado)
    {
        disparando = estado;
    }

    public bool getDisparando()
    {
        return disparando;
    }
}
