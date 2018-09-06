using UnityEngine;

/// <summary>
/// 负责将信息从面板传入AnimatorTriggerCtl的参数类
/// </summary>
[System.Serializable]
public class TriggerMessage
{
	public string triggerText;
	public float time;

	public TriggerMessage ()
	{
		triggerText = "TriggerName";
		time = 0f;
	}

	public Timer timer = new Timer (0f, 0f);
}

/// <summary>
/// 向Animator分发
/// </summary>
public class AnimatorCtlMaker : MonoBehaviour
{
	public Animator animator;
	public TriggerMessage[] triggerMessage;

	private void Awake ()
	{
		for (int i = 0; i < triggerMessage.Length; i++) {
			AnimatorTriggerCtl triggerCtl = new AnimatorTriggerCtl (triggerMessage [i].triggerText, triggerMessage [i].time, animator);
			triggerMessage [i].timer.ResetTimer (triggerMessage [i].time, 0f);
			triggerMessage [i].timer.CallBack += triggerCtl.TriggerAnimator;
			triggerMessage [i].timer.Start ();
		}
	}

	private void Update ()
	{
		for (int i = 0; i < triggerMessage.Length; i++) {
			triggerMessage [i].timer.Update (Time.deltaTime);
		}
	}
}


