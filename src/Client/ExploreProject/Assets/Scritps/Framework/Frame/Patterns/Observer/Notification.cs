/**
 * Notification.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameCore
{
    public class Notification : INotification
	{
        private string mName;
        private string mType;
        private object mBody;


        public Notification(string name) : this(name, null, null)
		{ }

        public Notification(string name, object body) : this(name, body, null)
		{ }

        public Notification(string name, object body, string type)
		{
			mName = name;
			mBody = body;
			mType = type;
		}

        public virtual string Name 
        {
            get {
                return mName;
            }
        }

        public virtual object Body 
        {
            get {
                return mBody;
            }
            set {
                mBody = value;
            }
        }

        public virtual string Type 
        {
            get {
                return mType;
            }
            set {
                mType = value;
            }
        }

        public override string ToString()
		{
			string msg = "Notification Name: " + Name;
			msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
			msg += "\nType:" + ((Type == null) ? "null" : Type);
			return msg;
		}
		
	}
}
