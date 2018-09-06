using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBufferAbsort : PlayerBufferBase
{
    private HealthState hs;
    public override void OnEnter()
    {
        hs = GameControl.gameControl.Player.healthState;
    }

    public override void OnExit()
    {
        pbvb.Des();
        Exit = true;
    }

    public override void RateRun()
    {
        hs.PlayerAbsort(Bi0);
        if (pbvb != null)
            pbvb.Play();   
    }
}
