using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private GameObject Player;
    private float TimeToVanish = 1.4f;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.gameObject.transform.eulerAngles.y == 180)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.gameObject.transform.position = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z);
        }
    }
    private void FixedUpdate()
    {
        TimeToVanish -= Time.fixedDeltaTime;
        if (TimeToVanish < 0f) Destroy(this.gameObject);
    }
}
