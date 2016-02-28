/*******************************************************************
** 文件名:	EctypeScene.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2016.01.03   23:29:28
** 版  本:	1.0
** 描  述:	副本场景管理  (写于武汉-深圳 T95火车上)
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class EctypeScene : SceneBase
{
    override public void Update()
    {

    }
    override public void OnSceneWillLoad()
    {

    }
    override public void OnSceneLoaded()
    {
        SceneCsvData csvData = GameMgr.Instance.m_csvMgr.mSceneCsv.Lookup(SceneID);
        if (csvData == null)
        {
            Debug.LogError("EctypeScene::OnSceneLoaded sceneCsvData load error id = " + SceneID);
            return;
        }

        GameObject scene = GameMgr.Instance.m_resMgr.LoadResource(csvData.sceneID);
        scene.name = csvData.sceneName;

        EctypeCsvData ectypeCsvData = GameMgr.Instance.m_csvMgr.mEctypeCsv.Lookup((int)EctypeId);
        if (ectypeCsvData == null)
        {
            Debug.LogError(string.Format("EctypeScene::OnSceneLoaded, m_ectypeId={0}", EctypeId));
            return;
        }

        int monsterID = ectypeCsvData.monsterID;    //副本Monster表
        EctypeMonsterCsv csv = GameMgr.Instance.m_csvMgr.MonsterCsvs.Lookup(monsterID);
        if (csv == null)
        {
            Debug.LogError(string.Format("EctypeScene::OnSceneLoaded, monsterID={0}", monsterID));
            return;
        }
        EctypeCreatContext ctx = new EctypeCreatContext();
        ctx.dwEctypeID = EctypeId;
        ctx.sceneID = SceneID;

        foreach (EctypeMonsterCsvData data in csv.EctypeMonsterDic.Values)
        {
            EntityCreateCtx defenders = new EntityCreateCtx();
            int enemyID = data.monsterID;
            defenders.type = (EntityType)GameMgr.Instance.m_csvMgr.mMonsterCsv.LookUpType(enemyID);
            defenders.id = enemyID;
            defenders.pos = new Vector3(2f, 0f, 0f);
            defenders.dir = Vector3.zero;
            defenders.scene = this;
            defenders.camp = EntityCamp.Defender;

            ctx.defenderTeam.entityList.Add(defenders);
        }

        EntityCreateCtx attackers = CreateAttacker();
        ctx.attackerTeam.entityList.Add(attackers);

        GameMgr.Instance.m_ectypeMgr.CreateEctype(ctx);
    }
    private EntityCreateCtx CreateAttacker()
    {
        EntityCreateCtx attacker = new EntityCreateCtx();
        attacker.type = (EntityType)GameMgr.Instance.m_csvMgr.mMonsterCsv.LookUpType(1);
        attacker.id = 1;
        attacker.pos = new Vector3(-2f, 0f, 0f);
        attacker.dir = Vector3.zero;
        attacker.scene = this;
        attacker.camp = EntityCamp.Attacker;

        return attacker;
    }
    override public void OnSceneWillUnload()
    {

    }
    override public void OnSceneUnloaded()
    {
        //GameMgr.Instance.m_entityMgr
    }
}
