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
        int currLevel = Mathf.FloorToInt(Hero.Singleton.shieldLevel);       // Прочитать текущую мощность защитного поля из объекта-одиночки Него.

        if (levelShown != currLevel)                                        // Если она отличаетсяя от levelShown.
        {
            levelShown = currLevel;

            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);      // Скорректировать смещение в текстуре, чтобы отобразить поле с другой мощностью.
        }

        float rZ = -(rotationsPerSecond * Time.deltaTime * 360) % 360f;     // Поворачивать поле в каждом кадре с постоянной скоростью.

        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}