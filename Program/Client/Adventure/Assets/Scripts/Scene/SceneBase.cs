/*******************************************************************
** 文件名:	SceneBase.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2016.01.03   22:06:28
** 版  本:	1.0
** 描  述:	场景基类  (写于武汉-深圳 T95火车上)
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;

public class SceneCreateCtx
{
    public string sceneId;
    public uint ectypeID;
}
public class SceneBase
{
    public string SceneID { set; get; }
    public uint EctypeId { set; get; }

    public SceneBase()
    {
    }

    virtual public void Enter(SceneCreateCtx ctx)
    {
        SceneID = ctx.sceneId;
        EctypeId = ctx.ectypeID;

        GameMgr.Instance.m_sceneMgr.EnterScene(this);
    }

    virtual public void Update()
    {

    }
    virtual public void OnSceneWillLoad()
    {
    }
    virtual public void OnSceneLoaded()
    {

    }
    virtual public void OnSceneWillUnload()
    { 
    }
    virtual public void OnSceneUnloaded()
    {

    }
}









