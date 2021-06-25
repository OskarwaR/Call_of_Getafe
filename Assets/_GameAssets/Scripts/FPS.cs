using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{

    public int target;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
    }
}
