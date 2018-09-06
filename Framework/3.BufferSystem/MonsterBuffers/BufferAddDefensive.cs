using UnityEngine;
using System.Collections;

/// <summary>
/// 增加怪物防御力的Buffer
/// </summary>
public class BufferAddDefensive : BufferStateBase
{
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{

		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<HealthState> ().AddDefenseEvent(0,0,CurrArgs.m_fValue1);
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
		CurrCtrl.GetComponentInChildren<HealthState> ().AddDefenseEvent(0, 0, -CurrArgs.m_fValue1);
        return true;
	}

	public override string CurrBufferInfo {
		get {
			return "增加防御力Buffer，防御力增加总防御力的" + CurrArgs.m_fValue1;
		}
	}
}
