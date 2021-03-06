﻿using System.Collections;
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
    public Transform powerup;
    public Transform instructionsText;

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
            audioSources[0].Play();
            instructionsText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("bottom")){
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
            audioSources[2].Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {

        if(other.transform.CompareTag("brick"))  {
            int randChance = Random.Range(1,101);
            if(randChance < 20) {
                Instantiate(powerup, other.transform.position, other.transform.rotation);
            }

            Brick brickScript = other.gameObject.GetComponent<Brick>();
            if(brickScript.hitsToBreak > 1) {
                brickScript.BreakBrick();
            } else{

                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
      
                Destroy(newExplosion.gameObject, 2.5f); 
                gm.UpdateScore(brickScript.points);
                gm.UpdateNumberOfBricks();
                Destroy(other.gameObject);
    
                audioSources[1].Play();

            }
    
        }
    
        audioSources[0].Play();
       
    }
}
