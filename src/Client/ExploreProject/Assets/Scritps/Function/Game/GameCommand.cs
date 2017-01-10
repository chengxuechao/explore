using GameCore;

/***
 * GameCommand.cs
 * 
 * @author abaojin
 */
namespace GameExplore
{
    /// <summary>
    /// 游戏启动命令
    /// </summary>
    public class GameStartUpCommand : MacroCommand
    {
        public override void Execute(INotification notification)
        {
            AddSubCommand(typeof(GameStartCommand));

            base.Execute(notification);
        }
    }

    /// <summary>
    /// 游戏清理命令
    /// </summary>
    public class GameClearUpCommand : MacroCommand
    {
        public override void Execute(INotification notification)
        {
            AddSubCommand(typeof(GameClearCommand));

            base.Execute(notification);
        }
    }
}

