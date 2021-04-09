using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager _instance;

    private int lifes;
    private int money;
    private int round;
    private int towerCost;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }

    private GameManager() {
        lifes = 100;
        money = 100;
        round = 0;
        towerCost = 50;
    }

    public void Reset() {
        lifes = 100;
        money = 100;
        round = 0;
        towerCost = 50;
    }

    public int GetRound() {
        return round;
    }

    public void RoundUp() {
        round++;
        money += 100;
    }

    public int GetLifes() {
        return lifes;
    }

    public void TakeDamage(int damage) {
        lifes -= damage;

        if (lifes <= 0) {
            Reset();
            WaveSpawner.EnemiesAlive = 0;
            SceneManager.LoadScene("YouLoseScene");
        }
    }

    public int GetMoney() {
        return money;
    }

    public void PurchaseTower() {
        this.money -= this.towerCost;
        this.towerCost += 10;
    }

    public void EnemyReward(int money) {
        this.money += money;
    }

    public int GetTowerCost() {
        return towerCost;
    }
}
