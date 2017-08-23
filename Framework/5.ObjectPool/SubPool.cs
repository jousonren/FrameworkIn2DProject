using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubPool
{

	#region 字段

	/// <summary>
	/// 存放池子内所有对象的链表
	/// </summary>
	List<GameObject> m_subPool = new List<GameObject> ();
	/// <summary>
	/// 池子中预设体的类型
	/// </summary>
	GameObject m_perfab;

	#endregion

	#region 属性

	public string Name {
		get {
			return m_perfab.name;
		}
	}

	#endregion

	#region 构造方法

	/// <summary>
	/// 外部可以通过传入预制体而构建一个池子
	/// </summary>
	/// <param name="prefab">Prefab.</param>
	public SubPool (GameObject prefab)
	{
		m_perfab = prefab;
	}

	public GameObject Spawn ()
	{
		GameObject obj = null;
		foreach (GameObject go in m_subPool) {
			if (go.activeSelf == false) {
				obj = go;
				break;
			}
		}
		if (obj == null) {
			obj = GameObject.Instantiate (m_perfab)as GameObject;
			m_subPool.Add (obj);
			//将生成的对象加入到不销毁的父物体中
			obj.transform.SetParent (GameObject.Find ("MonsterSceneUse").transform);
		}
		obj.SetActive (true);
		IReuse iResue = obj.GetComponent<IReuse> ();
		if (iResue != null) {
			iResue.Spawn ();
		}
		return obj;
	}

	public void UnSpawn (GameObject obj)
	{
		if (m_subPool.Contains (obj)) {
			IReuse iResue = obj.GetComponent<IReuse> ();
			if (iResue != null) {
				iResue.UnSpawn ();
			}
			obj.SetActive (false);
		}
	}

	public void UnSpawnAll ()
	{
		foreach (GameObject obj in m_subPool) {
			if (obj.activeSelf) {
				UnSpawn (obj);
			}
		}
	}

	public bool  Contains (GameObject go)
	{
		return m_subPool.Contains (go);
	}

	#endregion
}
