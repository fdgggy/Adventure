/*******************************************************************
** 文件名:	EntityDef.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   13:10:28
** 版  本:	1.0
** 描  述:	实体定义
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;

// 实体属性，当前属性和附加属性和基础属性的先后顺序必须保持一致
// 方便分段，偏移计算
public enum PropID
{
    Invalid = 0,        // 无效值
    MaxHP,              // 最大生命值
    MaxEnergy,          // 最大能量值
    HP,                 // 当前生命值
    Energy,             // 当前能量值
    Damage,				// 攻击力

    DamageScale,        // 攻击力比例 
    MovingSpeedScale,   // 移动速度比例
    SkillSpeedScale,    // 攻击速度比例

    Max,                    //属性最大值
};
public enum EntityType
{
    None = 0,
    Hero,
    Monster,
    
    Max,
}
public enum EntityState
{
    None = 0,
    Born,
    Alive,
    Dead,
    Hide,

    Max
}
public class EntityCreateCtx
{
    public int id;
    public EntityType type = EntityType.None;
    public Vector3 pos;
    public Vector3 dir;
    public SceneBase scene;
    public EntityCamp camp;
}
