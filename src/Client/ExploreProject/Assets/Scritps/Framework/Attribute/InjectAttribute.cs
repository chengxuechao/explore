using System;

/***
 * InjectAttribute.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectProxy : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class InjectCommand : Attribute
    {
        
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class InjectController : Attribute
    {
        
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAction : Attribute
    {
        private System.Object key;
        private Type value;

        public InjectAction(System.Object key, Type value)
        {
            this.key = key;
            this.value = value;
        }

        public string Key 
        {
            get {
                return key.ToString();
            }
        }

        public Type Value 
        {
            get {
                return value; 
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class InjectMediator : Attribute
    {

    }

}

