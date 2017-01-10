/***
 * SocketState.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    /// <summary>
    /// 当前网络状态
    /// </summary>
    public enum NetState
    {
        // 链接上
        Connected,
        // 断开链接
        Disconnect,
        // 网络不稳定
        Unsteadiness,
        // 网络异常
        Exception,
        // 链接超时
        TimeOut,
        // 账号异地登陆或服务器异常
        OtherLogOrSvrException,
    }

    /// <summary>
    /// Socket连接异常状态
    /// </summary>
    public enum SocketErrorCode
    {
        // 连接被拒绝
        ConnectionRefused = 10061,
        // 连接超时
        ConnectionTimeout = 10060,
        // 网络不通
        HostNotNetwork = 10051,
    }

    /// <summary>
    /// 服务器类型
    /// </summary>
    public enum ServerType
    {
        None,
        Login,
        Game,
    }
}
