using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript1 : MonoBehaviour
{
    [SerializeField]
    private FontScript fontscript1;
    [SerializeField]
    private StarGenerator stargenerator1;
    [SerializeField]
    private GameObject Planet, SpaceShip;
    private GameObject p1;
    private bool createPlanet = true;
    void FixedUpdate()
    {
        if (fontscript1.currentTextLine == fontscript1.TextLine.Length)
        {
            stargenerator1.isMoving = true;
        }
        if (stargenerator1.AnimationIsEnd() && createPlanet)
        {
            p1 = Instantiate(Planet, new Vector3 (-12f, -13f, 0f), Quaternion.identity);
            createPlanet = false;
        }
        if (stargenerator1.isMoving && !createPlanet)
        {
            p1.transform.position = new Vector3 (p1.transform.position.x, p1.transform.position.y + 0.15f);
        }
    }
}
