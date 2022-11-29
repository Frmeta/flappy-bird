using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool enable = false;
    public float jumpForce = 10;
    Rigidbody2D rb;
    GameManager gm;
    SoundManager sm;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        sm = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enable){
            if (Input.GetKeyDown(KeyCode.Space)){
                sm.Play("wing");
                Jump();
            }
        }

        float rot = Mathf.Atan(rb.velocity.y * 1000000);
        print(rb.velocity.y);
        rb.rotation =rot;
        
    }
    public void SetEnable(bool e){
        enable = e;
        if (enable) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        } else {
            rb.bodyType = RigidbodyType2D.Static;
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        sm.Play("hit");
        gm.GameOver();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Middle")){
            sm.Play("point");
            gm.AddScore();
        }
    }
    public void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
