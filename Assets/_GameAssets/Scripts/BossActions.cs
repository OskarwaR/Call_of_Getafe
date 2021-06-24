using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    public Boss boss;
    public SoundManager soundManager;
    [SerializeField] GameObject humoPisada;
    [SerializeField] GameObject humoSalto;
    [SerializeField] GameObject humoDash;
    [SerializeField] GameObject humoPisadaPointL;
    [SerializeField] GameObject humoPisadaPointR;
    [SerializeField] GameObject humoSaltoPoint;
    [SerializeField] GameObject humoDashPoint;
    [SerializeField] GameObject triggerJump;
    [SerializeField] GameObject triggerDash;
    [SerializeField] GameObject triggerGarra;
    private GameObject dash;
    public void Actions()
    {
        int action = Random.Range(0, 3);
        //int action = 1;
        switch(action)
        {
            case 0:
                boss.jump = true;
                break;

            case 1:
                if (boss.distanceToPlayer >= 45) boss.embestida = true;
                else
                {
                    if (Random.Range(0, 100) > 50) boss.walk = true;
                    else boss.jump = true;
                }
                
                break;

            case 2:
                if (boss.distanceToPlayer <= 60) boss.walk = true;
                else
                {
                    if (Random.Range(0,100)>50) boss.embestida = true;
                    else boss.jump = true;
                }
                break;
        }
    }

    public void Position()
    {
        boss.Position();
    }

    public void RotateOn()
    {
        boss.rotate = true;
    }

    public void RotateOff()
    {
        boss.rotate = false;
    }

    //SONIDOS
    public void Rugido()
    {
        soundManager.PlaySound(0,true,1,1.2f);
    }

    public void Pisada(string f="")
    {
        soundManager.PlaySound(Random.Range(1,5));
        if (f == "L") Instantiate(humoPisada, humoPisadaPointL.transform.position, humoPisadaPointL.transform.rotation);
        else Instantiate(humoPisada, humoPisadaPointR.transform.position, humoPisadaPointR.transform.rotation);
    }

    public void Dash()
    {
        soundManager.PlaySound(5);
        dash=Instantiate(humoDash, humoSaltoPoint.transform.position, humoSaltoPoint.transform.rotation);
        dash.transform.SetParent(humoSaltoPoint.transform);
    }
    public void DashEnd()
    {
        dash.GetComponent<ParticleSystem>().Stop();
    }

    public void JumpStart()
    {
        soundManager.PlaySound(6);
        Instantiate(humoSalto, humoSaltoPoint.transform.position, humoSaltoPoint.transform.rotation);
    }
    public void JumpEnd()
    {
        soundManager.PlaySound(7);
        Instantiate(humoSalto, humoSaltoPoint.transform.position, humoSaltoPoint.transform.rotation);
    }

    public void Garra()
    {
        soundManager.PlaySound(8);
    }

    public void JumpTrigger(int estado)
    {
        if(estado==1) triggerJump.SetActive(true);
        else triggerJump.SetActive(false);
    }

    public void DashTrigger(int estado)
    {
        if (estado == 1) triggerDash.SetActive(true);
        else triggerDash.SetActive(false);
    }

    public void GarraTrigger(int estado)
    {
        if (estado == 1) triggerGarra.SetActive(true);
        else triggerGarra.SetActive(false);
    }
}
