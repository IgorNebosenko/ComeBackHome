using UnityEngine;

namespace CBH.UI
{
    public class CanvasContainer : MonoBehaviour
    {
        public Transform viewContainer;
        public Transform popupContainer;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
