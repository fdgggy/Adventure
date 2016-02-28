/*******************************************************************
** 文件名:	Ectype.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.17   22:56:28
** 版  本:	1.0
** 描  述:	副本管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class Ectype
{
    public EctypeStatus mStatus { get; set; }
    private EctypeCreatContext m_EctypeCreateCtx = null;
    public EctypeCreatContext mCreateContext { get { return m_EctypeCreateCtx; } set { m_EctypeCreateCtx = value; } }
    // 副本攻击者team
    private EctypeTeam m_attackerTeam = null;
    // 副本防守者team
    private EctypeTeam m_defenderTeam = null;

    private EctypeLoader m_ectypeLoder = null;

    private Turn m_turn;
    private float m_timeTag;
    private float count = 1f;

    public Ectype()
    {
        mStatus = EctypeStatus.Idle;
        m_ectypeLoder = new EctypeLoader(this);
        m_turn = Turn.Invalid;
    }
    public bool Create(EctypeCreatContext createContext)
    {
        mCreateContext = createContext;
        m_attackerTeam = new EctypeTeamAttacker(this);
        m_defenderTeam = new EctypeTeamDefender(this);

        for ( int i = 0; i < mCreateContext.attackerTeam.entityList.Count; i++ )
        {
            EntityCreateCtx ctx = mCreateContext.attackerTeam.entityList[i];
            EctypeEntity attacker = new EctypeEntity();
            attacker.entity = GameMgr.Instance.m_entityMgr.Build(ctx);
            
            if (attacker.entity == null)
            {
                Debug.Log("Ectype::Create Attacker failed !");
                continue;
            }
            attacker.Status = EntityEctypeStatus.Idle;
            attacker.Camp = EntityCamp.Attacker;

            m_attackerTeam.m_entityList.Add(attacker);
            GameMgr.Instance.m_uiMgr.UIFight.CreateAttacker(attacker.entity);

        }

        for ( int i = 0; i < mCreateContext.defenderTeam.entityList.Count; i++ )
        {
            EntityCreateCtx ctx = mCreateContext.defenderTeam.entityList[i];
            EctypeEntity defender = new EctypeEntity();
            defender.entity = GameMgr.Instance.m_entityMgr.Build(ctx);

            if (defender.entity == null)
            {
                Debug.Log("Ectype::Create Defender failed !");
                continue;
            }
            defender.Status = EntityEctypeStatus.Idle;
            defender.Camp = EntityCamp.Defender;

            m_defenderTeam.m_entityList.Add(defender);
            GameMgr.Instance.m_uiMgr.UIFight.CreateDeffender(defender.entity);
        }

        return true;
    }
    public void Start()
    {
        m_timeTag = Time.time + count;
        //m_turn = Turn.Defender;
        SetTurnStatus(Turn.Defender);
    }
    public void Close()
    {

    }
    public void OnFixUpdate()
    {
        //if (Time.time > m_timeTag)
        //{
        //    if (m_turn == Turn.Defender)
        //    {
        //        m_attackerTeam.SkillTurn = false;
        //        m_defenderTeam.SkillTurn = true;
        //        m_turn = Turn.Attacker;
        //    }
        //    else if (m_turn == Turn.Attacker)
        //    {
        //        m_attackerTeam.SkillTurn = true;
        //        m_defenderTeam.SkillTurn = false;
        //        m_turn = Turn.Defender;
        //    }
        //    m_timeTag = Time.time + count;
        //}

        if (m_attackerTeam != null)
        {
            m_attackerTeam.OnUpdateEntity();
        }

        if (m_defenderTeam != null)
        {
            m_defenderTeam.OnUpdateEntity();
        }

        if (m_ectypeLoder != null)
        {
            m_ectypeLoder.OnFixedUpdate();
        }
        DoTask();
        DoTurnTask();
    }
    private void DoTurnTask()
    {
        switch (m_turn)
        {
            case Turn.Invalid:
                break;
            case Turn.Attacker:
                break;
            case Turn.Defender:
                break;
            default:
                break;
        }
    }
    private void DoTask()
    {
        switch (mStatus)
        {
            case EctypeStatus.Idle:
                break;
            case EctypeStatus.SynBegin:
                break;
            case EctypeStatus.WaitSynBegin:
                break;
            case EctypeStatus.Load:
                {
                    SetStatus(EctypeStatus.Loading);
                }
                break;
            case EctypeStatus.Loading:
                break;
            case EctypeStatus.Init:
                break;
            case EctypeStatus.Fighting:
                break;
            case EctypeStatus.SynEnd:
                break;
            case EctypeStatus.WaitSynEnd:
                break;
            case EctypeStatus.Award:
                break;
            case EctypeStatus.EctypeEnd:
                break;

            default:
                break;
        }
    }
    public void SetTurnStatus(Turn turn)
    {
        if (m_turn == turn)
            return;

        Turn oldTurn = m_turn;
        Turn newTurn = turn;

        OnExitTurn(oldTurn, newTurn);

        m_turn = turn;

        OnEnterTurn(oldTurn, newTurn);
    }
    private void OnExitTurn(Turn oldturn, Turn newturn)
    {
        switch (oldturn)
        {
            case Turn.Invalid:
                break;
            case Turn.Attacker:
                break;
            case Turn.Defender:
                break;
            default:
                break;
        }
    }
    private void OnEnterTurn(Turn oldturn, Turn newturn)
    {
        switch (newturn)
        {
            case Turn.Invalid:
                break;
            case Turn.Attacker:
                {
                    m_attackerTeam.DoAttack();
                }
                break;
            case Turn.Defender:
                {
                    m_defenderTeam.DoAttack();
                }
                break;
            default:
                break;
        }
    }
    private void SetStatus(EctypeStatus status)
    {
        if (mStatus == status)
            return;

        EctypeStatus oldStatus = mStatus;
        EctypeStatus newStatus = status;

        OnExit(oldStatus, newStatus);

        mStatus = status;

        OnEnter(oldStatus, newStatus);
    }

    private void OnEnter(EctypeStatus oldStatus, EctypeStatus newStatus)
    {
        switch (newStatus)
        {
            case EctypeStatus.SynBegin:
                break;
            case EctypeStatus.WaitSynBegin:
                break;
            case EctypeStatus.Load:
                {
                    GameMgr.Instance.m_clientMgr.EnterEctype(null);
                }
                break;
            case EctypeStatus.Loading:
                break;
            case EctypeStatus.Init:
                break;
            case EctypeStatus.SynEnd:
                break;
            case EctypeStatus.WaitSynEnd:
                break;
            case EctypeStatus.Award:
                break;
            case EctypeStatus.EctypeEnd:
                break;

            default:
                break;
        }

        return;
    }

    private void OnExit(EctypeStatus oldStatus, EctypeStatus newStatus)
    {
        switch (oldStatus)
        {
            case EctypeStatus.SynBegin:
                break;
            case EctypeStatus.WaitSynBegin:
                break;
            case EctypeStatus.Init:
                break;
            case EctypeStatus.SynEnd:
                break;
            case EctypeStatus.WaitSynEnd:
                break;
            case EctypeStatus.Award:
                break;
            case EctypeStatus.EctypeEnd:
                break;

            default:
                break;
        }

        return;
    }

    public EctypeEntity GetNearestTarget(Entity entity)
    {
        if (entity.Camp == EntityCamp.Attacker)
        {
            return m_defenderTeam.GetNearestTarget(entity);
        }

        return m_attackerTeam.GetNearestTarget(entity);
    }

}



