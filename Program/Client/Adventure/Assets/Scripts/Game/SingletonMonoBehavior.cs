/*******************************************************************
** 文件名:	SingletonMonoBehavior.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.17   21:28:28
** 版  本:	1.0
** 描  述:	单例MonoBehavior模版类
** 应  用:  子类可以用Mono的协程

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;

public class SingletonMonoBehavior<T> : MonoBehaviour where T :class
{
    private static T m_instance;
	protected virtual void Awake()
    {
        m_instance = this as T;
    }
    protected virtual void OnDestroy()
    {
        m_instance = null;
    }
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = Object.FindObjectOfType(typeof(T)) as T;       //返回Type类型第一个激活的加载的物体。
            }

            return m_instance;
        }
    }
}
