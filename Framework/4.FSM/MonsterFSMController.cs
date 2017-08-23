using UnityEngine;
using System.Collections;
using System;

public class MonsterFSMController : MonoBehaviour
{
	//public ArrayList statusArr = new ArrayList ();

	/// <summary>
	/// 控制器当前状态
	/// </summary>
	/// <value>The state of the curr monster.</value>
	public MonsterState CurrMonsterState {
		get;
		private set;
	}

	/// <summary>
	/// 当前状态对应的脚本
	/// </summary>
	public MonsterStatusBase status;

	/// <summary>
	/// 当前FSM控制器挂载的怪物对象
	/// </summary>
	/// <value>The curr ctrl.</value>
	public MonsterActionController CurrCtrl {
		get;
		private set;
	}

	/// <summary>
	/// 带参数的构造方法
	/// </summary>
	/// <param name="ctrl">Ctrl.</param>
	public MonsterFSMController (MonsterActionController ctrl)
	{
		CurrCtrl = ctrl;
		CurrMonsterState = MonsterState.None;
	}

	public void Update ()
	{
		if (status != null) {
			status.OnUpdate ();
		}
	}

	public void ChangeState (MonsterState newState)
	{
		if (CurrMonsterState == newState) {
			return;
		}
		if (status != null) {
			status.OnLeave ();
		}
		status = Activator.CreateInstance (Type.GetType (newState.ToString ()))as MonsterStatusBase;
		status.CurrCtrl = CurrCtrl;
		CurrMonsterState = newState;
		status.OnEnter ();
	}
}
