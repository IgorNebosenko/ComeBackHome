using System;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 movementVector;
    [SerializeField]float period = 10f;
    float movementFactor;
    [SerializeField][Range(0, 1)] float factor;
    
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle;
        if (period <= float.Epsilon)
            return;
        cycle = Time.time / period;
        float tau = Mathf.PI * 2;
        float MoveSin = Mathf.Sin(cycle * tau);
        movementFactor = (MoveSin + 1) / 2;
        factor = movementFactor;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
        
    }
}
