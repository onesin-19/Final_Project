using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats 
{

    public static float initialHp = 100;
    public static int startingLevel = 1;
    public static int damage = 3;
    public static int initScore = 0;
    
    public static int Score { get; set; }
    public static bool IsPlayerDead { get; set; }
    public static bool HasSaveSurvivor { get; set; } = true;

    public static int RequireNbAntidote { get; set; } = 2;
    public static int NbAntidote { get; set; } = 0;
    
    public static bool HasPlayed { get; set; }

    //private static int currency = 100;
    private static float hp;
    private static int currentLevel;
    public static int ammunition;
    public static int charge;

    //No need to implement init for now... resetAllStats() does it.

    public static void initialiazeScore()
    {
        Score = initScore;
    }
    public static void resetAllStats()
    {
        IsPlayerDead = false;
        //Currency = initialCurrency;
        Hp = initialHp;
        CurrentLevel = startingLevel;
        HasSaveSurvivor = true;
        RequireNbAntidote = 5;
        NbAntidote = 0;

    }

    /*public static int Currency
    {
        get { return currency; }
        private set { currency = value; }
    }*/

    public static float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public static int CurrentLevel
    {
        get { return currentLevel; }
        private set { currentLevel = value; }
    }

    public static void addCurrency(int amount)
    {
        Score += amount;
    }
    
    public static void addHp(float amount)
    {
        Hp += amount;
    }

    public static void decrementHp()
    {
        if (--Hp == 0)
        {
            IsPlayerDead = true;
        }
    }

    public static void subtractHp(int amount)
    {
        if (hp - amount <= 0)
        {
            hp = 0;
            //IsPlayerDead = true;
        }
        else
        {
            hp -= amount;
        }
    }
    
}
