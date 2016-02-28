/*******************************************************************
** 文件名:	EffectSDamage.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.2.1   16:28:28
** 版  本:	1.0
** 描  述:	普通伤害
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class EffectSDamage : EffectBase
{
    private float m_damageValue;
    public override bool Create(EffectCreateContext ctx)
    {
       if (base.Create(ctx) == false)
       {
           return false;
       }

       m_damageValue = ctx.EffectValue;
       
       return true;
    }

    public override void Start()
    {
        base.Start();
        DamageCreateContext dct = new DamageCreateContext();
        dct.SrcUID = m_srcID;
        dct.DamageValue = m_damageValue;
        dct.HitShowID = m_hitShowID;          
        
        if (m_owner != null)
        {
            if (m_owner.IsDead() == false)
            {
                m_owner.OnDamage(dct);
            }
        }
    }
}
