using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] camaras;
    public float delayCameraChange=0.2f;
    private bool canChangeCamera = true;
    private int currentCamera;
    private void Awake()
    {
        for(int i = 1; i < camaras.Length; i++)
        {
            camaras[i].SetActive(false);
        }
    }
    private void Update()
    {
        int delta = (int)Input.mouseScrollDelta.y;
        if (delta > 0)
        {
            currentCamera++;
            ChangeCamera();
        } else if (delta < 0)
        {
            currentCamera--;
            ChangeCamera();
        }
    }
    private void ChangeCamera()
    {
        if (!CanChange()) return;

        canChangeCamera = false;
        Invoke("ReactivateChangeCamera", delayCameraChange);
        if (currentCamera >= camaras.Length)
        {
            currentCamera = 0;
        }
        else if (currentCamera < 0)
        {
            currentCamera = camaras.Length - 1;
        }
        ActivateCamera(currentCamera);
    }
    private bool CanChange()
    {
        return canChangeCamera;
    }
    private void ActivateCamera(int currentCamera)
    {
        
        for(int i = 0; i < camaras.Length; i++)
        {
            camaras[i].SetActive(false);
        }
        camaras[currentCamera].SetActive(true);
    }
    private void ReactivateChangeCamera()
    {
        canChangeCamera = true;
    }
}
