/// <summary>
/// 各种Buffer的枚举类型
/// </summary>
public class BufferType
{
	#region 怪物Buff类型

	/// <summary>
	///非周期性正面Buffer
	/// </summary>
	public enum Buffer
	{
		AllType,
		/// <summary>
		/// 加速
		/// </summary>
		BufferSpeedUp,
		/// <summary>
		/// 加血
		/// </summary>
		BufferAddBlood,
		/// <summary>
		/// 增加攻击力
		/// </summary>
		BufferAddDamage,
		/// <summary>
		/// 无敌
		/// </summary>
		BufferInvincible,
		/// <summary>
		/// 狂暴
		/// </summary>
		BufferRage,
		/// <summary>
		///加攻击速度
		/// </summary>
		BufferAddAttackSpeed,
		/// <summary>
		/// 增加防御力
		/// </summary>
		BufferAddDefensive,
        /// <summary>
        /// 护盾
        /// </summary>
        BufferShield,
        /// <summary>
        /// 复活
        /// </summary>
        BufferRelive,
	}

	/// <summary>
	/// 非周期性负面Buffer
	/// </summary>
	public enum DeBuffer
	{
		AllType,
		/// <summary>
		/// 减速
		/// </summary>
		DeBufferSlowDown,
		/// <summary>
		/// 眩晕
		/// </summary>
		DeBufferDizzness,
		/// <summary>
		/// 眩晕
		/// </summary>
		DeBufferFreeze,
		/// <summary>
		/// 破甲
		/// </summary>
		DeBufferSunderArmor,
        /// <summary>
        /// 易伤
        /// </summary>
        DeBuffAptToAtttack,
    }

	/// <summary>
	/// 周期性的正面Buffer
	/// </summary>
	public enum PerBuffer
	{
		AllType,
		/// <summary>
		/// 加血
		/// </summary>
		PerBufferAddBlood,
	}

	/// <summary>
	/// 周期性的负面Buffer
	/// </summary>
	public enum PerDeBuffer
	{
		AllType,
		/// <summary>
		/// 流血
		/// </summary>
		PerDeBufferBleed,
		/// <summary>
		/// 中毒
		/// </summary>
		PerDeBufferPoison,
		/// <summary>
		/// 灼烧
		/// </summary>
		PerDeBufferIgnition,
	}

	#endregion

	#region 玩家Buff类型

	/// <summary>
	///玩家的非周期性正面Buffer
	/// </summary>
	public enum PlayerBuffer
	{

	}
	/// <summary>
	/// 玩家的非周期性负面Buffer
	/// </summary>
	public enum PlayerDeBuffer
	{
		/// <summary>
		/// 玩家减速
		/// </summary>
		PlayerDeBufferSlowDown,
	}

	/// <summary>
	/// 玩家的周期性的正面Buffer
	/// </summary>
	public enum PlayerPerBuffer
	{
        /// <summary>
        /// 回血
        /// </summary>
        PlayerBufferAbsort,
        /// <summary>
        /// 加防
        /// </summary>
        PlayerBufferDefense,
        /// <summary>
        /// 加攻
        /// </summary>
        PlayerBufferAttack,
        /// <summary>
        /// 加速
        /// </summary>
        PlayerBufferSpeed,
    }

    /// <summary>
    /// 玩家的周期性的负面Buffer
    /// </summary>
    public enum PlayerPerDeBuffer
	{
        /// <summary>
		/// 中毒
		/// </summary>
		PlayerDeBufferBleed,
        /// <summary>
        /// 减防
        /// </summary>
        PlayerDeBufferDefense,
        /// <summary>
        /// 减攻
        /// </summary>
        PlayerDeBufferAttack,
        /// <summary>
        /// 减速
        /// </summary>
        PlayerDeBufferSpeed,
    }

	#endregion
}