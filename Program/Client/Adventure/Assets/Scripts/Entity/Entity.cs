/*******************************************************************
** 文件名:	Entity.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   13:19:28
** 版  本:	1.0
** 描  述:	实体
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class Entity
{
    public delegate void VoidBloodChange(float cur, float max);
    public VoidBloodChange BloodChange;
    protected Vector3 m_pos;
    protected Vector3 m_dir;
    protected SceneBase m_scene;
    protected EntityCamp m_camp;
    public EntityCamp Camp
    {
        get { return m_camp; }
    }
    protected uint m_level;
    protected FightProp m_fightProp;
    protected EntityView m_entityView;
    public EntityView EntityView { get { return m_entityView; } }
    public virtual Vector3 Position { get; set; }
    public virtual Vector3 Direction { get; set; } 
    public uint UID { get; set; }
    public int ID { get; set; }
    public string Name { get; set; }
    public float MoveSpeed { get; set; }
    public int SkillID { get; set; }
    public EntityType EnType { get; set; }

    protected EntityPart[] m_Part = new EntityPart[(int)EntityPartType.Max];
    protected EntityState m_state = EntityState.None;
    public EntityState State
    {
        get { return m_state; }
        set { m_state = value; }
    }
    public float MoveSpeedScale
    {
        get
        {
            if (EnType == EntityType.Monster)
                return m_fightProp.GetProp(PropID.MovingSpeedScale);

            return 1.0f;
        }
    }
    public Entity()
    {

    }
    public virtual bool Create(EntityCreateCtx ctx)
    {
        EnType = ctx.type;
        m_pos = ctx.pos;
        m_dir = ctx.dir;
        m_scene = ctx.scene;
        m_camp = ctx.camp;
        m_level = GameMgr.Instance.m_csvMgr.mMonsterCsv.LookUpLevel(ctx.id);
        ID = ctx.id;
        Name = GameMgr.Instance.m_csvMgr.mMonsterCsv.LookUpName(ID);

        return true;
    }
    public virtual void Destroy()
    {
        for (int i = 0; i < (int)EntityPartType.Max; i++)
        {
            if (m_Part[i] != null)
                m_Part[i].Destroy();
        }

        if (m_entityView != null)
        {
            GameMgr.Instance.m_entityViewMgr.DelEntityView(m_entityView.UID);
            m_entityView = null;
        }
    }
    public virtual void UseSkill()
    {
        SkillPart skillpart = m_Part[(int)EntityPartType.SkillPart] as SkillPart;
        if (skillpart != null)
        {
            skillpart.UseSkill();
        }
    }
    public void MoveEntity()
    {
        MovePart movePart = m_Part[(int)EntityPartType.MovePart] as MovePart;

        if (EnType == EntityType.Monster)
        {
            if (movePart != null)
            {
                movePart.MoveTo(Vector3.zero);
            }

        }
        else if (EnType == EntityType.Hero)
        {
            if (movePart != null)
            {
                movePart.MoveTo(Vector3.zero);
            }
        }
    }
    public virtual void OnDamage(DamageCreateContext damage) { }
    public virtual void OnDead(DeadContext ctx) { }
    public void SetProp(PropID nID, float value)
    {
        if (m_fightProp != null)
        {
            m_fightProp.SetProp(nID, value);
        }
    }
    public float GetProp(PropID nID)
    {
        if (m_fightProp == null)
        {
            Debug.LogWarning("实体战斗属性未初始化 !!!");
            return 0.0f;
        }

        return m_fightProp.GetProp(nID);
    }
    public virtual bool IsDead()
    {
        return false;
        //return m_fightProp.GetProp(PropID.HP) <= COMMON_DEF.MIN_FLOAT;
    }
    public virtual void OnFixedUpdate()
    {
    }
}


















