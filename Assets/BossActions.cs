using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    public Boss boss;
    public SoundManager soundManager;
    public void Actions()
    {
        int action = Random.Range(0, 3);
        //int action = 2;
        switch(action)
        {
            case 0:
                boss.jump = true;
                break;
            case 1:
                if (boss.distanceToPlayer >= 45) boss.embestida = true;
                else Actions();
                break;
            case 2:
                if (boss.distanceToPlayer <= 50) boss.walk = true;
                else boss.embestida = true;
                break;
            case 3:
                boss.spawn = true;
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

    public void Pisada()
    {
        soundManager.PlaySound(Random.Range(1,5));
    }

    public void Dash()
    {
        soundManager.PlaySound(5);
    }

    public void JumpStart()
    {
        soundManager.PlaySound(6);
    }
    public void JumpEnd()
    {
        soundManager.PlaySound(7);
    }

    public void Garra()
    {
        soundManager.PlaySound(8);
    }

}
