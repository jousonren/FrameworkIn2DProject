using UnityEngine;

/// <summary>
/// 重生Buffer
/// </summary>
public class BufferRelive : BufferStateBase
{

	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<BeiJi> ().canRelive = true;
		CurrCtrl.GetComponentInChildren<BeiJi> ().reliveProbability = CurrArgs.m_fValue1;
		CurrCtrl.GetComponentInChildren<BeiJi> ().reliveHP = CurrArgs.m_fValue2;
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
		CurrCtrl.GetComponentInChildren<BeiJi> ().canRelive = false;
		CurrCtrl.GetComponentInChildren<BeiJi> ().reliveProbability = 0;
		CurrCtrl.GetComponentInChildren<BeiJi> ().reliveHP = 0;
		return true;
	}

	public override string CurrBufferInfo {
		get {
            return "怪物复活Buffer，有" + CurrArgs.m_fValue1 + "的概率复活并恢复" + CurrArgs.m_fValue2 + "的血量";
        }
	}
}
