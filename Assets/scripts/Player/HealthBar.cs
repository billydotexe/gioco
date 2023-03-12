using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public GameObject canvas;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    List<Image> healthBar = new List<Image>();
    public Vector2[] positions = {new Vector2(0, 0), new Vector2(20, 0),
                            new Vector2(40, 0), new Vector2(60, 0),
                            new Vector2(80, 0), new Vector2(0, -20),
                            new Vector2(20, -20), new Vector2(40, -20),
                            new Vector2(60, -20), new Vector2(80, -20)};

    public GameObject player;

    void OnEnable()
    {
        Player.OnHealthUpdate += Display;
    }

    void OnDisable()
    {
        Player.OnHealthUpdate -= Display;
    }

    void Display(int hp, int max)
    {
        bool half = hp % 2 == 1;
        int toAdd = (max / 2) - healthBar.Count;
        for (int i = 0; i < toAdd; i++)
        {
            GameObject heartGameObject = new GameObject("Heart", typeof(Image));
            heartGameObject.transform.SetParent(transform);
            heartGameObject.transform.localPosition = Vector3.zero;
            heartGameObject.GetComponent<RectTransform>().anchoredPosition = positions[healthBar.Count];
            heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 1f);
            heartGameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            Image heartImage = heartGameObject.GetComponent<Image>();
            heartImage.sprite = emptyHeart;
            healthBar.Add(heartImage);
        }
        for(int i = 0; i < healthBar.Count; i++)
        {
            if(i < hp / 2)
            {
                AddHeart(i, fullHeart);
            }
            else
            {
                AddHeart(i, emptyHeart);
            }
        }
        if (half)
        {
            AddHeart((hp/2), halfHeart);
        }
    }

    void AddHeart(int i, Sprite Image)
    {

        healthBar[i].sprite = Image;
    }

}
