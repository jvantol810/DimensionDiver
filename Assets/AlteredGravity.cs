using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
public class AlteredGravity : MonoBehaviour
{
    public float gravityMultiplier;
    public bool affectProjectiles;
    public float setProjectileGravityScale;
    public LayerMask layersToAffect;
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private PlayerAnimationController playerAnim;
    private float previousGravityScale;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        makeSpriteTranslucent();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent<Rigidbody2D>(out rb2d);
        collision.gameObject.TryGetComponent<PlayerAnimationController>(out playerAnim);
        playerAnim.flipVertically(-1f);
        if (collision.gameObject.tag == "Projectile" && affectProjectiles)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = setProjectileGravityScale;
        }
        if (!((layersToAffect.value & (1 << collision.gameObject.layer)) > 0)) { return; }
        
        else
        {
            previousGravityScale = rb2d.gravityScale;
            alterGravity(gravityMultiplier, rb2d);
        }
        makeSpriteOpaque();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rb2d.gravityScale = previousGravityScale;
        playerAnim.flipVertically(1f);
        makeSpriteTranslucent();
    }

    private void alterGravity(float multiplier, Rigidbody2D rb2d)
    {
        rb2d.gravityScale *= multiplier;
    }

    private void makeSpriteTranslucent()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f);
    }

    private void makeSpriteOpaque()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
    }
}
