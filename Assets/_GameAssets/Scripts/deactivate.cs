using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivate : MonoBehaviour
{
    public float timeToDeactivate;
    void Start()
    {
        StartCoroutine(time());
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(timeToDeactivate);
        gameObject.SetActive(false);
    }
}
