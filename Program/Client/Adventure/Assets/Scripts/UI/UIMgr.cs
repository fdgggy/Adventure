﻿/*******************************************************************
** 文件名:	UIMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.19   21:19:28
** 版  本:	1.0
** 描  述:	UI管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class UIMgr
{
    private Transform m_uiRoot;
    private UILogin m_uiLogin;
    public UILogin UILogIn
    {
        get 
        {
            if (m_uiLogin == null)
            {
                m_uiLogin = LoadUI<UILogin>();
            }

            return m_uiLogin;
        }
    }
    private UIFight m_uiFight;
    public UIFight UIFight
    {
        get
        {
            if (m_uiFight == null)
            {
                m_uiFight = LoadUI<UIFight>();
            }

            return m_uiFight;
        }
    }
    public UIMgr()
    {
        InitUI();
    }
    private void InitUI()
    {
        m_uiRoot = GameMgr.Instance.transform.Find("UI");
        Transform canvas = m_uiRoot.Find("Canvas");


        GameObject goLogin = GameMgr.Instance.m_resMgr.LoadResource("Login");
        goLogin.name = "UILogin";
        goLogin.transform.SetParent(canvas, false);

        GameObject goFight = GameMgr.Instance.m_resMgr.LoadResource("Fight");
        goFight.name = "UIFight";
        goFight.transform.SetParent(canvas, false);

        for ( int i = 0; i < canvas.childCount; i++ )
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
    }
    T LoadUI<T>() where T : UIBase
    {
        string prefabName = typeof(T).Name;

        Transform node = m_uiRoot.FindChild("Canvas/" + prefabName);
        if (node == null)
        {
            Debug.LogError("load UI " + prefabName + " faild!");
            return null;
        }

        T o = node.GetComponent<T>();
        
        if (o == null)
        {
            o = node.gameObject.AddComponent<T>();
        }

        return o;
    }
}