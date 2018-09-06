using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物的减速非周期性Buffer
/// </summary>
public class DeBufferSlowDown : BufferStateBase
{
	/// <summary>
	/// 减速Buff
	/// </summary>
	private GameObject speedBuff;
	/// <summary>
	/// 减速Buff
	/// </summary>
	private GameObject temp;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<HealthState> ().AddSpeedEvent(0,-CurrArgs.m_fValue1);
		if (CurrArgs.m_iBufferUI == 1) {
			speedBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterSlowDownBuff")as GameObject;
			temp = GameObject.Instantiate (speedBuff, CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position, Quaternion.identity)as GameObject;	
			temp.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform);
			temp.transform.localScale = new Vector3 (m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
			temp.transform.position = new Vector3 (temp.transform.position.x + m_BufferCrl.xDis, temp.transform.position.y + m_BufferCrl.yDis, temp.transform.position.z);
		} else if (CurrArgs.m_iBufferUI == 2) {
			speedBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterCrystalBuff")as GameObject;
			temp = GameObject.Instantiate (speedBuff, CurrCtrl.GetComponent<MonsterMessage> ().monsterBody.transform.position, Quaternion.identity)as GameObject;	
			temp.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterBody.transform);
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
		CurrCtrl.GetComponentInChildren<HealthState> ().AddSpeedEvent(0,CurrArgs.m_fValue1);
		if (CurrArgs.m_iBufferUI != 0) {
			GameObject.Destroy (temp);
		}
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "减速Buffer，减少移速的" + CurrArgs.m_fValue1;
		}
	}
}
