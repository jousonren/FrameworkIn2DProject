using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物的加速Buffer
/// </summary>
public class BufferSpeedUp : BufferStateBase
{
	/// <summary>
	/// 加速Buff1
	/// </summary>
	private GameObject speedBuff1;
	/// <summary>
	/// 加速Buff2
	/// </summary>
	private GameObject speedBuff2;
	/// <summary>
	/// 加速Buff1
	/// </summary>
	private GameObject temp1;
	/// <summary>
	/// 加速Buff2
	/// </summary>
	private GameObject temp2;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;

	public override bool OnEnter ()
	{
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		CurrCtrl.GetComponentInChildren<HealthState> ().AddSpeedEvent(0,CurrArgs.m_fValue1);
		if (CurrArgs.m_iBufferUI == 1) {
			speedBuff1 = Resources.Load ("MonsterResources/MonsterBuffs/MonsterSpeedUpBuff1")as GameObject;
			speedBuff2 = Resources.Load ("MonsterResources/MonsterBuffs/MonsterSpeedUpBuff2")as GameObject;
			temp1 = GameObject.Instantiate (speedBuff1, CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position, Quaternion.identity)as GameObject;	
			temp2 = GameObject.Instantiate (speedBuff2, CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position, Quaternion.identity)as GameObject;
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
			GameObject.Destroy (temp1, 3f);
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
		if (CurrArgs.m_iBufferUI == 1) {
			GameObject.Destroy (temp2);
		}
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "加速Buffer，提速移速的" + CurrArgs.m_fValue1;
		}
	}
}
