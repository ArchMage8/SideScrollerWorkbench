using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float distance = 0.5f;
    public float speed = 1.0f;

    private Vector2 initialPosition;
    private Vector2 tempPos = new Vector2();

    private void Awake()
    {
        // Store the initial position as a Vector2
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the position based on the initial position and the sine wave
        tempPos.x = initialPosition.x;
        tempPos.y = initialPosition.y + Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * distance;
        transform.position = tempPos;
    }
}
