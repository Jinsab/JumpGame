using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rigid;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        BlockStatus block = collision.gameObject.GetComponent<BlockStatus>();

        if (collision.CompareTag("Util Block") && Input.GetKey(KeyCode.E))
        {
            if (block.type == "Portal Enter")
            {
                Vector3 anotherProtalPos = block.portal.transform.position;
                collision.transform.position = anotherProtalPos;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        BlockStatus block = collision.gameObject.GetComponent<BlockStatus>();
         
        if (collision.collider.CompareTag("Util Block") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            if (block.type == "Up")
            {
                Vector2 upVelocity = new Vector2(0, block.value);
                //collision.collider.attachedRigidbody.velocity = upVelocity;
                rigid.velocity = upVelocity;
            }
        }
    }
}
