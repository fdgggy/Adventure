/*******************************************************************
** 文件名:	IAnimation.cs
** 版  权:	(C) 深圳冰川网络技术有限公司 2015 - Atom
** 创建人:	周健
** 日  期:	2015/08/06
** 版  本:	1.0
** 描  述:	统一2D精灵动画接口
** 应  用 	
            1.生成SpriteCollection  2.生成SpriteAnimation
**************************** 修改记录 ******************************
** 修改人  
** 日  期  
** 描  述
********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 动画状态枚举 
/// </summary>
public enum AniState2D
{
    None = -1,
    Idle,
    Walk,
    Run,
    Build,
    Attack,

    Num
}
/// <summary>
/// 方向枚举
/// </summary>
public enum AniDirection
{
    None,
    N,
    NE,
    E,
    ES,
    S,
    SW,
    W,
    WN
}
public class I2dSpriteAnimation 
{
    /// <summary>
    /// 播放动画的Animator
    /// </summary>
    private tk2dSpriteAnimator m_tAnimator;
    /// <summary>
    /// 存储的动画
    /// </summary>
    private Dictionary<AniState2D, tk2dSpriteAnimation>m_dAniClips = new Dictionary<AniState2D,tk2dSpriteAnimation>();
    /// <summary>
    /// 动作，放向
    /// </summary>
    private string m_sAction, m_sDirection;

    public I2dSpriteAnimation()
    {
        //m_tAnimator = new tk2dSpriteAnimator();
    }

    public void Destroy()
    {
        m_dAniClips.Clear();
        m_tAnimator = null;
    }
    /// <summary>
    /// 添加片段
    /// </summary>
    /// <param name="state"></param>
    /// <param name="animation"></param>
    public void AddAniClip(AniState2D state, tk2dSpriteAnimation animation)
    {
        if (!m_dAniClips.ContainsKey(state))
        {
            m_dAniClips[state] = animation;
        }
        else
        {
            Debug.Log("I2dSpriteAnimation::AddAniClip 已经存在此动画片段" + state);
        }
    }

    public bool BindSprite(Transform obj)
    {
        if (obj)
        {

        }
        return false;
    }
    /// <summary>
    /// 绑定Animator
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool BindComp(Transform obj)
    {
        if (obj)
        {
            m_tAnimator = obj.GetComponent<tk2dSpriteAnimator>();
            if (m_tAnimator == null)
            {
                m_tAnimator = obj.gameObject.AddComponent<tk2dSpriteAnimator>();
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 设置朝向
    /// </summary>
    /// <param name="dir"></param>
    public void Turn(AniDirection dir)
    {
        m_sDirection = dir.ToString();
    }
    /// <summary>
    /// 设置动画状态
    /// </summary>
    /// <param name="state"></param>
    public void ChangeAniState(AniState2D state)
    {
        if (m_dAniClips.ContainsKey(state))
        {
            m_sAction = state.ToString();
            m_tAnimator.Library = m_dAniClips[state];
        }
        else
        {
            Debug.Log("I2dSpriteAnimation::ChangeAniState 不包含动画状态" + state);
        }
    }
    /// <summary>
    /// 设置播放模式
    /// </summary>
    /// <param name="model"></param>
    public void PlayMode(tk2dSpriteAnimationClip.WrapMode model)
    {
        m_tAnimator.CurrentClip.wrapMode = model;
    }
    /// <summary>
    /// 播放动画 
    /// </summary>
    public void Play()
    {
        m_tAnimator.Play(m_sAction + "_" + m_sDirection);
    }
    public void Play(string name)
    {
        m_tAnimator.Play(name);
    }
}
