using GameCore;
using System;

/***
 * GameFacade.cs
 * 
 * @author abaojin 
 */
namespace GameExplore
{
    /// <summary>
    /// Game应用门面
    /// </summary>
    public class GameFacade : Facade
    {
        static GameFacade()
        {
            Instance.RegisterCommand(NotifConst.GAME_STARTUP_APP, typeof(GameStartUpCommand));
            Instance.RegisterCommand(NotifConst.GAME_CLEARUP_APP, typeof(GameClearUpCommand));
        }

        public static void StartUp()
        {
            SendNotification(NotifConst.GAME_STARTUP_APP);
        }

        public new static void RegisterProxy(IProxy proxy)
        {
            Instance.RegisterProxy(proxy);
        }

        public new static IProxy GetProxy(string proxyName)
        {
            return Instance.GetProxy(proxyName);
        }

        public new static IProxy RemoveProxy(string proxyName)
        {
            return Instance.RemoveProxy(proxyName);
        }

        public new static bool HasProxy(string proxyName)
        {
            return Instance.HasProxy(proxyName);
        } 

        public new static void RegisterCommand(string commandName, Type type)
        {
            Instance.RegisterCommand(commandName, type);
        }

        public new static void RemoveCommand(string commandName)
        {
            Instance.RemoveCommand(commandName);
        }

        public new static bool HasCommand(string commandName)
        {
            return Instance.HasCommand(commandName);
        }

        public new static void RegisterMediator(IMediator mediator)
        {
            Instance.RegisterMediator(mediator);
        }

        public new static IMediator RemoveMediator(string mediatorName)
        {
            return Instance.RemoveMediator(mediatorName);
        }

        public new static IMediator GetMediator(string mediatorName)
        {
            return Instance.GetMediator(mediatorName);
        }

        public new static bool HasMediator(string mediatorName)
        {
            return Instance.HasMediator(mediatorName);
        }

        public new static void SendNotification(string notificationName)
        {
            Instance.SendNotification(notificationName);
        }

        public new static void SendNotification(string notificationName, object body)
        {
            Instance.SendNotification(notificationName, body);
        }

        public new static void SendNotification(string notificationName, object body, string type)
        {
            Instance.SendNotification(notificationName, body, type);
        }

        public static void ClearUp()
        {
            SendNotification(NotifConst.GAME_CLEARUP_APP);
        }
    }
}
