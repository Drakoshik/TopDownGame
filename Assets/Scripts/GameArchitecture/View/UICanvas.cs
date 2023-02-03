using UnityEngine;

namespace GameArchitecture.View
{
    public class UICanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _backInTimeText;
        [SerializeField] private GameObject _title;

        public void ShowBackInTime()
        {
            _backInTimeText.SetActive(true);
        }
        
        public void HideBackInTime()
        {
            _backInTimeText.SetActive(false);
        }
        public void ShowTitle()
        {
            _title.SetActive(true);
        }
        
        public void HideTitle()
        {
            _title.SetActive(false);
        }
    }
}
