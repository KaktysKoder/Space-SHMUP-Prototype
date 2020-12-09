using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1}")]
    public float waveFrequency = 2.0f;                      // Число секунд полного цикла синусоиды.
    public float waveWith = 4.0f;                           // Ширина синусоиды в метрах.
    public float waveRotY = 45.0f;

    private float x0;                                       // Начальное значение координаты X.
    private float birthTime;

    private void Start()
    {
        x0 = Pos.x;                                         // Установить начальную координату Х обьекта Enemy_1.

        birthTime = Time.time;
    }

    public override void Move()
    {
        Vector3 tempPosition = Pos;

        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;   // Значение theta изменяется со временем.
        float sin = Mathf.Sin(theta);

        tempPosition.x = x0 + waveWith * sin;

        Pos = tempPosition;
       
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);    // повернуть немного относительно оси Y.
        this.transform.rotation = Quaternion.Euler(rot);

        base.Move();                                        // Обрабатывает движение вниз, вдоль оси Y.
    }
}