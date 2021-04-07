using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;

    private int lifes;
    private int money;
    private int round;

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
        money = 300;
        round = 0;
    }

    public void Reset() {
        lifes = 100;
        money = 300;
        round = 0;
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
    }

    public int GetMoney() {
        return money;
    }

    public void EnemyReward(int money) {
        this.money += money;
    }
}
