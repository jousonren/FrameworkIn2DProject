using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪物的一次性加血Buffer,回复值为总血量的百分比
/// </summary>
public class BufferAddBlood : BufferStateBase
{
	/// <summary>
	/// 治疗Buff
	/// </summary>
	private GameObject healBuff;
    private MonsterMessage mm;
    public override bool OnEnter ()
	{
        mm = CurrCtrl.GetComponent<MonsterMessage>();
        mm.bufferCtl = CurrCtrl.GetComponentInChildren<BufferController> ();
		if (CurrArgs.m_bUsePercent) {
            mm.my_HealState.AddHp ((int)(CurrCtrl.GetComponentInChildren<HealthState> ().FullHp * CurrArgs.m_fValue1));
		} else {
            mm.my_HealState.AddHp ((int)(CurrArgs.m_fValue1));
        }
        if (CurrArgs.m_bUsePercent) {
            CreateProp.createProp.SubBloodView(mm.my_HealState.GrowBlood, mm.monsterBody.transform.position + new Vector3(mm.beiji.PX, mm.beiji.PY, 0), Color.green, Vector3.zero, false, true);
        } else {
            CreateProp.createProp.SubBloodView(mm.my_HealState.GrowBlood, mm.monsterBody.transform.position + new Vector3(mm.beiji.PX, mm.beiji.PY, 0), Color.green, Vector3.zero, false, true);
        }
        if (CurrArgs.m_iBufferUI == 1) {
			healBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterHealBuff")as GameObject;
			GameObject temp1 = GameObject.Instantiate (healBuff, CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position, Quaternion.identity)as GameObject;	
			if (CurrCtrl.transform.rotation.y != 0f) {
				temp1.transform.rotation = new Quaternion (0f, 180f, 0f, 0f);
			}
			temp1.transform.localScale = new Vector3 (mm.bufferCtl.buffScale* CurrArgs.m_fValue2, mm.bufferCtl.buffScale * CurrArgs.m_fValue2, 1f);
			temp1.transform.position = new Vector3 (temp1.transform.position.x + mm.bufferCtl.xDis, temp1.transform.position.y + mm.bufferCtl.yDis, temp1.transform.position.z);
			temp1.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform);
			GameObject.Destroy (temp1, 3f);
		}
		return true;
	}

	public override bool OnUpdate ()
	{
		if ((CurrArgs.m_ContinuousTime -= Time.deltaTime) <= CurrArgs.m_EndTime) {
			CurrArgs.isOver = true;
		}
		return true;
	}

	public override bool OnExit ()
	{
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "增加血量Buffer，血量增加总血量的" + CurrArgs.m_fValue1;
		}
	}
}
