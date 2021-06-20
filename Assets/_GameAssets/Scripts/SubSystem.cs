using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubSystem : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Text texto;
    
    public void ShowSub(string sub)
    {
        panel.SetActive(true);
        texto.text=sub;
        StartCoroutine(FadeOff());
    }

    IEnumerator FadeOff()
    {
        yield return new WaitForSeconds(5);
        panel.SetActive(false);
        texto.text = "";
    }
}
