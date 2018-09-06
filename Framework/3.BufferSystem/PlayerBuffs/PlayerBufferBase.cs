using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBufferBase
{
    /// <summary>
    /// int 型参数
    /// </summary>
    public int Bi0,Bi1,Bi2;
    /// <summary>
    /// float 型参数
    /// </summary>
    public float Bf0,Bf1, Bf2;
    /// <summary>
    /// 持续时间 和频率
    /// </summary>
    public float ContinueTime, Rate;
    public int BuffUI;
    private float ReRate;
    private bool Running = false;
    public bool Exit = false;
    public int BufferState;
    public PlayerBufferViewBase pbvb;
    public HealthState hs;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="_bi0"></param>
    /// <param name="_bi1"></param>
    /// <param name="_bi2"></param>
    /// <param name="_bf0"></param>
    /// <param name="_bf1"></param>
    /// <param name="_bf2"></param>
    /// <param name="_Contunuetime"></param>
    /// <param name="_Rate"></param>
    /// <param name="_buffUI"></param>
    public virtual void Instance(int _bi0,int _bi1,int _bi2,float _bf0,float _bf1,float _bf2,float _Contunuetime,float _Rate,int _buffUI,HealthState _hs=null)
    {
        Bi0 = _bi0;
        Bi1 = _bi1;
        Bi2 = _bi2;
        Bf0 = _bf0;
        Bf1 = _bf1;
        Bf2 = _bf2;
        ContinueTime= _Contunuetime;
        Rate = _Rate;
        ReRate = Rate;
        BuffUI = _buffUI;
        OnEnter();
        Running = true;
    }
    /// <summary>
    /// 开始Buffer 
    /// </summary>
    public abstract void OnEnter();
    /// <summary>
    /// 移除Buff
    /// </summary>
    public abstract void OnExit();
    public virtual void OnUpdate()
    {
        if (Running)
        {
            if (ContinueTime != 0)
            {
                ContinueTime -= Time.deltaTime;
                if (ReRate != 0)
                {
                    ReRate -= Time.deltaTime;
                    if (ReRate <= 0)
                    {
                        ReRate = Rate;              
                        RateRun();
                    }
                }
                if (ContinueTime <= 0)
                {
                    Running = false;
                    ContinueTime = 0;
                    if (!Exit)
                        OnExit();
                }
            }
        }
    }
    /// <summary>
    /// 频率执行
    /// </summary>
    public abstract void RateRun();

}
