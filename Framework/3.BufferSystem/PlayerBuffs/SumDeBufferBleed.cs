using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumDeBufferBleed : PlayerBufferBase
{
    private int Sub;
    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
        if (BuffUI != 0)
            GameControl.gameControl.Player.playerBufferController.SetTimerNow(2);
        Exit = true;
    }
    public override void RateRun()
    {
        if (Bi0 != 0)
        {
            hs.GetComponent<CallAnimationEvent>().BeHitted(Bi0,0,SumHartType.None,0,Vector3.zero);
        }
        else
        {
            Sub = (int)(hs.Hp * Bf0);
            if (Sub <= hs.Hp - 1)
            {
                hs.GetComponent<CallAnimationEvent>().BeHitted(Sub,0, SumHartType.None, 0, Vector3.zero);
            }
        }
    }

    // Use this for initialization
}
