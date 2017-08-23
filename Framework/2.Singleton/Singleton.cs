using UnityEngine;
using System.Collections;

// 非Unity单例类
// System.IDisposable: 系统的内存管理类, Dispose()是内存回收后的一个回调方法.
// 单例类的泛型模板, 用于生成各种单例类.
// T表示类型, 这里必须是一个类, 并且这个类必须可以new出来.(where T : new())
public abstract class Singleton<T> : System.IDisposable where T : new()
{
	#region 单例

	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = new T ();
			}
			return instance;
		}
	}

	#endregion

	public virtual void Dispose ()
	{

	}

}
