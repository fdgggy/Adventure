/*******************************************************************
** 文件名:	EntityMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.19   22:54:28
** 版  本:	1.0
** 描  述:	实体管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityMgr
{
    private Dictionary<uint, Entity> m_entityDic;
    // 唯一id计数器
    private static uint uFlowUID;
    public EntityMgr()
    {
        uFlowUID = 0;
        m_entityDic = new Dictionary<uint, Entity>();
    }
    private static uint GetUID()
    {
        // TODO:uid的生成需要保证唯一，可采用playerid + entityType + flowid的方式生成，或者由服务器生成
        uFlowUID++;

        return uFlowUID;
    }
    public Entity Build(EntityCreateCtx ctx)
    {
        if (ctx.type <= EntityType.None || ctx.type >= EntityType.Max)
        {
            return null;
        }

        Entity entity = null;
        if (ctx.type == EntityType.Hero)
        {
            entity = BuildMonster(ctx);
            entity.EnType = EntityType.Hero;
        }
        else if (ctx.type == EntityType.Monster)
        {
            entity = BuildMonster(ctx);
        }

        return entity;
    }
    private Entity BuildHerro(EntityCreateCtx ctx)
    {
        return new Entity();
    }
    private Entity BuildMonster(EntityCreateCtx ctx)
    {
        Monster monster = new Monster();
        monster.UID = GetUID();
        if (monster.Create(ctx) == false)
        {
            return null;
        }

        if (m_entityDic.ContainsKey(monster.UID))
        {
            m_entityDic[monster.UID] = monster;
        }
        else
        {
            m_entityDic.Add(monster.UID, monster);
        }

        return monster;
    }
    public Entity Get(uint id)
    {
        Entity entity;
        bool ret = m_entityDic.TryGetValue(id, out entity);
        if (ret)
            return entity;

        return null;
    }
    public void OnFixedUpdate()
    {
        foreach (KeyValuePair<uint, Entity>entity in m_entityDic)
        {
            // if not Dead
            entity.Value.OnFixedUpdate();
        }
    }
    public void DestroyAll()
    {
        foreach (KeyValuePair<uint, Entity> entity in m_entityDic)
        {
            entity.Value.Destroy();
        }
        m_entityDic.Clear();
    }
    // 根据ID删除实体
    public bool Remove(uint entityID)
    {
        Entity entity = Get(entityID);
        if (entity == null)
        {
            return false;
        }

        return Remove(entity);
    }

    // 删除实体
    public bool Remove(Entity entity)
    {
        if (entity == null)
        {
            return false;
        }

        uint uid = entity.UID;

        //销毁实体
        entity.Destroy();

        m_entityDic.Remove(uid);

        return true;
    }
}
