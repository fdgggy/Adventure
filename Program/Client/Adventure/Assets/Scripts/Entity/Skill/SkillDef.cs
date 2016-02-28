/*******************************************************************
** 文件名:	SkillDef.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   13:19:28
** 版  本:	1.0
** 描  述:	技能
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class DamageCreateContext
{
    public uint SrcUID;
    public uint HitShowID;
    public float DamageValue;
}
public struct DeadContext
{
    public uint SrcEntityID;		//攻击者ID
    public uint DeadShowID;
}
public enum SkillEffectType
{
    Invalid = 0,
    Damage,         //瞬间伤害一次
    Cure,           //瞬间治疗一次

    Max,
}
public enum UseSkillResult
{
    None = -1,
    OK = 0,               //使用技能成功
    ColdTime,             //正在冷却中
    NoEntity,
}
public enum SkillType
{
    None,
    SDamage,

    Max,
}
