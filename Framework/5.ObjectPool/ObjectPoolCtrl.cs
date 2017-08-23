using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolCtrl : MonoSingleton<ObjectPoolCtrl>
{
	#region 字段

	Dictionary<string ,SubPool> m_pools = new Dictionary<string, SubPool> ();

	#endregion

	#region 面向用户的方法

	/// <summary>
	/// 从对象池取得对象
	/// </summary>
	/// <param name="name">Name.</param>
	public GameObject Spawn (object name)
	{
		SubPool subPool;
		if (!m_pools.ContainsKey (name.ToString ())) {
			RegisterSubPool (name);
		}
		subPool = m_pools [name.ToString ()];
		return subPool.Spawn ();
	}

	/// <summary>
	/// 向对象池注册对象
	/// </summary>
	/// <param name="name">Name.</param>
	private void RegisterSubPool (object name)
	{
		GameObject prefab = LoadResourcesMgr.Instance.LoadObject<GameObject> (name);
		SubPool subPool = new SubPool (prefab);
		m_pools.Add (subPool.Name, subPool);
	}

	/// <summary>
	/// 对象池回收对象
	/// </summary>
	/// <param name="obj">Object.</param>
	public void UnSpawn (GameObject obj)
	{
		foreach (SubPool pool in m_pools.Values) {
			if (pool.Contains (obj)) {
				pool.UnSpawn (obj);
				break;
			}
		}
	}

	/// <summary>
	/// 对象池回收全部对象
	/// </summary>
	public void UnSpawnAll ()
	{
		foreach (SubPool pool in m_pools.Values) {
			pool.UnSpawnAll ();
		}
	}

	#endregion
}
