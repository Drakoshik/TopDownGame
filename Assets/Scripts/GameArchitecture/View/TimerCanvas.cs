using DG.Tweening;
using UnityEngine;

namespace GameArchitecture.View
{
    public class TimerCanvas : MonoBehaviour
    {
        [SerializeField] private Transform _startSliderPoint;
        [SerializeField] private Transform _endSliderPoint;
        [SerializeField] private Transform _slider;

        private Sequence _timerSequence;

        public void ResetSlider(float maxDuration)
        {
            _timerSequence.Kill();
            _timerSequence = DOTween.Sequence();
            _timerSequence.AppendCallback((delegate
            {
                _slider.transform.localPosition = _startSliderPoint.transform.localPosition;
            }));
            _timerSequence.Join(_slider.DOLocalMove(_endSliderPoint.localPosition, maxDuration));
        }
    }
}
