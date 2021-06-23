using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    public float yScale = 1f;
    [SerializeField]
    private string ystate, xstate;
    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb2d.velocity;
        UpdateVelocityState();
        UpdateAnimator();
    }

    //This function sets the xstate and ystate, which can be useful to determine what is happened to the player
    //It will also adjust the horizontal scale of the character, allowing it to face right and left.
    private void UpdateVelocityState()
    {
        if (velocity.y > 0f)
        {
            ystate = "Jumping";
        }
        else if (velocity.y < 0f)
        {
            ystate = "Falling";
        }
        else
        {
            ystate = "Landed";
        }
        if (velocity.x > 0f)
        {
            xstate = "Moving Right";
            if (transform.localScale.x != 1f)
            {
                transform.localScale = new Vector3(1f, yScale, 1f);
            }
        }
        else if (velocity.x < 0f)
        {
            xstate = "Moving Left";
            if (transform.localScale.x != -1f)
            {
                transform.localScale = new Vector3(-1f, yScale, 1f);
            }
        }
        else
        {
            xstate = "Still";
        }
    }

    //This function updates the Animator Component
    //It first determines if jumping or falling, because that takes precedence over moving left and right
    //If this setup and animator controller's setup is confusing, feel free to ask me about it!
    private void UpdateAnimator()
    {
        if (ystate != "Landed")
        {
            animator.SetFloat("VelocityY", velocity.y);
            if (animator.GetFloat("VelocityX") != 0f)
            {
                animator.SetFloat("VelocityX", 0f);
            }
        }
        else
        {
            if (animator.GetFloat("VelocityY") != 0f)
            {
                animator.SetFloat("VelocityY", 0f);
            }

            animator.SetFloat("VelocityX", Mathf.Abs(velocity.x));
        }
    }

    public void flipVertically(float direction)
    {
        yScale = direction;
        transform.localScale = new Vector3(transform.localScale.x, yScale, 1f);
    }

    //Public Getter Functions for the string state values
    public string GetStateX()
    {
        return xstate;
    }

    public string GetStateY()
    {
        return ystate;
    }
}
