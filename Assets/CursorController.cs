using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D mouseDefault;
    [SerializeField] private Texture2D mouseOnReadyCrop;
    private enum CursorType
    {
        Default,
        OnReadyCrop,
        OnEnemy,
        OnNPC
    }
    private CursorType currentCursor;
    public void Start()
    {
        currentCursor = CursorType.Default;
        SetDefaultCursor();
    }
    private void Update()
    {
        CastRay();
    }
    private void CastRay()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -10;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, mousePosition, 20);
        //Debug.DrawRay(mousePosition, mousePosition - Camera.main.ScreenToWorldPoint(mousePosition),Color.blue);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "CropObject" && hit.collider.gameObject.GetComponent<CropGrow>().IsCropGrow())
            {
                if (currentCursor != CursorType.OnReadyCrop)
                {
                    Debug.Log("CropObject");
                    currentCursor = CursorType.OnReadyCrop;
                    Cursor.SetCursor(mouseOnReadyCrop, Vector2.zero, CursorMode.ForceSoftware);
                }
            }
            else if (hit.collider.gameObject.tag == "Player")
            {
                if (currentCursor != CursorType.OnNPC)
                {
                    Debug.Log("Player");
                    currentCursor = CursorType.OnNPC;
                    SetDefaultCursor();
                }
            }
            else if (hit.collider.gameObject.tag == "Enemy")
            {
                if (currentCursor != CursorType.OnEnemy)
                {
                    Debug.Log("Enemy");
                    currentCursor = CursorType.OnEnemy;
                    SetDefaultCursor();
                }
            }
            else if (currentCursor != CursorType.Default)
            {
                Debug.Log("Default");
                currentCursor = CursorType.Default;
                SetDefaultCursor();
            }
        }
    }
    private void SetDefaultCursor()
    {
        Cursor.SetCursor(mouseDefault, Vector2.zero, CursorMode.ForceSoftware);
    }
}
