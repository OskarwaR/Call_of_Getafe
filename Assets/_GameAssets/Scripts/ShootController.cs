using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public GameObject Target;

    void Start()
    {
        // Initialise ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown");
            // Reset ray with new mouse position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //if (hit.collider.gameObject.tag == "Target")
                //{
                    Target = hit.collider.gameObject;
                    Debug.Log("Hit: " + Target.tag + " (" + Target.name+ ")");
                //}

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
}
