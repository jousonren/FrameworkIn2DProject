using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeBufferBleed :PlayerBufferBase
{
    private int Sub;
    public override void OnEnter()
    {
        hs = GameControl.gameControl.Player.healthState;
    }

    public override void OnExit()
    {
        if (BuffUI != 0)
        {
            switch (BuffUI)
            {
                case 0:
                    break;
                case 1://流血
                    GameControl.gameControl.Player.playerBufferController.SetTimerNow(2);
                    break;
                case 2://灼烧
                    GameControl.gameControl.Player.playerBufferController.SetTimerNow(7);
                    break;
                case 3://滴血
                    GameControl.gameControl.Player.playerBufferController.SetTimerNow(9);
                    break;
            }
          
        }
        Exit = true;
    }
    public override void RateRun()
    {
        if (Bi0 != 0)
        {
            GameControl.gameControl.Player.playerBody.BussBack = false;
            GameControl.gameControl.Player.playerBody.HartToPlayer(Bi0, 0, 0, HartForPlayerType.OnlyDropBlood, Vector3.zero, Color.red);
        }
        else
        {
            Sub = (int)(hs.Hp * Bf0);
            if (Sub <= hs.Hp - 1)
            {
                GameControl.gameControl.Player.playerBody.BussBack = false;
                GameControl.gameControl.Player.playerBody.HartToPlayer(Sub, 0, 0, HartForPlayerType.OnlyDropBlood, Vector3.zero, Color.red);
            }
        }
    }

    // Use this for initialization
}
