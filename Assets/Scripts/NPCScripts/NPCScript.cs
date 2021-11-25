using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private List<NPCSchedule> schedule = new List<NPCSchedule>();
    private DayManager dayMng;
    private float curTime;
    private bool isMoving = false;
    private Vector2 moveStart, moveGoal;
    private float moveSpeed = 0.02f;
    private int currentAffair = 0;
    private void Awake()
    {
        dayMng = FindObjectOfType<DayManager>();
    }
    private void Update()
    {
        curTime = dayMng.getTime();
        Action();
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, moveGoal, moveSpeed);
        }
    }
    private void Action()
    {
        if (schedule.Count > currentAffair && schedule[currentAffair].time == (int)curTime)
        {
            Debug.Log((int)curTime);
            moveStart = this.transform.position;
            moveGoal = schedule[currentAffair].endPos;
            isMoving = true;
            currentAffair++;
        }
    }
}
[System.Serializable]
public struct NPCSchedule
{
    public float time;
    public Vector2 endPos;
    public NPCAffairs affairs;
}
public enum NPCAffairs
{
    Idle,
    Walk,
    Work,
}