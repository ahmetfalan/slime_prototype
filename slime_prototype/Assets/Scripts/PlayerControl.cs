using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    public LayerMask whatIsGround;
    public float jumpForce;
    public float speed;
    Rigidbody2D rb;
    public float distance = 1.0f;
    public float delayInSeconds = 0.5f;
    public bool canJump = true;
    public bool controlIsActive = false;
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (/*controlIsActive ==*/true)
        {
            if (Input.GetKeyDown("w"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
                StartCoroutine(ShootDelay());
            }
            float x = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(x * speed, rb.velocity.y, 0f);
            rb.velocity = move;
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        //transform.Rotate(0, 0, speed * Time.deltaTime, Space.Self);
        if (Input.GetKey("d"))
        {
            rb.rotation -= 0.1f;
        }
        if (Input.GetKey("a"))
        {
            rb.rotation += 0.1f;
        }
    }
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, whatIsGround);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        canJump = true;
    }
}
