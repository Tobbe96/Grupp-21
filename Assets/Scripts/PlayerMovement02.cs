using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement02 : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    //[SerializeField] private float jumpForce;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = new Vector3(Input.GetAxis("Horizontal"), body.velocity.y, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
    }
}
