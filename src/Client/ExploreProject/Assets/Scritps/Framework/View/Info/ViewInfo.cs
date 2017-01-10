using UnityEngine;

/***
 * ViewInfo.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class ViewInfo
    {
        private string mKey;
        private GameObject mGOject;
        private System.Object mOpenParam;

        public string Key 
        {
            get {
                return mKey;
            }
            set {
                mKey = value;
            }
        }

        public GameObject GObject
        {
            get {
                return mGOject;
            }
            set {
                mGOject = value;
            }
        }

        public System.Object OpenParam 
        {
            get {
                return mOpenParam;
            }
            set {
                mOpenParam = value;
            }
        }

        public bool ActiveSelf 
        {
            get {
                if(GObject == null) {
                    return false;
                }
                return GObject.activeSelf;
            }
        }

        public void SetActive(bool isActive)
        {
            if(GObject == null) {
                return;
            }

            mGOject.SetActive(isActive);
        }

    }
}
