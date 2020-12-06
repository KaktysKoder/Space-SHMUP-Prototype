using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Singleton;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;          // Массив шаблонов Enemy

    public float enemySpawnPerSecond = 0.5f;    // Спавн вражеских короблей в секунду.
    public float enemyDefaultPadding = 1.5f;    // Отступ для позиционирования.

    private BoundsCheck bndCheck;


    private void Awake()
    {
        Singleton = this;

        bndCheck = GetComponent<BoundsCheck>();             // Chache ref.

        Invoke("SpawnEnemy", 1.0f / enemySpawnPerSecond);   // Возов SpawnEnemy 1 раз в 2 секунды (default values).
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);    // Выбор случайного Enemy шаблона для создания.

        GameObject go = Instantiate(prefabEnemies[ndx]);

        // Разместить вражеский корабль над экраном в случайной позиции х
        float enemyPadding = enemyDefaultPadding;

        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;                         // Установить начальные координаты созданного вражеского корабля

        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;

        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;

        go.transform.position = pos;

        Invoke("SpawnEnemy", 1.0f / enemySpawnPerSecond);
    }
}