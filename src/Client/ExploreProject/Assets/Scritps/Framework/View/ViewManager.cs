using System.Collections.Generic;
using UnityEngine;

/***
 * PanelManager.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public static class ViewManager
    {
        // 显示界面容器
        private static List<string> mViewList = new List<string>();
        private static Facade mFacade = Facade.Instance;

        private static GameObject mRoot;

        private static Queue<string> mOpenQueue = new Queue<string>();

        private static GameObject Root
        {
            get {
                if (mRoot == null) {
                    mRoot = GameObject.Find("UI Root");
                }
                return mRoot;
            }
        }

        public static GameObject GetObject(string viewName)
        {
            if (Root == null) {
                return null;
            }

            Transform tr = Root.transform.FindChild(viewName);
            if (tr == null) {
                return null;
            }

            return tr.gameObject;
        }

        public static void AddMediator<T>()
        {
            string name = typeof(T).FullName;

            if (!mViewList.Contains(name)) {
                mViewList.Add(name);
            } else {
                Debug.Log(string.Format("{0} mediator is already add.", name));
            }
        }

        public static ViewBaseMediator GetMediator<T>()
        {
            string name = typeof(T).FullName;

            IMediator mediator = mFacade.GetMediator(name);
            if(mediator != null) {
                return mediator as ViewBaseMediator;
            }

            return null;
        }

        public static void Open<T>(string key, System.Object param)
        {
            string name = typeof(T).FullName;

            IMediator meditor = mFacade.GetMediator(name);
            if(meditor == null) {
                return;
            }

            ViewBaseMediator baseMediator = meditor as ViewBaseMediator;
            baseMediator.DoOpen(key, param);
        }

        public static void SetActive<T>(string key, bool isActive)
        {
            string name = typeof(T).FullName;

            IMediator meditor = mFacade.GetMediator(name);
            if (meditor == null) {
                return;
            }

            ViewBaseMediator baseMediator = meditor as ViewBaseMediator;
            baseMediator.DoActive(key, isActive);
        }

        public static void SetAllActive(bool isActive)
        {
            int count = mViewList.Count;
            if (count == 0) {
                return;
            }

            for (int i = 0; i < count; ++i) {
                string name = mViewList[i];
                IMediator meditor = mFacade.GetMediator(name);
                if (meditor == null) {
                    continue;
                }

                if(meditor is ViewMultiMediator) {
                    ViewMultiMediator multiMediator = meditor as ViewMultiMediator;
                    List<ViewInfo> panelList = multiMediator.PanelList;
                    for(int j = 0; j < multiMediator.PanelCount; ++j) {
                        multiMediator.DoActive(panelList[i].Key, isActive);
                    }
                } else if(meditor is ViewSingleMediator) {
                    (meditor as ViewBaseMediator).DoActive(null, isActive);
                }
            }
        }

        public static void Close<T>()
        {
            string name = typeof(T).FullName;

            IMediator meditor = mFacade.GetMediator(name);
            if (meditor == null) {
                return;
            }

            ViewBaseMediator baseMediator = meditor as ViewBaseMediator;
            baseMediator.DoClose();
        }

        public static void CloseAll()
        {
            int count = mViewList.Count;
            if (count == 0) {
                return;
            }

            for (int i = 0; i < count; ++i) {
                string name = mViewList[i];
                IMediator meditor = mFacade.GetMediator(name);
                if(meditor == null) {
                    continue;
                }

                ViewBaseMediator baseMediator = meditor as ViewBaseMediator;
                baseMediator.DoClose();
            }

            mOpenQueue.Clear();
        }

        public static void Destroy<T>()
        {
            mFacade.RemoveMediator(typeof(T).FullName);
        }

        public static bool IsOpen(string name)
        {
            IMediator meditor = mFacade.GetMediator(name);
            if (meditor == null) {
                return false;
            }

            bool ObjActive = false;
            if(meditor is ViewMultiMediator) {
                ViewMultiMediator PanelMulti = meditor as ViewMultiMediator;
                ViewInfo panelVO = PanelMulti[name];
                if(panelVO != null) {
                    ObjActive = panelVO.ActiveSelf;
                }
            } else if(meditor is ViewSingleMediator) {
                ViewSingleMediator panelSingle = meditor as ViewSingleMediator;
                ViewInfo vo = panelSingle.ViewVO;
                if (vo != null) {
                    ObjActive = vo.ActiveSelf;
                }
            } else {
                Debug.LogError("Not exist panel mediator.");
                return false;
            }

            return ObjActive;
        }

        public static void DestroyAll()
        {
            int count = mViewList.Count;
            if (count == 0) {
                return;
            }
            for (int i = 0; i < count; ++i) {
                string name = mViewList[i];
                mFacade.RemoveMediator(name);
            }

            mViewList.Clear();
            mOpenQueue.Clear();

            mRoot = null;
        }
    }
}

