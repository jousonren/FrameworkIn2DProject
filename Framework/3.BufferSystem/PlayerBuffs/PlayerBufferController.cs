using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class PlayerBufferController : MonoBehaviour
{
    //BuffState：0减防 ；1加防；2流血；3加攻;4减攻;5加速;6减速;7:暴走 21持续加血
    public Transform[] PlayerBodyPoint;
    public List<PlayerBufferBase> playerBufferBase =new List<PlayerBufferBase>();
    public float[] Timer;
    // Use this for initialization
    /// <summary>
    /// 设置Buff显示时间
    /// </summary>
    /// <param name="SelectTime">类型计时器</param>
    /// <param name="Sw">总计时器指针 0：减防；1：加防；2：流血；3：加攻；4：减攻；5：加速；6：减速；7:灼烧;8:暴走 9:滴血 10:无敌 11:闪烁无敌 12:甲无敌</param>
    /// <returns></returns>
    public void SetTimerNow(int Sw)
    {
        Timer[Sw]--;
        if (Timer[Sw] == 0)
        {
            #region 执行取消Buff显示
            switch (Sw)
            {
                case 0://减防
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[2].GetComponent<ParticleSystem>().Stop();
                    break;
                case 1://加防
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[1].GetComponent<ParticleSystem>().Stop();
                    break;
                case 2://流血
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[0].GetComponent<ParticleSystem>().Stop();
                    break;
                case 3://加攻
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[5].GetComponent<ParticleSystem>().Stop();
                    break;
                case 4://减攻
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[6].GetComponent<ParticleSystem>().Stop();
                    break;
                case 5://加速
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].GetComponent<ParticleSystem>().Stop();
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].GetComponent<ParticleSystem>().loop = false;
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(0).GetComponent<ParticleSystem>().loop = false;
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(1).GetComponent<ParticleSystem>().loop = false;
                    break;
                case 6://减速
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[10].GetComponent<ParticleSystem>().Stop();
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[10].GetComponent<ParticleSystem>().loop = false;
                    break;
                case 7://灼烧
                    GameControl.gameControl.Player.playerController.effectManager.EffectUse[0].SetActive(false);
                    break;
                case 8://暴走
                    GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[14].GetComponent<ParticleSystem>().Stop();
                    break;
                case 9://滴血
                    GameControl.gameControl.Player.playerController.effectManager.EffectUse[1].SetActive(false);
                    break;
                case 10://无敌
                    if (Timer[11] == 0&&Timer[12]==0)
                    {
                        GameControl.gameControl.Player.playerBody.GetComponent<BoxCollider2D>().enabled = true;
                        GameControl.gameControl.Player.playerController.isInvincible = false;
                    }
                    break;
                case 11://闪烁无敌
                    if (Timer[10] == 0&&Timer[12]==0)
                    {
                        GameControl.gameControl.Player.playerBody.GetComponent<BoxCollider2D>().enabled = true;
                        GameControl.gameControl.Player.playerController.isInvincible = false;                   
                    }
                    GameControl.gameControl.Player.playerController.blink.StopBlink();
                    break;
                case 12://甲无敌
                    if (Timer[10] == 0&&Timer[11]==0)
                    {
                        GameControl.gameControl.Player.playerBody.GetComponent<BoxCollider2D>().enabled = true;
                        GameControl.gameControl.Player.playerController.isInvincible = false;
                    }
                    SkeletonAnimation SA = GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[12].GetComponent<SkeletonAnimation>();
                    SA.AnimationName = null;
                    SA.Initialize(true);
                    break;
            }
            #endregion          
        }
    }
    public void ReOpenParticleSystem()
    {
        for(int i = 0; i < Timer.Length; i++)
        {
            if (Timer[i] != 0)
            {
                switch (i)
                {
                    case 0:
                        GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[2].GetComponent<ParticleSystem>().Play();
                        break;
                    case 1:
                        GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[1].GetComponent<ParticleSystem>().Play();
                        break;
                    case 2:
                        ParticleSystem PS = GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[0].GetComponent<ParticleSystem>();
                        PS.loop = true;
                        PS.Play();
                        break;
                    case 3:
                        GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[5].GetComponent<ParticleSystem>().Play();
                        break;
                    case 4:
                        GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[6].GetComponent<ParticleSystem>().Play();
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[14].GetComponent<ParticleSystem>().Play();
                        break;
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (playerBufferBase.Count != 0)
        {
            for (int i = 0; i < playerBufferBase.Count; i++)
            {
                playerBufferBase[i].OnUpdate();
                if (playerBufferBase[i].Exit)
                {
                    playerBufferBase.Remove(playerBufferBase[i]);
                }
            }
            SubTime();
        }     
    }
    /// <summary>
    /// 流血
    /// </summary>
    /// <param name="value">掉血数值</param>
    /// <param name="value2">百分比掉血</param>
    /// <param name="continueTime">持续时间</param>
    /// <param name="Rate">掉血间隔</param>
    /// <param name="bufferUI">0：无 1：中毒 </param>
    /// <param name="addMore">是否叠加</param>
    /// <param name="permanent">是否永久</param>
    public PlayerBufferBase DeBufferBleed(int value, float value2, float continueTime, float Rate, int bufferUI)
    {
        PlayerBufferBase pbb = new PlayerDeBufferBleed();
        pbb.BufferState = 2;
        pbb.Instance(value, 0, 0, value2, 0, 0, continueTime, Rate, bufferUI);
        switch (bufferUI)
        {
            case 0:
                break;
            case 1:
                if (Timer[2] == 0)
                {
                    ParticleSystem PS = GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[0].GetComponent<ParticleSystem>();
                    PS.loop = true;
                    PS.Play();
                }
                Timer[2]++;
                break;
            case 2:
                if (Timer[7] == 0)
                {
                    GameControl.gameControl.Player.playerController.effectManager.EffectUse[0].SetActive(true);
                }
                Timer[7]++;
                break;
            case 3:
                if (Timer[9] == 0)
                {
                    GameControl.gameControl.Player.playerController.effectManager.EffectUse[1].SetActive(true);
                }
                Timer[9]++;
                break;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 持续加血
    /// </summary>
    /// <param name="value">加血数值</param>
    /// <param name="continueTime">持续时间</param>
    /// <param name="Rate">时间间隔</param>
    /// <param name="bufferUI">特效显示</param>
    /// <param name="addMore">叠加</param>
    /// <param name="permanent">永久</param>
	public PlayerBufferBase BufferAbsort(int value, float continueTime, float Rate, int bufferUI)
    {
        PlayerBufferBase pbb = new PlayerBufferAbsort();
        pbb.BufferState = 21;
        pbb.Instance(value, 0, 0, 0, 0, 0, continueTime, Rate, bufferUI);
        if (bufferUI != 0)
        {
            GameObject Buf = Instantiate(GameControl.gameControl.playerBufferAbsort[bufferUI - 1], this.transform.position, Quaternion.identity);
            Buf.transform.SetParent(PlayerBodyPoint[1]);
            pbb.pbvb = Buf.GetComponent<PlayerBufferViewBase>();
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 减防一次
    /// </summary>
    /// <param name="value">Self减防数</param>
    /// <param name="value2">Base减防数</param>
    /// <param name="value3">减防百分比</param>
    /// <param name="bufferUI">是否显示Buff</param>
    /// <returns></returns>
    public void BufferSubDefenseOneTimes(int value, int value2, float value3, int bufferUI)
    {
        GameControl.gameControl.Player.healthState.AddDefenseEvent(-value, -value2, -value3);
        if (bufferUI != 0)
        {
            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[4].GetComponent<ParticleSystem>().Play();
        }
    }
    /// <summary>
    /// 加防一次
    /// </summary>
    /// <param name="value">Self加防</param>
    /// <param name="value2">Base加防</param>
    /// <param name="value3">百分比加防</param>
    /// <param name="bufferUI">是否显示</param>
    public void BufferAddDefenseOneTimes(int value, int value2, float value3, int bufferUI)
    {
        GameControl.gameControl.Player.healthState.AddDefenseEvent(value, value2, value3);
        if (bufferUI != 0)
        {
            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[3].GetComponent<ParticleSystem>().Play();
        }
    }
    /// <summary>
    /// 时间减防
    /// </summary>
    /// <param name="value">self减防</param>
    /// <param name="value2">Base减防</param>
    /// <param name="value3">减防百分比</param>
    /// <param name="bufferUI">是否显示</param>
    /// <param name="Time">持续时间</param>
    /// <returns></returns>
    public PlayerBufferBase BufferSubDefense(int value, int value2, float value3, int bufferUI, float Time)
    {
        PlayerBufferBase pbb = new PlayerDeBufferDefense();
        pbb.BufferState = 0;
        pbb.Instance(value, value2, 0, value3, 0, 0, Time, 0, bufferUI);
        if (bufferUI != 0)
        {
            if (Timer[0] == 0)
            {
                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[2].GetComponent<ParticleSystem>().Play();
            }
            Timer[0]++;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 持续加防
    /// </summary>
    /// <param name="value">self加防</param>
    /// <param name="value2">Base加防</param>
    /// <param name="value3">百分比加防</param>
    /// <param name="bufferUI">是否显示</param>
    /// <param name="Time">持续时间</param>
    /// <returns></returns>
    public PlayerBufferBase BufferAddDefense(int value, int value2, float value3, int bufferUI, float Time)
    {
        PlayerBufferBase pbb = new PlayerBufferDefense();
        pbb.Instance(value, value2, 0, value3, 0, 0, Time, 0, bufferUI);
        pbb.BufferState = 1;
        if (bufferUI != 0)
        {
            if (Timer[1] == 0)
            {
                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[1].GetComponent<ParticleSystem>().Play();
            }
            Timer[1]++;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 减攻击一次性
    /// </summary>
    /// <param name="_SelfAttack">Self减攻</param>
    /// <param name="_BaseAttack">Base减攻</param>
    /// <param name="_AddAttackCentage">百分比减攻</param>
    /// <param name="_ZBATCentage">装备减攻</param>
    /// <param name="_OhterAddAttack">其他减攻</param>
    /// <param name="BuffUI">是否显示UI</param>
    public void BufferSubAtackOneTimes(int _SelfAttack, int _BaseAttack, float _AddAttackCentage, float _ZBATCentage, int _OhterAddAttack, int BuffUI)
    {
        GameControl.gameControl.Player.healthState.ADDAttackNumEvent(-_SelfAttack, -_BaseAttack, -_AddAttackCentage, -_ZBATCentage, -_OhterAddAttack);
        if (BuffUI != 0)
            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[8].GetComponent<ParticleSystem>().Play();
    }
    /// <summary>
    /// 一次性加攻
    /// </summary>
    /// <param name="_SelfAttack">Self加攻</param>
    /// <param name="_BaseAttack">Base加攻</param>
    /// <param name="_AddAttackCentage">百分比加攻</param>
    /// <param name="_ZBATCentage">装备加攻</param>
    /// <param name="_OhterAddAttack">其他加攻</param>
    /// <param name="BuffUI">是否显示</param>
    public void BufferAddAtackOneTimes(int _SelfAttack, int _BaseAttack, float _AddAttackCentage, float _ZBATCentage, int _OhterAddAttack, int BuffUI)
    {
        GameControl.gameControl.Player.healthState.ADDAttackNumEvent(_SelfAttack, _BaseAttack, _AddAttackCentage, _ZBATCentage, _OhterAddAttack);
        if (BuffUI != 0)
            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[7].GetComponent<ParticleSystem>().Play();
    }
    /// <summary>
    /// 时间段加攻
    /// </summary>
    /// <param name="_SelfAttack">Self加攻</param>
    /// <param name="_BaseAttack">Base加攻</param>
    /// <param name="_AddAttackCentage">百分比加攻</param>
    /// <param name="_ZBATCentage">装备加攻</param>
    /// <param name="_OhterAddAttack">其他加攻</param>
    /// <param name="BuffUI">是否显示</param>
    /// <param name="Time">持续时间</param>
    /// <returns></returns>
    public PlayerBufferBase BufferAddAtack(int _SelfAttack, int _BaseAttack, float _AddAttackCentage, float _ZBATCentage, int _OhterAddAttack, int BuffUI, float Time)
    {
        PlayerBufferBase pbb = new PlayerBufferAttack();
        pbb.Instance(_SelfAttack, _BaseAttack, _OhterAddAttack, _AddAttackCentage, _ZBATCentage, 0, Time, 0, BuffUI);
        pbb.BufferState = 3;
        if (BuffUI != 0)
        {
            if (Timer[3] == 0)
                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[5].GetComponent<ParticleSystem>().Play();
            Timer[3]++;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 时间段暴走
    /// </summary>
    /// <param name="_AddSpeedCer">加速</param>
    /// <param name="_AddAtkRateCer">加攻速</param>
    /// <param name="BuffUI">显示Buff</param>
    /// <param name="Time">持续时间</param>
    /// <returns></returns>
    public PlayerBufferBase BufferAddOutBreak(float _AddSpeedCer,float _AddAtkRateCer,int BuffUI,float Time)
    {
        PlayerBufferBase pbb = new PlayerBufferOutbreak();
        pbb.Instance(0, 0, 0, _AddSpeedCer, _AddSpeedCer, 0, Time, 0, BuffUI);
        pbb.BufferState = 7;
        if (BuffUI != 0)
        {
            if (Timer[8] == 0)
                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[14].GetComponent<ParticleSystem>().Play();
            Timer[8]++;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 时间段减攻
    /// </summary>
    /// <param name="_SelfAttack">Self减攻</param>
    /// <param name="_BaseAttack">Base减攻</param>
    /// <param name="_AddAttackCentage">百分比减攻</param>
    /// <param name="_ZBATCentage">装备减攻</param>
    /// <param name="_OhterAddAttack">其他减攻</param>
    /// <param name="BuffUI">是否显示</param>
    /// <param name="Time">减攻持续时间</param>
    /// <returns></returns>
    public PlayerBufferBase BufferSubAtack(int _SelfAttack, int _BaseAttack, float _AddAttackCentage, float _ZBATCentage, int _OhterAddAttack, int BuffUI, float Time)
    {
        PlayerBufferBase pbb = new PlayerDeBufferAttack();
        pbb.Instance(_SelfAttack, _BaseAttack, _OhterAddAttack, _AddAttackCentage, _ZBATCentage, 0, Time, 0, BuffUI);
        pbb.BufferState = 4;
        if (BuffUI != 0)
        {
            if (Timer[4] == 0)
                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[6].GetComponent<ParticleSystem>().Play();
            Timer[4]++;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 时间段加速
    /// </summary>
    /// <param name="_MoveSpeed">数值加速</param>
    /// <param name="_AddSpeedCentage">百分比加速</param>
    /// <param name="Time">持续时间</param>
    /// <param name="BuffUI">是否显示</param>
    /// <returns></returns>
    public PlayerBufferBase BufferAddSpeed(int _MoveSpeed, float _AddSpeedCentage, float Time, int BuffUI)
    {
        PlayerBufferBase pbb = new PlayerBufferSpeed();
        pbb.Instance(_MoveSpeed, 0, 0, _AddSpeedCentage, 0, 0, Time, 0, BuffUI);
        pbb.BufferState = 5;
        if (BuffUI != 0)
        {
            Timer[5]++;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    /// <summary>
    /// 持续时间减速
    /// </summary>
    /// <param name="_MoveSpeed">数值减速</param>
    /// <param name="_AddSpeedCentage">百分比减速</param>
    /// <param name="Time">减速时间</param>
    /// <param name="BuffUI">是否显示</param>
    /// <returns></returns>
    public PlayerBufferBase BufferSubSpeed(int _MoveSpeed, float _AddSpeedCentage, float Time, int BuffUI)
    {
        PlayerBufferBase pbb = new PlayerDeBufferSpeed();
        pbb.Instance(_MoveSpeed, 0, 0, _AddSpeedCentage, 0, 0, Time, 0, BuffUI);
        pbb.BufferState = 6;
        if (BuffUI != 0)
        {
            Timer[6]++;
        }
        playerBufferBase.Add(pbb);
        return pbb;
    }
    public PlayerBufferBase BufferInvincible(float _Timer,int BuffUI)
    {
        PlayerBufferBase pbb = new PlayerBufferInvincible();
        pbb.Instance(0,0,0,0,0,0,_Timer,0,BuffUI,null);
        pbb.BufferState = 8;
        if (Timer[10] == 0&&Timer[11]==0)
        {
            GameControl.gameControl.Player.playerBody.GetComponent<BoxCollider2D>().enabled = false;
            GameControl.gameControl.Player.playerController.isInvincible = true;
        }
        switch (BuffUI)
        {
            case 0:
                Timer[10]++;
                break;
            case 1:
                Timer[11]++;
                break;
            case 2:
                if (Timer[12] == 0)
                {
                    SkeletonAnimation SA = GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[12].GetComponent<SkeletonAnimation>();
                    SA.AnimationName = "animation";
                    SA.Initialize(true);
                }
                Timer[12]++;

                break;
        }
   
        playerBufferBase.Add(pbb);
        return pbb;
    }
    void SubTime()
    {
        for (int i = 0; i < Timer.Length; i++)
        {
            if (Timer[i] != 0)
            {
                #region Buff运行中状态
                switch (i)
                {
                    case 5:
                        if (!GameControl.gameControl.Player.player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle"))
                        {
                            if (!GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].GetComponent<ParticleSystem>().loop)
                            {
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].GetComponent<ParticleSystem>().loop = true;
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(0).GetComponent<ParticleSystem>().loop = true;
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(1).GetComponent<ParticleSystem>().loop = true;
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].GetComponent<ParticleSystem>().Play();
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                            }

                        }
                        else
                        {
                            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].GetComponent<ParticleSystem>().loop = false;
                            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(0).GetComponent<ParticleSystem>().loop = false;
                            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[11].transform.GetChild(1).GetComponent<ParticleSystem>().loop = false;
                        }
                        break;
                    case 6:
                        if (GameControl.gameControl.Player.playerController.playerState == PlayerState.move ||
                             GameControl.gameControl.Player.playerController.playerState == PlayerState.attack_run)
                        {
                            if (!GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[10].GetComponent<ParticleSystem>().loop)
                            {
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[10].GetComponent<ParticleSystem>().loop = true;
                                GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[10].GetComponent<ParticleSystem>().Play();
                            }

                        }
                        else
                        {
                            GameControl.gameControl.Player.playerController.effectManager.particleEffectObjects[10].GetComponent<ParticleSystem>().loop = false;
                        }
                        break;
                }

                #endregion
            }
        }
    }
    public string WriteBuffer()
    {
        string Record="";
        for(int i = 0; i < playerBufferBase.Count; i++)
        {
            Record += playerBufferBase[i].BufferState+"~";
            switch (playerBufferBase[i].BufferState)
            {
                case 0:
                    #region 减防
                    Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].Bi1 + "~" + playerBufferBase[i].Bf0 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].BuffUI+";";
                    #endregion
                    break;
                case 1:
                    #region 加防
                    Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].Bi1 + "~" + playerBufferBase[i].Bf0 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                case 2:
                    #region  流血
                    if (playerBufferBase[i].BuffUI == 2)
                        playerBufferBase[i].ContinueTime = 0.1f;
                        Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].Bf0 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].Rate + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                case 3:
                    #region 加攻
                    Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].Bi1 + "~" + playerBufferBase[i].Bi2 + "~" + playerBufferBase[i].Bf0 + "~" + playerBufferBase[i].Bf1 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                case 4:
                    #region 减攻
                    Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].Bi1 + "~" + playerBufferBase[i].Bi2 + "~" + playerBufferBase[i].Bf0 + "~" + playerBufferBase[i].Bf1 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                case 5:
                    #region 加速
                    Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].Bf0 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                case 6:
                    #region 减速
                    Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].Bf0 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                case 7:
                    #region 暴走
                    Record+=playerBufferBase[i].Bf0+"~"+playerBufferBase[i].Bf1+"~"+ playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                case 8:
                    #region 无敌
                    Record += playerBufferBase[i].ContinueTime+"~"+playerBufferBase[i].BuffUI+";";
                    #endregion
                    break;
                case 21:
                    #region 加血
                    Record += playerBufferBase[i].Bi0 + "~" + playerBufferBase[i].ContinueTime + "~" + playerBufferBase[i].Rate + "~" + playerBufferBase[i].BuffUI + ";";
                    #endregion
                    break;
                default:
                    break;
            }
        }
        return Record;
    }
    public void ReadBuffer()
    {
        string BufferAll = ReadTables.readTables.SaveArray[6][20];
        string[] Order = BufferAll.Split(';');
        string[] BufferMessage;
        for(int i = 0; i < Order.Length; i++)
        {
            BufferMessage = Order[i].Split('~');
            switch (BufferMessage[0])
            {
                case "0":
                    #region 减防
                    BufferSubDefense(int.Parse(BufferMessage[1]), int.Parse(BufferMessage[2]), float.Parse(BufferMessage[3]), int.Parse(BufferMessage[5]), float.Parse(BufferMessage[4]));
                    #endregion
                    break;
                case "1":
                    #region 加防
                    BufferAddDefense(int.Parse(BufferMessage[1]), int.Parse(BufferMessage[2]), float.Parse(BufferMessage[3]), int.Parse(BufferMessage[5]), float.Parse(BufferMessage[4]));
                    #endregion
                    break;
                case "2":
                    #region  流血
                    DeBufferBleed(int.Parse(BufferMessage[1]),float.Parse(BufferMessage[2]),float.Parse(BufferMessage[3]),float.Parse(BufferMessage[4]),int.Parse(BufferMessage[5]));
                    #endregion
                    break;
                case "3":
                    #region 加攻
                    BufferAddAtack(int.Parse(BufferMessage[1]), int.Parse(BufferMessage[2]), int.Parse(BufferMessage[3]), float.Parse(BufferMessage[4]), int.Parse(BufferMessage[5]), int.Parse(BufferMessage[7]), float.Parse(BufferMessage[6]));
                    #endregion
                    break;
                case "4":
                    #region 减攻
                    BufferSubAtack(int.Parse(BufferMessage[1]), int.Parse(BufferMessage[2]), int.Parse(BufferMessage[3]), float.Parse(BufferMessage[4]), int.Parse(BufferMessage[5]), int.Parse(BufferMessage[7]), float.Parse(BufferMessage[6]));
                    #endregion
                    break;
                case "5":
                    #region 加速
                    BufferAddSpeed(int.Parse(BufferMessage[1]),float.Parse(BufferMessage[2]),float.Parse(BufferMessage[3]),int.Parse(BufferMessage[4]));
                    #endregion
                    break;
                case "6":
                    #region 减速
                    BufferSubSpeed(int.Parse(BufferMessage[1]), float.Parse(BufferMessage[2]), float.Parse(BufferMessage[3]), int.Parse(BufferMessage[4]));
                    #endregion
                    break;
                case "7":
                    #region 暴走
                    BufferAddOutBreak(float.Parse(BufferMessage[1]), float.Parse(BufferMessage[2]), int.Parse(BufferMessage[4]), float.Parse(BufferMessage[3]));
                    #endregion
                    break;
                case "8":
                    #region 无敌
                    BufferInvincible(float.Parse(BufferMessage[1]),int.Parse(BufferMessage[2]));
                    #endregion
                    break;
                case "21":
                    #region 加血
                    BufferAbsort(int.Parse(BufferMessage[1]), float.Parse(BufferMessage[2]), float.Parse(BufferMessage[3]), int.Parse(BufferMessage[4]));
                    #endregion
                    break;
                default:
                    break;
            }
        }
    }
}
