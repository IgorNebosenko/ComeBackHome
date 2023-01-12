using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Gps : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Camera _camera;
    [SerializeField] Transform _pointerIconTransform;
    private void FixedUpdate()
    {
        Vector3 fromPlayerToTarget = transform.position - _playerTransform.position;
        Ray ray = new Ray(_playerTransform.position, fromPlayerToTarget);
        Debug.DrawRay( _playerTransform.position,fromPlayerToTarget, Color.yellow);

        // 0 - left , 1 - Right , 2 - Down, 3 - Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistance = Mathf.Infinity;

        for(int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if(distance < minDistance)
                {
                    minDistance = distance; 
                }
            }
        }
        minDistance = Mathf.Clamp(minDistance - 1.1f, 0, fromPlayerToTarget.magnitude);
        Vector3 worldPosition = ray.GetPoint(minDistance);
        _pointerIconTransform.position = _camera.WorldToScreenPoint(worldPosition);
        
    }
}
