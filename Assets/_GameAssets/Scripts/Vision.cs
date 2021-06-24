using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private GameObject visionDetectorAlarm;
    [SerializeField] private float detectionDistance;
    [SerializeField] private float visionAngle;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform visionOrigin;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = detectionDistance;
        GetComponent<SphereCollider>().isTrigger = true;
        visionAngle = visionAngle / 2;
    }
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isVisible(other.gameObject.transform.position))
            {
                PlayerDetected(true);
            }
            else
            {
                PlayerDetected(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDetected(false);
        }
    }
    private void PlayerDetected(bool detected)
    {
        visionDetectorAlarm.SetActive(detected);
    }

    private bool isVisible(Vector3 posicion)
    {
        Vector3 targetDirecion = (posicion - visionOrigin.position).normalized;
        Debug.DrawRay(visionOrigin.position, targetDirecion, Color.red);
        Debug.DrawRay(visionOrigin.position, visionOrigin.forward, Color.blue);
        if (Vector3.Angle(targetDirecion, visionOrigin.forward)<=visionAngle)
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }
}
