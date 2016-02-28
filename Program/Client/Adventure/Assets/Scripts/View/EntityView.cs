/*******************************************************************
** 文件名:	EntityView.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   16:17:28
** 版  本:	1.0
** 描  述:	实体表现基类
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class EntityView
{
    protected Entity m_owner;
    protected GameObject m_EntityObj = null;
    protected I2dSpriteAnimation m_Animation = null;
    protected EntityViewType m_Type = EntityViewType.None;
    public uint UID { get; set; }

    protected Vector3 m_vPos = Vector3.zero;
    protected Vector3 m_vSize = Vector3.one;
    protected Quaternion m_Rot = Quaternion.identity;
    // 位置
    public Vector3 Pos
    {
        set
        {
            m_vPos = value;
            if (m_EntityObj != null)
            {
                m_EntityObj.transform.localPosition = m_vPos;
            }
        }
        get { return m_vPos; }
    }

    // 旋转
    public Quaternion Rot
    {
        set
        {
            m_Rot = value;
            if (m_EntityObj != null)
            {
                m_EntityObj.transform.localRotation = m_Rot;
            }
        }
        get { return m_Rot; }
    }

    // 大小
    public Vector3 Size
    {
        set
        {
            m_vSize = value;
            if (null != m_EntityObj)
            {
                m_EntityObj.transform.localScale = m_vSize;
            }
        }
        get { return m_vSize; }
    }

    public virtual bool Init(EntityViewCreateCtx ctx)
    {
        if (ctx.Owner == null)
            return false;
        m_owner = ctx.Owner;
        m_EntityObj = new GameObject();
        m_EntityObj.name = ctx.Name;

        return true;
    }
    //将实体挂接到场景中
    public virtual void SetParent(GameObject parent)
    {
        if (m_EntityObj != null)
        {
            if (parent == null)
            {
                Transform p = GameMgr.Instance.transform.Find("Entitys");
                m_EntityObj.transform.parent = p;
            }
            else
            {
                m_EntityObj.transform.parent = parent.transform;
            }
            
            m_EntityObj.transform.localPosition = m_vPos;
        }
    }
    public void PlayEfficacyView(uint id)
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void PlayAnimation(string name)
    {
        m_Animation.Play(name);
    }
    public virtual void Destroy()
    {
        GameObject.Destroy(m_EntityObj);
    }
}
