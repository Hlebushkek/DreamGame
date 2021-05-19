using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayManager : MonoBehaviour
{
    public float xtime;
    const float secondsInDay = 540; //10 min InGameTime = 5 seconds
    float time;
    int day = 1;
    [SerializeField] GameObject tilemap;
    [SerializeField] TMPro.TextMeshProUGUI TextTime;
    [SerializeField] Gradient gradient;
    [SerializeField] Light2D colorLight;
    private void Update()
    {
        time += Time.deltaTime * xtime;
        TextTime.text = "Day " + day.ToString() + ":" + (TimeToHour+6).ToString() + ":" + TimeToMinute.ToString();
        colorLight.color = gradient.Evaluate(time / 500);
        if (time > secondsInDay)
        {
            time = 0;
            day++;
            tilemap.SendMessage("Grow");
        }
    }
    int TimeToHour
    {
        get {return Mathf.FloorToInt(time / 30f);}
    }
    int TimeToMinute
    {
        get {return Mathf.FloorToInt((time - TimeToHour * 30)/ 5);}
    }
}
