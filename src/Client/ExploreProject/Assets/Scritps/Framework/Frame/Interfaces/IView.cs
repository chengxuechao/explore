/**
 * IView.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public interface IView
	{
		void RegisterObserver(string notificationName, IObserver observer);

		void RemoveObserver(string notificationName, object notifyContext);

		void NotifyObservers(INotification note);

		void RegisterMediator(IMediator mediator);

		IMediator GetMediator(string mediatorName);

        IMediator RemoveMediator(string mediatorName);
		
		bool HasMediator(string mediatorName);

	}
}
