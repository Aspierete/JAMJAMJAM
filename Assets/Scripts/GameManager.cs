using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI roundGoalText;
    [SerializeField] private TextMeshProUGUI earnedMoneyText;

    public int earnedMoney { get; private set; }
    public int roundGoal { get; private set; } = 400;

    public static GameManager Instance { get; private set; }


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

    public void RoundResult()
    {
        if (earnedMoney >= roundGoal)
        {
            print("win");
        }
        else
        {
            print("lose");
        }
    }
}
