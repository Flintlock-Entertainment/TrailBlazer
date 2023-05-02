using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static Func<int> GenerateDice(int d)
    {
        return () => UnityEngine.Random.Range(1, d + 1);
    }

    public static Func<int> d4 = GenerateDice(4);
    public static Func<int> d6 = GenerateDice(6);
    public static Func<int> d8 = GenerateDice(8);
    public static Func<int> d10 = GenerateDice(10);
    public static Func<int> d12 = GenerateDice(12);
    public static Func<int> d20 = GenerateDice(20);
    public static Func<int> d100 = GenerateDice(100);

}

public enum OutCome
{
    CritFail = 0,
    Fail = 1,
    Success = 2,
    CritSuccess = 3
}
