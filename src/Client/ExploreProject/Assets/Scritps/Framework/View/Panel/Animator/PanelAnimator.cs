using UnityEngine;

/***
 * PanelAnimator.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    /// <summary>
    /// 界面动画控制器
    /// 
    /// 具体项目具体实现
    /// </summary>
    public class PanelAnimator : MonoBehaviour
    {
        public Animator animator;

        public void Play(string name)
        {
            if(animator == null) {
                return;
            }
            animator.Play(name);
        }

        public AnimationClip GetClip(string name)
        {
            if(animator == null) {
                return null;
            }
            AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
            for (int i = 0; i < clips.Length; ++i){
                if (clips[i].clip.name.Equals(name)) {
                    return clips[i].clip;
                }
            }
            return null;
        }

        public void AddAnimationEvent(string name, string action, float time)
        {
            AnimationClip clip = GetClip(name);
            if(clip == null) {
                return;
            }
            AnimationEvent e = new AnimationEvent();
            if (time < 0) {
                e.time = 0;
            } else if(time > clip.length) {
                e.time = clip.length;
            } else {
                e.time = time;
            }
            e.functionName = action;
            clip.AddEvent(e);
        }

        public void AddStartEvent(string name, string action)
        {
            this.AddAnimationEvent(name, action, -1);
        }

        public void AddEndEvent(string name, string action)
        {
            this.AddAnimationEvent(name, action, int.MaxValue);
        }

    }
}

