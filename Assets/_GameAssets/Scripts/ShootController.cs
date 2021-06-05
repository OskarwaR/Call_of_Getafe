using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private GameObject Target;

    private string arma="escopeta";
    public int dañoPistola=30;
    public int dañoEscopeta=0;
    public int dañoCuchillo=10;
    public int dañoRifle=20;

    public GameObject sangreImpacto;
    public float sangreImpactoSize;

    public GameObject player;
    private GameObject brazos;

    public GameObject explosionPoint;
    public GameObject explosionPointEscopeta;
    public GameObject explosionPointM16;
    public GameObject explosionPointPistola;
    public GameObject explosionPointPF;
    public GameObject explosionPointPFEscopeta;
    public GameObject explosionPointPFM16;
    public GameObject explosionPointPFPistola;

    [SerializeField] private LayerMask layerMask;


    void Start()
    {
        // Initialise ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        brazos = GameObject.FindGameObjectWithTag("Brazos");
        explosionPoint = explosionPointEscopeta;
        explosionPointPF = explosionPointPFEscopeta;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown: "+arma);
            brazos.GetComponent<Animator>().SetTrigger("Disparo");
            GameObject tempExplosion=Instantiate(explosionPointPF, explosionPoint.transform.position, explosionPoint.transform.rotation);
            tempExplosion.transform.parent = explosionPoint.transform;
            

            //brazos.GetComponent<Animator>().SetBool("DisparoB",true);
            // Reset ray with new mouse position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,1000, layerMask))
            {
                Target = hit.collider.gameObject;
                Debug.Log("Hit: " + Target.tag + " (" + Target.name + ") - " + hit.distance + " metros");
                if (hit.collider.gameObject.tag == "Enemigo" || hit.collider.gameObject.tag == "Head")
                {
                    Health enemigo = Target.GetComponentInParent<Health>();
                    if (hit.collider.gameObject.tag == "Head")
                    {
                        enemigo.setSalud(-dañoEscopeta*2, Target.name);
                    }
                    else
                    {
                        enemigo.setSalud(-dañoEscopeta, Target.name);
                    }

                    GameObject sangre = Instantiate(sangreImpacto, hit.point, Quaternion.LookRotation(hit.normal));
                    sangre.transform.localScale = new Vector3(sangreImpactoSize, sangreImpactoSize, sangreImpactoSize);
                    //sangre.transform.SetParent(Target.transform);
                }

            }

            //Retroceso M16
            //player.transform.Rotate(Random.Range(-1f,1f), Random.Range(-1f, 1f), 0);

            //Retroceso Escopeta
            player.transform.Rotate(-10, 0, 0);

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
}
