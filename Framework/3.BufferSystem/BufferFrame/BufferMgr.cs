using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Buffer管理器
/// </summary>
public class BufferMgr
{
	// 1.该控制器不需要挂载到游戏对象身上
	// 2不同的buffer之间可以同时工作
	// 3.需要一个容器, 里面存放所有的buffer状态
	// 4.每一帧 按照添加的顺序, 不停的调用每一个状态的OnUpdate方法
	// 5.根据状态的m_addition条件来判断是否更新buffer的持续时间

	// 公开的接口
	// 受到buffer系统影响的游戏对象(在此特指游戏中的怪物)
	public Transform CurrCtrl {
		get;
		set;
	}

	// 构造方法
	public BufferMgr (Transform trans)
	{
		CurrCtrl = trans;
	}

	// Buffer容器
	private List<BufferStateBase> m_bufferList = new List<BufferStateBase> ();

	// 1.添加一个新的Buffer(包括刷新或者重新添加)
	private BufferStateBase BufferCreate (object buffer, BufferArgs bufferArgs)
	{
		BufferStateBase newBuffer =
			Activator.CreateInstance (
				Type.GetType (buffer.ToString ())
			)
        as BufferStateBase;

		newBuffer.CurrArgs = bufferArgs;
		newBuffer.CurrCtrl = CurrCtrl;
		newBuffer.CurrBufferType = buffer.GetType ();
		newBuffer.CurrBufferState = buffer;

		return newBuffer;
	}

	private BufferStateBase BufferAddBase (object buffer, BufferArgs bufferArgs)
	{
		// 实例化一个Buffer
		BufferStateBase newBuffer = BufferCreate (buffer, bufferArgs);
		// 判断该buffer是否应该添加到容器中
		if (newBuffer.CurrArgs.m_Addition == true) {
			// 添加到容器中
			newBuffer.OnEnter ();
			m_bufferList.Add (newBuffer);
		} else {
			// 判断该状态在容器中是否存在
			if (BufferCheckExistsBase (buffer)) { // 存在, 更新作用时间
				BufferStateBase oldBuffer = m_bufferList.Find (delegate (BufferStateBase obj) {
					return obj.CurrBufferState.Equals (buffer);
				});
				if (oldBuffer != null) {
                    oldBuffer.OnRenovate(newBuffer.CurrArgs);
                }
			} else { // 不存在, 直接添加进去
				// 添加到容器中
				newBuffer.OnEnter ();
				m_bufferList.Add (newBuffer);
			}
		}
		return newBuffer;
	}

	public BufferStateBase BufferAdd (BufferType.Buffer buffer, BufferArgs bufferArgs)
	{
		// 根据buffer参数创建一个新的BufferStateBase对象
		return BufferAddBase (buffer, bufferArgs);
	}

	public BufferStateBase BufferAdd (BufferType.DeBuffer buffer, BufferArgs bufferArgs)
	{
		return BufferAddBase (buffer, bufferArgs);
	}

	public BufferStateBase BufferAdd (BufferType.PerBuffer buffer, BufferArgs bufferArgs)
	{
		return BufferAddBase (buffer, bufferArgs);
	}

	public BufferStateBase BufferAdd (BufferType.PerDeBuffer buffer, BufferArgs bufferArgs)
	{
		return BufferAddBase (buffer, bufferArgs);
	}

	public BufferStateBase BufferAdd (BufferType.PlayerBuffer buffer, BufferArgs bufferArgs)
	{
		// 根据buffer参数创建一个新的BufferStateBase对象
		return BufferAddBase (buffer, bufferArgs);
	}

	public BufferStateBase BufferAdd (BufferType.PlayerDeBuffer buffer, BufferArgs bufferArgs)
	{
		return BufferAddBase (buffer, bufferArgs);
	}

	public BufferStateBase BufferAdd (BufferType.PlayerPerBuffer buffer, BufferArgs bufferArgs)
	{
		return BufferAddBase (buffer, bufferArgs);
	}

