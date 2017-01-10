using GameCore;
using System;
using System.Reflection;

/***
 * GameStartCommand.cs
 * 
 * @author abaojin
 */
namespace GameExplore
{
    public class GameStartCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Type[] types = GetAllType();
            int count = types.Length;
            for (int i = 0; i < count; ++i) {
                InjectObject(types[i]);
            }
        }

        private void InjectObject(Type type)
        {
            if(type == null) {
                return;
            }
            object[] attributeObjs = type.GetCustomAttributes(false);
            int count = attributeObjs.Length;
            for(int i = 0; i < count; ++i) {
                object attributeObj = attributeObjs[i];
                if (attributeObj is InjectCommand) {
                    RegisterCommand(type);
                    break;
                } else if (attributeObj is InjectMediator) {
                    RegisterMediator(type);
                    break;
                } else if (attributeObj is InjectProxy) {
                    RegisterProxy(type);
                    break;
                } else if (attributeObj is InjectController) {
                    RegisterAction(type);
                    break;
                }
            }
        }

        private void RegisterCommand(Type type)
        {
            Facade.RegisterCommand(type.Name, type);
        }

        private void RegisterAction(Type type)
        {
            MethodInfo[] methodinfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
            int count = methodinfos.Length;
            for(int i = 0; i < count; ++i) {
                MethodInfo info = methodinfos[i];
                object[] methodObjs = info.GetCustomAttributes(typeof(InjectAction), false);
                if (methodObjs.Length > 0) {
                    InjectAction injectAction = (InjectAction)methodObjs[0];
                    MethodInfo facadeInfo = Facade.GetType().GetMethod("RegisterAction");
                    facadeInfo = facadeInfo.MakeGenericMethod(new Type[] {
                        injectAction.Value
                    });
                    Type delegateType = typeof(Action<>).MakeGenericType(injectAction.Value);
                    facadeInfo.Invoke(Facade, new object[] {
                        injectAction.Key,
                        Delegate.CreateDelegate(delegateType, info)
                    });
                }
            }
        }

        private void RegisterProxy(Type type)
        {
            Facade.RegisterProxy(Activator.CreateInstance(type) as IProxy);
        }

        private void RegisterMediator(Type type)
        {
            Facade.RegisterMediator(Activator.CreateInstance(type) as IMediator);
        }

        private Type[] GetAllType()
        {
            return Assembly.GetExecutingAssembly().GetTypes();
        }
    }
}
