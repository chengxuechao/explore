using System.Collections.Generic;

/***
 * Model.cs
 * 
 * @author abaojin 
 */
namespace GameCore
{
    public class Model : Singleton<Model>, IModel
    {
        private readonly static object mStaticSyncObject = new object();
        private readonly object mSyncObject = new object();

        private IDictionary<string, IProxy> mProxyMap;

        public Model()
		{
			mProxyMap = new Dictionary<string, IProxy>();
			InitModel();
		}

		public virtual void RegisterProxy(IProxy proxy)
		{
			lock (mSyncObject) {
                if (mProxyMap.ContainsKey(proxy.ProxyName)) {
                    return;
                }
				mProxyMap[proxy.ProxyName] = proxy;
			}

			proxy.OnRegister();
		}

		public virtual IProxy GetProxy(string proxyName)
		{
			lock (mSyncObject) {
                if (!mProxyMap.ContainsKey(proxyName)) {
                    return null;
                }
				return mProxyMap[proxyName];
			}
		}

		public virtual bool HasProxy(string proxyName)
		{
			lock (mSyncObject) {
				return mProxyMap.ContainsKey(proxyName);
			}
		}

		public virtual IProxy RemoveProxy(string proxyName)
		{
			IProxy proxy = null;

			lock (mSyncObject) {
				if (mProxyMap.ContainsKey(proxyName)) {
					proxy = GetProxy(proxyName);
					mProxyMap.Remove(proxyName);
				}
			}

            if (proxy != null) {
                proxy.OnRemove();
            }

			return proxy;
		}

		public virtual void InitModel()
		{
		}

    }
}
