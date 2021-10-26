using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject star1, star2;
    private float randX, randY;
    private List<GameObject> allStars1 = new List<GameObject>();
    private List<GameObject> allStars2 = new List<GameObject>();
    public bool isMoving = false;
    private bool AnimationEnd = false;
    public float speed = 0;
    private float deltaspeed = 0.002f;
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            randX = Random.Range(-17f, 17f);
            randY = Random.Range(-10f, 10f);
            allStars1.Add(Instantiate(star1, new Vector3(randX, randY, 1f), Quaternion.identity));
        }
        for (int i = 0; i < 100; i++)
        {
            randX = Random.Range(-17f, 17f);
            randY = Random.Range(-10f, 10f);
            allStars2.Add(Instantiate(star2, new Vector3(randX, randY, 1f), Quaternion.identity));
        }
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            for (int i = 0; i < allStars1.Count; i++)
            {
                if (allStars1[i].transform.position.y > 10f) allStars1[i].transform.position = new Vector3(allStars1[i].transform.position.x, -10f, allStars1[i].transform.position.z);
                else if (allStars1[i].transform.position.y < -10f) allStars1[i].transform.position = new Vector3(allStars1[i].transform.position.x, 10f, allStars1[i].transform.position.z);
                else allStars1[i].transform.position = new Vector3(allStars1[i].transform.position.x, allStars1[i].transform.position.y + speed, allStars1[i].transform.position.z);
            }
            for (int i = 0; i < allStars2.Count; i++)
            {
                if (allStars2[i].transform.position.y > 10f) allStars2[i].transform.position = new Vector3(allStars2[i].transform.position.x, -10f, allStars2[i].transform.position.z);
                else if (allStars2[i].transform.position.y < -10f) allStars2[i].transform.position = new Vector3(allStars2[i].transform.position.x, 10f, allStars2[i].transform.position.z);
                else allStars2[i].transform.position = new Vector3(allStars2[i].transform.position.x, allStars2[i].transform.position.y + speed, allStars2[i].transform.position.z);
            }
            speed += deltaspeed;
            if (speed >= 0.5f) deltaspeed = -0.005f;
            if (deltaspeed < 0f && speed < 0.5f) AnimationEnd = true;
            if (speed <= 0.1f)
            {
                isMoving = false;
            }
        }
    }
    public bool AnimationIsEnd()
    {
        return AnimationEnd;
    }
}
