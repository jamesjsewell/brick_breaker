using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public GameManager gm;
    AudioSource audio;
    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver){
            inPlay = false;
            rb.velocity = Vector2.zero;
            return;
        }

        if(!inPlay){
            transform.position = paddle.position;
        }

        if(Input.GetButtonDown("Jump") && !inPlay){
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
            audioSources[3].Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("bottom")){
            Debug.Log("Ball hit the bottom of the screen");
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
            audioSources[2].Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {


        
        if(other.transform.CompareTag("brick"))  {
            Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);
            gm.UpdateScore(1);
            gm.UpdateNumberOfBricks();
            Destroy(other.gameObject);

            audioSources[1].pitch = Random.Range(.2f,.4f);
            audioSources[1].Play();
            return;
        }
        audioSources[0].pitch = Random.Range(1.5f,2.5f);
        audioSources[0].Play();
       
    }
}
