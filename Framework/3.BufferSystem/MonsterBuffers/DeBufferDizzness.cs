using UnityEngine;
using System.Collections;
using System;
using BehaviorDesigner.Runtime;
using Spine.Unity;

/// <summary>
/// 怪物眩晕Buffer
/// </summary>
public class DeBufferDizzness : BufferStateBase {
    /// <summary>
    /// 眩晕Buff
    /// </summary>
    private GameObject dizznessBuff;
    /// <summary>
    /// 眩晕Buff
    /// </summary>
    private GameObject temp;
    /// <summary>
    /// 该Buffer的控制器
    /// </summary>
    private BufferController m_BufferCrl;
    /// <summary>
    /// 行为树
    /// </summary>
    private BehaviorTree myTree;
    /// <summary>
    /// 动画状态机
    /// </summary>
    private Animator myAni;
    /// <summary>
    /// Animation
    /// </summary>
    private SkeletonAnimation skeletonAnimation;
    /// <summary>
    /// 导航代理
    /// </summary>
    private PolyNavAgent agent;
    /// <summary>
    /// 被击脚本
    /// </summary>
    private MonsterActionCtl actionCtl;

    public override bool OnEnter() {
        CurrArgs.m_bValue2 = false;
        myTree = CurrCtrl.GetComponentInChildren<BehaviorTree>();
        myAni = CurrCtrl.GetChild(0).GetComponent<Animator>();
        skeletonAnimation = CurrCtrl.GetChild(0).GetComponent<SkeletonAnimation>();
        agent = CurrCtrl.GetComponentInChildren<PolyNavAgent>();
        m_BufferCrl = CurrCtrl.GetComponentInChildren<BufferController>();
        actionCtl = CurrCtrl.GetComponent<MonsterActionCtl>();
        //怪物停止移动
        if (CurrArgs.m_bValue1) {
            UseUI();
            CurrArgs.m_bValue2 = true;
            actionCtl.EnterFetter();
        } else {
            if (!actionCtl.superArmor) {
                UseUI();
                CurrArgs.m_bValue2 = true;
                actionCtl.EnterFetter();
            }
        }

        return true;
    }
    private void UseUI() {
        if (CurrArgs.m_iBufferUI == 1) {
            dizznessBuff = Resources.Load("MonsterResources/MonsterBuffs/MonsterDizznessBuff") as GameObject;
            temp = GameObject.Instantiate(dizznessBuff, CurrCtrl.GetComponent<MonsterMessage>().monsterHead.transform.position, Quaternion.identity) as GameObject;
            if (CurrCtrl.transform.rotation.y != 0f) {
                temp.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            temp.transform.SetParent(CurrCtrl.GetComponent<MonsterMessage>().monsterHead.transform);
            temp.transform.localScale = new Vector3(m_BufferCrl.buffScale, m_BufferCrl.buffScale, m_BufferCrl.buffScale);
            temp.transform.position = new Vector3(temp.transform.position.x + m_BufferCrl.xDis, temp.transform.position.y + m_BufferCrl.yDis, temp.transform.position.z);
        }
    }
    public override bool OnUpdate() {
        if ((CurrArgs.m_ContinuousTime -= Time.deltaTime) <= CurrArgs.m_EndTime) {
            if (!CurrArgs.m_Permanent) {
                CurrArgs.isOver = true;
            }
        }
        return true;
    }

    public override bool OnExit() {
        if (CurrArgs.m_bValue2) {
            actionCtl.ExitFetter();
        }
        if (temp) {
            GameObject.Destroy(temp);
        }
        return true;
    }

    public override string CurrBufferInfo {
        get {
            return "怪物眩晕Buffer，眩晕剩余时间为" + CurrArgs.m_ContinuousTime;
        }
    }
}
