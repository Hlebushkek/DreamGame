using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Date
{
    public int year;
    public Season season;
    public int day;
    public Date(int y, Season s, int d)
    {
        year = y;
        season = s;
        day = d;
    }
}
public enum Season
{
    Summer,
    Winter    
}
