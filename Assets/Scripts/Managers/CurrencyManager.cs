﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages everything related to the player's currency
/// </summary>
public class CurrencyManager : MonoBehaviour
{
    // The current gold
    [SerializeField] int gold = 100;
    [SerializeField] int maxGold = 9999;
    [SerializeField] Text goldText = null;

    public int Gold { get { return gold; } }

    void OnEnable()
    {
        ShopScreen.onItemBought += OnItemBought;
        ShopScreen.onItemSold += OnItemSold;
        Enemy.onEnemyDeath += OnEnemyDeath;
    }

    void Start()
    {
        UpdateGoldText();
    }

    public void AddGold(int amount)
    {
        Mathf.Clamp(gold += amount, 0, maxGold);
        UpdateGoldText();
    }

    public void SubtractGold(int amount)
    {
        gold -= amount;
        UpdateGoldText();
    }

    public bool CanBuy(int price)
    {
        return gold >= price;
    }

    void UpdateGoldText()
    {
        goldText.text = "Gold: " + gold.ToString();
    }

    void OnItemBought(Item item)
    {
        SubtractGold(item.ItemPrice);
    }

    void OnItemSold(Item item)
    {
        AddGold(item.ItemSellPrice);
    }

    void OnEnemyDeath(Enemy enemy)
    {
        AddGold(enemy.GoldReward);
    }

    void OnDisable()
    {
        ShopScreen.onItemBought -= OnItemBought;
        ShopScreen.onItemSold -= OnItemSold;
        Enemy.onEnemyDeath -= OnEnemyDeath;
    }
}
