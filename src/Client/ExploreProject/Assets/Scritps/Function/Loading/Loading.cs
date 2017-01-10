using System.Collections;
using UnityEngine;

/***
 * Loading.cs
 * 
 * @author abaojin
 */
namespace GameExplore
{
    public class Loading : MonoBehaviour
    {
        public UISlider Slider;
        public UILabel Label;

        private float progress;

        private void Awake()
        {
            if (null == Slider || null == Label) {
                Debug.LogError("Loading slider or label is null.");
                return;
            }
            Slider.value = 0.0f;
            Label.text = "0%";
        }

        private void Start()
        {
            StartCoroutine(StartLoading(SceneManager.TargetName));
        }

        private void Update()
        {
            if (null == Slider || null == Label) {
                return;
            }
            Slider.value = progress;
            Label.text = string.Format("{0}%", (int)(progress * 100));
        }

        private void SetLoadingPercentage(int percentage)
        {
            progress = percentage / 100.0f;
        }

        private IEnumerator StartLoading(string name)
        {
            int displayProgress = 0;
            int toProgress = 0;
            AsyncOperation op = Application.LoadLevelAsync(name);
            op.allowSceneActivation = false;
            while (op.progress < 0.9f) {
                toProgress = (int)op.progress * 100;
                while (displayProgress < toProgress) {
                    ++displayProgress;
                    SetLoadingPercentage(displayProgress);
                    yield return new WaitForEndOfFrame();
                }
            }

            toProgress = 100;
            while (displayProgress < toProgress) {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
            op.allowSceneActivation = true;
        }
    }
}