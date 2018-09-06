using UnityEngine;
using System.Collections;

/// <summary>
/// 怪物破甲Buffer，在一段时间内护甲减少一定数值
/// </summary>
public class DeBufferSunderArmor : BufferStateBase
{
	/// <summary>
	/// 破甲Buff
	/// </summary>
	private GameObject sunderArmorBuff;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<HealthState> ().AddDefenseEvent((int)-CurrArgs.m_fValue1,0,0);
		if (CurrArgs.m_iBufferUI == 1) {
			sunderArmorBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterSunderArmorBuff")as GameObject;
			GameObject temp1 = Object.Instantiate (sunderArmorBuff, CurrCtrl.GetComponent<MonsterMessage> ().monsterBody.transform.position, Quaternion.identity)as GameObject;	
			temp1.transform.localScale = new Vector3 (m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
			temp1.transform.position = new Vector3 (temp1.transform.position.x + m_BufferCrl.xDis, temp1.transform.position.y + m_BufferCrl.yDis, temp1.transform.position.z);
			temp1.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterBody.transform);
		}
		return true;
	}

	public override bool OnUpdate ()
	{
		if ((CurrArgs.m_ContinuousTime -= Time.deltaTime) <= CurrArgs.m_EndTime) {
			if (CurrArgs.m_ContinuousTime <= 0) {
				if (!CurrArgs.m_Permanent) {
					CurrArgs.isOver = true;
				}
			}
		}
		return true;
	}
    public override bool OnRenovate(BufferArgs args) {
        CurrCtrl.GetComponentInChildren<HealthState>().AddDefenseEvent((int)CurrArgs.m_fValue1, 0, 0);
        CurrArgs.m_ContinuousTime = args.m_ContinuousTime;
        CurrArgs.m_fValue1 = args.m_fValue1;
        CurrCtrl.GetComponentInChildren<HealthState>().AddDefenseEvent((int)-CurrArgs.m_fValue1, 0, 0);
        if (CurrArgs.m_iBufferUI == 1) {
            GameObject temp1 = GameObject.Instantiate(sunderArmorBuff, CurrCtrl.GetComponent<MonsterMessage>().monsterBody.transform.position, Quaternion.identity) as GameObject;
            temp1.transform.localScale = new Vector3(m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
            temp1.transform.position = new Vector3(temp1.transform.position.x + m_BufferCrl.xDis, temp1.transform.position.y + m_BufferCrl.yDis, temp1.transform.position.z);
            temp1.transform.SetParent(CurrCtrl.GetComponent<MonsterMessage>().monsterBody.transform);
        }
        return true;
    }
    public override bool OnExit ()
	{
		CurrCtrl.GetComponentInChildren<HealthState> ().AddDefenseEvent((int)CurrArgs.m_fValue1,0,0);
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "破甲Buffer，护甲减少" + CurrArgs.m_fValue1;
		}
	}
}
