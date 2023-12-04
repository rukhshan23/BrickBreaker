using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    //inPlay false, ball should be on paddle
    public bool inPlay;
    public Transform paddle;

    public Transform explosion;

    public GameManager gm;

    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPlay){
            transform.position = paddle.position;

            if(Input.GetButtonDown("Jump")){
                inPlay = true;
                rb.AddForce(Vector2.up  * speed);
            }
        }

        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bottom"))
        {
            Debug.Log("Ball hit the bottom of the screen.");
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }


    void OnCollisionEnter2D(Collision2D other){
        if(other.transform.CompareTag("Brick")){
            Transform newExplosion =Instantiate(explosion, other.transform.position, other.transform.rotation);
            Debug.Log("Ball hit a brick.");
            Destroy(newExplosion.gameObject,2.5f);
            gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            Destroy(other.gameObject);
        }
    }
}
