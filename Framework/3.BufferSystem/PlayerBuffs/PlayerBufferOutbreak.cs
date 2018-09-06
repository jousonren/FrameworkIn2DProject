using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBufferOutbreak : PlayerBufferBase
{
    public override void OnEnter()
    {
        GameControl.gameControl.Player.healthState.AddSpeedEvent(0, Bf0);
        GameControl.gameControl.Player.healthState.AddAttackRateNumEvent(0, 0, Bf1, 0);
       
    }

    public override void OnExit()
    {
        GameControl.gameControl.Player.healthState.AddSpeedEvent(0, -Bf0);
        GameControl.gameControl.Player.healthState.AddAttackRateNumEvent(0, 0, -Bf1, 0);
        if (BuffUI != 0)
            GameControl.gameControl.Player.playerBufferController.SetTimerNow(8);
        Exit = true;
    }

    public override void RateRun()
    {
        return;
    }
}
