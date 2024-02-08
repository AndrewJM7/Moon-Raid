using UnityEngine;

public class PushWall : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    private Vector3 targetPosition;
    public bool started = false;

    void Start()
    {
        // Front of the room
        targetPosition = transform.position + new Vector3(0f, 0f, -10f);
        MoveTowardsTarget();
    }

    void Update()
    {
        if (started)
        {
            MoveTowardsTarget();
        }
    }

    public void MoveTowardsTarget()
    {
        // Pushes the wall towards the player to force the player into the game
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    public void Started()
    {
        started = true;
    }
}
