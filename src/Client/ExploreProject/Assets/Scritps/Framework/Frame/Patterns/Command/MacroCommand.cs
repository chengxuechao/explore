using System;
using System.Collections.Generic;

/***
 * MacroCommand.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class MacroCommand : Notifier, ICommand, INotifier
	{
        private IList<Type> mSubCommands;

        public MacroCommand()
		{
			mSubCommands = new List<Type>();
			InitMacroCommand();
		}

		public virtual void Execute(INotification notification)
		{
			while (mSubCommands.Count > 0)
			{
				Type commandType = mSubCommands[0];
				object commandInstance = Activator.CreateInstance(commandType);

				if (commandInstance is ICommand)
				{
					((ICommand) commandInstance).Execute(notification);
				}

				mSubCommands.RemoveAt(0);
			}
		}

		protected virtual void InitMacroCommand()
		{
		}

        protected void AddSubCommand(Type commandType)
		{
            mSubCommands.Add(commandType);
		}

		
	}
}
