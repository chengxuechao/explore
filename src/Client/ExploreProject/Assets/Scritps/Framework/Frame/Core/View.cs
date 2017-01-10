using System.Collections.Generic;

/***
 * View.cs
 * 
 * @author abaojin 
 */
namespace GameCore
{
    public class View : Singleton<View>, IView
    {
        private readonly static object mStaticSyncObject = new object();
        private readonly object mSyncObject = new object();

        private IDictionary<string, IMediator> mMediatorMap;
        private IDictionary<string, IList<IObserver>> mObserverMap;

        public View()
		{
			mMediatorMap = new Dictionary<string, IMediator>();
			mObserverMap = new Dictionary<string, IList<IObserver>>();

            InitView();
		}

		public virtual void RegisterObserver(string notificationName, IObserver observer)
		{
			lock (mSyncObject) {
				if (!mObserverMap.ContainsKey(notificationName)) {
					mObserverMap[notificationName] = new List<IObserver>();
				}
				mObserverMap[notificationName].Add(observer);
			}
		}

		public virtual void NotifyObservers(INotification notification)
		{
			IList<IObserver> observers = null;

			lock (mSyncObject) {
				if (mObserverMap.ContainsKey(notification.Name)) {
					IList<IObserver> observersRef = mObserverMap[notification.Name];
					observers = new List<IObserver>(observersRef);
				}
			}

			if (observers != null) {
				for (int i = 0; i < observers.Count; i++) {
					IObserver observer = observers[i];
					observer.NotifyObserver(notification);
				}
			}
		}

		public virtual void RemoveObserver(string notificationName, object notifyContext)
		{
			lock (mSyncObject) {
				if (mObserverMap.ContainsKey(notificationName)) {
					IList<IObserver> observers = mObserverMap[notificationName];

					for (int i = 0; i < observers.Count; i++) {
						if (observers[i].CompareNotifyContext(notifyContext)) {
							observers.RemoveAt(i);
							break;
						}
					}

					if (observers.Count == 0) {
						mObserverMap.Remove(notificationName);
					}
				}
			}
		}

		public virtual void RegisterMediator(IMediator mediator)
		{
			lock (mSyncObject) {
                if (mMediatorMap.ContainsKey(mediator.MediatorName)) {
                    return;
                }
				mMediatorMap[mediator.MediatorName] = mediator;

				IList<string> interests = mediator.ListNotificationInterests();
                int count = interests.Count;
				if (count > 0) {
					IObserver observer = new Observer(mediator.HandleNotification, mediator);
					for (int i = 0; i < count; i++) {
						RegisterObserver(interests[i].ToString(), observer);
					}
				}
			}

			mediator.OnRegister();
		}

		public virtual IMediator GetMediator(string mediatorName)
		{
			lock (mSyncObject) {
                if (!mMediatorMap.ContainsKey(mediatorName)) {
                    return null;
                }
				return mMediatorMap[mediatorName];
			}
		}

		public virtual IMediator RemoveMediator(string mediatorName)
		{
			IMediator mediator = null;

			lock (mSyncObject) {
                if (!mMediatorMap.ContainsKey(mediatorName)) {
                    return null;
                }
				mediator = (IMediator) mMediatorMap[mediatorName];

				IList<string> interests = mediator.ListNotificationInterests();

				for (int i = 0; i < interests.Count; i++) {
					RemoveObserver(interests[i], mediator);
				}

                mMediatorMap.Remove(mediatorName);
			}

            if (mediator != null) {
                mediator.OnRemove();
            }

			return mediator;
		}

		public virtual bool HasMediator(string mediatorName)
		{
			lock (mSyncObject) {
				return mMediatorMap.ContainsKey(mediatorName);
			}
		}

        public virtual void InitView()
		{
		}

    }
}
