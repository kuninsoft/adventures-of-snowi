using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static readonly int Running = Animator.StringToHash("Running");
    
    public Text foodLabelGame;
    public Text hiScoreGame;
    
    public ReadLevels levelLoader;
    public bool gameStarted;
    public Canvas jumpCanvas;
    public Canvas failCanvas;
    public Canvas winCanvas;

    private AdScript _ad;
    private Animator _characterAnimator;

    private byte _level;
    private CameraBehavior _camera;
    
    private void Start()
    {
        jumpCanvas.enabled = true;
        failCanvas.enabled = false;
        winCanvas.enabled = false;
        
        _characterAnimator = GetComponentInChildren<Animator>();
        
        if (!PlayerPrefs.HasKey("FoodCount")) PlayerPrefs.SetInt("FoodCount", 0);
        if (!PlayerPrefs.HasKey("HiScore")) PlayerPrefs.SetInt("HiScore", 0);
        if (!PlayerPrefs.HasKey("PlayerProgress")) PlayerPrefs.SetInt("PlayerProgress", 101);

        _level = (byte) PlayerPrefs.GetInt("PlayerProgress");

        _ad = GameObject.FindGameObjectWithTag("Ad").GetComponent<AdScript>();
        _camera = GameObject.Find("GameObject").GetComponent<CameraBehavior>();
        
        StartGame();
    }

    private void LateUpdate()
    {
        foodLabelGame.text = PlayerPrefs.GetInt("FoodCount").ToString();
        hiScoreGame.text = "Score: " + GetComponentInChildren<RabbitBehavior>().score;
    }

    public void StartGame()
    {
        gameStarted = true;
        failCanvas.enabled = false;
        winCanvas.enabled = false;
        jumpCanvas.enabled = true;
        _characterAnimator.SetBool(Running, true);
        _camera.StartGame();
    }

    public void EndGame()
    {
        gameStarted = false;
        _characterAnimator.SetBool(Running, false);

        _camera.EndGame();
        
        if (Random.Range(0, 10) < 3) _ad.ShowNormalAd();

        if (failCanvas != null) failCanvas.enabled = true;
        if (jumpCanvas != null) jumpCanvas.enabled = false;
    }

    public void WinGame()
    {
        gameStarted = false;
        _characterAnimator.SetBool(Running, false);
        _camera.EndGame();
        _level++;
        PlayerPrefs.SetInt("PlayerProgress", _level);
        if (winCanvas != null) winCanvas.enabled = true;
        if (jumpCanvas != null) jumpCanvas.enabled = false;
    }

    public void NextLevel()
    {
        transform.position = new Vector3(-35.6f, 5.1f, -20);
        gameObject.GetComponentInChildren<RabbitBehavior>().transform.localPosition = new Vector3(-4.85f, 0, 10);
        _camera.ResetSpeed();
        for (var i = 0; i < GameObject.FindGameObjectsWithTag("Pickup").Length; i++)
            Destroy(GameObject.FindGameObjectsWithTag("Pickup")[i]);
        levelLoader.ReadLevel();
        StartGame();
    }

    public void Retry()
    {
        transform.position = new Vector3(-35.6f, 5.1f, -20);
        gameObject.GetComponentInChildren<RabbitBehavior>().transform.localPosition = new Vector3(-4.85f, 0, 10);
        _camera.ResetSpeed();
        for (var i = 0; i < GameObject.FindGameObjectsWithTag("Pickup").Length; i++)
            Destroy(GameObject.FindGameObjectsWithTag("Pickup")[i]);
        levelLoader.SpawnObjects();
        StartGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
