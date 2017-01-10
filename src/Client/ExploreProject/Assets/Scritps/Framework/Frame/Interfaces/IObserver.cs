using System;

/**
 * IObserver.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public interface IObserver
    {
		Action<INotification> NotifyMethod { set; }

		object NotifyContext { set; }

        void NotifyObserver(INotification notification);
		
		bool CompareNotifyContext(object obj);
    }
}
