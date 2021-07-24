using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 15f;

    [SerializeField] AudioClip paddleBounce;
    [SerializeField] AudioClip blockBounce;
    [SerializeField] AudioClip wallBounce;




    Vector2 paddleToBallVector;
    bool hasStarted = false;
    AudioClip audioClip;
    AudioSource audioSource;    //If I change the volume for this, do it for the block breaking as well



    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }        
    }
    
    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            string tag = collision.gameObject.tag;

            switch (tag)
            {
                case "Player":
                    audioClip = paddleBounce;
                    break;

                case "Wall":
                    audioClip = wallBounce;
                    break;

                case "Block":
                    audioClip = blockBounce;
                    break;

                default:
                    return;
            }
            audioSource.PlayOneShot(audioClip);
        }        
    }
}
