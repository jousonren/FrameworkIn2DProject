using UnityEngine;
using System.Collections;

/// <summary>
/// 怪物周期性加血Buffer，CurrArgs.m_bValue1回复值为总血量的百分比
/// </summary>
public class PerBufferAddBlood : BufferStateBase
{
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;
	/// <summary>
	/// 上次加血的时间
	/// </summary>
	private float lastTime;

	public override bool OnEnter ()
	{
		if (CurrArgs.m_bUsePercent) {
			CurrCtrl.GetComponentInChildren<HealthState> ().AddHp ((int)(CurrCtrl.GetComponentInChildren<HealthState> ().FullHp * CurrArgs.m_fValue1));
		} else {
			CurrCtrl.GetComponentInChildren<HealthState> ().AddHp ((int)(CurrArgs.m_fValue1));
		}
		lastTime = CurrArgs.m_ContinuousTime;
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		return true;
	}

	public override bool OnUpdate ()
	{
		CurrArgs.m_ContinuousTime -= Time.deltaTime;
		if (CurrArgs.m_ContinuousTime <= CurrArgs.m_EndTime) {
			if (!CurrArgs.m_Permanent) {
				CurrArgs.isOver = true;
			}
		}
		if (!CurrArgs.isOver) {
			if ((lastTime - CurrArgs.m_ContinuousTime) >= CurrArgs.m_fValue2) {
				lastTime = CurrArgs.m_ContinuousTime;
				if (CurrArgs.m_bUsePercent) {
					CurrCtrl.GetComponentInChildren<HealthState> ().AddHp ((int)(CurrCtrl.GetComponentInChildren<HealthState> ().FullHp * CurrArgs.m_fValue1));
				} else {
					CurrCtrl.GetComponentInChildren<HealthState> ().AddHp ((int)(CurrArgs.m_fValue1));
				}
			}
		}
		return true;
	}

	public override bool OnExit ()
	{
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "怪物回血Buffer，每隔" + CurrArgs.m_fValue1 + "秒血量回复" + CurrArgs.m_iValue1;
		}
	}
}
