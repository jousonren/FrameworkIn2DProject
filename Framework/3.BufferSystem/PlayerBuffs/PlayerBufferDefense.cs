using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBufferDefense : PlayerBufferBase
{
    public override void OnEnter()
    {
        GameControl.gameControl.Player.healthState.AddDefenseEvent(Bi0,Bi1,Bf0);
       
    }

    public override void OnExit()
    {
        GameControl.gameControl.Player.healthState.AddDefenseEvent(-Bi0, -Bi1, -Bf0);
        if (BuffUI != 0)
            GameControl.gameControl.Player.playerBufferController.SetTimerNow(1);
        Exit = true;
    }

    public override void RateRun()
    {
        return;
    }
}
