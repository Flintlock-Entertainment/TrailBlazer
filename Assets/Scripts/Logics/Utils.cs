using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static Func<int> GenerateDice(int d)
    {
        return () =>
        {
            int roll = UnityEngine.Random.Range(1, d + 1);
            MenuManager.Instance.AddLog($"rolled 1d{d}: {roll}\n");
            return roll;
        };
    }
    public static Func<int> d4 = GenerateDice(4);
    public static Func<int> d6 = GenerateDice(6);
    public static Func<int> d8 = GenerateDice(8);
    public static Func<int> d10 = GenerateDice(10);
    public static Func<int> d12 = GenerateDice(12);
    public static Func<int> d20 = GenerateDice(20);
    public static Func<int> d100 = GenerateDice(100);

    public static int CheckRoll()
    {
        int roll = d20(); 
        if(roll == 20)
        {
            roll += 10;
            
        }
        else if(roll == 1)
        {
            roll -= 10;
        }
        return roll;
    }



    public static OutCome CalculateOutCome(int roll, int DC)
    {
        switch (roll - DC)
        {
            case int n when n >= 10:
                MenuManager.Instance.AddLog("Crit success!\n");
                return OutCome.CritSuccess;
            case int n when n >= 0:
                MenuManager.Instance.AddLog("Success!\n");
                return OutCome.Success;
            case int n when n > -10:
                MenuManager.Instance.AddLog("Fail!\n");
                return OutCome.Fail;
            default:
                MenuManager.Instance.AddLog("Crit fail!\n");
                return OutCome.CritFail;
        }
    }


}

public enum OutCome
{
    CritFail = 0,
    Fail = 1,
    Success = 2,
    CritSuccess = 3
}
