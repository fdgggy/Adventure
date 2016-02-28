/*******************************************************************
** 文件名:	EntityPart.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   13:19:28
** 版  本:	1.0
** 描  述:	实体部件
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;

public enum EntityPartType
{
    SkillPart,
    MovePart,
    Max,
}
public class EntityPart
{
    protected Entity m_owner;
    public virtual bool Init(Entity owner) 
    {
        if (owner == null)
            return false;

        m_owner = owner;
        return true; 
    }
    public virtual void Destroy() { }
    public virtual void OnFixedUpdate() { }
}
