/*******************************************************************
** 文件名:	ResMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.19   23:16:28
** 版  本:	1.0
** 描  述:	资源管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class ResMgr
{
    public GameObject LoadResource(string id)
    {
        ResourceCsvData resData = GameMgr.Instance.m_csvMgr.mResourceCsv.Lookup(id);
        string path = resData.relativePath;

        Object obj = Resources.Load(path);
        if (obj != null)
            return GameObject.Instantiate(obj) as GameObject;
        return null;
    }
    public void Destroy(Object obj)
    {
        GameObject.DestroyObject(obj);
    }
}
