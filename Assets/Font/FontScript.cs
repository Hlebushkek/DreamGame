using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontScript : MonoBehaviour
{
    public Texture2D charSheet;
    private Sprite[] charSprites;
    public string[] TextLine;
    [SerializeField]
    private Material material1;
    [SerializeField]
    float[] x;
    [SerializeField]
    float[] y;
    private Dictionary<char, CharData> charData;
    private List<Vector3> CoordOfTextWithEffect1 = new List<Vector3>();
    private List<GameObject> TextWithEffect1 = new List<GameObject>();
    private List<Vector3> CoordOfTextWithEffect2 = new List<Vector3>();
    private List<GameObject> TextWithEffect2 = new List<GameObject>();
    private char[] chars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    private int activeEffect = 0;
    private float deltax = 0f;
    private Color textcolor = Color.white;
    public int currentTextLine = -1;
    private float time = 0;
    void Start()
    {
        GetSubsprite();
        CreatDictionary();
    }
    void FixedUpdate()
    {
        time += Time.deltaTime;
        SmoothMoveEffect();
        ShakingEffect();
        if (Input.GetKeyUp(KeyCode.Return) && time > 0.1f || time > 5f)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            TextWithEffect1.Clear();
            TextWithEffect2.Clear();
            currentTextLine++;
            if (currentTextLine < TextLine.Length)
            {
                ConvertString(TextLine[currentTextLine], x[currentTextLine], y[currentTextLine]);
            }
            foreach (Transform child in transform)
            {
                var tempcolor = child.gameObject.GetComponent<SpriteRenderer>().material.color;
                tempcolor.a = 0;
                child.gameObject.GetComponent<SpriteRenderer>().material.color = tempcolor;
            }
            time = 0.0f;
        }
        foreach (Transform child in transform)
        {
            var tempcolor = child.gameObject.GetComponent<SpriteRenderer>().material.color;
            if (tempcolor.a < 1f)
            {   
                tempcolor.a += 0.01f;
                child.gameObject.GetComponent<SpriteRenderer>().material.color = tempcolor;
            }
        }
    }
    public void GetSubsprite()
    {
        Sprite[] subsprites = Resources.LoadAll<Sprite>(charSheet.name);
        charSprites = subsprites;
    }
    public void CreatDictionary()
    {
        charData = new Dictionary<char, CharData>();
        for (int i = 0; i < 25; i++)
        {
            charData.Add(chars[i], new CharData(5, charSprites[i]));
        }
    }
    public struct CharData
    {
        public int width;
        public Sprite sprite;
        public CharData(int width, Sprite sprite)
        {
            this.width = width;
            this.sprite = sprite;
        }
    }
    public void ConvertString(string MyString, float x, float y)
    {
        for (int i = 0, k = 0; i < MyString.Length; i++, k++)
        {
            if (MyString[i] == '<' && MyString[i + 1] == 'n')
            {
                k = -1;
                y -= 2;
                i+=2;
            }
            else if (MyString[i] == '<')
            {
                i = ReadTag(MyString, i);
                k--;
            }
            else if (MyString[i] == ' ') continue;
            else //Create Letter GameObject
            {
                var gameObjLetter = new GameObject();
                gameObjLetter.transform.parent = gameObject.transform;
                var spriteRenderer = gameObjLetter.AddComponent<SpriteRenderer> ();
                spriteRenderer.sprite = charData[MyString[i]].sprite;
                spriteRenderer.material = material1;
                spriteRenderer.color = textcolor;
                gameObjLetter.transform.position = new Vector3(k + x, y, 0f);
                if (activeEffect == 1)
                {
                    TextWithEffect1.Add(gameObjLetter);
                    CoordOfTextWithEffect1.Add(gameObjLetter.transform.position);
                }
                else if (activeEffect == 2)
                {
                    TextWithEffect2.Add(gameObjLetter);
                    CoordOfTextWithEffect2.Add(gameObjLetter.transform.position);
                }
            }
        }
    }
    int ReadTag(string fullline, int i)
    {
        i++;
        if (fullline[i] == 'c')
            {
                switch (fullline[i+1])
                {
                case '1':
                    textcolor = Color.red;
                    break;
                case '2':
                    textcolor = Color.green;
                    break;
                case '3':
                    textcolor = Color.blue;
                    break;
                default :
                    Debug.Log("Incorrect Color");
                    break;
                }
                i+=2;
            }
        else if (fullline[i] == '/' && fullline[i+1] == 'c')
            {
                textcolor = Color.white;
                i+=2;
            }
        else if (fullline[i] == 'e')
            {
                switch (fullline[i+1])
                {
                case '1':
                    activeEffect = 1;
                    break;
                case '2':
                    activeEffect = 2;
                    break;
                default :
                    Debug.Log("Incorrect effect");
                    break;
                }
                i+=2;
            }
        else if (fullline[i] == '/' && fullline[i+1] == 'e')
            {
                activeEffect = 0;
                i+=2;
            }
        return i;
    }
    void SmoothMoveEffect()
    {
        if (TextWithEffect1.Count > 0)
        {
            deltax += 0.25f;
            for (int i = 0; i < TextWithEffect1.Count; i++)
            {
                TextWithEffect1[i].transform.position = new Vector3(TextWithEffect1[i].transform.position.x, CoordOfTextWithEffect1[i].y + 0.5f * Mathf.Sin(deltax));
            }
            if (deltax > 360.0f) deltax = 0.0f;
        }
    }
    void ShakingEffect()
    {
        if (TextWithEffect2.Count > 0)
        {
            for (int i = 0; i < TextWithEffect2.Count; i++)
            {
                Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * 4f);
                newPos.x += CoordOfTextWithEffect2[i].x;
                newPos.y += CoordOfTextWithEffect2[i].y;
                newPos.z = 0f;
                TextWithEffect2[i].transform.position = newPos;
            }
        }
    }
}
