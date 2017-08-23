using UnityEngine;
using System.Collections;

// 如果场景中没有任何对象挂载了该组件, 就会创建一个对象, 然后把这个组件挂载到该游戏对象身上.
// 如果该类的子类挂载到了一个游戏对象身上,  则会执行Awake方法, 将instance赋值.
// 对于调用该类的任意方法, 不用考虑场景中是否拥有组件.
public abstract class MonoSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
	#region 单例

	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				GameObject obj = new GameObject (typeof(T).Name);
				instance = obj.AddComponent<T> ();
			}
			return instance;
		}
	}

	#endregion

	#region Unity回调

	protected virtual void Awake ()
	{
		instance = this as T;
	}

	#endregion

}
