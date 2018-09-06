using UnityEngine;

public class BufferShield : BufferStateBase {
    /// <summary>
    /// Buff特效
    /// </summary>
    private GameObject shieldBuff;
    /// <summary>
    /// Buff特效
    /// </summary>
    private GameObject temp;
    /// <summary>
    /// 该Buffer的控制器
    /// </summary>
    private BufferController m_BufferCrl;
    private HealthState hs;
    private int lastHuDun = 0;
    public override bool OnEnter() {
        m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController>();
        hs = CurrCtrl.GetComponentInChildren<HealthState>();
        hs.HuDun = (int)CurrArgs.m_fValue1;
        lastHuDun = hs.HuDun;
        if (CurrArgs.m_iBufferUI == 1) {
            shieldBuff = Resources.Load("MonsterResources/MonsterBuffs/MonsterShieldBuff1") as GameObject;
            temp = Object.Instantiate(shieldBuff, CurrCtrl.GetComponent<MonsterMessage>().monsterFoot.transform.position, CurrCtrl.transform.rotation) as GameObject;
            temp.transform.SetParent(CurrCtrl.GetComponent<MonsterMessage>().monsterFoot.transform);
            temp.transform.localScale = new Vector3(m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
        }
        return true;
    }

    public override bool OnUpdate() {
        if (hs.HuDun <= 0f) {
            CurrArgs.isOver = true;
            OnExit();
            return true;
        }
        
        if (CurrArgs.m_iBufferUI == 1) {
            if (lastHuDun > hs.HuDun) {
                temp.GetComponentInChildren<Animator>().SetTrigger("Hitted");
                lastHuDun = hs.HuDun;
            } 
        }
        if ((CurrArgs.m_ContinuousTime -= Time.deltaTime) <= CurrArgs.m_EndTime) {
            if (!CurrArgs.m_Permanent) {
                CurrArgs.isOver = true;
            }
        }
        return true;
    }

    public override bool OnExit() {
        hs.HuDun = 0;
        if (CurrArgs.m_iBufferUI == 1) {
            temp.GetComponentInChildren<Animator>().SetTrigger("Over");
        }
        return true;
    }
    public override bool OnRenovate(BufferArgs newArgs) {
        CurrArgs.m_ContinuousTime = newArgs.m_ContinuousTime;
        CurrArgs.m_fValue1 = newArgs.m_fValue1;
        hs.HuDun = (int)newArgs.m_fValue1;
        lastHuDun = hs.HuDun;
        return true;
    }
    public override string CurrBufferInfo {
        get {
            return "增加护盾Buffer，护盾增加" + CurrArgs.m_fValue1;
        }
    }
}
