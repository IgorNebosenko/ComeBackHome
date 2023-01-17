using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 10f;
    
    private float movementFactor;

    private void Start()
    {
        startPos = transform.position;
    }
    
    private void Update()
    {
        if (period <= float.Epsilon)
            return;
        
        var cycle = Time.time / period;
        var tau = Mathf.PI * 2;
        var MoveSin = Mathf.Sin(cycle * tau);
        movementFactor = (MoveSin + 1) / 2;
        
        var offset = movementVector * movementFactor;
        transform.position = startPos + offset;
        
    }
}
