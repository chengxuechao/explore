/***
 * ViewBaseMediator.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public abstract class ViewBaseMediator : Mediator
    {
        public virtual void OnInit() { }

        public virtual void OnStart() { }

        public virtual void OnOpen(System.Object param) { }

        public virtual void OnClose() { }

        public virtual void OnDestroy() { }

        public abstract void AddPanel(string name);

        public abstract void DoOpen(string key, System.Object param);

        public abstract void DoActive(string name, bool isActive);

        public abstract void DoClose();
    }
}
