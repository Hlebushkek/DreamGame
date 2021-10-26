using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private Dictionary<int, Vector2> schedule = new Dictionary<int, Vector2>()
    {
        {1, new Vector2(7, 5)},
        {140, new Vector2(0, 5)},
        {240, new Vector2(0, 0)}
    };
    private DayManager dayMng;
    private float curTime;
    private bool isMoving = false;
    private Vector2 moveGoal;
    private void Awake()
    {
        dayMng = FindObjectOfType<DayManager>();
    }
    private void Update()
    {
        curTime = dayMng.getTime();
        Action();
        if (isMoving)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, moveGoal, 0.5f);
        }
    }
    private void Action()
    {
        if (schedule.ContainsKey((int)curTime))
        {
            moveGoal = schedule[(int)curTime];
            isMoving = true;
        }
    }
}
