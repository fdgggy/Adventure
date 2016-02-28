/*******************************************************************
** 文件名:	CsvMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.19   22:59:28
** 版  本:	1.0
** 描  述:	配置表管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class CsvMgr
{
    private string RootPath { get; set; }

    private ResourceCsv m_ResourceCsv = null;
    private SceneCsv m_SceneCsv = null;
    private MonsterCsv m_MonsterCsv = null;
    private EctypeCsv m_EctypeCsv = null;
    private EctypeMonsterMgr m_monsterCsvMgr = null;
    private SkillCsv m_skillCsv = null;
    public ResourceCsv mResourceCsv { get { return m_ResourceCsv; } }
    public SceneCsv mSceneCsv { get { return m_SceneCsv; } }
    public MonsterCsv mMonsterCsv { get { return m_MonsterCsv; } }
    public EctypeCsv mEctypeCsv { get { return m_EctypeCsv; } }
    public EctypeMonsterMgr MonsterCsvs
    {
        get { return m_monsterCsvMgr; }
    }
    public SkillCsv mSkillCsv { get { return m_skillCsv; } }
    public CsvMgr(string rootPath)
    {
        RootPath = rootPath;
        m_ResourceCsv = new ResourceCsv();
        m_SceneCsv = new SceneCsv();
        m_MonsterCsv = new MonsterCsv();
        m_EctypeCsv = new EctypeCsv();
        m_monsterCsvMgr = new EctypeMonsterMgr();
        m_skillCsv = new SkillCsv();

        Init();
    }
    private void Init()
    {
        LoadSchemeByResPath("Scp/Resource", m_ResourceCsv);
        LoadSchemeByResPath("Scp/Scene", m_SceneCsv);
        LoadSchemeByResPath("Scp/Ectype", mEctypeCsv);
        LoadSchemeByResPath("Scp/Monster", mMonsterCsv);
        LoadSchemeByResPath("Scp/Skill", mSkillCsv);
    }
    public void LoadSchemeByResPath(string path, ISchemeUpdateSink sink)
    {
        TextAsset textAsset = Resources.Load(path) as TextAsset;
        if (textAsset == null)
        {
            Debug.LogError("CsvMgr::LoadSchemeByResPath--text资源不存在，ResPath = " + path);
            return;
        }

        CsvReader reader = new CsvReader();
        // 读取配置文件
        if (!reader.LoadByData(path, textAsset.bytes))
        {
            Debug.LogError("CsvMgr::LoadSchemeByResPath--读取配置文件失败，文件路径 = " + path);
            return;
        }

        // 执行配置加载回调
        sink.OnSchemeLoad(reader);
        sink.mIsLoaded = true;
    }
}
