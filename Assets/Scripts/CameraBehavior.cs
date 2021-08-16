using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    public float speed = 5;
    public float oldSpeed = 5;

    public bool gameStarted;
    public bool speedy;

    private void LateUpdate()
    {
        if (gameStarted) transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    public void StartGame()
    {
        gameStarted = true;
        StartCoroutine(AddSpeed());
    }

    public void EndGame()
    {
        gameStarted = false;
        oldSpeed = 5;
    }

    public void ResetSpeed()
    {
        speed = 5;
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