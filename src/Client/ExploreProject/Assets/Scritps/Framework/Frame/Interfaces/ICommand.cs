/**
 * ICommand.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public interface ICommand
    {
		void Execute(INotification notification);
    }
}
