using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.UI.Menu.Components
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button buttonInstance;
        [SerializeField] private TMP_Text levelNumberText;
        [SerializeField] private TMP_Text bestTime;
        [SerializeField] private GameObject borderImage;

        private Action _cachedEvent;
        
        public void Init(Action onClick, int levelId, string bestTimeText, bool isInteractable)
        {
            _cachedEvent = onClick;
            
            buttonInstance.onClick.AddListener(() => _cachedEvent?.Invoke());
            levelNumberText.text = $"{levelId + 1}";
            
            bestTime.text = bestTimeText;
            bestTime.gameObject.SetActive(isInteractable);
            
            buttonInstance.interactable = isInteractable;
            borderImage.SetActive(isInteractable);
        }

        private void OnDestroy()
        {
            if (_cachedEvent != null)
                buttonInstance.onClick.RemoveListener(() => _cachedEvent?.Invoke());
        }
    }
}