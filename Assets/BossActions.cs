using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    public Boss boss;
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
                else Actions();
                break;
            case 2:
                boss.walk = true;
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
}
