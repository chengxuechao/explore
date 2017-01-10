using GameCore;
using UnityEngine;

/***
 * InputManager.cs
 * 
 * @author abaojin
 */
namespace GameExplore
{
    public class InputManager : Singleton<InputManager>
    {
        /// <summary>
        /// 物理帧更新
        /// </summary>
        public void FixedUpdate()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            ViewInput();
            SkillInput();
#endif
            InteractInput();
        }

        void ViewInput()
        {
            if (Input.GetKeyDown(KeyCode.P)) {
                BagMediator.Open();
            }
        }

        /// <summary>
        /// 技能处理
        /// </summary>
        void SkillInput()
        {

        }

        /// <summary>
        /// 快捷键释放技能的接口
        /// </summary>
        /// <param name="code">Code.</param>
        private void CastSkill(KeyCode code)
        {

        }

        /// <summary>
        /// 交互对象处理
        /// </summary>
        void InteractInput()
        {
            if (UICamera.hoveredObject != null) {
                if (UICamera.hoveredObject.GetComponent<Collider>() != null) {
                    return;
                }
            }
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            if(Camera.main == null)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#else
            if (Input.GetMouseButtonDown(0)) {
                if (Camera.main == null) {
                    return;
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, -1)) {
                    GameObject hitObj = hit.collider.gameObject;
                    if (MainPlayer.Instance != null) {
                        MainPlayer.Instance.SetPosition(hit.point);
                    }
                }
            }
        }
    }
}
