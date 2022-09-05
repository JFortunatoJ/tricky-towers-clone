using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiniclipTrick.Utility
{
    public class FadeController : SingletonBehaviour<FadeController>
    {
        private CanvasGroup _canvasGroup;

        protected override void SetupInstance()
        {
            if (!TryGetComponent(out Canvas canvas))
            {
                gameObject.AddComponent<Canvas>();
            }
            
            if (!TryGetComponent(out GraphicRaycaster graphicRaycaster))
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }
            
            if (!TryGetComponent(out Image image))
            {
                image = gameObject.AddComponent<Image>();
                image.color = Color.clear;
            }
            
            
            if (!TryGetComponent(out _canvasGroup))
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.alpha = 0;
            }
        }

        public void FadeIn(float duration = 0.5f, Action onFadeComplete = null)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(0, duration).SetUpdate(true).onComplete = () =>
            {
                _canvasGroup.blocksRaycasts = false;
                onFadeComplete?.Invoke();
            };
        }

        public void FadeOut(float duration = 0.5f, Action onFadeComplete = null)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1, duration).SetUpdate(true).onComplete = () => onFadeComplete?.Invoke();
        }

        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, Action callback = null,
            float fadeDuration = 0.5f)
        {
            FadeOut(fadeDuration, onFadeComplete: () => StartCoroutine(LoadSceneCoroutine(sceneName, fadeDuration, mode, callback)));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName, float fadeDuration, LoadSceneMode mode,
            Action callback)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, mode);
            async.allowSceneActivation = false;

            while (async.progress < .9f)
            {
                yield return null;
            }

            async.allowSceneActivation = true;
            callback?.Invoke();

            while (!async.isDone)
            {
                yield return null;
            }

            FadeIn(fadeDuration, onFadeComplete: () => Resources.UnloadUnusedAssets());
        }
    }
}