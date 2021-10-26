using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoeny : MonoBehaviour
{
    [SerializeField] private MoneyItem moneyAmount;
    [SerializeField] private Sprite moneyImage;
    private void Start()
    {
        this.GetComponent<Image>().sprite = moneyImage;
        ShowMoneyAmount();
    }
    private void Update()
    {
        ShowMoneyAmount();
    }
    public void ShowMoneyAmount()
    {
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = moneyAmount.moneyAmount.ToString();
    }
    public void AddMoney(int amount)
    {
        moneyAmount.moneyAmount += amount;
        ShowMoneyAmount();
    }
    public void ReduceMoney(int amount)
    {
        moneyAmount.moneyAmount -= amount;
        ShowMoneyAmount();
    }
}
