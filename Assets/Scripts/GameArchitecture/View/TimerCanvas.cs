using DG.Tweening;
using UnityEngine;

namespace GameArchitecture.View
{
    public class TimerCanvas : MonoBehaviour
    {
        [SerializeField] private Transform _startSliderPoint;
        [SerializeField] private Transform _endSliderPoint;
        [SerializeField] private Transform _slider;

        public void ResetSlider(float maxDuration)
        {
            _slider.transform.localPosition = _startSliderPoint.transform.localPosition;
            _slider.DOLocalMove(_endSliderPoint.localPosition, maxDuration);
        }
    }
}
