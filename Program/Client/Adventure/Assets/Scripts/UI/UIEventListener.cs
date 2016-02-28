using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIEventListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public object parameter;

    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;

    static public UIEventListener Get(GameObject go)
    {
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null) listener = go.AddComponent<UIEventListener>();
        return listener;
    }

    /// <summary>
    /// 给事件侦听器设置一个参数
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public UIEventListener SetParameter(object val)
    {
        parameter = val;
        return this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (onEnter != null) onEnter(gameObject);
    //}
    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (onExit != null) onExit(gameObject);
    //}
    //public void OnSelect(BaseEventData eventData)
    //{
    //    if (onSelect != null) onSelect(gameObject);
    //}
    //public void OnUpdateSelected(BaseEventData eventData)
    //{
    //    if (onUpdateSelect != null) onUpdateSelect(gameObject);
    //}
}