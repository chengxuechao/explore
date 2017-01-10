using System.Collections.Generic;

/***
 * Mediator.cs
 * 
 * @author  abaojin 
 */
namespace GameCore
{
    public class Mediator : Notifier, IMediator, INotifier
	{
		public static string NAME = "Mediator";

        public string mMediatorName;
        public object mViewComponent;

        public Mediator() : this(NAME, null)
        {
		}

        public Mediator(string mediatorName) : this(mediatorName, null)
        {
		}

		public Mediator(string mediatorName, object viewComponent)
		{
			mMediatorName = (mediatorName != null) ? mediatorName : NAME;
			mViewComponent = viewComponent;
		}

		public virtual IList<string> ListNotificationInterests()
		{
			return new List<string>();
		}

		public virtual void HandleNotification(INotification notification)
		{
		}

		public virtual void OnRegister()
		{
		}

		public virtual void OnRemove()
		{
		}

		public virtual string MediatorName
		{
			get {
                return mMediatorName;
            }
		}

		public virtual object ViewComponent
		{
			get {
                return mViewComponent;
            }
			set {
                mViewComponent = value;
            }
		}
       
	}
}
