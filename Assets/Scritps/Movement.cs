using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(moveSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Move(-moveSpeed);
        }
    }

    void Move(float speed)
    {
        rb.MovePosition(rb.position + Vector2.up * (speed * Time.deltaTime));
    }
}
