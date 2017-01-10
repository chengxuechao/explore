using System;

/***
 * Facade.cs
 * 
 * author : abaojin 
 */
namespace GameCore
{
    public class Facade : Singleton<Facade>, IFacade
	{
        private static readonly object mStaticSyncObject = new object();

        private IController mController;
        private IModel mModel;
        private IView mView;

        public Facade() 
        {
			InitFacade();
		}

		public virtual void RegisterProxy(IProxy proxy)
		{
			mModel.RegisterProxy(proxy);
		}

        public virtual IProxy GetProxy(string proxyName)
		{
			return mModel.GetProxy(proxyName);
		}

        public virtual IProxy RemoveProxy(string proxyName)
		{
			return mModel.RemoveProxy(proxyName);
		}

        public virtual bool HasProxy(string proxyName)
		{
			return mModel.HasProxy(proxyName);
		}

        public virtual void RegisterCommand(string notificationName, Type commandType)
        {
            mController.RegisterCommand(notificationName, commandType);
        }

        public virtual void RegisterAction<T>(string notificationName, Action<T> commandAction)
        {
            ActionHolder holder = new ActionHolder(notificationName, (p) => {
                commandAction((T)p);
            });
            mController.RegisterCommand(notificationName, holder);
        }

        public virtual void RemoveCommand(string notificationName)
		{
			mController.RemoveCommand(notificationName);
		}

        public virtual bool HasCommand(string notificationName)
		{
			return mController.HasCommand(notificationName);
		}

        public virtual void RegisterMediator(IMediator mediator)
		{
			mView.RegisterMediator(mediator);
		}

        public virtual IMediator GetMediator(string mediatorName)
		{
			return mView.GetMediator(mediatorName);
		}

        public virtual IMediator RemoveMediator(string mediatorName)
		{
			return mView.RemoveMediator(mediatorName);
		}

        public virtual bool HasMediator(string mediatorName)
		{
			return mView.HasMediator(mediatorName);
		}

        public virtual void NotifyObservers(INotification notification)
		{
			mView.NotifyObservers(notification);
		}

        public virtual void SendNotification(string notificationName)
		{
			NotifyObservers(new Notification(notificationName));
		}

        public virtual void SendNotification(string notificationName, object body)
		{
			NotifyObservers(new Notification(notificationName, body));
		}

        public virtual void SendNotification(string notificationName, object body, string type)
		{
			NotifyObservers(new Notification(notificationName, body, type));
		}

        public virtual void InitFacade()
        {
			InitModel();
			InitController();
			InitView();
		}

        public virtual void InitController()
        {
            if (mController != null) {
                return;
            }
			mController = Controller.Instance;
		}

        public virtual void InitModel()
        {
            if (mModel != null) {
                return;
            }
			mModel = Model.Instance;
		}
		
        public virtual void InitView()
        {
            if (mView != null) {
                return;
            }
			mView = View.Instance;
		}
		
	}
}
