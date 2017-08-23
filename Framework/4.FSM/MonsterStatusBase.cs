using UnityEngine;
using System.Collections;

public abstract class MonsterStatusBase
{
	public MonsterActionController CurrCtrl{ get; set; }

	/// <summary>
	/// 进入该状态
	/// </summary>
	public abstract void OnEnter ();

	/// <summary>
	/// 在状态中
	/// </summary>
	public abstract void OnUpdate ();

	/// <summary>
	/// 离开该状态
	/// </summary>
	public abstract void OnLeave ();

}
