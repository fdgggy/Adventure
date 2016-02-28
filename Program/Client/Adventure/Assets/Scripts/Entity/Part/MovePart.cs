/*******************************************************************
** 文件名:	MovePart.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   16:51:28
** 版  本:	1.0
** 描  述:	移动部件
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePart : EntityPart
{
    enum State
    {
        None,
        Idle,
        Moving,
        MovingOrgin,
        Max,
    }

    private State m_MoveState = State.None;
    private Vector3 m_targetPos = Vector3.zero;
    private Vector3 m_originPos = Vector3.zero;
    private float m_totalDis;
    float value = 0.0f;
    public override bool Init(Entity owner)
    {
        if (base.Init(owner) == false) 
        {
            Debug.LogError("MovePart::Init 失败,owner == null");
            return false;
        }

        SetState(State.Idle);
        m_originPos = m_owner.Position;
        value = m_owner.MoveSpeed;

        return true;
    }

    private void SetState(State state)
    {
        State nOldState = m_MoveState;

        OnExit(nOldState);

        m_MoveState = state;

        OnEnter(state);
    }
    private void OnEnter(State nState)
    {
        switch (nState)
        {
            case State.Idle:
                {
                    m_owner.EntityView.PlayAnimation("S");
                }
                break;
            case State.Moving:
                {
                    m_totalDis = Vector3.Distance(m_owner.Position, m_targetPos);

                    string aniName = string.Empty;
                    if (m_owner.EnType == EntityType.Monster)
                    {
                        aniName = "W";
                    }
                    else if (m_owner.EnType == EntityType.Hero)
                    {
                        aniName = "E";
                    }
                    m_owner.EntityView.PlayAnimation(aniName);
                }
                break;
            case State.MovingOrgin:
                {
                    m_targetPos = m_originPos;
                    m_totalDis = Vector3.Distance(m_owner.Position, m_targetPos);

                    string aniName = string.Empty;
                    if (m_owner.EnType == EntityType.Monster)
                    {
                        aniName = "E";
                    }
                    else if (m_owner.EnType == EntityType.Hero)
                    {
                        aniName = "W";
                    }
                    m_owner.EntityView.PlayAnimation(aniName);
                }
                break;
        }
    }

    private void OnExit(State nState)
    {
        switch (nState)
        {
            case State.Idle:
                {

                }
                break;
            case State.Moving:
                {
                }
                break;
            case State.MovingOrgin:
                {

                }
                break;
        }
    }
    public bool MoveTo(Vector3 target)
    {
        m_targetPos = target;
        
        SetState(State.Moving);
        
        return true;
    }
    public override void OnFixedUpdate()
    {
        switch (m_MoveState)
        {
            case State.Idle:
                break;
            case State.Moving:
                {
                    if (MoveToTarget() == true)
                    {
                        m_owner.UseSkill();

                        SetState(State.MovingOrgin);
                    }
                }
                break;
            case State.MovingOrgin:
                {
                    if (MoveToTarget() == true)
                    {
                        SetState(State.Idle);

                        if (m_owner.EnType == EntityType.Monster)
                        {
                            GameMgr.Instance.m_ectypeMgr.m_Ectype.SetTurnStatus(Turn.Attacker);
                        }
                        else if (m_owner.EnType == EntityType.Hero)
                        {
                            GameMgr.Instance.m_ectypeMgr.m_Ectype.SetTurnStatus(Turn.Defender);
                        }
                    }
                }
                break;
        }
    }
    float aSpeed = 1.0f;
    public bool MoveToTarget()
    {
        //float s = value * Time.deltaTime + 0.5f * aSpeed * Time.deltaTime * Time.deltaTime;
        //m_totalDis -= s;
        //if (m_totalDis < 0.1f)
        //{
        //    value = 0.0f;
        //    return true;
        //}
        //value += Time.deltaTime * value;
        //return false;
        float movedist = Time.fixedDeltaTime * m_owner.MoveSpeed;
        float distToTarget = Vector3.Distance(m_owner.Position, m_targetPos);
        if (movedist > distToTarget)
        {
            m_owner.Position = m_targetPos;
        }
        else
        {
            Vector3 templePos = m_owner.Position + (movedist * (m_targetPos - m_owner.Position));
            m_owner.Position = templePos;
        }

        distToTarget = Vector3.Distance(m_owner.Position, m_targetPos);
        if (distToTarget <= 0.01f)
        {
            m_owner.Position = m_targetPos;
            return true;
        }

        return false;
    }
}
