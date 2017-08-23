using UnityEngine;
using System.Collections;

/// <summary>
/// Buff行对于怪物的Z轴位置
/// </summary>
public class MonsterBuffPositionCtr : MonoBehaviour
{
	/// <summary>
	/// 是否覆盖怪物
	/// </summary>
	public bool coverMonster = true;
	/// <summary>
	/// 距离怪物Z轴长度
	/// </summary>
	private float dis = -0.001f;
	// Use this for initialization
	void Start ()
	{
		if (!coverMonster) {
			dis = -dis;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.parent.transform.position.z + dis);
		gameObject.transform.rotation = new Quaternion (0, 0, 0, 1);
	}
}
