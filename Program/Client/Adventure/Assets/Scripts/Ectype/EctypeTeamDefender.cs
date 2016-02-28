/*******************************************************************
** 文件名:	EctypeTeamDefender.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   22:28:28
** 版  本:	1.0
** 描  述:	副本防守方
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class EctypeTeamDefender : EctypeTeam
{
    public EctypeTeamDefender(Ectype ectype)
        : base(ectype)
    {
    }

    public override void OnUpdateEntity()
    {
       
    }
    private void OnUpdateEntityStatus(EctypeEntity ectypeEntity)
    {
        switch (ectypeEntity.Status)
        {
            case EntityEctypeStatus.Idle:
                break;
            case EntityEctypeStatus.Alert:
                break;
            case EntityEctypeStatus.Chase:
                break;
            case EntityEctypeStatus.Combat:
                {
                }
                break;
        }
    }
    public override void DoAttack()
    {
        //Debug.Log("Deffender");
        foreach (EctypeEntity ectypeEntity in m_entityList)
        {
            if (ectypeEntity.entity == null || ectypeEntity.entity.IsDead())
            {
                continue;
            }
            if (ectypeEntity.entity.EnType == EntityType.Monster)
            {
                ectypeEntity.entity.MoveEntity();
            }
        }
    }
}
