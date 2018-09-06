using UnityEngine;
using System.Collections;

/// <summary>
/// 怪物流血特效的控制脚本，使四个流血效果循环播放，时间随机
/// </summary>
public class MonsterBleedBuffCtl : MonoBehaviour
{
	public GameObject[] props;
	public float cd1;
	public float cd2;
	private int label = 0;
	/// <summary>
	/// 低落在地面的血液
	/// </summary>
	public GameObject bleedGround;
	/// <summary>
	/// 怪物最外层
	/// </summary>
	[HideInInspector]
	public GameObject monster;
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("Play", 0f, cd1);
		//InvokeRepeating ("CreatBleedGround", 1.5f, 1.5f);
	}

	void Play ()
	{
		for (int i = 0; i < props.Length; i++) {
			Invoke ("PlayAni", CreatRandom.Instance.CreatRandomBetween (0, cd2));
		}
	}

	void PlayAni ()
	{
		props [label].GetComponent<Animator> ().SetTrigger ("Play");
		label++;
		if (label == props.Length) {
			label = 0;
		}
	}

	/// <summary>
	/// 生成地面的血液
	/// </summary>
	void CreatBleedGround ()
	{
		GameObject.Instantiate (bleedGround, monster.transform.position, monster.transform.rotation);
	}
}
