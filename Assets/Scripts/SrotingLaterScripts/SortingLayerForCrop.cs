using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerForCrop : MonoBehaviour
{
    [SerializeField] private int SortingOrderBase = 5000;
    private Renderer myRenderer;
    private CropGrow growScript;
    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        growScript = gameObject.GetComponent<CropGrow>();
    }
    private void LateUpdate()
    {
        if (growScript.GetCropState() == 0) myRenderer.sortingOrder = 100;
        else myRenderer.sortingOrder = (int)(SortingOrderBase - transform.position.y);
    }
}
