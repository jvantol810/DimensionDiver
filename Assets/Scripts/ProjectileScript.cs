using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //Public Variables for Customization
    public float speed;
    public float bulletdrop;
    public bool shotByPlayer;

    //Private Variables
    private Rigidbody2D rb2d;
    private Vector2 projectileForce;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (transform.localScale.x < 0f)
        {
            rb2d.AddForce(new Vector2(-1f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(new Vector2(1f, 0f), ForceMode2D.Impulse);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(shotByPlayer)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                //Grab Enemy Health and deal damage, or whatever feels right!
                //Debug.Log("Hit");
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(5);
            }
        }
    }
}
