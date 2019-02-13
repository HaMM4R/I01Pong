using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D ball;
    public GameObject gameManager; 

    //Force is added to give it non straight direction then speed is set
    void Start()
    {
        ball = gameObject.GetComponent<Rigidbody2D>();
        ball.AddForce(new Vector2(-40, 10), ForceMode2D.Force);
        ball.velocity = ball.velocity.normalized * 6;
    }

    public void getGameManager(GameObject g)
    {
        gameManager = g; 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var gameM = gameManager.GetComponent<GameManager>();

        //Make sure ball doesn't stop after hitting other ball

        ball.velocity = ball.velocity.normalized * 6;
        if (collision.gameObject.name == "goal1")
            gameM.goal(0);
        if (collision.gameObject.name == "goal2")
            gameM.goal(1);
    }
}
