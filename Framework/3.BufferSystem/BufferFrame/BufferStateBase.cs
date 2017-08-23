using UnityEngine;

/// <summary>
/// Buffer状态基类
/// </summary>
public abstract class BufferStateBase
{
	/// <summary>
	/// 当前状态的控制对象
	/// </summary>
	/// <value>The curr ctrl.</value>
	public Transform CurrCtrl {
		set;
		get;
	}

	/// <summary>
	/// 当前Buffer的类型-> Buffer? Debuffer? PerBuffer? PerDeBuffer?
	/// </summary>
	/// <value>The type of the curr buffer.</value>
	public object CurrBufferType {
		get;
		set;
	}

	/// <summary>
	/// 当前Buffer的具体值 -> SpeedUp? Disarm?
	/// </summary>
	/// <value>The state of the curr buffer.</value>
	public object CurrBufferState {
		get;
		set;
	}

	/// <summary>
	/// 当前Buffer的详细描述
	/// </summary>
	/// <value>The curr buffer info.</value>
	public abstract string CurrBufferInfo {
		get	;	
	}

	/// <summary>
	/// 当前Buffer的具体参数
	/// </summary>
	/// <value>The curr arguments.</value>
	public BufferArgs CurrArgs {
		set;
		get;
	}

	public abstract bool OnEnter ();

	public abstract bool OnUpdate ();

	public abstract bool OnExit ();
}
