using UnityEngine;

public class GameManagerHandler : MonoBehaviour
{
    [SerializeField] private static GameManagerHandler gameManager;
    private void Awake()
    {
        if (gameManager != null)
            Destroy(gameObject);
        else
        {
            gameManager = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
