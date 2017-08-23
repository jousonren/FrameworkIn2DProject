using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CreatRandom:Singleton<CreatRandom>
{
	/// <summary>
	/// 返回一个介于两者之间（包含两者）的浮点数
	/// </summary>
	/// <returns>The <see cref="System.Single"/>.</returns>
	/// <param name="num1">Num1.</param>
	/// <param name="num2">Num2.</param>
	public float CreatRandomBetween (float num1, float num2)
	{
		return Random.Range (num1, num2);
	}

	/// <summary>
	/// 返回一个概率的结果
	/// </summary>
	/// <returns><c>true</c>, if random probability was gotten, <c>false</c> otherwise.</returns>
	/// <param name="num">从0到100的浮点数.</param>
	public bool GetRandomResult (float num)
	{
		if (Random.Range (0, 100) < num) {
			return true;
		} else {
			return false;
		}
	}

	/// <summary>
	/// 返回一个平面圆形中的任意一个点(传入V3.position返回V3.position但是Z值不变)
	/// </summary>
	/// <returns>圆中的任意位置.</returns>
	/// <param name="center">圆形位置.</param>
	/// <param name="radius">半径.</param>
	public Vector3 GetRandomPosInCircle (Vector3 center, float radius)
	{
		Vector2 temp = Random.insideUnitCircle * radius;
		return (new Vector3 (center.x + temp.x, center.y + temp.y, center.z));
	}

	/// <summary>
	/// 获取一个2D上随机旋转
	/// </summary>
	/// <returns>The random quaternion.</returns>
	public Quaternion GetRandomQuaternion (float minRotate, float maxRotate)
	{
		return(Quaternion.Euler (new Vector3 (0f, 0f, Random.Range (minRotate, maxRotate))));
	}

	System.Random random = new System.Random ();
	ArrayList arrayList = new ArrayList ();

	/// <summary>
	/// 从0到numberTotal中返回number个整数(包含0和最大值，返回的值不可重复)
	/// </summary>
	/// <returns>The random number.</returns>
	/// <param name="numberTotal">Number total.</param>
	/// <param name="number">Number.</param>
	public ArrayList GetRandomUnEqualNum (int numberTotal, int number)
	{
		arrayList.Clear ();
		do {
			int a = random.Next (0, numberTotal + 1);
			if (!arrayList.Contains (a)) {
				arrayList.Add (a);
			}
		} while (arrayList.Count < number);
		return arrayList;
	}

	/// <summary>
	/// 从0到numberTotal中返回number个整数(包含0和最大值，返回的值可以重复)
	/// </summary>
	/// <returns>The random number.</returns>
	/// <param name="numberTotal">Number total.</param>
	/// <param name="number">Number.</param>
	public ArrayList GetRandomNum (int numberTotal, int number)
	{
		arrayList.Clear ();
		do {
			int a = random.Next (0, numberTotal + 1);
			arrayList.Add (a);
		} while (arrayList.Count < number);
		return arrayList;
	}
}