	public BufferStateBase BufferAdd (BufferType.PlayerPerDeBuffer buffer, BufferArgs bufferArgs)
	{
		return BufferAddBase (buffer, bufferArgs);
	}
	// 2.移除某一种Buffer, 或者某一类Buffer
	private void BufferRemoveBase (object bufferType = null)
	{
		if (bufferType == null) {
			m_bufferList.RemoveAll (delegate (BufferStateBase obj) {
				return (obj.OnExit () || true);
			});
			return;
		}

		// 删除一类Buffer还是删除一个Buffer
		if ((int)bufferType == 0) {
			m_bufferList.RemoveAll (delegate (BufferStateBase obj) {
				return obj.CurrBufferType.Equals (bufferType.GetType ()) && (obj.OnExit () || true);
			});
		} else {
			m_bufferList.RemoveAll (delegate (BufferStateBase obj) {
				return obj.CurrBufferState.Equals (bufferType) && (obj.OnExit () || true);
			});
		}
	}

	/// <summary>
	/// 临时存储遍历出来的buffer
	/// </summary>
	private BufferStateBase temp = null;

	/// <summary>
	/// 移除某个特定的Buffer.
	/// </summary>
	/// <param name="buffer">Buffer.</param>
	public void BufferRemoveOne (object buffer)
	{
		foreach (var item in m_bufferList) {
			if (item == buffer) {
				temp = item;
			}
		}
		//在遍历list数据时无法直接操作list，因此将移除放到遍历结束执行
		if (temp != null) {
			temp.OnExit ();
			m_bufferList.Remove (temp);
			temp = null;
		}
	}

	public void BufferRemove (BufferType.Buffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove (BufferType.DeBuffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove (BufferType.PerBuffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove (BufferType.PerDeBuffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove (BufferType.PlayerBuffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove (BufferType.PlayerDeBuffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove (BufferType.PlayerPerBuffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove (BufferType.PlayerPerDeBuffer buffer)
	{
		BufferRemoveBase (buffer);
	}

	public void BufferRemove ()
	{
		BufferRemoveBase ();
	}
    public void RenovateBuff(BufferStateBase buffer, BufferArgs args) {
        foreach (var item in m_bufferList) {
            if (item == buffer) {
                temp = item;
            }
        }
        if (temp != null) {
            temp.OnRenovate(args);
        }
    }
    // 3.检查某个Buffer是否存在
    private bool BufferCheckExistsBase (object buffer)
	{
		BufferStateBase findBuffer = m_bufferList.Find (delegate (BufferStateBase obj) {
			return obj.CurrBufferState.Equals (buffer);
		});
		return findBuffer == null ? false : true;
	}

	public bool BufferCheckExists (BufferType.Buffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}

	public bool BufferCheckExists (BufferType.DeBuffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}

	public bool BufferCheckExists (BufferType.PerBuffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}

	public bool BufferCheckExists (BufferType.PerDeBuffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}

	public bool BufferCheckExists (BufferType.PlayerBuffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}

	public bool BufferCheckExists (BufferType.PlayerDeBuffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}

	public bool BufferCheckExists (BufferType.PlayerPerBuffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}

	public bool BufferCheckExists (BufferType.PlayerPerDeBuffer buffer)
	{
		return BufferCheckExistsBase (buffer);
	}
	// 4.驱动OnUpdate的方法
	public void Update ()
	{
		// 调用每个Buffer的update方法(该方法有问题,采用直接更新整个bufferlist的方法进行的重写)
		/*		m_bufferList.Find (delegate (BufferStateBase obj) {
        			return (obj.OnUpdate () || true);

        		});*/
		for (int i = 0; i < m_bufferList.Count; i++) {
			m_bufferList [i].OnUpdate ();
		}
		// 检查结束的Buffer, 然后移除之
		m_bufferList.RemoveAll (delegate (BufferStateBase obj) {
			return obj.CurrArgs.isOver == true && (obj.OnExit () || true);
		});
	}
}