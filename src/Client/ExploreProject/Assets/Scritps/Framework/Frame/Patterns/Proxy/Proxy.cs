/**
 * Proxy.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public class Proxy : Notifier, IProxy, INotifier
    {
        public static string NAME = "Proxy";

        private string mProxyName;
        private object mData;

        public Proxy() : this(NAME, null)
        {
        }

        public Proxy(string proxyName) : this(proxyName, null)
        {
        }

		public Proxy(string proxyName, object data)
        {

            mProxyName = (proxyName != null) ? proxyName : NAME;
            if (data != null) {
                mData = data;
            }
        }

        public virtual string ProxyName
        {
            get {
                return mProxyName;
            }
        }

        public virtual object Data 
        {
            get {
                return mData;
            }
            set {
                mData = value;
            }
        }

        public virtual void OnRegister()
		{
		}

		public virtual void OnRemove()
		{
		}
		
	}
}
