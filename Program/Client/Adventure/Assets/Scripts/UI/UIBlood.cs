/*******************************************************************
** 文件名:	UIBlood.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.2.2   11:31:28
** 版  本:	1.0
** 描  述:	UI血条
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIBlood : UIBase
{
    private Entity m_owner;
    public Entity Owner 
    {
        get { return m_owner; }
        set
        {
            m_owner = value;
            m_owner.BloodChange = SetHp;
        }
    }
    private Slider m_slider;
    private Text m_bloodValue;
    protected override void Init()
    {
        m_slider = UIBase.Get<Slider>(this.transform);
        m_slider.maxValue = 1.0f;
        
        Transform go = this.transform.Find("Value");
        m_bloodValue = UIBase.Get<Text>(go);
    }
    private void SetHp(float cur, float max)
    {
        m_slider.value = cur / max;
        SetCurHpValue(cur);
    }
    public void SetCurHpValue(float cur)
    {
        m_bloodValue.text = string.Format("{0}", cur);
    }
}
