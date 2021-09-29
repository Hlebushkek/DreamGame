using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUsePlatformer : MonoBehaviour
{
    [SerializeField] private InventoryDisplay invD;
    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject FireSword;
    [SerializeField] private GameObject Bullet;
    private int CurrentlySelectedCell = 0;
    private float cooldownTimer = 0f;
    private void Start()
    {
        CurrentlySelectedCell = invD.GetHiglightedCell();
        Debug.Log("Current cell = " + CurrentlySelectedCell);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0)
        {
            CurrentlySelectedCell = invD.GetHiglightedCell();
            this.GetComponent<SpriteScript>().enabled = false;
            cooldownTimer = WhatIsUsedPlatformer(CurrentlySelectedCell);
        }
        if (cooldownTimer <= 0) this.GetComponent<SpriteScript>().enabled = true;
        else cooldownTimer -= Time.fixedDeltaTime;
    }
    private float WhatIsUsedPlatformer(int cellIndex)
    {
        ItemType type;
        if (cellIndex >= 0 && cellIndex < 8)
        {
            type = invD.inventory.GetItemType(cellIndex);
            Debug.Log("Type of selected item = " + type.ToString());
            switch(type)
            {
            case ItemType.Sword:
                Debug.Log(invD.inventory.GetItemDamage(cellIndex));
                Character.GetComponent<HeroAttackScript>().Attack(invD.inventory.GetItemDamage(cellIndex));
                InstantiateFireSword();
                return 1f;
            case ItemType.Gun:
                Debug.Log(invD.inventory.GetItemDamage(cellIndex));
                InstantiateBullet(cellIndex);
                return 0.5f;
            default :
                Debug.Log("Can't use item of type " + type.ToString());
                return 0;
            }
        } else return 0;
    }
    private void InstantiateFireSword()
    {
        var swordObj = Instantiate(FireSword);
        swordObj.transform.position = new Vector3 (this.gameObject.transform.position.x + 0.25f, this.gameObject.transform.position.y + 0.75f, -1);
    }
    private void InstantiateBullet(int cellIndex)
    {
        var bulletObj = Instantiate(Bullet, this.gameObject.transform.position, Quaternion.identity);
        //Vector2 bulletDirection = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - this.gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - this.gameObject.transform.position.y);
        float speed = 0.25f;
        if (this.gameObject.transform.eulerAngles.y == 180) speed = -speed;
        bulletObj.GetComponent<BulletScript>().SetStraightDirection(speed);
        bulletObj.GetComponent<BulletScript>().SetDamage(invD.inventory.GetItemDamage(cellIndex));
    }
}
