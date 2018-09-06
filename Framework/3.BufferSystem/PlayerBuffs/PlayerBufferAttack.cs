using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBufferAttack : PlayerBufferBase
{
    public override void OnEnter()
    {
        GameControl.gameControl.Player.healthState.ADDAttackNumEvent(Bi0, Bi1, Bf0,Bf1,Bi2);
    }

    public override void OnExit()
    {
        GameControl.gameControl.Player.healthState.ADDAttackNumEvent(-Bi0, -Bi1, -Bf0, -Bf1, -Bi2);
        if (BuffUI != 0)
            GameControl.gameControl.Player.playerBufferController.SetTimerNow(3);
        Exit = true;
    }

    public override void RateRun()
    {
        return;
    }
}
