using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeBufferSpeed : PlayerBufferBase
{
    public override void OnEnter()
    {
        GameControl.gameControl.Player.healthState.AddSpeedEvent(-Bi0, -Bf0);
    }

    public override void OnExit()
    {
        GameControl.gameControl.Player.healthState.AddSpeedEvent(Bi0, Bf0);
        if (BuffUI!=0)
            GameControl.gameControl.Player.playerBufferController.SetTimerNow(6);
        Exit = true;
    }

    public override void RateRun()
    {
        return;
    }
}
