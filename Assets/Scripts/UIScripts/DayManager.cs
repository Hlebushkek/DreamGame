using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayManager : MonoBehaviour
{
    [SerializeField] private float xtime;
    private const float secondsInDay = 540; //10 min InGameTime = 5 seconds
    private float time;
    private Date date = new Date(2400, Season.Summer, 1);
    [SerializeField] GameObject tilemap;
    [SerializeField] TMPro.TextMeshProUGUI TextTime;
    [SerializeField] Gradient gradient;
    [SerializeField] Light2D colorLight;
    private void Update()
    {
        time += Time.deltaTime * xtime;
        TextTime.text = "Day " + date.day.ToString() + ":" + (TimeToHour+6).ToString() + ":" + TimeToMinute.ToString();
        colorLight.color = gradient.Evaluate(time / 500);
        if (time > secondsInDay)
        {
            time = 0;
            date.day++;
            tilemap.SendMessage("Grow"); //Rework it
        }
    }
    private int TimeToHour
    {
        get {return Mathf.FloorToInt(time / 30f);}
    }
    private int TimeToMinute
    {
        get {return Mathf.FloorToInt((time - TimeToHour * 30)/ 5);}
    }
    public Date getDate()
    {
        return date;
    }
    public float getTime()
    {
        return time;
    }
}
