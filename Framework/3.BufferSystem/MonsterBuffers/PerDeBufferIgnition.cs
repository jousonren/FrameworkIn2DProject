using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物的灼烧Buffer，进入时触发第一次，一段时间内每隔一断时间掉一次血
/// </summary>
public class PerDeBufferIgnition : BufferStateBase
{
	/// <summary>
	/// 灼烧Buff
	/// </summary>
	private GameObject bleedBuff;
	/// <summary>
	/// 灼烧Buff
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
		lastTime = CurrArgs.m_ContinuousTime;
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		if (CurrArgs.m_iBufferUI == 1) {
			bleedBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterFiringBuff")as GameObject; 
			temp = GameObject.Instantiate (bleedBuff, CurrCtrl.GetComponent<MonsterMessage> ().monsterBody.transform.position, Quaternion.identity)as GameObject;
			if (CurrCtrl.transform.rotation.y != 0f) {
				temp.transform.rotation = new Quaternion (0f, 180f, 0f, 0f);
			}
			temp.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterBody.transform);
			temp.transform.position = new Vector3 (temp.transform.position.x + m_BufferCrl.xDis, temp.transform.position.y + m_BufferCrl.yDis, temp.transform.position.z);
			temp.transform.localScale = new Vector3 (m_BufferCrl.buffScale, m_BufferCrl.buffScale, 1f);
		}
        CurrCtrl.GetComponentInChildren<BeiJi>().OtherSubHp(CurrArgs.m_iValue1, new Color(1f, 0.5f, 0f, 1f));

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
			if ((lastTime - CurrArgs.m_ContinuousTime) >= CurrArgs.m_fValue1) {
				lastTime = CurrArgs.m_ContinuousTime;
                CurrCtrl.GetComponentInChildren<BeiJi>().OtherSubHp(CurrArgs.m_iValue1, new Color(1f, 0.5f, 0f, 1f));

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
			return "怪物灼烧Buffer，每隔" + CurrArgs.m_fValue1 + "秒血量减少" + CurrArgs.m_iValue1;
		}
	}
}
