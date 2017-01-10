using System;

/**
 * IController.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public interface IController
    {
        void RegisterCommand(string notificationName, System.Object commandType);

		void ExecuteCommand(INotification notification);

		void RemoveCommand(string notificationName);

		bool HasCommand(string notificationName);
	}
}
