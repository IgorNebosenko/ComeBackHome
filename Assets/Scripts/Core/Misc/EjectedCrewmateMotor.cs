using UnityEngine;

namespace CBH.Core
{
    public class EjectedCrewmateMotor : MonoBehaviour
    {
        private float _timePassed;

        private const float HalfRadius = 180f;
        private const float QuartRadius = 90f;
        private const float RotationTimeCycle = 8.5f;
        private const float SpeedInSecond = 60f;
        
        private void Update()
        {
            _timePassed += Time.deltaTime;
            
            transform.eulerAngles = Vector3.Lerp(Vector3.left * HalfRadius, Vector3.right * HalfRadius, 
                _timePassed % RotationTimeCycle / RotationTimeCycle) + Vector3.up * QuartRadius;

            transform.position += Vector3.right * Time.deltaTime * SpeedInSecond;
        }
    }
}
