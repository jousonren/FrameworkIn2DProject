/// <summary>
/// 各种Buffer的枚举类型
/// </summary>
public class BufferType
{
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
}
