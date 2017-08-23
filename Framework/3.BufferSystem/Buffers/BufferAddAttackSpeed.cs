using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物增加攻击速度的Buffer
/// </summary>
public class BufferAddAttackSpeed : BufferStateBase
{
	/// <summary>
	/// 攻速Buff
	/// </summary>
	private GameObject attackSpeedBuff;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<HealthState>().AddAttackRateNumEvent(0,0,CurrArgs.m_fValue1,0);
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
		CurrCtrl.GetComponentInChildren<HealthState> ().AddAttackRateNumEvent(0,0,-CurrArgs.m_fValue1,0);
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "增加攻击速度Buffer，攻速增加基础攻速的" + CurrArgs.m_fValue1;
		}
	}
}
