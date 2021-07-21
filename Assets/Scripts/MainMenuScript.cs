using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Text food, score;
    string foodCount, hiScore;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("FoodCount")) PlayerPrefs.SetInt("FoodCount", 0);
        if (!PlayerPrefs.HasKey("HiScore")) PlayerPrefs.SetInt("HiScore", 0);

        foodCount = PlayerPrefs.GetInt("FoodCount").ToString();
        hiScore = PlayerPrefs.GetInt("HiScore").ToString();
    }

    void Update()
    {
        food.text = foodCount;
        score.text = "Hi Score: " + hiScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
