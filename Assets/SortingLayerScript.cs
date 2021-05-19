using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerScript : MonoBehaviour
{
    [SerializeField] private int SortingOrderBase = 5000;
    private Renderer myRenderer;
    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }
    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(SortingOrderBase - transform.position.y);
    }
}
