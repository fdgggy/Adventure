/*******************************************************************
** 文件名:	AssetToPrefabTool.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.16   15:33:28
** 版  本:	1.0
** 描  述:	
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using UnityEditor;

public class AssetToPrefabTool
{
    static private AssetToPrefab m_toPrefab = null;

    [MenuItem("Tools/Build Sprite To Prefabs")]
    static private void BuildSpriteToPrefab()
    {
        m_toPrefab = new SpriteToPrefab();
    }
}
