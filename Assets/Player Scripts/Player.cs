using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    public bool isGrounded = true;
    private float movement;
    public float moveSpeed = 5f;
    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (movement > 0 && !facingRight)
        {
            facingRight = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (movement < 0 && facingRight)
        {
            facingRight = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump();
            isGrounded = false;
            animator.SetBool("Jump", true);
        }
        if (Mathf.Abs(movement) > .1f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if (movement < .1f)
        {
            animator.SetFloat("Run", 0f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(movement * 0.1f, 0, 0) * Time.deltaTime * moveSpeed;
    }
    void jump()
    {
        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator .SetBool("Jump", false);
        }
        
    }
}
