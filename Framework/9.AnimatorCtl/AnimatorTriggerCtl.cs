using UnityEngine;

/// <summary>
/// 一个Animator的Trigger触发对象
/// </summary>
public class AnimatorTriggerCtl : MonoBehaviour {
    /// <summary>
    /// 触发器的字符
    /// </summary>
    private string triggerName;
    /// <summary>
    /// 字符串触发时间
    /// </summary>
    private float triggerTime;
    /// <summary>
    /// 需要触发的动画状态机
    /// </summary>
    private Animator animatorToTrigger;
    public AnimatorTriggerCtl(string tString,float tFloat,Animator animator) {
        triggerName = tString;
        triggerTime = tFloat;
        animatorToTrigger = animator;
    }

    /// <summary>
    ///触发Trigger
    /// </summary>
    public void TriggerAnimator() {
        animatorToTrigger.SetTrigger(triggerName);
    }
}
