using UnityEngine;
using System.Collections;

public class MonsterActionController : MonoBehaviour
{
	/// <summary>
	/// 当前怪物的FSM控制器
	/// </summary>
	MonsterFSMController m_FSMCtl;
	// Use this for initialization
	void Start ()
	{
		m_FSMCtl = new MonsterFSMController (this) as MonsterFSMController;
	}

	void Update ()
	{
		if (m_FSMCtl.CurrMonsterState != MonsterState.None) {
			m_FSMCtl.Update ();
		}
	}

	public  void SetIdle ()
	{
		m_FSMCtl.ChangeState (MonsterState.Idle);
	}
		
}
