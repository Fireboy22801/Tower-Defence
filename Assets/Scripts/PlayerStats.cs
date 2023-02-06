using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 300;

    public static int Lives;
    public static int startLives = 3;
    public static int Rounds;

    public TMP_Text moneyText;
    public TMP_Text livesText;
    public TMP_Text roundsText;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }

    private void Update()
    {
        moneyText.text = "$" + Money;
        livesText.text = Lives + " LIVES";
        roundsText.text = Rounds + " Round";
    }

    public void TakeDamage(int damage)
    {
        Lives -= damage;
        if (Lives < 0)
            Lives = 0;
    }
}
