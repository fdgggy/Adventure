/*******************************************************************
** 文件名:	Monster.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.22   14:18:28
** 版  本:	1.0
** 描  述:	逻辑怪物
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Monster : Entity
{
    public override Vector3 Position
    {
        get
        {
            return m_pos;
        }
        set
        {
            m_pos = value;
            if (m_entityView != null)
            {
                m_entityView.Pos = value;
            }
        }
    }
    public override Vector3 Direction
    {
        get
        {
            return m_dir;
        }
        set
        {
            m_dir = value;
            m_dir.Normalize();
            Quaternion Rot = Quaternion.identity;
            Rot.SetLookRotation(m_dir);
            if (m_entityView != null)
            {
                m_entityView.Rot = Rot;
            }
        }
    }
    private MonsterCsvData m_csvData;
	public Monster()
    {
        m_fightProp = new FightProp(this);
    }
    public override bool Create(EntityCreateCtx ctx)
    {
        if (base.Create(ctx) == false)
        {
            Debug.LogError("Monster::Create 创建Monster失败 !!!");
            return false;
        }
        
        m_csvData = GameMgr.Instance.m_csvMgr.mMonsterCsv.Lookup(ctx.id);
        
        MoveSpeed = m_csvData.moveSpeed;
        SkillID = m_csvData.skillID;
        EnType = EntityType.Monster;

        EntityViewCreateCtx ctxView = new EntityViewCreateCtx();
        ctxView.Owner = this;
        ctxView.Type = EntityViewType.Monster;
        ctxView.Name = "Monster_" + UID.ToString();
        ctxView.ResName = m_csvData.resourceID;

        m_entityView = GameMgr.Instance.m_entityViewMgr.CreateEntityView(ctxView);
        if (m_entityView == null)
        {
            Debug.LogError("Monster::Create Monster failed !!!");
            return false;
        }
        m_entityView.SetParent(null);
        Position = ctx.pos;
        Direction = ctx.dir;

        m_Part[(int)EntityPartType.MovePart] = new MovePart();
        if (m_Part[(int)EntityPartType.MovePart].Init(this) == false)
        {
            Debug.LogError("Monster::Create 移动部件初始化失败 !!!");
            return false;
        }

        m_Part[(int)EntityPartType.SkillPart] = new SkillPart();
        if (m_Part[(int)EntityPartType.SkillPart].Init(this) == false)
        {
            Debug.LogError("Monster::Create 技能部件初始化失败 !!!");
            return false;
        }

        if (!m_fightProp.Init())
        {
            Debug.LogError("Monster::Create初始化战斗属性失败 !!!");
            return false;
        }
        return true;
    }
    public override void OnDamage(DamageCreateContext damage)
    {
        base.OnDamage(damage);

        SetProp(PropID.HP, -damage.DamageValue);
        if (IsDead())
        {
            DeadContext deadCtx = new DeadContext();
            deadCtx.SrcEntityID = damage.SrcUID;
            OnDead(deadCtx);
            return;
        }

        m_entityView.PlayEfficacyView(damage.HitShowID);
        
        float fcurhp = GetProp(PropID.HP);
        float fmaxhp = GetProp(PropID.MaxHP);
        if (BloodChange != null)
        {
            BloodChange(fcurhp,fmaxhp);
        }
    }
    public override void OnDead(DeadContext ctx)
    {
        base.OnDead(ctx);
        Debug.Log("xxxxxxxxxxxxx Monster Dead !!!");
    }
    public override void Destroy()
    {
        base.Destroy();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        for (int i = 0; i < (int)EntityPartType.Max; i++)
        {
            if (m_Part[i] != null)
                m_Part[i].OnFixedUpdate();
        }

    }
}
