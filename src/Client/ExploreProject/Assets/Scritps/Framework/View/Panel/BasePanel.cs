using UnityEngine;

/***
 * BasePanel.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    /// <summary>
    /// 所有界面基类
    /// </summary>
    public class BasePanel : MonoBehaviour
    {
        [SerializeField]
        private PanelInfo mViewInfo;

        public PanelInfo ViewInfo 
        {
            get {
                return mViewInfo;
            }
        }

        public virtual void Init() { }

    }
}

