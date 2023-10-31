using Monnaie;
using UnityEngine;

public class TestKillian : MonoBehaviour
{
    public void ChangeMoneyAmount(int amount)
    {
        MoneySystem.AddMoney?.Invoke(amount);
    }
}
