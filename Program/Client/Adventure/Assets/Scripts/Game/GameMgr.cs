/*******************************************************************
** 文件名:	GameMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.17   21:38:28
** 版  本:	1.0
** 描  述:	游戏管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;

public class GameMgr : SingletonMonoBehavior<GameMgr>
{
    #region <<各系统模块声明>>
    public GameClientMgr m_clientMgr;
    public UIMgr m_uiMgr;
    public EntityViewMgr m_entityViewMgr;
    public EntityMgr m_entityMgr;
    public EctypeMgr m_ectypeMgr;
    public CsvMgr m_csvMgr;
    public HomeMgr m_homeMgr;
    public ResMgr m_resMgr;
    public AudioMgr m_audioMgr;
    public CameraMgr m_cameraMgr;
    public SceneMgr m_sceneMgr;

    private Player m_player;
    #endregion
    // 用户根目录
    private string m_userRoot;

    protected override void Awake()
    {
        Init();
        m_csvMgr = new CsvMgr(m_userRoot);
        m_resMgr = new ResMgr();
        m_uiMgr = new UIMgr();
        m_clientMgr = new GameClientMgr();
        m_clientMgr.Init();
        m_entityMgr = new EntityMgr();
        m_ectypeMgr = new EctypeMgr();
        m_sceneMgr = new SceneMgr();
        m_entityViewMgr = new EntityViewMgr();
    }
    void Init()
    {
        m_userRoot = GameUtil.GetUserRoot();
    }
    void Update()
    {
        if (m_sceneMgr != null)
        {
            m_sceneMgr.OnUpdate();
        }
        if (m_entityViewMgr != null)
        {
            m_entityViewMgr.OnUpdate();
        }
    }
     void FixedUpdate()
    {
        if (m_clientMgr != null)
        {
            m_clientMgr.OnFixedUpdate();
        }
        if (m_entityMgr != null)
        {
            m_entityMgr.OnFixedUpdate();
        }
        if (m_ectypeMgr != null)
         {
             m_ectypeMgr.OnFixedUpdate();
         }
    }
}
