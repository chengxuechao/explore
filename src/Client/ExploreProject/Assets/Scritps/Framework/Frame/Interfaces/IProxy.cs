/**
 * IProxy.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public interface IProxy
    {
		string ProxyName { get; }

		object Data { get; set; }

		void OnRegister();

		void OnRemove();
    }
}
