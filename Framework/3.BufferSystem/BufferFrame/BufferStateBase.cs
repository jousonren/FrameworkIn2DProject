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
	/// 当前Buffer的具体值 -> (本系统中值为buff本身)
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
		get;
	}

	/// <summary>
	/// 当前Buffer的具体参数
	/// </summary>
	/// <value>The curr arguments.</value>
	public BufferArgs CurrArgs {
		set;
		get;
	}
    /// <summary>
    /// 当buff进入时
    /// </summary>
    /// <returns></returns>
	public abstract bool OnEnter ();
    /// <summary>
    ///buff处于更新中
    /// </summary>
    /// <returns></returns>
	public abstract bool OnUpdate ();
    /// <summary>
    ///  当加入新的buff并且需要和旧buff融合时
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual bool OnRenovate(BufferArgs args) {
        CurrArgs.m_ContinuousTime = args.m_ContinuousTime;
        return true;
    }
    /// <summary>
    /// 当buff结束时
    /// </summary>
    /// <returns></returns>
	public abstract bool OnExit ();
}