/*******************************************************************
** 文件名:	EctypeLoader.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.31   17:54:28
** 版  本:	1.0
** 描  述:	副本加载器   写于深圳->武汉G1012高铁上
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
class EctypeLoader
{
    private Ectype m_owner = null;

    public EctypeLoader(Ectype owner)
    {
        m_owner = owner;
    }

    public void OnFixedUpdate()
    {
        DoTask();
    }

    private void DoTask()
    {

    }
}
