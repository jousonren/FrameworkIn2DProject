using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物的无敌Buff
/// </summary>
public class BufferInvincible : BufferStateBase
{

	/// <summary>
	/// 无敌Buff1
	/// </summary>
	private GameObject attackBuff1;
	/// <summary>
	///  无敌Buff2
	/// </summary>
	private GameObject attackBuff2;
	/// <summary>
	///  无敌Buff1
	/// </summary>
	private GameObject temp1;
	/// <summary>
	/// 无敌Buff2
	/// </summary>
	private GameObject temp2;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<BeiJi> ().isInvincible = true;
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
		CurrCtrl.GetComponentInChildren<BeiJi> ().isInvincible = false;
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "怪物无敌时间持续" + CurrArgs.m_fValue1 + "秒";
		}
	}
}
