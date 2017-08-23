using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Buffer管理器
/// </summary>
public class BufferMgr
{
	// 1.该控制器不需要挂载到游戏对象身上
	// 2.和人物控制不同, 不同的buffer之间可以同时工作
	// 3.所以我们需要一个容器, 里面存放所有的buffer状态
	// 4.我们在每一帧, 按照添加的顺序, 不停的调用每一个状态的OnUpdate方法
	// 5.根据状态的m_addition条件来判断是否更新buffer的持续时间

	// 公开的接口
	// 受到buffer系统影响的游戏对象(在此特制游戏中的怪物)
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

	private void BufferAddBase (object buffer, BufferArgs bufferArgs)
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
				BufferStateBase oldBuffer = m_bufferList.Find (delegate(BufferStateBase obj) {
					return obj.CurrBufferState.Equals (buffer);	
				});
				if (oldBuffer != null) {
					oldBuffer.CurrArgs.m_EndTime -= newBuffer.CurrArgs.m_ContinuousTime;
				}
			} else { // 不存在, 直接添加进去
				// 添加到容器中
				newBuffer.OnEnter ();
				m_bufferList.Add (newBuffer);
			}
		}
	}

	public void BufferAdd (BufferType.Buffer buffer, BufferArgs bufferArgs)
	{
		// 根据buffer参数创建一个新的BufferStateBase对象
		BufferAddBase (buffer, bufferArgs);
	}

	public void BufferAdd (BufferType.DeBuffer buffer, BufferArgs bufferArgs)
	{
		BufferAddBase (buffer, bufferArgs);
	}

	public void BufferAdd (BufferType.PerBuffer buffer, BufferArgs bufferArgs)
	{
		BufferAddBase (buffer, bufferArgs);
	}

	public void BufferAdd (BufferType.PerDeBuffer buffer, BufferArgs bufferArgs)
	{
		BufferAddBase (buffer, bufferArgs);
	}

	// 2.移除某一种Buffer, 或者某一类Buffer
	private void BufferRemoveBase (object bufferType = null)
	{
		if (bufferType == null) {
			m_bufferList.RemoveAll (delegate(BufferStateBase obj) {
				return (obj.OnExit () || true);
			});
			return;
		}

		// 删除一类Buffer还是删除一个Buffer?
		if ((int)bufferType == 0) {
			m_bufferList.RemoveAll (delegate(BufferStateBase obj) {
				return obj.CurrBufferType.Equals (bufferType.GetType ()) && (obj.OnExit () || true);
			});
		} else {
			m_bufferList.RemoveAll (delegate(BufferStateBase obj) {
				return obj.CurrBufferState.Equals (bufferType) && (obj.OnExit () || true);
			});
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

	public void BufferRemove ()
	{
		BufferRemoveBase ();
	}

	// 3.检查某个Buffer是否存在
	private bool BufferCheckExistsBase (object buffer)
	{
		BufferStateBase findBuffer = m_bufferList.Find (delegate(BufferStateBase obj) {
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

	// 4.驱动OnUpdate的方法
	public void Update ()
	{
		// 调用每个Buffer的update方法
/*		m_bufferList.Find (delegate(BufferStateBase obj) {
			return (obj.OnUpdate () || true);

		});*/
		for (int i = 0; i < m_bufferList.Count; i++) {
			m_bufferList [i].OnUpdate ();
		}
		// 检查结束的Buffer, 然后移除之
		m_bufferList.RemoveAll (delegate(BufferStateBase obj) {
			return obj.CurrArgs.isOver == true && (obj.OnExit () || true);
		});
	}
}
