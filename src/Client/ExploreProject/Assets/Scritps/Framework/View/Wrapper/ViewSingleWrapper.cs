/***
 * ViewSingleWrapper.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class ViewSingleWrapper<T> : ViewSingleMediator
    {
        public static void Open()
        {
            ViewManager.Open<T>(null, null);
        }

        public static void Open(System.Object param)
        {
            ViewManager.Open<T>(null, param);
        }

        public static void SetActive(bool isActive)
        {
            ViewManager.SetActive<T>(null, isActive);
        }

        public static void Close()
        {
            ViewManager.Close<T>();
        }
    }
}
