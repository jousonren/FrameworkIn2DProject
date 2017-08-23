using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 怪物中毒Buffer的位置控制
/// </summary>
public class MonsterPoisonBuff2Ctl : MonoBehaviour
{
	public GameObject[] buffers = new GameObject[16];
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("Play1", 0f, 2.5f);
		InvokeRepeating ("Play2", 0.5f, 2.5f);
	}

	private void Play1 ()
	{
		if (Random.Range (0, 100) >= 50f) {
			int temp1 = GetRamdom ();
			buffers [temp1].GetComponent<Animator> ().SetTrigger ("Play");
			int temp2;
			do {
				temp2 = GetRamdom ();
			} while (temp2 == temp1);
			buffers [temp2].GetComponent<Animator> ().SetTrigger ("Play");
		} else {
			int temp1 = GetRamdom ();
			buffers [temp1].GetComponent<Animator> ().SetTrigger ("Play");
		}
			
	}

	private void Play2 ()
	{
		if (Random.Range (0, 100) >= 50f) {
			int temp1 = GetRamdom ();
			buffers [temp1].GetComponent<Animator> ().SetTrigger ("Play");
			int temp2;
			do {
				temp2 = GetRamdom ();
			} while (temp2 == temp1);
			buffers [temp2].GetComponent<Animator> ().SetTrigger ("Play");
		} else {
			int temp1 = GetRamdom ();
			buffers [temp1].GetComponent<Animator> ().SetTrigger ("Play");
		}

	}

	private int GetRamdom ()
	{
		return  Random.Range (0, buffers.Length);
	}
}
