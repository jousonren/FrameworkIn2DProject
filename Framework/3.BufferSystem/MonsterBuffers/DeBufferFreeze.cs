using UnityEngine;
using BehaviorDesigner.Runtime;
using Spine.Unity;

/// <summary>
/// 怪物冰冻Buffer
/// </summary>
public class DeBufferFreeze : BufferStateBase
{
	/// <summary>
	/// 冰冻Buff
	/// </summary>
	private GameObject freeezBuff;
	/// <summary>
	/// 冰冻Buff
	/// </summary>
	private GameObject temp;
	/// <summary>
	/// 该Buffer的控制器
	/// </summary>
	private BufferController m_BufferCrl;
	/// <summary>
	/// 行为树
	/// </summary>
	private BehaviorTree myTree;
	/// <summary>
	/// 动画状态机
	/// </summary>
	private Animator myAni;
	/// <summary>
	/// Animation
	/// </summary>
	private SkeletonAnimation skeletonAnimation;
	/// <summary>
	/// 导航代理
	/// </summary>
	private PolyNavAgent agent;
	/// <summary>
	/// 被击脚本
	/// </summary>
	private MonsterActionCtl actionCtl;

	public override bool OnEnter ()
	{
		CurrArgs.m_bValue1 = false;
		myTree = CurrCtrl.GetComponentInChildren<BehaviorTree> ();
		myAni = CurrCtrl.GetChild (0).GetComponent<Animator> ();
		skeletonAnimation = CurrCtrl.GetChild (0).GetComponent<SkeletonAnimation> ();
		agent = CurrCtrl.GetComponentInChildren<PolyNavAgent> ();
		m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController> ();
		actionCtl = CurrCtrl.GetComponent<MonsterActionCtl> ();
        //怪物停止移动
        if (!actionCtl.superArmor) {
			CurrArgs.m_bValue1 = true;
			if (CurrArgs.m_iBufferUI == 1) {
				freeezBuff = Resources.Load ("MonsterResources/MonsterBuffs/MonsterFreezeBuff")as GameObject;
				temp = GameObject.Instantiate (freeezBuff, new Vector3 (-100, 100, 0) + new Vector3 (0, 0, -0.01f), Quaternion.identity)as GameObject;
				temp.transform.localScale = new Vector3 (m_BufferCrl.buffScale, m_BufferCrl.buffScale, m_BufferCrl.buffScale);
				temp.transform.position = CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform.position + new Vector3 (0f, 0f, -0.01f);
				temp.transform.SetParent (CurrCtrl.GetComponent<MonsterMessage> ().monsterFoot.transform);
			}
			actionCtl.EnterFreeze ();
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
		if (CurrArgs.m_bValue1) {
			actionCtl.ExitFreeze ();
		}
		if (!actionCtl.superArmor) {
			if (CurrArgs.m_iBufferUI == 1) {
                if (CurrCtrl.GetComponentInChildren<HealthState>().isDeath)
                {
                    if (temp) {
                        GameObject.Destroy(temp);
                    }
                }
                else
                {
                    if (temp) {
                        temp.transform.SetParent(null);
                        temp.GetComponentInChildren<Animator>().SetTrigger("Play");
                    }
                }
            }
		}
		return true;
	}

	public override string CurrBufferInfo {
		get {
			return "怪物冰冻Buffer，冰冻剩余时间为" + CurrArgs.m_ContinuousTime;
		}
	}
}
