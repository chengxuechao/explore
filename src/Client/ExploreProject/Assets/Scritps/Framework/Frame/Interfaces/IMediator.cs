using System.Collections.Generic;

/**
 * IMediator.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public interface IMediator
	{
		string MediatorName { get; }
		
		object ViewComponent { get; set; }

        IList<string> ListNotificationInterests();
		
		void HandleNotification(INotification notification);
		
		void OnRegister();

		void OnRemove();
	}
}
