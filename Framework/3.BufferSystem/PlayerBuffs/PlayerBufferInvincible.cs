using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerBufferInvincible : PlayerBufferBase
{
    public SkeletonAnimation SA;
    public override void OnEnter()
    {
        switch (BuffUI)
        {
            case 2:
                SA = GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[12].GetComponent<SkeletonAnimation>();
                break;
        }
    }
    public override void OnExit()
    {
        switch (BuffUI)
        {
            case 0:
                GameControl.gameControl.Player.playerBufferController.SetTimerNow(10);
                break;
            case 1:
                GameControl.gameControl.Player.playerBufferController.SetTimerNow(11);
                break;
            case 2:
                GameControl.gameControl.Player.playerBufferController.SetTimerNow(12);
                break;
        }     
        Exit = true;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!Exit)
        {
            if (BuffUI.Equals(2))
            {
                if (ContinueTime <= 2 && SA.AnimationName.Equals("animation"))
                {
                    SA.AnimationName = "animation2";
                    SA.Initialize(true);
                }
            }
        }

    }
    public override void RateRun()
    {
        return;
    }
}
