/*******************************************************************
** 文件名:	SceneMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2016.01.03   22:24:28
** 版  本:	1.0
** 描  述:	场景管理  (写于武汉-深圳 T95火车上)
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class SceneMgr
{
    SceneBase m_currentScene;
    public SceneBase CurrentScene
    {
        get { return m_currentScene; }
        set { m_currentScene = value; }
    }

    private EctypeScene m_ectypeScene;
    public EctypeScene EctypeScene
    {
        get 
        { 
            if (m_ectypeScene == null)
                m_ectypeScene = new EctypeScene(); 

            return m_ectypeScene; 
        }
    }
    public void EnterScene(SceneBase scene)
    {
        if (CurrentScene != null)
        {
            CurrentScene.OnSceneWillUnload();
            CurrentScene.OnSceneUnloaded();
        }

        CurrentScene = scene;
        CurrentScene.OnSceneWillLoad();
        CurrentScene.OnSceneLoaded();
    }

    public void OnUpdate()
    {
        if (CurrentScene != null)
        {
            CurrentScene.Update();
        }
    }
}












