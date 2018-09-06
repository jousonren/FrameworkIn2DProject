using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物的中毒Buffer，中毒即触发第一次减血,按减血数值减血
/// </summary>
public class PerDeBufferPoison : BufferStateBase
{
	/// <summary>
	/// 中毒Buff
	/// </summary>
	private GameObject poisonBuff;
	/// <summary>
	/// 中毒Buff
	/// </summary>
	private GameObject temp;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;
	/// <summary>
	/// 上次减血的时间
	/// </summary>
	private float lastTime;

	public override bool OnEnter ()
	{
		CurrCtrl.GetComponentInChildren<BeiJi> ().OtherSubHp(CurrArgs.m_iValue1, new Color (0.41f, 0f, 0.53f, 1f));
		lastTime = CurrArgs.m_ContinuousTime;
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		if (CurrArgs.m_iBufferUI == 1) {
			poisonBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterPoisonBuff")as GameObject;
			temp = GameObject.Instantiate (poisonBuff, CurrCtrl.GetComponent<MonsterMessage> ().monsterBody.transform.position, Quaternion.identity)as GameObject;
			if (CurrCtrl.transform.rotation.y != 0f) {
				temp.transform.rotation = new Quaternion (0f, 180f, 0f, 0f);
			}
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
		if (!CurrArgs.isOver) {
			if ((lastTime - CurrArgs.m_ContinuousTime) >= CurrArgs.m_fValue1) {
				lastTime = CurrArgs.m_ContinuousTime;
				CurrCtrl.GetComponentInChildren<BeiJi> ().OtherSubHp(CurrArgs.m_iValue1, new Color (0.41f, 0f, 0.53f, 1f));
			}
		}
		return true;
	}

	public override bool OnExit ()
	{
		if (CurrArgs.m_iBufferUI == 1) {
			GameObject.Destroy (temp);
		}
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "怪物中毒Buffer，每隔" + CurrArgs.m_fValue1 + "秒血量减少" + CurrArgs.m_iValue1;
		}
	}
}
