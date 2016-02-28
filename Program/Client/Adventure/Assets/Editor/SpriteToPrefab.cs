/*******************************************************************
** 文件名:	SpriteToPrefab.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.16   15:33:28
** 版  本:	1.0
** 描  述:	Asset -> Prefab
** 应  用:  Sprite制作成Prefab

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
public class SpriteToPrefab : AssetToPrefab
{
    override public void BuildPrefab(string src, string des, string filetype)
    {
        if (filetype.Equals(".jpg") || filetype.Equals(".png"))
        {
            Sprite sprite = Resources.LoadAssetAtPath<Sprite>(src);     //src和des Build的时候路径必须是Asset开头，全路径不行
            if (sprite == null)
            {
                Debug.Log("SpriteToPrefab::BuildPrefab  确保UI已是精灵");
            }

            GameObject go = new GameObject(sprite.name);
            go.AddComponent<SpriteRenderer>().sprite = sprite;
            string prefabPath = des + "/" + sprite.name + ".prefab";
            PrefabUtility.CreatePrefab(prefabPath, go);

            GameObject.DestroyImmediate(go);
        }
    }
}
