using System;

/**
 * Observer.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
	public class Observer : IObserver
	{
        private Action<INotification> mNotifyMethod;
        private object mNotifyContext;

        private readonly object mSyncObject = new object();

        public Observer(Action<INotification> notifyMethod, object notifyContext)
		{
			mNotifyMethod = notifyMethod;
			mNotifyContext = notifyContext;
		}

		public virtual void NotifyObserver(INotification notification)
		{
            Action<INotification> method;

			lock (mSyncObject) {
				method = NotifyMethod;
			}

            if (method != null) {
                method(notification);
            }
        }

		public virtual bool CompareNotifyContext(object obj)
		{
			lock (mSyncObject) {
				return NotifyContext.Equals(obj);
			}
		}

		public virtual Action<INotification> NotifyMethod
		{
			private get {
				return mNotifyMethod;
			}
			set {
				mNotifyMethod = value;
			}
		}

		public virtual object NotifyContext
		{
			private get {
				return mNotifyContext;
			}
			set {
				mNotifyContext = value;
			}
		}
		
	}
}
