using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    private static readonly int Running = Animator.StringToHash("Running");

    public ReadLevels levelLoader;

    public Canvas jumpCanvas;
    public Canvas failCanvas;
    public Canvas winCanvas;

    public Text foodLabelGame;
    public Text hiScoreGame;

    public float speed = 5;
    public float oldSpeed = 5;

    public bool gameStarted;
    public bool speedy;
    public bool endlessLevel;

    private AdScript _ad;
    private Animator _characterAnimator;

    private byte _level;
    
    private void Start()
    {
        _characterAnimator = GetComponentInChildren<Animator>();
        jumpCanvas.enabled = true;
        failCanvas.enabled = false;
        winCanvas.enabled = false;

        if (!PlayerPrefs.HasKey("FoodCount")) PlayerPrefs.SetInt("FoodCount", 0);
        if (!PlayerPrefs.HasKey("HiScore")) PlayerPrefs.SetInt("HiScore", 0);
        if (!PlayerPrefs.HasKey("PlayerProgress")) PlayerPrefs.SetInt("PlayerProgress", 101);

        _level = (byte) PlayerPrefs.GetInt("PlayerProgress");

        _ad = GameObject.FindGameObjectWithTag("Ad").GetComponent<AdScript>();

        StartGame();
    }

    private void LateUpdate()
    {
        if (gameStarted) transform.Translate(Vector3.right * (speed * Time.deltaTime));
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
        StartCoroutine(AddSpeed());
    }

    public void EndGame()
    {
        gameStarted = false;
        oldSpeed = 5;
        _characterAnimator.SetBool(Running, false);

        if (Random.Range(0, 10) < 3) _ad.ShowNormalAd();

        if (failCanvas != null) failCanvas.enabled = true;
        if (jumpCanvas != null) jumpCanvas.enabled = false;
    }

    public void WinGame()
    {
        gameStarted = false;
        oldSpeed = 5;
        _characterAnimator.SetBool(Running, false);
        _level++;
        PlayerPrefs.SetInt("PlayerProgress", _level);
        if (winCanvas != null) winCanvas.enabled = true;
        if (jumpCanvas != null) jumpCanvas.enabled = false;
    }

    public void NextLevel()
    {
        transform.position = new Vector3(-35.6f, 5.1f, -20);
        gameObject.GetComponentInChildren<RabbitBehavior>().transform.localPosition = new Vector3(-4.85f, 0, 10);
        speed = 5.0f;
        if (endlessLevel)
            for (var i = 0; i < GameObject.FindGameObjectsWithTag("Block").Length; i++)
                Destroy(GameObject.FindGameObjectsWithTag("Block")[i]);
        for (var i = 0; i < GameObject.FindGameObjectsWithTag("Pickup").Length; i++)
            Destroy(GameObject.FindGameObjectsWithTag("Pickup")[i]);
        levelLoader.ReadLevel();
        StartGame();
    }

    public void Retry()
    {
        transform.position = new Vector3(-35.6f, 5.1f, -20);
        gameObject.GetComponentInChildren<RabbitBehavior>().transform.localPosition = new Vector3(-4.85f, 0, 10);
        speed = 5.0f;
        if (endlessLevel)
            for (var i = 0; i < GameObject.FindGameObjectsWithTag("Block").Length; i++)
                Destroy(GameObject.FindGameObjectsWithTag("Block")[i]);
        for (var i = 0; i < GameObject.FindGameObjectsWithTag("Pickup").Length; i++)
            Destroy(GameObject.FindGameObjectsWithTag("Pickup")[i]);
        levelLoader.SpawnObjects();
        StartGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SpeedyCarrot()
    {
        if (speedy) return;
        StopCoroutine(AddSpeed());
        oldSpeed = speed;
        StartCoroutine(SpeedyCarrotAddSpeed());
    }

    public void SlowCarrot()
    {
        speed -= 0.5f;
    }

    private IEnumerator AddSpeed()
    {
        while (gameStarted)
        {
            speed += 0.03f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SpeedyCarrotAddSpeed()
    {
        speedy = true;
        speed += 5f;
        yield return new WaitForSeconds(1f);
        speed = oldSpeed;
        StartCoroutine(AddSpeed());
        speedy = false;
    }
}