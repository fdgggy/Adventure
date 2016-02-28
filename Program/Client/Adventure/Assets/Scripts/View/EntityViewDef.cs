/*******************************************************************
** 文件名:	EntityMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   16:21:28
** 版  本:	1.0
** 描  述:	实体表现定义
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/

using UnityEngine;
public enum EntityViewType
{
    None,
    Hero,
    Monster,

    Max,
}

public struct EntityViewCreateCtx
{
    public Entity Owner;
    public EntityViewType Type;
    public string Name;
    public string ResName;
}