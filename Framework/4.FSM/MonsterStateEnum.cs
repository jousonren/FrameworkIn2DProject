using UnityEngine;
using System.Collections;

/// <summary>
/// 怪物状态的枚举(权重依次增加)
/// </summary>
public enum MonsterState
{
	/// <summary>
	/// 空状态
	/// </summary>
	None,
	/// <summary>
	/// 待机
	/// </summary>
	Idle,
	/// <summary>
	/// 巡逻
	/// </summary>
	Patrol,
	/// <summary>
	/// 追击 
	/// </summary>
	Chase,
	/// <summary>
	/// 攻击
	/// </summary>
	Attack,
	/// <summary>
	/// 被控制(乱走)
	/// </summary>
	UnderControl,
	/// <summary>
	/// 被束缚(无法移动,类似于眩晕)
	/// </summary>
	Fettered,
	/// <summary>
	/// 被轻击
	/// </summary>
	UnderAttack,
	/// <summary>
	/// 被重击
	/// </summary>
	UnderThumpAttack,
	/// <summary>
	/// 被冻结（动画暂停）
	/// </summary>
	Frozen,
	/// <summary>
	/// 死亡
	/// </summary>
	Death,
}
