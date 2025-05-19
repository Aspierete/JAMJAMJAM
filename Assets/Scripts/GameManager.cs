using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int earnedMoney;

    [SerializeField] private TextMeshProUGUI roundGoalText;
    [SerializeField] private TextMeshProUGUI earnedMoneyText;
    [SerializeField] private int roundGoal = 400;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RoundGoalText();
    }


    public void EarnMoney(int deliveredPartAmount)
    {
        earnedMoney += deliveredPartAmount * 100;
        EarnedMoneyUpdate(earnedMoney);
    }

    private void EarnedMoneyUpdate(int money)
    {
        earnedMoneyText.SetText(money.ToString());
    }

    private void RoundGoalText()
    {
        roundGoalText.SetText(roundGoal.ToString());
    }
}
