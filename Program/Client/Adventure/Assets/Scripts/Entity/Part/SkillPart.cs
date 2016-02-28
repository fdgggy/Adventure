/*******************************************************************
** 文件名:	SkillPart.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   13:19:28
** 版  本:	1.0
** 描  述:	技能部件
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillPart : EntityPart
{
    private List<Skill> m_skill = new List<Skill>();
    private SkillCsvData m_skillData;

    public override bool Init(Entity owner) 
    { 
        if (base.Init(owner) == false)
        {
            return false;
        }

        m_skillData = GameMgr.Instance.m_csvMgr.mSkillCsv.Lookup(m_owner.SkillID);
        return true;
    }
    public override void OnFixedUpdate() 
    {
        for ( int i = m_skill.Count - 1; i >= 0; i-- )
        {
            m_skill[i].OnFixedUpdate();

            if (m_skill[i].EndFlag)
            {
                m_skill.RemoveAt(i);
            }
        }
    }
    public UseSkillResult UseSkill()
    {
        EctypeEntity ectypeEntity = GameMgr.Instance.m_ectypeMgr.m_Ectype.GetNearestTarget(m_owner);
        SkillCreateContext ctx = new SkillCreateContext();
        ctx.Owner = m_owner;
        ctx.SkillData = m_skillData;
        ctx.TargetUID = ectypeEntity.entity.UID;

        Skill skill = CreateSkill(ctx);
        if (skill == null)
        {
            return UseSkillResult.None;
        }

        AddSkill(skill);

        return UseSkillResult.OK;
    }
    public void AddSkill(Skill skill)
    {
        m_skill.Add(skill);
    }
    private Skill CreateSkill(SkillCreateContext ctx)
    {
        Skill skill;
        SkillType skillType = (SkillType)ctx.SkillData.Type;
        switch (skillType)
        {
            case SkillType.SDamage:
                skill = new SkillSDamage();
                break;
            default:
                {
                    Debug.LogError("SkillPart::CreateSkill Skill Type Not Implement!!!");
                    return null;
                }
                break;
        }

        if (skill.Create(ctx) == false)
        {
            Debug.LogError("SkillPart::CreateSkill Skill Failed !!!");
            return null;
        }

        return skill;
    }
}
