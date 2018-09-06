public class MonsterCDTimer {
    //CD是否结束
    public bool isOver;
    //计时器是否在运行
    private bool isRunning;
    //当前时间
    public float currentTime;
    //目标时间
    private float triggerTime;
    /// <summary>
    /// 是否是随机时间计时器
    /// </summary>
    private bool isRandomTimer;
    /// <summary>
    /// 最大时间
    /// </summary>
    private float maxTime;
    /// <summary>
    /// 最小时间
    /// </summary>
    private float minTime;
    /// <summary>
    ///  初始化
    /// </summary>
    /// <param name="second">Trigger Time</param>
    public MonsterCDTimer (float TriggerTime) {
        currentTime = 0.0f;
        triggerTime = TriggerTime;
        isOver = false;
        isRunning = false;
        isRandomTimer = false;
    }
    /// <summary>
    ///  随机时间计时器初始化（每次都随机技能CD）
    /// </summary>
    /// <param name="second">Trigger Time</param>
    public MonsterCDTimer(float minTimer,float maxTimer) {
        currentTime = 0.0f;
        isOver = false;
        isRunning = false;
        isRandomTimer = true;
        maxTime = maxTimer;
        minTime = minTimer;
    }
    /// <summary>
    /// 开始计时
    /// </summary>
    public void Start () {
        if (isRandomTimer) {
            triggerTime = CreatRandom.Instance.CreatRandomBetween(minTime, maxTime);
        }
        currentTime = 0.0f;
        isRunning = true;
        isOver = false;
    }

    /// <summary>
    /// Update Time
    /// </summary>
    public void Update (float deltaTime) {
        if (isRunning) {
            currentTime += deltaTime;
            //计时结束
            if (currentTime > triggerTime) {
                isOver = true;
                isRunning = false;
            }
        }
    }

    /// <summary>
    /// 暂停计时
    /// </summary>
    public void Stop () {
        isRunning = false;
    }

    /// <summary>
    /// 继续计时
    /// </summary>
    public void Continue () {
        isRunning = true;
    }

    /// <summary>
    /// 重新开始
    /// </summary>
    public void Restart () {
        if (isRandomTimer) {
            triggerTime = CreatRandom.Instance.CreatRandomBetween(minTime, maxTime);
        }
        isRunning = true;
        isOver = false;
        currentTime = 0.0f;
    }

    /// <summary>
    /// 设置CD重新计时
    /// </summary>
    /// <param name="TriggerTiem">Trigger tiem.</param>
    public void ResetTimer (float TriggerTime) {
        currentTime = 0.0f;
        triggerTime = TriggerTime;
        isOver = false;
        isRunning = true;
    }

    /// <summary>
    /// 随机时间设置CD重新计时
    /// </summary>
    /// <param name="minTimer">最小时间</param>
    /// <param name="maxTimer">最大时间</param>
    public void ResetTimer(float minTimer, float maxTimer) {
        currentTime = 0.0f;
        triggerTime = CreatRandom.Instance.CreatRandomBetween(minTime, maxTime);
        isOver = false;
        isRunning = true;
    }

    /// <summary>
    /// 直接结束CD
    /// </summary>
    public void SetCdOver () {
        isOver = true;
        isRunning = false;
    }
}
