using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float damage = 0;
    private float timeToVanish = 1f;
    private Vector2 direction = new Vector2(0, 0);
    public void FixedUpdate()
    {
        if (timeToVanish >= 0)
        {
            timeToVanish -= Time.deltaTime;
            this.transform.position += new Vector3(direction.x, direction.y, 0);
        } else Destroy(this.gameObject);
    }
    public void SetDamage(float d)
    {
        damage = d;
    }
    public void SetStraightDirection(float speed)
    {
        direction.x = speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") return;
        else if (other.gameObject.tag == "Map") Debug.Log("MAP");
        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT ENEMY"); 
            other.GetComponent<EnemyAI>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
