using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物狂暴Buffer,增加移动速度和攻击力
/// </summary>
public class BufferRage : BufferStateBase
{
	/// <summary>
	/// 狂暴Buff
	/// </summary>
	private GameObject rageBuff;
	/// <summary>
	/// 狂暴Buff
	/// </summary>
	private GameObject temp;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<HealthState> ().AddSpeedEvent(0,CurrArgs.m_fValue1);
		CurrCtrl.GetComponentInChildren<HealthState> ().ADDAttackNumEvent((int)CurrArgs.m_fValue2,0,0,0,0);
		if (CurrArgs.m_iBufferUI == 1) {
			rageBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterRageBuff")as GameObject;
			temp = GameObject.Instantiate (rageBuff, CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position, Quaternion.identity)as GameObject;
			if (CurrCtrl.transform.rotation.y != 0f) {
				temp.transform.rotation = new Quaternion (0f, 180f, 0f, 0f);
			}
			temp.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform);
			temp.transform.localScale = new Vector3 (m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
			temp.transform.position = new Vector3 (temp.transform.position.x + m_BufferCrl.xDis, temp.transform.position.y + m_BufferCrl.yDis, temp.transform.position.z);
		}
		return true;
	}

	public override bool OnUpdate ()
	{
		if ((CurrArgs.m_ContinuousTime -= Time.deltaTime) <= CurrArgs.m_EndTime) {
			if (!CurrArgs.m_Permanent) {
				CurrArgs.isOver = true;
			}
		}
		return true;
	}

	public override bool OnExit ()
	{
		CurrCtrl.GetComponentInChildren<HealthState> ().AddSpeedEvent(0,-CurrArgs.m_fValue1);
		CurrCtrl.GetComponentInChildren<HealthState> ().ADDAttackNumEvent((int)-CurrArgs.m_fValue2,0,0,0,0);
		if (CurrArgs.m_iBufferUI == 1) {
			GameObject.Destroy (temp);
		}
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "增加移动速度和攻击力Buffer，攻击力增加" + CurrArgs.m_fValue2 + "移动速度增加移速的" + CurrArgs.m_fValue1;
		}
	}
}
