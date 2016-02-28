/*******************************************************************
** 文件名:	EctypeMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.17   22:56:28
** 版  本:	1.0
** 描  述:	副本管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class EctypeMgr
{
    public Ectype m_Ectype = null;
    private EctypeCreatContext m_EctypeCreateCtx;

    public void EnterEctype(EctypeEnterContext ctx)
    {
        SceneCreateCtx scenectx = new SceneCreateCtx();
        scenectx.sceneId = ctx.sceneID;
        scenectx.ectypeID = ctx.ectypeID;

        GameMgr.Instance.m_sceneMgr.EctypeScene.Enter(scenectx);
    }
    public bool CreateEctype(EctypeCreatContext data)
    {
        m_Ectype = new Ectype();

        if (m_Ectype == null)
        {
            return false;
        }

        if (!m_Ectype.Create(data))
        {
            return false;
        }

        // 启动副本
        m_Ectype.Start();

        return true;
    }
	
    public void OnFixedUpdate()
    {
        if (m_Ectype != null)
        {
            m_Ectype.OnFixUpdate();
        }
    }
    public void CloseEctype()
    {
        if (m_Ectype != null)
        {
            m_Ectype.Close();
            m_Ectype = null;     // 关闭是一定要置空
        }

        //Game.inst.entityMgr.RemoveAll();
    }

}
