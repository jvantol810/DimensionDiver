using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Public Variables to customize
    public float maxHealth;
    public float speed;
    public float minX, maxX; //The maximum path it can take in the x direction.

    //Public Variables
    public GameObject HPMeter;
    public GameObject HPBar;

    //Private Variables
    private Rigidbody2D rb2d;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject playerstats = GameObject.Find("PlayerStatController");
            playerstats.GetComponent<PlayerStats>().TakeDamage(2);
        }
    }

    public void TakeDamage(int amount)
    {
        health = Mathf.Clamp(health - Mathf.Abs(amount), 0, maxHealth);
        if(health != maxHealth)
        {
            HPBar.SetActive(true);
        }
        float ratio;
        if (health == 0)
        {
            ratio = 0;
            StartCoroutine(DeathAnimation());
        }
        else
        {
            ratio = health / maxHealth;
        }
        
        HPMeter.transform.localScale = new Vector3(0.95f * (ratio), 0.6f, 1f);
    }

    private IEnumerator DeathAnimation()
    {
        rb2d.isKinematic = true;
        //Running out of time! I'll leave this here for anyone else to implement.
        yield return new WaitForSeconds(.2f);
        GameObject.Destroy(gameObject);
    }
}
