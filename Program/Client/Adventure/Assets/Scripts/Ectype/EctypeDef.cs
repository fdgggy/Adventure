/*******************************************************************
** 文件名:	EctypeDef.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   21:51:28
** 版  本:	1.0
** 描  述:	
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
// 副本状态
using System.Collections.Generic;
public enum EctypeStatus
{
    Idle = 0,
    SynBegin,       // 同步服务器-开始
    WaitSynBegin,   // 等待返回
    Load,           // 加载
    Loading,        // 加载中
    Init,           // 副本开始
    Fighting,		// 副本战斗中
    Guiding,		// 引导（测试用）
    AutoGetBox,		// 播放战斗结束动画
    SynEnd,         // 同步服务器-结束
    WaitSynEnd,     // 等待返回
    Award,		    // 奖励
    EctypeEnd,      // 副本结束
    Max,
}
public enum Turn
{
    Invalid,
    Defender,
    Attacker,

    Num,
}
public class TeamContext
{
    public List<EntityCreateCtx> entityList;

    public TeamContext()
    {
        entityList = new List<EntityCreateCtx>();
    }
}
public enum EntityCamp
{
    Neutral = 0,    // 中立
    Attacker = 1,   // 攻击方
    Defender = 2,   // 防守方
}
// 副本使用类型
public enum EctypeUseType
{
    None = 0,       // 空类型
    Normal = 1,     // 主线副本
    Exercise = 2,   // 试练副本
    MakeMoney = 3,  // 打钱副本
    Expedition = 4, // 远征副本
    Arena = 5,      // 竞技场
    Clan = 6,       // 公会副本

    Max,            // 最大类型
}
public class EctypeEnterContext
{
    public string sceneID;
    public uint ectypeID;
}
 public class EctypeCreatContext
 {
     public EctypeUseType type;         // 副本类型
     public uint dwEctypeID;            // 副本ID
     public bool bGuide;                // 是否是引导副本
     public TeamContext attackerTeam;   // 攻击方team
     public TeamContext defenderTeam;   // 防守方team列表
     public string sceneID;             // 场景ID

     public EctypeCreatContext()
     {
         defenderTeam = new TeamContext();
         attackerTeam = new TeamContext();
     }
 }
 public enum TeamStatus
 {
     Idle = 0,
     Create,         // 队伍创建
     Move,           // 队伍行军
     Combat,         // 队伍战斗
     Rest,           // 队伍休整
     Celebrate,      // 庆祝
     Max,
 }
 public enum EntityEctypeStatus
 {
     Idle = 0,
     Alert,		    // 警戒状态
     Chase,		    // 追击
     Combat,		// 战斗状态
     Max,
 }