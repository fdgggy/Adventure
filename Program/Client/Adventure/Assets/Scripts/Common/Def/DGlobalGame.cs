/*******************************************************************
** 文件名:	GameUtil.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.20   16:12:28
** 版  本:	1.0
** 描  述:	全局定义
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/

public static class COMMON_DEF
{
	//游戏测试版本号,修改此版本号之后，登陆会重新创建玩家信息
	public const string GAME_VERSION_STRING = "Ver 20141104.1000";

	//游戏测试版本号,修改此版本号之后，登陆会重新创建玩家信息
    public const int GAME_TEST_VERSION = 20141104;

	//public const string GAME_SERVER_IP = "202.104.113.230";	// 内网开发服
	//public const ushort GAME_SERVER_PORT = 10001;

	public const string GAME_SERVER_IP = "183.60.126.122";	// 外网亲友服
	public const ushort GAME_SERVER_PORT = 9000;
	
	//public const string GAME_SERVER_IP = "127.0.0.1";	// 外网亲友服
	//public const ushort GAME_SERVER_PORT = 9000;
	
	

	// 英雄间距
	public const float HERO_POS_DIS = 1.8f;

	//重力加速度 m/s2
	public const int GRAVITY = 50;

	//受击表现效果播放的最小伤害百分比 (大于该值，才播放受击动画)重击 播放受击表现ID
	public const float INTERRUPT_PERCENT_1 = 0.2f;


	//受击表现效果播放的最小伤害百分比 (大于该值，才播放受击动画) 轻击 播放受击动作
	public const float INTERRUPT_PERCENT_2 = 0.1f;

	//精英副本每天的最大次数
	public const int MAIN_SECTION_MAX_TIME = 3;

	//子弹表现效果ID队列最大数量
	public const int MAX_BULLET_SHOW_NUM = 5;

	//尸体停留时间
	public const float BODY_STAY_TIME = 2.0f;

	// 比较float型变量是否为零的最小误差
	public const float MIN_FLOAT = 0.0001f;

	// 比较double型变量是否为零的最小误差
	public const double MIN_DOUBLE = 0.0001f;

    //主场景ID
    public const int MAIN_SCENE_ID = 999;

    // 副本中的最大英雄数
    public const int HERO_ECTYPE_MAX_NUM = 5;      
             
    // 最小距离 - 暂留
    public const float MIN_DISTANCE = 0.03f;

    // 最大拐点数，超过这个数自动截断
    public const int MAX_PATH_CORNER = 16;

    // 每次命令索引递增数
    public const int COMMAND_INDEX_STEP = 50;

    // 命令执行时间差值
    public const int MAX_ENTITY_COMMAND_EXCUTE_TIME_RANGE = 600;

    // 玩家内存池最大值
    public const short MAX_PERSON_POOL_SIZE = 16;

    // 怪物内存池最大值
    public const short MAX_MONSTER_POOL_SIZE = 32;

    // 子弹内存池最大值
    public const short MAX_BULLET_POOL_SIZE = 64;

    // PI值
    public const float __PI = 3.1415f;
	
	// 信息文字最大长度,64
	public const byte GAME_MSGSTR_MAXSIZE = 64;

    // 进入副本最大英雄数
    public const byte MAX_HERO_COUNT = 5;

	//每个英雄装备最大数量
    public const int HERO_EQUIP_MAX_NUM = 6;

	//英雄最大技能数
	public const int HERO_SKILL_MAX_NUM = 6;
	
	// 最大星数
    public const int HERO_STAR_MAX_NUM = 5;
	
    // 英雄id最大值
    public const int HERO_ID_MAX = 0xFFFF;

    // 最大技能数
    public const byte MAX_SKILL_NUM = 4;

    // 技能最大效果数
    public const byte MAX_SKILL_IMPACT_NUM = 4;

    // key-value最大键值
    public const int MAX_KEY_VALUE_NUM = 50;

    // 副本中最大距离
    public const double MaxDistance = 9999999.0f;

    // 副本最多小节数
    public const byte ECTYPE_PASS_MAX = 3;
    
    // 每组刷怪点最多成员
    public const byte ECTYPE_TEAM_MEMBER_MAX = 5;

    // 刷新频率
    public const short UPDATE_COUNTER = 10;

    // 当前最大的抽宝箱个数
    public const int MAX_GIFT_NUM = 10;

    // 超时时间
    public const float ECTYPE_TIME_OUT = 100;

	// 地图网格距离
	public const float MAP_GRID_DIS = 1.0f;

    // 技能点更新间隔时间 120秒
    public const int SP_UPDATE_INTERNAL = 120;
    // 技能点上线
    public const int SP_LIMIT = 20;

    // C#日期时间Ticks与秒数换算关系
    public const long TICKS_TO_SECONDS = 10000000;
    
	// databox无效key
    public const int DATA_BOX_INVALID_KEY = 0;
    public const int DATA_BOX_INVALID_VALUE = 0;

   // 远征副本总数
   public const int EXPEDITION_MAX_NUM = 15;

    // 远征最大段位
    public const int EXPEDITION_MAX_DAN = 100;

    // 远征积分分段
    public const int EXPEDITION_POINT_NUM = 5;

    // 最大的英雄血量记录
    public const int MAX_HERO_HPMP_NUM = 104;

    // 英雄起始ID
    public const int HERO_BEGIN_ID = 1001;

    // 名称最大长度,32
    public const int DBDEF_GAME_NAME_MAXSIZE = 32;

    // 进入远征的最小英雄等级
    public const int INPUT_EXT_MIN_HEROLV = 1;
	
	// 进入商店的最小英雄等级
	public const int INPUT_SHOP_MIN_HEROLV = 5;

    // 一天中的秒数
    public const uint DAY_SEC = 86400;

    // 读取Player 部件网络消息BUFF长度
    public const int PLAYER_PART_BUFF_LEN = 32767;
	// 副本最高星级
	public const int ECTYPE_STAR_MAX = 3;

    // 竞技场目标玩家个数
    public const int ARENA_TARGET_NUM = 15;

    // 竞技场等待攻击结果的时间
    public const int ARENA_WAIT_FIGHT_TIME = 180;
    
    // 竞技场显示排行榜的玩家数据
    public const int ARENA_SHOW_ORDER_NUM = 50;

    // 竞技场副本ID
    public const int ARENA_ECTYPE_ID = 921;

    // 竞技场日志条数
    public const int ARENA_LOG_SAVE_NUM = 20;

    // 竞技场显示目标条数
    public const int ARENA_SHOW_TARGET_NUM = 3;
	
	// 掉落金币id
	public const int DROP_GOLD_ID = -1;

    // 购买一次获得的竞技场次数
    public const int ARENA_BUY_TO_ATTNUM = 5;
}


//错误码定义
public enum EMErrorCode
{
	None = 0,
	OK,						//成功
	Fail,					//失败
	Unkown,					//未知
	ArgInvalid,				//内部通用错误
	NotExist,				//物品不存在
	NotEnough,				//数量不够
	HeroNotExist,			//英雄不存在
	CanNotEquip,			//无法装备
	LackOfEquip,			//缺少装备
	ReachMaxGrade,			//已经达到最大阶数
	ReachMaxStar,			//已经达到最大星数
	SkillNotOpen,			//技能未开启
	ReachMaxSkillLevel,	    //技能能进达到最大
	UnKownMoneyType,		//未知的货币类型
	NotEnoughGold,			//金币不够
	NotEnoughDiamond,		//钻石不够
	CanNotUse,				//物品无法使用
	CardNotEnough,			//灵魂石不足
	ColdTime,				//CD中
	SignInFail,			    //签到失败
    ExerciseFail,           //试炼失败
    MakeMoneyFail,          //打钱失败
    TodayTimesOver,		    //今日次数使用完了
    SpiritNotEnough,	    //当前剩余体力不足
    TeamLvLow,			    //队伍等级不足
    EctypeTimesMax,		    //副本已经达到最大次数
    FristReward,		    //请先领取奖励
    MaxPass,			    //已经挑战完成
    CannotPass,			    //不能挑战
    Reward,				    //已领取
    NotTarget,			    //目标不存在
    NotAttNum,			    //没有攻击次数
    FightIng,			    //战斗中
    HeroListNull,           //阵型不能为空
	HeroLvLow,				//英雄等级不足
    HaveNum,			    //还有可用次数
    RewardNull,			    //没有奖励
    MaxBuyNum,			    //达到最大购买次数
    MaxClearNum,		    //达到最大清理次数
    ArenaListOld,           //竞技列表更新
    MaxRecharge,		    //达到最大模拟充值数
    MaxRefreshNum,		    //商店最大刷新次数
    ItemSellOut,		    //商品已经卖完了
	MaxCode,				//最大
};

/// <summary>
/// GlobalKeyValue配置表的key枚举
/// </summary>
public enum EmGlobalConfigKey
{
	None = 0,
	PDBID,							//玩家DBID
	PlayerName,						//玩家名字
    DeviceSerial,					//设备序列号
    ZoneID,                         //上次登录游戏服务器ID
	Max,
}