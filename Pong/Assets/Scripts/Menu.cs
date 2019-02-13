using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Menu : MonoBehaviour
{
    public Button start; 

    void Start()
    {
        start.onClick.AddListener(startGame);
    }

    void startGame()
    {
        SceneManager.LoadScene("Pong"); 
    }
}
