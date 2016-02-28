/*******************************************************************
** 文件名:	EctypeEntity.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   22:23:28
** 版  本:	1.0
** 描  述:	副本实体
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class EctypeEntity
{
    public Entity entity;
    public EntityEctypeStatus Status { get; set; }
    public EntityCamp Camp { get; set; }

    public int[] counter = new int[(int)EntityEctypeStatus.Max];

    public void ChangeStatus(EntityEctypeStatus changeStatus)
    {
        if (changeStatus == Status)
        {
            return;
        }

        EntityEctypeStatus oldStatus = Status;
        EntityEctypeStatus newStatus = changeStatus;

        OnEnter(oldStatus, newStatus);

        Status = changeStatus;

        OnExit(oldStatus, newStatus);
    }
    private void OnEnter(EntityEctypeStatus oldStatus, EntityEctypeStatus newStatus)
    {
        switch (newStatus)
        {
            case EntityEctypeStatus.Combat:
                {
                }
                break;

            case EntityEctypeStatus.Idle:
                {
                }
                break;

            case EntityEctypeStatus.Alert:
                {

                }
                break;
            case EntityEctypeStatus.Chase:
                {
                }
                break;
            default:
                break;
        }

        return;
    }

    private void OnExit(EntityEctypeStatus oldStatus, EntityEctypeStatus newStatus)
    {

    }

}
