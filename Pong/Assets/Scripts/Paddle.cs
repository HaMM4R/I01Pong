using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    float paddleSpeed = 0;

    public float PaddleSpeed
    {
        get { return paddleSpeed; }
        set { paddleSpeed = value; }
    }

    float moveSpeed; 
    Transform paddleTransform;

    KeyCode up;
    KeyCode down; 

    void Start()
    {
        paddleTransform = transform;
    }
    
    //Gets the movement keys from the game manager 
    public void getStartInfo(KeyCode u, KeyCode d)
    {
        up = u;
        down = d; 
    }

    void Update()
    {
        playerInput(); 
        move();
    }

    //Changes movement speed depending on direction 
    void playerInput()
    {
        if (Input.GetKey(up))
            moveSpeed = Mathf.Abs(paddleSpeed);
        else if (Input.GetKey(down))
            moveSpeed = -paddleSpeed;
        else
            moveSpeed = 0;
    }

    void move()
    {
        //May have been better not to have this in the constant update loop but only call when button pressed 
        paddleTransform.position = Vector2.Lerp((Vector2)paddleTransform.position, new Vector2(paddleTransform.position.x, paddleTransform.position.y + moveSpeed), Time.deltaTime);
    }
}
