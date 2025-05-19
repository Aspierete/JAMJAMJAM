using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private float roundDuration = 5f;
    [SerializeField] private Image timerImage;

    private bool isRoundStarted;
    void Start()
    {
        Time.timeScale = 0f;
        timeLeft = roundDuration;
    }

    // Update is called once per frame
    void Update()
    {
        RoundStart();
    }

    void RoundStart()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRoundStarted)
        {
            Time.timeScale = 1f;
            isRoundStarted = true;
        }

        if (isRoundStarted && timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;

            var timeLeftPercentage = timeLeft / roundDuration;
            timerImage.fillAmount = timeLeftPercentage;
        }
        else
        {
            // TODO: round end logic should goes here. 
            isRoundStarted = false;
            timeLeft = roundDuration;
            timerImage.fillAmount = roundDuration; 
        }
    }
}
