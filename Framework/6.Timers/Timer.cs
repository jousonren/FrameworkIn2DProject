public class Timer
{

	//计时器是否在运行
	private bool isRunning;
	//当前时间
	private float currentTime;
	//中途时间(不需要时为0)
	private float middleTime;
	//目标时间
	private float triggerTime;
	//是否已经触发中途时时间
	private bool hasMiddleTrigger;
	//保存事件的代理
	public delegate void EventHandler ();
	//触发事件列表
	public event EventHandler CallBack;
	//中途触发事件列表
	public event EventHandler MiddleCallBack;

	/// <summary>
	///  初始化
	/// </summary>
	/// <param name="second">Trigger Time</param>
	public Timer (float TriggerTiem, float MiddleTime)
	{
		currentTime = 0.0f;
		triggerTime = TriggerTiem;
		middleTime = MiddleTime;
	}

	/// <summary>
	/// 开始计时
	/// </summary>
	public void Start ()
	{
		currentTime = 0.0f;
		isRunning = true;
		hasMiddleTrigger = false;
	}

	/// <summary>
	/// Update Time
	/// </summary>
	public void Update (float deltaTime)
	{
		if (isRunning) {
			currentTime += deltaTime;
			//计时中途触发事件
			if (currentTime > middleTime && !hasMiddleTrigger) {
                if (MiddleCallBack != null) {
                    MiddleCallBack();
                }
                hasMiddleTrigger = true;
			}
			//计时结束
			if (currentTime > triggerTime) {
				isRunning = false;
                if (CallBack!=null) {
                    CallBack();
                }
            }
		}
	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	/// <param name="fixeddDeltaTime">Fixedd delta time.</param>
	public void FixedUpdate (float fixeddDeltaTime)
	{

		if (isRunning) {
			currentTime += fixeddDeltaTime;
			//计时中途触发事件
			if (middleTime != 0f && currentTime > middleTime && !hasMiddleTrigger) {
				MiddleCallBack ();
				hasMiddleTrigger = true;
			}
			//计时结束
			if (currentTime > triggerTime) {
				isRunning = false;
				CallBack ();
			}
		}
	}

	/// <summary>
	/// 停止计时
	/// </summary>
	public void Stop ()
	{
		isRunning = false;
	}

	/// <summary>
	/// 继续
	/// </summary>
	public void Continue ()
	{
		isRunning = true;
	}

	/// <summary>
	/// 重新开始
	/// </summary>
	public void Restart ()
	{
		isRunning = true;
		currentTime = 0.0f;
		hasMiddleTrigger = false;
	}

	/// <summary>
	/// 获取计时器进行了多长时间
	/// </summary>
	/// <returns>The time.</returns>
	public float GetRunTime ()
	{
		return currentTime;
	}

	/// <summary>
	/// 重置计时器（MiddleTime不需要则为0）,如果计时器在运行中,则计时器重新从0运行
	/// </summary>
	/// <param name="TriggerTiem">Trigger tiem.</param>
	/// <param name="MiddleTime">Middle time.</param>
	public void ResetTimer (float TriggerTime, float MiddleTime)
	{
		currentTime = 0.0f;
		triggerTime = TriggerTime;
		middleTime = MiddleTime;
	}

	/// <summary>
	/// 在计时进行时重新设置计时时间（MiddleTime不需要则为0）
	/// </summary>
	/// <param name="second">Trigger Time</param>
	public void ResetTriggerTime (float TriggerTime, float MiddleTime)
	{
		triggerTime = TriggerTime;
		middleTime = MiddleTime;
	}

    /// <summary>
    ///是否已经出发了中间行为
    /// </summary>
    /// <returns></returns>
    public bool MiddleTriggerOver() {
        return hasMiddleTrigger;
    }
}
