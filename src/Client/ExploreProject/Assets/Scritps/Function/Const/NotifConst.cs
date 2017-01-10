/***
 * NotifConst.cs
 * 
 * @author abaojin
 */
namespace GameExplore
{
    /// <summary>
    /// 定义通知常量注意事项：
    /// 
    /// 1. 常量命名命名全大写加下划线分割
    /// 2. 常量值必须和通知的类名相同
    /// </summary>
    public static class NotifConst
    {
        // 启动游戏
        public const string GAME_STARTUP_APP = "GameStartCommand";
        // 清理游戏
        public const string GAME_CLEARUP_APP = "GameClearCommand";
    }
}