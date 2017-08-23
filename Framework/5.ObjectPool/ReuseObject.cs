using UnityEngine;

/// <summary>
/// 所有类的基类，想要进入池子的对象不要继承在MonoBehaviour下，而是继承此类
/// </summary>
public abstract class ReuseObject : MonoBehaviour,IReuse
{

	public abstract void Spawn ();

	public abstract void UnSpawn ();
}
