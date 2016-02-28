/*******************************************************************
** 文件名:	GameClient.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.19   22:33:28
** 版  本:	1.0
** 描  述:	游戏客户端管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;

// 游戏流程类型
public enum ClientState
{
    None = 0,		// 无用的
    Init,			// 初始化
    CheckVer,		// 版本检查(包括下载操作)
    Create,		    // 系统模块创建(调用Game.Create())
    Login,			// 登录
    Home,		    // 主城游戏
    World,          // 世界地图
    Ectype,			// 副本游戏中
    Logout,			// 登出
    Error,			// 错误状态
    Close,			// 关闭
    MAX,            // 最大值
}
// 进入主城上下文
public struct EnterHomeContext
{
    public int nMapID;          // 主城ID
    public bool bGuide;         // 是否由引导副本进入主城
    public bool LoadLevel;      //是否加载主场景
}
// 进入副本上下文
public class EnterEctypeContext
{
    public EctypeCreatContext m_EctypeCreateData;
    public EnterEctypeContext()
    {
        m_EctypeCreateData = new EctypeCreatContext();
    }
}
public class GameClientMgr
{
    // 游戏流程类型
    private ClientState m_nState = ClientState.None;
    
    // 请求进入某个状态
    private bool[] m_bRequestEnter = new bool[(int)ClientState.MAX];
   
    // 进入主城上下文
    private EnterHomeContext m_EnterHomeContext;

    // 进入副本上下文
    private EnterEctypeContext m_EnterEctypeContext;

    // 游戏流程类型
    public ClientState clientState { get { return m_nState; } }
    public GameClientMgr()
	{
        m_EnterHomeContext.nMapID = COMMON_DEF.MAIN_SCENE_ID;
	}
    public void Init()
    {
        SetState(ClientState.Init);
    }
    public bool EnterEctype(EnterEctypeContext context)
    {
        if (ClientState.Ectype == m_nState)
        {
            return false;
        }

        m_EnterEctypeContext = context;
        m_bRequestEnter[(int)ClientState.Ectype] = true;
        return true;
    }
    public bool EnterHome(EnterHomeContext context)
    {
        if (ClientState.Home == m_nState)
        {
            return false;
        }

        m_EnterHomeContext = context;
        return true;
    }
    public void OnFixedUpdate()
    {
        DoTask();
    }
    private bool SetState(ClientState nState)
    {
        if (nState == m_nState)
        {
            return false;
        }

        // 清除请求状态
        m_bRequestEnter[(int)nState] = false;
        m_bRequestEnter[(int)m_nState] = false;

        // 旧的流程
        ClientState nOldState = m_nState;

        // 当游戏流程退出
        OnExit(nOldState, nState);

        // 当游戏流程进入
        OnEnter(nState, nOldState);

        // 改变流程
        m_nState = nState;

        Debug.Log("GameClientMgr.SetState():" + nOldState.ToString() + "->" + nState.ToString());

        return true;
    }
    private void OnEnter(ClientState nState, ClientState nOldState)
    {
        // 流程
        switch (nState)
        {
            case ClientState.None:		// 无用的
                {

                }
                break;
            case ClientState.Init:		// 初始化
                {
                    //播放Logo动画
                    GameMgr.Instance.m_uiMgr.UILogIn.Play();
                }
                break;
            case ClientState.CheckVer:		// 版本检查(包括下载操作)
                {
                    //启动版本检查模块
                    //Game.m_singleton.ResVerManager.StartVerCheck();
                }
                break;
            case ClientState.Create:		 // 系统模块创建(调用Game.Create())
                {
                    // 启动创建游戏所有模块
                    //Game.m_singleton.Create();
                }
                break;
            case ClientState.Login:		// 登录
                {
                    // 播放登陆音乐
                    //PlayLoginAudio(true);

                    // 启动登录管理流程
                    //Game.m_singleton.LoginMgr.Start();
                }
                break;
            case ClientState.Home:		// 主城游戏
                {
                    //开始加载场景资源
                    //Game.m_singleton.Home.EnterHome();
                }
                break;
            case ClientState.Ectype:			// 副本游戏中
                {
                    EctypeEnterContext ctx = new EctypeEnterContext();
                    ctx.sceneID = m_EnterEctypeContext.m_EctypeCreateData.sceneID;
                    ctx.ectypeID = m_EnterEctypeContext.m_EctypeCreateData.dwEctypeID;

                    GameMgr.Instance.m_ectypeMgr.EnterEctype(ctx);
                }
                break;
            case ClientState.Logout:		// 登出
                {
                }
                break;
            case ClientState.Error:		// 错误
                {
                }
                break;
            case ClientState.Close:		// 关闭
                {
                }
                break;
        }

    }

    /// <summary>
    /// 驱动游戏流程
    /// </summary>
    private void DoTask()
    {

        //////////////////////////////////////////////////////////////////////////
        // 流程
        switch (m_nState)
        {
            case ClientState.None:		// 无用的
                {
                }
                break;
            case ClientState.Init:		// 初始化
                {
                    //if (Game.m_singleton.UIManager.LogoUI.bIsShowNormal)
                    //{
                    //    SetState(ClientState.CheckVer);
                    //    break;
                    //}
                    if (m_bRequestEnter[(int)ClientState.Ectype])
                    {
                        SetState(ClientState.Ectype);
                        break;
                    }
                }
                break;
            case ClientState.CheckVer:		// 版本检查
                {
                    //if (Game.m_singleton.ResVerManager.bResVerMgrIsOver)
                    //{
                    //    SetState(ClientState.Create);
                    //    break;
                    //}
                }
                break;
            case ClientState.Create:		 // 系统模块创建(调用Game.Create())
                {
                    //if (Game.m_singleton.UIManager.LogoUI.bIsShowFinish)
                    //{
                    //    SetState(ClientState.Login);
                    //    break;
                    //}
                }
                break;
            case ClientState.Login:		// 登录
                {
                    //if (Game.m_singleton.LoginMgr.IsGameLogin)
                    //{
                    //    Game.m_singleton.ResPreLoader.mUserType = ResPreLoadUserType.LoginToHome;
                    //    SetState(ClientState.Home);
                    //    break;
                    //}
                }
                break;
            case ClientState.Home:		// 主城游戏
                {
                    //if (m_bRequestEnter[(int)ClientState.Ectype])
                    //{
                    //    Game.m_singleton.ResPreLoader.mUserType = ResPreLoadUserType.HomeToEctype;
                    //    SetState(ClientState.Ectype);
                    //    break;
                    //}
                }
                break;
            case ClientState.Ectype:			// 副本游戏中
                {
                    //if (m_bRequestEnter[(int)ClientState.Home])
                    //{
                    //    if (m_EnterHomeContext.bGuide)
                    //    {
                    //        Game.m_singleton.ResPreLoader.mUserType = ResPreLoadUserType.GuideEctypeToHome;
                    //    }
                    //    else
                    //    {
                    //        Game.m_singleton.ResPreLoader.mUserType = ResPreLoadUserType.EctypeToHome;
                    //    }

                    //    SetState(ClientState.Home);
                    //    break;
                    //}
                }
                break;
            case ClientState.Logout:		// 登出
                {
                }
                break;
            case ClientState.Error:		// 错误
                {
                }
                break;
            case ClientState.Close:		// 关闭
                {
                }
                break;
        }
    }
    private void OnExit(ClientState nState, ClientState nNewState)
    {
        // 流程
        switch (nState)
        {
            case ClientState.None:		// 无用的
                {
                }
                break;
            case ClientState.Init:		// 初始化
                {
                }
                break;
            case ClientState.CheckVer:		// 版本检查(包括下载操作)
                {
                }
                break;
            case ClientState.Create:		 // 系统模块创建(调用Game.Create())
                {
                }
                break;
            case ClientState.Login:		// 登录
                {
                    //登录完成之后需要隐藏登陆窗口和注销事件订阅
                    //Game.m_singleton.UIManager.LoginUI.HideWindow();
                }
                break;
            case ClientState.Home:		// 主城游戏
                {
                    //卸载主场景资源
                    //Game.m_singleton.Home.LeaveHome();
                }
                break;
            case ClientState.Ectype:			// 副本游戏中
                {
                    //Game.m_singleton.EctypeManager.CloseEctype();
                }
                break;
            case ClientState.Logout:		// 登出
                {
                }
                break;
            case ClientState.Error:		// 错误
                {
                }
                break;
            case ClientState.Close:		// 关闭
                {
                }
                break;
        }
    }
}
