﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 2;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver){
            return;
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        if(transform.position.x < leftScreenEdge) {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }

        if(transform.position.x > rightScreenEdge) {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("extraLife")){
            audio.Play();
            gm.UpdateLives(1);
            Destroy(other.gameObject);
        }
    }
}
