using UnityEngine;

[ExecuteAlways]
public class Gps : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform pointerIconTransform;

    private const int CountDirections = 4;
    private const float ClampModifier = 1.1f;
    
    private void FixedUpdate()
    {
        var distanceToPlayer = transform.position - playerTransform.position;
        var ray = new Ray(playerTransform.position, distanceToPlayer);
        Debug.DrawRay( playerTransform.position,distanceToPlayer, Color.yellow);
        
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);

        var minDistance = Mathf.Infinity;

        for(var i = 0; i < CountDirections; i++)
        {
            if (!planes[i].Raycast(ray, out var distance)) 
                continue;
            if(distance < minDistance)
                minDistance = distance;
        }
        minDistance = Mathf.Clamp(minDistance - ClampModifier, 0, distanceToPlayer.magnitude);
        var worldPosition = ray.GetPoint(minDistance);
        pointerIconTransform.position = camera.WorldToScreenPoint(worldPosition);
        
    }
}
