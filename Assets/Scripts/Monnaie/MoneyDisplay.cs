using TMPro;
using UnityEngine;

namespace Monnaie
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyCount;

        private void OnEnable()
        {
            MoneySystem.UpdateMoney += OnMoneyUpdated;
        }

        private void OnDisable()
        {
            MoneySystem.UpdateMoney -= OnMoneyUpdated;
        }

        private void OnMoneyUpdated(int newAmount)
        {
            _moneyCount.text = newAmount.ToString();
        }
    }
}
