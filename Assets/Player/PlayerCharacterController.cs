using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : TimeReversal
{
    public float JumpForce = 400f;
    public float MoveForce = 8f;
    [Range(0, .3f)] public float MovementSmoothing = .05f;
    public LayerMask GroundLayer;
    public bool isFacingRight = true;
    public bool isDead = false;
    public bool isJumpPressed = false;

    private Vector3 currentVelocity = Vector3.zero;
    private Animator animator;
    private CircleCollider2D groundCollider;

    // MARK:- TIME REVERSAL METHODS
    protected override void AdditionalStart()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("HSpeed", 0);
        animator.SetFloat("VSpeed", 0);
        animator.SetBool("Grounded", true);

        groundCollider = GetComponent<CircleCollider2D>();
    }

    protected override void AdditionalUpdate()
    {
        CheckIfDead();

        if (!isReversingTime & !isDead)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            isJumpPressed = Input.GetButtonDown("Jump");


            UpdateLocation(horizontalMovement);
        }

    }

    protected override void StoreData()
    {
        GetComponent<Animator>().enabled = true;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        objectDataStore.Insert(0, new ObjectData(transform.position, transform.rotation, objectRigidbody.velocity, sprite, isDead, isFacingRight));
    }

    protected override void ApplyData(ObjectData objectData)
    {
        transform.position = objectData.position;
        transform.rotation = objectData.rotation;
        objectRigidbody.velocity = objectData.velocity;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = objectData.sprite;
        isDead = objectData.isDead;

        if (isFacingRight != objectData.isFacingRight)
        {
            Flip();
        }
        isFacingRight = objectData.isFacingRight;
    }

    protected override void ReversalStopped()
    {
        isReversingTime = false;
        MakeKinematic(false);
        GetComponent<Animator>().Play("Idle");
    }

    // MARK:- PRIVATE METHODS
    private void UpdateLocation(float horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(horizontalMovement * MoveForce, objectRigidbody.velocity.y);
        objectRigidbody.velocity = Vector3.SmoothDamp(objectRigidbody.velocity, targetVelocity, ref currentVelocity, MovementSmoothing);

        animator.SetFloat("HSpeed", Mathf.Abs(objectRigidbody.velocity.x));
        animator.SetFloat("VSpeed", objectRigidbody.velocity.y);

        if (horizontalMovement > 0 && !isFacingRight)
        {
            Flip();
        }
        if (horizontalMovement < 0 && isFacingRight)
        {
            Flip();
        }
        if (groundCollider.IsTouchingLayers(GroundLayer))
        {
            animator.SetBool("Grounded", true);
            if (isJumpPressed)
            {
                objectRigidbody.AddForce(new Vector2(0f, JumpForce));
                isJumpPressed = false;
                animator.SetTrigger("Jump");
            }
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void CheckIfDead()
    {
        if (isDead)
        {
            animator.SetBool("Dead", true);
        } else
        {
            animator.SetBool("Dead", false);
        }
    }
}
