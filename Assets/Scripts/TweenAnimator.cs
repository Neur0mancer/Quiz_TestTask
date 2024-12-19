using DG.Tweening;
using System;
using UnityEngine;

namespace Quiz_Bezuglyi
{
    public class TweenAnimator : MonoBehaviour
    {  
        private Tween _tween;
        private Sequence _sequence;

        public void FadeIn(float duration, CanvasGroup canvasGroup)
        {
            Fade(1, duration, canvasGroup, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        public void FadeOut(float duration, CanvasGroup canvasGroup)
        {
            Fade(0, duration, canvasGroup, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        private void Fade(float endValue, float duration, CanvasGroup canvasGroup, TweenCallback onEnd)
        {            
            _tween = canvasGroup.DOFade(endValue, duration);
        }

         public void Bounce(GameObject obj, float duration, Action onAnimationComplete)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(obj.transform.DOScale(1.5f, duration).SetEase(Ease.OutBounce));
            _sequence.Append(obj.transform.DOScale(1f, duration).SetEase(Ease.OutBounce))
                .OnComplete(() =>
                {
                    onAnimationComplete?.Invoke();
                });
            _sequence.Play();
        }

        public void Shake(GameObject obj, float duration)
        {
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
                obj.transform.DOShakePosition(duration, new Vector3(1, 0, 0), 5, 90, false, true)
                    .OnComplete(() =>
                    {
                        collider.enabled = true;
                    });
            }
        }

        private void OnDisable()
        {
            _tween.Kill();
            _sequence.Kill();
        }
    }
}