/*******************************************************************
** 文件名:	SkillSDamage.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.11.30   22:03:28
** 版  本:	1.0
** 描  述:	普通技能伤害
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class SkillSDamage : Skill
{
    public SkillSDamage()
    {

    }
    public override bool Create(SkillCreateContext ctx)
    {
        if (base.Create(ctx) == false)
        {
            return false;
        }

        return true;
    }
    public override void OnExcuteEffect()
    {
        Entity target = GameMgr.Instance.m_entityMgr.Get(m_targeUID);
        if (target == null || target.IsDead())
        {
            return;
        }

        EffectCreateContext ctx = new EffectCreateContext();
        ctx.EffectType = SkillEffectType.Damage;
        ctx.SrcUID = m_owner.UID;
        ctx.TargetUID = m_targeUID;
        ctx.ShowID = (uint)m_skillData.fireEffID;
        ctx.HitShowID = (uint)m_skillData.hitEffID;
        ctx.EffectValue = m_skillData.Damage;

        CreateSkillEffect(ctx);
    }
}
