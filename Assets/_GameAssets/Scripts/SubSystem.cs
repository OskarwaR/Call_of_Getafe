using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubSystem : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject panelAlerta;
    [SerializeField] Text texto;
    [SerializeField] Text textoAlerta;

    public void ShowSub(string sub)
    {
        panel.SetActive(true);
        texto.text=sub;
        StartCoroutine(FadeOff(panel));
    }

    public void ShowSubAlerta(string sub)
    {
        panelAlerta.SetActive(true);
        textoAlerta.text = sub;
        StartCoroutine(FadeOff(panelAlerta));
    }

    IEnumerator FadeOff(GameObject p)
    {
        yield return new WaitForSeconds(5);
        p.SetActive(false);
        texto.text = "";
    }
}
