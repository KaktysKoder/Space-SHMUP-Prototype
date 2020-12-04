﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;

    [Header("Set Dynamically")]
    public int levelShown = 0;

    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Прочитать текущую мощность защитного поля из объекта-одиночки Него
        int currLevel = Mathf.FloorToInt(Hero.Singleton.shieldLevel);

        // Если она отличаетсяя от levelShown
        if (levelShown != currLevel)
        {
            levelShown = currLevel;

            // Скорректировать смещение в текстуре, чтобы отобразить поле с другой мощностью
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }

        // Поворачивать поле в каждом кадре с постоянной скоростью
        float rZ = -(rotationsPerSecond * Time.deltaTime * 360) % 360f;

        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
