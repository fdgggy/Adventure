/*******************************************************************
** 文件名:	EctypeTeamAttacker.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   22:28:28
** 版  本:	1.0
** 描  述:	副本攻击方
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class EctypeTeamAttacker : EctypeTeam
{
    public EctypeTeamAttacker(Ectype ectype)
        : base(ectype)
    {
    }
    public override void OnUpdateEntity()
    {
        foreach (EctypeEntity ectypeEntity in m_entityList)
        {

        }
    }
    public override void DoAttack()
    {
        //Debug.Log("Attacker");
        foreach (EctypeEntity ectypeEntity in m_entityList)
        {
            if (ectypeEntity.entity == null || ectypeEntity.entity.IsDead())
            {
                continue;
            }
            if (ectypeEntity.entity.EnType == EntityType.Hero)
            {
                ectypeEntity.entity.MoveEntity();
            }
        }
    }
}
