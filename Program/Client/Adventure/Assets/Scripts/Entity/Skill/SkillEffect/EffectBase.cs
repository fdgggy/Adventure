/*******************************************************************
** 文件名:	EffectBase.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   15:17:28
** 版  本:	1.0
** 描  述:	技能效果基类
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public struct EffectCreateContext
{
    public SkillEffectType EffectType;    //效果类型
    public uint SrcUID;                 //施法者UID
    public uint TargetUID;              //受击者UID
    public uint ShowID;	                //表现效果ID
    public uint HitShowID;              //受击表现效果ID
    public float EffectValue;           //效果作用值
}
public class EffectBase
{
    protected Entity m_owner;
    protected SkillEffectType m_effectType;
    protected uint m_srcID;
    protected uint m_targetID;
    protected uint m_showID;
    protected uint m_hitShowID;
    public bool EndFlag { get; set; }
    public virtual bool Create(EffectCreateContext ctx)
    {
        m_owner = GameMgr.Instance.m_entityMgr.Get(ctx.TargetUID);
        if (m_owner == null)
        {
            return false;
        }

        m_effectType = ctx.EffectType;
        m_srcID = ctx.SrcUID;
        m_targetID = ctx.TargetUID;
        m_showID = ctx.ShowID;
        m_hitShowID = ctx.HitShowID;

        return true;
    }
    public virtual void Start()
    {
        if (m_showID > 0)
        {
            Entity entity = GameMgr.Instance.m_entityMgr.Get(m_srcID);
            if (entity != null)
            {
                entity.EntityView.PlayEfficacyView(m_showID);
            }
        }
    }
    public void OnFixedUpdate()
    {
        if (EndFlag)
            return;

    }
    public virtual void Excute()
    {
        EndFlag = false;
    }
    public virtual void End()
    {
        EndFlag = true;
    }
}
