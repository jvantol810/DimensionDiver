using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public Variables for customization
    public bool useDefaultOptions; //If this is true, then default settings will be applied
    public float jumpCooldownTimer; //How long they must wait before jumping again
    public float horizontalSpeed; //How fast the character can move vertically
    public float jumpPower; //How high the player can jump
    //Public Variables for Gun Mechanic
    //When the player fires the harpoon, we simply swap the arms out by making on active and the other inactive.
    public GameObject GunArm;
    public GameObject LeftArm;

    //Private Variables
    private Rigidbody2D rb2d;
    private PlayerAnimationController animController;
    private HarpoonController harpoonController;
    private float horizontal;
    [SerializeField]
    private float jumpCooldown;

    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animController = GetComponent<PlayerAnimationController>();
        harpoonController = GetComponent<HarpoonController>();
        if (useDefaultOptions)
        {
            //CHANGE THESE TO INTERNAL VARIABLES!
            jumpCooldownTimer = 2f;
            horizontalSpeed = 750f;
            jumpPower = 20f;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        RunCooldowns();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (animController.GetStateY() == "Landed" && jumpCooldown <= 0f)
            {
                jumpCooldown = jumpCooldownTimer;
                rb2d.AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            harpoonController.SetHoldingF(true);
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            harpoonController.SetHoldingF(false);
        }
    }

    private void FixedUpdate()
    {
        //With ForceMode2D.Force, it requires a bigger value to get the results you want.
        Vector2 hforce = new Vector2(650f * horizontal * Time.deltaTime, 0);
        rb2d.AddForce(hforce, ForceMode2D.Force);
    }

    //This function is where the logic for cooldowns is handled
    //Currently: the jumpcooldown will be decremented here.
    private void RunCooldowns()
    {
        if(jumpCooldown > 0f)
        {
            jumpCooldown -= Time.deltaTime;
            if(jumpCooldown < 0f)
            {
                jumpCooldown = 0f;
            }
        }
    }
}
