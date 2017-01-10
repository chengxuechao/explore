using GameCore;

/***
 * BagGetBagCommand.cs
 * 
 * @author abaojin
 */ 
namespace GameExplore
{
    [InjectCommand]
    public class BagGetBagCommand : SimpleCommand
    {
        public static new string NAME = "BagGetBagCommand";

        public override void Execute(INotification notification)
        {
            Log.Error("===: " + notification.Name);
        }
    }
}