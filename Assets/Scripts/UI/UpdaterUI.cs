using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quiz_Bezuglyi
{
    public class UpdaterUI : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private GameObject _replayPanel;
        [SerializeField]
        private Button _replayButton;
        [SerializeField]
        private GameObject _messageWindow;
        [SerializeField]
        private Button _messageButton;
        [SerializeField]
        private CanvasGroup _fadePanel;

        public event Action OnMessageWindowClosed;

        private TweenAnimator _tweenAnimator;
        private CanvasGroup _messageWindowCanvas;

        private void Start()
        {
            _replayButton.onClick.AddListener(() =>
            {
                FadeOutAndRestart(3f);
            });
            _messageButton.onClick.AddListener(() =>
            {
                _messageWindow.SetActive(false);
                OnMessageWindowClosed?.Invoke();
            });

            _tweenAnimator = GetComponent<TweenAnimator>();
            _messageWindowCanvas = _messageWindow.GetComponent<CanvasGroup>();

            _replayPanel.SetActive(false);
            _text.gameObject.SetActive(true);
            _tweenAnimator.FadeOut(1f, _fadePanel);
            _tweenAnimator.FadeIn(1f, _messageWindowCanvas);
        }
        public void UpdateText(string text)
        {
            _text.text = text;
        }
        public void ShowReplay()
        {
            _replayPanel.SetActive(true);
        }
        public void DisableText()
        {
            _text.gameObject.SetActive(false);
        }
        private void FadeOutAndRestart(float duration)
        {
            StartCoroutine(FadeOutAndRestartCoroutine(duration));
        }
        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        private IEnumerator FadeOutAndRestartCoroutine(float duration)
        {
            _tweenAnimator.FadeIn(duration, _fadePanel);
            yield return new WaitForSeconds(duration);
            RestartGame();
        }
    }
}