/*******************************************************************
** 文件名:	UIBase.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.16   16:57:28
** 版  本:	1.0
** 描  述:	UI基类
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class UIBase : MonoBehaviour
{
    private List<UIBase> m_hideUI = new List<UIBase>();


    void Awake()
    {
        Init();
    }

    virtual protected void Init()
    {

    }

    virtual public void Show()
    {
        if (m_hideUI.Count == 0)
        {
            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
            }
        }
    }

    virtual public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public bool Visible
    {
        get
        {
            return this.gameObject.activeSelf;
        }
    }
    /// <summary>
    /// 淡入
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration = 0.5f)
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        if (group == null)
        {
            group = this.gameObject.AddComponent<CanvasGroup>();
        }

        if (Visible == false)
        {
            group.alpha = 0.0f;
        }

        Show();
        group.DOKill();
        group.DOFade(1.0f, duration);
    }
    public void Fadein(GameObject go, TweenCallback fun = null, float duration = 0.2f)
    {
        CanvasGroup group = Get<CanvasGroup>(go);
        group.alpha = 0.0f;
        go.SetActive(true);
        group.DOKill();
        group.DOFade(1.0f, duration).OnComplete(fun);
    }
    public void Fadeout(GameObject go, TweenCallback fun = null, float duration = 0.2f)
    {
        CanvasGroup group = Get<CanvasGroup>(go);
        group.alpha = 1.0f;
        go.SetActive(true);
        group.DOKill();
        group.DOFade(0.0f, duration).OnComplete(fun);
    }
    GameObject fadeObj;
    public void FadePingPong(GameObject go)
    {
        fadeObj = go;
        PingPong();
    }
    private void PingPong()
    {
        CanvasGroup group = Get<CanvasGroup>(fadeObj);
        float alpha = group.alpha;

        if (alpha == 0)
        {
            Fadein(fadeObj, PingPong, 2.0f);
        }
        else if (alpha == 1)
        {
            Fadeout(fadeObj, PingPong, 2.0f);
        }
    }
    /// <summary>
    /// 找到子节点的组件
    /// </summary>
    /// <param name="xpath"></param>
    /// <returns></returns>
    virtual public T FindChildComponent<T>(string xpath) where T : Component
    {
        Transform child = transform.Find(xpath);
        if (child != null)
        {
            return child.GetComponent<T>();
        }
        return null;
    }
    /// <summary>
    /// 获取组件，如果没有则创建
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    static public T Get<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null) t = go.AddComponent<T>();
        return t;
    }

    static public T Get<T>(Transform trans) where T : Component
    {
        return Get<T>(trans.gameObject);
    }
    /// <summary>
    /// 获取子节点的事件侦听器对象
    /// </summary>
    /// <param name="xpath"></param>
    /// <returns></returns>
    virtual public UIEventListener UIEvent(string xpath)
    {
        Transform child = transform.Find(xpath);
        if (child != null)
        {
            return UIEvent(child.gameObject);
        }
        return null;
    }

    /// <summary>
    /// 获取子节点的事件侦听器对象
    /// </summary>
    /// <param name="xpath"></param>
    /// <returns></returns>
    virtual public UIEventListener UIEvent(Transform child)
    {
        if (child != null)
        {
            return UIEvent(child.gameObject);
        }
        return null;
    }

    /// <summary>
    /// 获取子节点的事件侦听器对象
    /// </summary>
    /// <param name="xpath"></param>
    /// <returns></returns>
    virtual public UIEventListener UIEvent(GameObject go)
    {
        if (go != null)
        {
            return UIEventListener.Get(go);
        }
        return null;
    }
}
