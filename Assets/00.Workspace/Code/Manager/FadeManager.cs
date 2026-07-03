using System;
using System.Collections;
using _00.Workspace.Code.CoreSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager {
    public class FadeManager : MonoSingleton<FadeManager> {
        [SerializeField] private CanvasGroup loadPanel;
        [SerializeField] private Image fadeImage;
        [SerializeField] private TextMeshProUGUI loadText;

        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float loadTime = 1f;
        [SerializeField] private float loadingTextTime = 0.4f;

        private const string LoadTextString = "티켓 조각을 준비 중";
        
        private readonly float _fadeInAlpha = 0f;
        private readonly float _fadeOutAlpha = 1f;
        private WaitForSeconds _loadWait;
        private WaitForSeconds _loadingTextWait;
        private bool _isTextShow = false;

        protected override void Awake() {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            
            loadPanel.alpha = 0;
            loadPanel.blocksRaycasts = false;
            _loadWait = new WaitForSeconds(loadTime);
            _loadingTextWait = new WaitForSeconds(loadingTextTime);
        }

        public void MoveScene(string sceneName) {
            StartCoroutine(MoveSceneCoroutine(sceneName));
        }

        private IEnumerator MoveSceneCoroutine(string sceneName) {
            loadPanel.blocksRaycasts = true;
            yield return loadPanel.DOFade(_fadeOutAlpha, fadeDuration).WaitForCompletion();
            _isTextShow = true;
            StartCoroutine(TextCoroutine(0));
            SceneManager.LoadScene(sceneName);
            yield return _loadWait;
            _isTextShow = false;
            yield return loadPanel.DOFade(_fadeInAlpha, fadeDuration).WaitForCompletion();
            loadPanel.blocksRaycasts = false;
        }

        private IEnumerator TextCoroutine(int count) {
            if (!_isTextShow) {
                loadText.SetText("");
                yield break;
            }
            
            count %= 3;
            TextChange(count);
            yield return _loadingTextWait;
            StartCoroutine(TextCoroutine(++count));
        }

        private void TextChange(int count) {
            switch (count) {
                case 0:
                    loadText.SetText(LoadTextString + ".");
                    break;
                case 1:
                    loadText.SetText(LoadTextString + "..");
                    break;
                case 2:
                    loadText.SetText(LoadTextString + "...");
                    break;
            }
        }
    }
}