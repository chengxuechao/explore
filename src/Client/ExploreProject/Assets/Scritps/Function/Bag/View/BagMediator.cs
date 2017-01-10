using GameCore;

/***
 * BagMediator.cs
 * 
 * @author abaojin
 */
namespace GameExplore
{
    [InjectMediator]
    public class BagMediator : ViewSingleWrapper<BagMediator>
    {
        public override void AddPanel(string name)
        {
            base.AddPanel(name);
        }
    }
}

