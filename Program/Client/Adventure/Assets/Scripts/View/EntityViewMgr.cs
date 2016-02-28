/*******************************************************************
** 文件名:	EntityViewMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.19   21:19:28
** 版  本:	1.0
** 描  述:	实体表现管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EntityViewMgr
{
    private List<EntityView> m_EntityViewList = new List<EntityView>();
    // 唯一id计数器
    private static uint uFlowUID;
    public EntityViewMgr()
    {
        uFlowUID = 0;
    }
    private static uint GetUID()
    {
        // TODO:uid的生成需要保证唯一，可采用playerid + entityType + flowid的方式生成，或者由服务器生成
        uFlowUID++;

        return uFlowUID;
    }
    public EntityView CreateEntityView(EntityViewCreateCtx ctx)
    {
        EntityView view;
        switch (ctx.Type)
        {
            case EntityViewType.Hero:
                view = null;
                break;
            case EntityViewType.Monster:
                view = new MonsterView();
                break;
            default:
                view = null;
                break;
        }

        if (view == null || !view.Init(ctx))
        {
            return null;
        }

        view.UID = GetUID();
        m_EntityViewList.Add(view);

        return view;
    }
    //获取实体表现
    public EntityView GetEntityView(int Id)
    {
        for (int i = 0; i < m_EntityViewList.Count; i++)
        {
            if (m_EntityViewList[i].UID == Id)
            {
                return m_EntityViewList[i];
            }
        }
        return null;
    }

    //删除实体表现
    public bool DelEntityView(uint Id)
    {
        for (int i = 0; i < m_EntityViewList.Count; i++)
        {
            if (m_EntityViewList[i].UID == Id)
            {
                m_EntityViewList[i].Destroy();
                m_EntityViewList.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    //清空所有实体表现
    public void Clear()
    {
        for (int i = 0; i < m_EntityViewList.Count; )
        {
            m_EntityViewList[i].Destroy();
        }
        m_EntityViewList.Clear();
    }
    public void OnUpdate()
    {
        for (int i = 0; i < m_EntityViewList.Count; i++)
        {
            if (m_EntityViewList[i] != null)
            {
                m_EntityViewList[i].OnUpdate();
            }
        }
    }
}
