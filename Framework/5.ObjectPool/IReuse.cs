using UnityEngine;

/// <summary>
/// 对象池基类接口类
/// </summary>
public interface IReuse
{

	void Spawn ();

	void UnSpawn ();
}
