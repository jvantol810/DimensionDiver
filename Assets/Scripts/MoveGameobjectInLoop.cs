using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameobjectInLoop : MonoBehaviour
{
    public List<ObjectPath> paths;
    private Rigidbody2D rb2d;
    private float loopTime;
    private float loopTimer;
    private int activePath = 0;
    [System.Serializable]
    //An object path contains the direction the object should move, the duration it should move, and at what speed
    public struct ObjectPath {
        public Vector2 direction;
        public float duration;
        public float speed;
        public bool flipSprites;
        public ObjectPath(Vector2 direction, float duration, float speed, bool flipSprites)
        {
            this.direction = direction;
            this.duration = duration;
            this.speed = speed;
            this.flipSprites = flipSprites;
        }
        public void MoveAlongPath(GameObject obj)
        {
            obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
            Vector2 posChange = new Vector2(direction.x, direction.y) * Time.fixedDeltaTime * speed;
            rb.MovePosition(new Vector2(rb.position.x + posChange.x, rb.position.y + posChange.y));
            obj.TryGetComponent<SpriteRenderer>(out SpriteRenderer objSprite);
            objSprite.flipX = flipSprites;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        loopTime = paths[activePath].duration;
        loopTimer = loopTime;
    }

    private void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
        RunLoop();
        paths[activePath].MoveAlongPath(gameObject);
        
    }

    private void RunLoop()
    {
        if (loopTimer > 0f)
        {
            loopTimer -= Time.deltaTime;
            if (loopTimer < 0f)
            {
                IncrementPath();
            }
        }
    }

    private void IncrementPath()
    {
        if (activePath > paths.Count-2) { activePath = 0; }
        else { activePath++; }
        loopTime = paths[activePath].duration;
        loopTimer = loopTime;
    }
}