using UnityEngine;

/***
 * ViewSingleMediator.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class ViewSingleMediator : ViewBaseMediator
    {
        private ViewInfo mViewInfo;

        protected ViewSingleMediator()
        {
            mMediatorName = GetType().FullName;
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
            ViewInfo info = new ViewInfo();

            info.Key = name;
            info.GObject = ViewManager.GetObject(name);
            info.OpenParam = null;
            info.SetActive(false);

            mViewInfo = info;
        }

        public override void DoActive(string key, bool isActive)
        {
            if(ViewVO == null) {
                return;
            }

            ViewVO.SetActive(isActive);
        }

        public override void DoOpen(string key, System.Object param)
        {
            if (ViewVO == null) {
                return;
            }

            ViewVO.SetActive(true);
            ViewVO.OpenParam = param;

            OnOpen(param);
        }

        public override void DoClose()
        {
            if (ViewVO == null) {
                return;
            }

            ViewVO.SetActive(false);
            ViewVO.OpenParam = null;

            OnClose();
        }

        public ViewInfo ViewVO 
        {
            get {
                return mViewInfo;
            }
        }

        public string Key 
        {
            get {
                if (ViewVO == null) {
                    return null;
                }
                return ViewVO.Key;
            }
        }

        public GameObject GObject 
        {
            get {
                if(ViewVO == null) {
                    return null;
                }
                return ViewVO.GObject;
            }
        }

        public System.Object Param 
        {
            get {
                if (ViewVO == null) {
                    return null;
                }
                return ViewVO.OpenParam;
            }
        }
       
    }
}
