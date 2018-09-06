using UnityEngine;
using System.Collections;

public class LoadResourcesMgr : Singleton<LoadResourcesMgr>
{

	public T LoadObject<T> (object enumName) where T:Object
	{
		string enumType = enumName.GetType ().Name;
		string filePath = string.Empty;
		switch (enumType) {
		case "UnderAttackEnum":
			{
				filePath = "MonsterResources/UnderAttackEffects/WeaponAttackEffects/" + enumName.ToString ();
				break;
			}
		case "FaBaoAttackEffect":
			{
				filePath = "MonsterResources/UnderAttackEffects/FaBaoAttackEffects/" + enumName.ToString ();
				break;
			}
		case "GongFaAttackEffect":
			{
				filePath = "MonsterResources/UnderAttackEffects/GongFaAttackEffects/" + enumName.ToString ();
				break;
			}
		case "MonsterProps":
			{
				filePath = "MonsterResources/MonsterProps/" + enumName.ToString ();
				break;
			}
		case "MonsterSceneUse":
			{
				filePath = "MonsterResources/MonsterSceneUse/" + enumName.ToString ();
				break;
			}
		default:
			break;
		}
		return Resources.Load<T> (filePath);
	}
}
