using System.Collections;
using UnityEngine;


/// <summary>
/// Buffer的各种参数
/// </summary>
public class BufferArgs {
    /// <summary>
    /// 是否允许重复添加一个相同的Buff(为false时如果有一个一样类型的buff则更新旧buff的作用时间为新buff的时间)
    /// </summary>
    public bool m_Addition = false;
    /// <summary>
    /// 周期性Buffer的的每次间隔
    /// </summary>
    public float m_Timer;
    /// <summary>
    /// 按次数计算的Buffer次数
    /// </summary>
    public int m_Times;
    /// <summary>
    /// 持续时间
    /// </summary>
    public float m_ContinuousTime;
    /// <summary>
    /// 技能结束值(当CD被刷新时减少此值)
    /// </summary>
    public float m_EndTime = 0f;
    /// <summary>
    /// 是否永久存在
    /// </summary>
    public bool m_Permanent = false;
    /// <summary>
    /// 是否结束
    /// </summary>
    public bool isOver = false;

    #region 各种类型的参数

    /// <summary>
    /// buffer的UI显示，为0时表示无UI
    /// </summary>
    public int m_iBufferUI;
    public int m_iValue1;
    public int m_iValue2;
    public int m_iValue3;
    public int m_iValue4;

    public float m_fValue1;
    public float m_fValue2;
    public float m_fValue3;
    public float m_fValue4;

    public string m_sValue1;
    public string m_sValue2;
    public string m_sValue3;
    public string m_sValue4;

    public PlayerBufferViewBase playerbufferViewBase;
    public ParticleSystem particleSystem;

    /// <summary>
    /// m_bUsePercent为真时表示数值为百分比，为假时为实际数值
    /// </summary>
    public bool m_bUsePercent;
    public bool m_bValue1;
    public bool m_bValue2;
    public bool m_bValue3;
    public bool m_bValue4;

    #endregion

}