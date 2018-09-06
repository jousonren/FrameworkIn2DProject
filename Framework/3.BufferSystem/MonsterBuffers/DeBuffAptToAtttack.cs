using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物易伤buff（收到的伤害加倍）
/// </summary>
public class DeBuffAptToAtttack : BufferStateBase {

    /// <summary>
    /// 破甲Buff
    /// </summary>
    //private GameObject sunderArmorBuff;
    /// <summary>
    /// 该Buffer的控制器
    /// </summary>
    private BufferController m_BufferCrl;
    private BeiJi beiji;
    public override bool OnEnter() {
        m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController>();
        beiji = CurrCtrl.GetComponentInChildren<BeiJi>();
        beiji.UnderDouble+= CurrArgs.m_fValue1;
        return true;
    }

    public override bool OnUpdate() {
        if ((CurrArgs.m_ContinuousTime -= Time.deltaTime) <= CurrArgs.m_EndTime) {
            if (CurrArgs.m_ContinuousTime <= 0) {
                if (!CurrArgs.m_Permanent) {
                    CurrArgs.isOver = true;
                }
            }
        }
        return true;
    }

    public override bool OnExit() {
        if (beiji) {
            beiji.UnderDouble -= CurrArgs.m_fValue1;
        }
        return true;
    }

    public override string CurrBufferInfo {
        get {
            return "破甲Buffer，护甲减少" + CurrArgs.m_fValue1;
        }
    }
}
