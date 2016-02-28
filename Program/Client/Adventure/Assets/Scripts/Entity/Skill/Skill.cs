/*******************************************************************
** 文件名:	Skill.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   13:19:28
** 版  本:	1.0
** 描  述:	技能
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public struct SkillCreateContext
{
    public Entity Owner;
    public SkillCsvData SkillData;
    public uint TargetUID;
}
public class Skill
{
    enum State
    {
        None = 0,
        Init,       //技能空闲
        Fire,       //技能释放
        End,        //技能结束
        Max,
    }
    protected Entity m_owner;
    protected SkillCsvData m_skillData;
    protected uint m_targeUID;
    private State m_state = State.None;
    public bool EndFlag { get; set; }
    private EffectBase m_effect = null;

    public virtual bool Create(SkillCreateContext ctx)
    {
        m_owner = ctx.Owner;
        m_skillData = ctx.SkillData;
        m_targeUID = ctx.TargetUID;

        SetState(State.Fire);
        return true;
    }
    public virtual void CreateSkillEffect(EffectCreateContext ctx)
    {
        SkillEffectType effectType = ctx.EffectType;
        switch (effectType)
        {
            case SkillEffectType.Damage:
                {
                    m_effect = new EffectSDamage();
                }
                break;
            default:
                {
                    Debug.LogError("Skill::CreateSkillEffect Effect Type Not Implement!!!");
                }
                break;
        }

        if (m_effect.Create(ctx) == false)
        {
            Debug.LogError("Skill::Create " + effectType + " Failed !!!");
        }

        m_effect.Start();
    }
    private void SetState(State state)
    {
        if (state == m_state)
        {
            return;
        }

        State nOldState = m_state;
        OnExit(nOldState);

        m_state = state;
        
        OnEnter(state);
    }
    private void OnExit(State nState)
    {
        switch(nState)
        {
            case State.None:
                {

                }
                break;
            case State.Init:
                {

                }
                break;
            case State.Fire:
                {

                }
                break;
            case State.End:
                {
                    EndFlag = true;
                }
                break;
        }
    }
    private void OnEnter(State nState)
    {
        switch (nState)
        {
            case State.None:
                {

                }
                break;
            case State.Init:
                {

                }
                break;
            case State.Fire:
                {

                }
                break;
            case State.End:
                {

                }
                break;
        }
    }
    public virtual void OnFixedUpdate()
    {
        StateUpdate();

        if (m_effect != null)
        {
            m_effect.OnFixedUpdate();
        }
    }
    private void StateUpdate()
    {
        switch(m_state)
        {
            case State.None:
                {

                }
                break;
            case State.Init:
                {

                }
                break;
            case State.Fire:
                {
                    OnExcuteEffect();
                    SetState(State.End);
                }
                break;
            case State.End:
                {

                }
                break;
        }
    }
    public virtual void OnExcuteEffect(){}
}
