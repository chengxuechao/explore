using System;
using UnityEngine;

/***
 * PanelInfo.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    [Serializable]
    public class PanelInfo
    {
        // 是否是全屏界面
        [SerializeField]
        private bool mIsFullScreen = false;

        // 是否有打开动画
        [SerializeField]
        private bool mIsOpenAni = false;
        // 是否有关闭动画
        [SerializeField]
        private bool mIsCloseAni = false;

        // 界面类型
        [SerializeField]
        private PanelType mViewType = PanelType.Normal;


        public bool IsFullScreen 
        {
            get {
                return mIsFullScreen; 
            }
        }

        public bool IsOpenAni 
        {
            get {
                return mIsOpenAni;
            }
        }

        public bool IsCloseAni 
        {
            get {
                return mIsCloseAni;
            }
        }

        public PanelType ViewType 
        {
            get {
                return mViewType;
            }
        }

    }
}

