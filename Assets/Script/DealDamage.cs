using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private float timeStart = 0.3f;

    void Update()
    {
        if (timeStart > 0.0f)
        {
            timeStart -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButtonDown(0) && timeStart <= 0.1f)
        {
            timeStart = 0.3f;
            if (other.gameObject.tag.Equals("Enemy"))
            {
                other.gameObject.SendMessage("DealDamage1");
            }
        }
    }
}
