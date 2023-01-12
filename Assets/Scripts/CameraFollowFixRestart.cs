using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFixRestart : MonoBehaviour
{
    [SerializeField] private GameObject targetFollowPrefab;
    private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        CheckLinkFollowTarget();
    }
    
    private void CheckLinkFollowTarget()
    {
        if (targetFollowPrefab == null)
        {
            targetFollowPrefab = GameObject.FindGameObjectWithTag("Player");

            if (targetFollowPrefab != null)
            {
                if (_virtualCamera.Follow == null)
                    _virtualCamera.Follow = targetFollowPrefab.transform;
            }
        }
    }
}
