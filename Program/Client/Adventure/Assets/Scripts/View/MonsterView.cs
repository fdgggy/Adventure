/*******************************************************************
** 文件名:	MonsterView.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.22   20:57:28
** 版  本:	1.0
** 描  述:	怪物表现
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class MonsterView : EntityView
{
    protected GameObject m_ModelObj = null;
    public MonsterView()
    {
        m_Type = EntityViewType.Monster;
    }
    public override bool Init(EntityViewCreateCtx ctx)
    {
        if (!base.Init(ctx))
            return false;
        m_ModelObj = GameMgr.Instance.m_resMgr.LoadResource(ctx.ResName);
        if (m_ModelObj == null)
        {
            Debug.LogError("MonsterView::Init 加载模型预制失败 resPath=" + ctx.ResName);
            return false;
        }
        m_ModelObj.name = ctx.Name;
        m_ModelObj.transform.SetParent(m_EntityObj.transform);

        InitAnimation();
        
        return true;
    }
    public void InitAnimation()
    {
        if (m_Animation == null)
        {
            m_Animation = new I2dSpriteAnimation();
            m_Animation.BindComp(m_ModelObj.transform);
        }
    }
    public override void Destroy()
    {
         if (m_ModelObj != null)
         {
             GameMgr.Instance.m_resMgr.Destroy(m_ModelObj);
         }
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
