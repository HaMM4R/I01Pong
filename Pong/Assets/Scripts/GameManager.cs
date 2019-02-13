using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Holds the player score, speed and their paddle object
    [System.Serializable]
    public struct Player
    {
        public float speed;
        [HideInInspector]
        public GameObject pPaddle; 
        int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }

    public Player player;
    public Player player2;

    public GameObject ballPrefab;
    public GameObject playerPrefab;

    bool gameOver;
    string winner; 

    GameObject[] balls = new GameObject[3];
    int numOfBalls = 0; 

    void Start()
    {
        gameOver = false; 
        createWorldBoundaries();
        spawnBalls();
        spawnPlayers(); 
    }

    void Update()
    {
        
    }

    //Creates colliders to match exact camera size
    void createWorldBoundaries()
    {
        Transform[] borders = new Transform[4];

        for(int i = 0; i < borders.Length; i++)
        { 
            borders[i] = new GameObject().transform;
            borders[i].gameObject.AddComponent<BoxCollider2D>();
            borders[i].parent = transform; 
        }

        //Postions the colliders
        borders[0].position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        borders[1].position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        borders[2].position = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        borders[3].position = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        //Scales the colliders
        borders[0].localScale = new Vector3(0.01f, Screen.height, 0.01f);
        borders[1].localScale = new Vector3(Screen.width, 0.01f, 0.01f);
        borders[2].localScale = new Vector3(0.01f, Screen.height, 0.01f);
        borders[3].localScale = new Vector3(Screen.width, 0.01f, 0.01f);

        borders[0].name = "goal1";
        borders[2].name = "goal2"; 
    }
    
    //Instantiates the players
    void spawnPlayers()
    {
        player.pPaddle = Instantiate(playerPrefab, transform.position, Quaternion.identity) as GameObject;
        player2.pPaddle = Instantiate(playerPrefab, transform.position, Quaternion.identity) as GameObject;

        var paddle1 = player.pPaddle.GetComponent<Paddle>();
        var paddle2 = player2.pPaddle.GetComponent<Paddle>();

        //Sets movement speed as well as control keys 
        player.pPaddle.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.5f, 10));
        paddle1.getStartInfo(KeyCode.W, KeyCode.S);
        paddle1.PaddleSpeed = player.speed;
        
        player2.pPaddle.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0.5f, 10));
        paddle2.getStartInfo(KeyCode.I, KeyCode.K);
        paddle2.PaddleSpeed = player2.speed;
    }

    //Spawns in all the balls but hides them for later use
    void spawnBalls()
    {
        for(int i = 0; i < balls.Length; i++)
        {
            balls[i] = Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject;
            balls[i].GetComponent<Ball>().getGameManager(gameObject);
            balls[i].SetActive(false);
        }

        balls[0].SetActive(true);
    }

    //Called by ball when it collides with a goal 
    public void goal(int p)
    {
        var player1T = player.pPaddle.transform.localScale; 
        var player2T = player2.pPaddle.transform.localScale;

        if (p == 0)
        {
            player2.Score++;
            player2T = new Vector3(player2T.x, player2T.y / 2, player2T.z);

        }
        if (p == 1)
        {
            player.Score++;
            player1T = new Vector3(player1T.x, player2T.y / 2, player1T.z);
        }

        if (numOfBalls < balls.Length - 1)
        {
            numOfBalls++;
            balls[numOfBalls].SetActive(true);
        }

        //This could definately have been cleaned up with some better logic
        if (player.Score >= 20)
            endGame(1);
        else if (player2.Score >= 20)
            endGame(2);
        
    }

    //Simple end game
    void endGame(int win)
    {
        for(int i = 0; i < balls.Length; i++)
        {
            balls[i].SetActive(false);
        }

        gameOver = true;
        winner = "Player " + win;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 20, 20), player.Score.ToString());
        GUI.Label(new Rect(Screen.width - 20, 10, 20, 20), player2.Score.ToString());

        if (gameOver)
            GUI.Box(new Rect(20,20, Screen.width - 40, Screen.height - 40), winner + "wins!");
    }
}
