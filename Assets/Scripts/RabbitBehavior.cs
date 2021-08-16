using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class RabbitBehavior : MonoBehaviour
{
    [SerializeField] private bool inJump;
    private bool _isInTrigger;
    
    private float _xOffset;

    private Animator _characterAnimator;

    [FormerlySerializedAs("m_camera")] public GameManager gameManager;
    public CameraBehavior m_camera;
    
    public int jumpForce = 400;

    public Text failScore;
    public Text winScore;

    public int score;

    private static readonly int InJump = Animator.StringToHash("InJump");
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Start()
    {
        _characterAnimator = GetComponent<Animator>();
        _xOffset = transform.localPosition.x;
    }
 
    void LateUpdate()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && inJump == false) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            inJump = true;
            _characterAnimator.SetBool(InJump, true);
        }

        if (CrossPlatformInputManager.GetButtonDown("PlPref"))
        {
            PlayerPrefs.SetInt("FoodCount", 0);
            PlayerPrefs.SetInt("HiScore", 0);
        }

        if (inJump)
        {
            _characterAnimator.SetFloat(Speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

        if (score > PlayerPrefs.GetInt("HiScore"))
        {
            PlayerPrefs.SetInt("HiScore", score);
        }

        if (transform.localPosition.x < _xOffset)
        {
            transform.Translate(Vector3.right * (0.2f * Time.deltaTime));
        }
    }

    private void OnCollisionEnter2D()
    {
        if (!inJump) return;
        inJump = false;
        _characterAnimator.SetBool(InJump, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isInTrigger) return;
        _isInTrigger = true;
        if (collision.gameObject.name.Contains("Carrot"))
        {
            score++;
            Destroy(collision.gameObject);
            if (collision.gameObject.name.Contains("Speedy"))
            {
                m_camera.SpeedyCarrot();
                score += 2;
            }
            if (collision.gameObject.name.Contains("Slowy"))
            {
                m_camera.SlowCarrot();
            }
            if (collision.gameObject.name.Contains("Trap"))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * 4, ForceMode2D.Impulse);
            }
            PlayerPrefs.SetInt("FoodCount", score);
        }
        if (collision.gameObject.name.Contains("Finish")) gameManager.WinGame();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isInTrigger) return;
        _isInTrigger = false;
    }

    private void OnBecameInvisible()
    {
        if (failScore != null) failScore.text = "+ " + score;
        if (winScore != null) winScore.text = "+ " + score;
        gameManager.EndGame();
        score = 0;
    }
}
