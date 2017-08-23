using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物增加攻击力的Buffer（按攻击力大小加）
/// </summary>
public class BufferAddDamage : BufferStateBase
{

	/// <summary>
	/// 攻击力Buff1
	/// </summary>
	private GameObject attackBuff1;
	/// <summary>
	/// 攻击力Buff2
	/// </summary>
	private GameObject attackBuff2;
	/// <summary>
	/// 攻击力Buff1
	/// </summary>
	private GameObject temp1;
	/// <summary>
	/// 攻击力Buff2
	/// </summary>
	private GameObject temp2;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		if (CurrArgs.m_bUsePercent) {
			CurrCtrl.GetComponentInChildren<HealthState> ().ADDAttackNumEvent(0,0,CurrArgs.m_fValue1,0,0);
		} else {
			CurrCtrl.GetComponentInChildren<HealthState> ().ADDAttackNumEvent((int)CurrArgs.m_fValue1,0,0,0,0);
		}
		if (CurrArgs.m_iBufferUI == 1) {
			attackBuff1 = Resources.Load ("MonsterResources/MonsterBuffs/MonsterAttackBuff1")as GameObject;
			attackBuff2 = Resources.Load ("MonsterResources/MonsterBuffs/MonsterAttackBuff2")as GameObject;

			temp1 = GameObject.Instantiate (attackBuff1, CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position, Quaternion.identity)as GameObject;	
			temp2 = GameObject.Instantiate (attackBuff2, CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position, Quaternion.identity)as GameObject;

			if (CurrCtrl.transform.rotation.y != 0f) {
				temp1.transform.rotation = new Quaternion (0f, 180f, 0f, 0f);
				temp2.transform.rotation = new Quaternion (0f, 180f, 0f, 0f);
			}
			temp1.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform);
			temp2.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform);
			temp1.transform.localScale = new Vector3 (m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
			temp2.transform.localScale = new Vector3 (m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
			temp1.transform.position = new Vector3 (temp1.transform.position.x + m_BufferCrl.xDis, temp1.transform.position.y + m_BufferCrl.yDis, temp1.transform.position.z);
			temp2.transform.position = new Vector3 (temp2.transform.position.x + m_BufferCrl.xDis, temp2.transform.position.y + m_BufferCrl.yDis, temp2.transform.position.z);
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
		if (CurrArgs.m_bUsePercent) {
			CurrCtrl.GetComponentInChildren<HealthState> ().ADDAttackNumEvent(0,0,-CurrArgs.m_fValue1,0,0);
		} else {
			CurrCtrl.GetComponentInChildren<HealthState> ().ADDAttackNumEvent((int)-CurrArgs.m_fValue1,0,0,0,0);
		}
		if (CurrArgs.m_iBufferUI == 1) {
			GameObject.Destroy (temp2);
		}
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "增加攻击力Buffer，攻击力增加" + CurrArgs.m_fValue1;
		}
	}
}
