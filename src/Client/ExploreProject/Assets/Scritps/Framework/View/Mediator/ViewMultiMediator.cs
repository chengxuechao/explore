using System.Collections.Generic;
using UnityEngine;

/***
 * ViewMultiMediator.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class ViewMultiMediator : ViewBaseMediator
    {
        private List<ViewInfo> mPanelList;
        private int mPanelCount;

        public ViewMultiMediator()
        {
            mMediatorName = GetType().FullName;

            mPanelList = new List<ViewInfo>();
            mPanelCount = 0;
        }

        public override void OnRegister()
        {
            OnInit();
            OnStart();
        }

        public override void OnRemove()
        {
            
        }

        public override void AddPanel(string name)
        {
            ViewInfo vo = new ViewInfo();

            vo.Key = name;
            vo.GObject = ViewManager.GetObject(name);
            vo.OpenParam = null;
            vo.SetActive(false);

            if (mPanelList != null) {
                mPanelList.Add(vo);
                mPanelCount++;
            }
        }

        public override void DoActive(string name, bool isActive)
        {
            ViewInfo panelVO = this[name];

            if(panelVO == null) {
                return;
            }

            panelVO.SetActive(isActive);
        }

        public override void DoOpen(string name, System.Object param)
        {
            if (PanelList == null) {
                return;
            }

            ViewInfo panelVO = this[name];
            if (panelVO == null) {
                return;
            }
            panelVO.SetActive(true);
            panelVO.OpenParam = param;

            OnOpen(param);
        }

        public override void DoClose()
        {
            
        }

        public List<ViewInfo> PanelList 
        {
            get {
                return mPanelList;
            }
        }

        public int PanelCount
        {
            get {
                return mPanelCount;
            }
        }

        public ViewInfo this[string key] 
        {
            get {
                if(PanelCount <= 0) {
                    return null;
                }

                for (int i = 0; i < PanelCount; ++i) {
                    ViewInfo vo = PanelList[i];
                    if(vo == null) {
                        continue;
                    }
                    if(key == vo.Key) {
                        return vo;
                    }
                }

                return null;
            }
        }

        public string GetKey(string key)
        {
            ViewInfo vo = this[key];
            if (vo != null) {
                return vo.Key;
            }

            return null;
        }

        public GameObject GetGObject(string key)
        {
            ViewInfo vo = this[key];
            if (vo != null) {
                return vo.GObject;
            }

            return null;
        }

        public System.Object GetParam(string key)
        {
            ViewInfo vo = this[key];
            if (vo != null) {
                return vo.OpenParam;
            }

            return null;
        }

    }
}
