using System;

/***
 * IFacade.cs
 * 
 * @author : abaojin 
 */
namespace GameCore
{
    public interface IFacade : INotifier
	{
		void RegisterProxy(IProxy proxy);

		IProxy GetProxy(string proxyName);
		
        IProxy RemoveProxy(string proxyName);

		bool HasProxy(string proxyName);

        void RegisterCommand(string notificationName, Type commandType);

        void RegisterAction<T>(string notificationName, Action<T> commandAction);

        void RemoveCommand(string notificationName);

		bool HasCommand(string notificationName);

		void RegisterMediator(IMediator mediator);

		IMediator GetMediator(string mediatorName);

        IMediator RemoveMediator(string mediatorName);

		bool HasMediator(string mediatorName);

		void NotifyObservers(INotification note);
	}
}
