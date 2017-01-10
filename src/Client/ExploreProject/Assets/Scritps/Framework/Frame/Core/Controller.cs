using System;
using System.Collections.Generic;

/***
 * Controller.cs
 * 
 * @author abaojin 
 */
namespace GameCore
{
    public class Controller : Singleton<Controller>, IController
	{
        private readonly static object mStaticSyncObject = new object();
        private readonly object mSyncObject = new object();

        private IView mView;
        private IDictionary<string, System.Object> mCommandMap;

        public Controller()
		{
            mCommandMap = new Dictionary<string, System.Object>();

			InitController();
		}

		public virtual void ExecuteCommand(INotification note)
		{
            System.Object obj = null;

			lock (mSyncObject) {
                if(!mCommandMap.TryGetValue(note.Name, out obj)) {
                    return;
                }
			}
            if(obj == null) {
                return;
            }
            if(obj is Type) {
                object command = Activator.CreateInstance(obj as Type);
                if (command is ICommand) {
                    ((ICommand)command).Execute(note);
                }
            } else {
                (obj as ActionHolder).Action(note.Body);
            }
		}

		public virtual void RegisterCommand(string notificationName, System.Object commandObj)
		{
            lock (mSyncObject) {
				if (!mCommandMap.ContainsKey(notificationName)) {
					mView.RegisterObserver(notificationName, new Observer(ExecuteCommand, this));
				}

                mCommandMap[notificationName] = commandObj;
			}
		}

		public virtual bool HasCommand(string notificationName)
		{
			lock (mSyncObject) {
				return mCommandMap.ContainsKey(notificationName);
			}
		}

		public virtual void RemoveCommand(string notificationName)
		{
			lock (mSyncObject) {
				if (mCommandMap.ContainsKey(notificationName)) {
					mView.RemoveObserver(notificationName, this);
					mCommandMap.Remove(notificationName);
				}
			}
		}

		public virtual void InitController()
		{
			mView = View.Instance;
		}

    }
}
