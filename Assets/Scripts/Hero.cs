using System;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero Singleton;

    [Header("Set in Inspector")]
    public float speed = 30.0f;
    public float rollMult = -45.0f;
    public float pitchMult = 30;
    public float gameRestartDelay = 2.0f;
    public float projectileSpeed = 40.0f;

    [Header("Set reference")]
    public GameObject projectilePrefab;

    [Header("Set Dynamically")]
    [SerializeField] private float _shieldLevel = 4;

    private GameObject lastTriggetGo = null;                                            // Эта переменная хранит ссылку на последний столкнувшейся игровой объект.
    private float factor = 0.6f;                                                        // Множитель для ускорения.

    /// <summary>
    /// Защитное поле.
    /// </summary>
    public float ShieldLevel
    {
        get => _shieldLevel;

        set
        {
            _shieldLevel = Mathf.Min(value, 4);

            // Если уровень защиты космического коробля меньше нуля, уничтожает Hero - т.е главного героя.
            if (value < 0)
            {
                Destroy(this.gameObject);

                // Сообщить объекту Main.S о необходимости перезапустить игру
                Main.Singleton.DelayedRestart(gameRestartDelay);
            }
        }
    }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");                                      // Извлечь информацию из класса Input.
        float yAxis = Input.GetAxis("Vertical");

        Vector3 heroPosition = transform.position;                                      // Изменить transform.position, опираясь на информацию по осям.

        heroPosition.x += xAxis * speed * Time.deltaTime;
        heroPosition.y += yAxis * speed * Time.deltaTime;

        transform.position = heroPosition;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);  // Повернуть корабль, чтобы придать ощущение динамизма.

        // Ускорение главного героя на Crt + (W, A, S, D)
        if (Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
        {
            FlightAcceleration(heroPosition, factor, xAxis, yAxis);
        }

        // Позволить коаблю выстрелить.

    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        //print($"Triggered: {go.name}.");

        if (go == lastTriggetGo) return;                                                // Гарантировать невозможность повторного столкновения с тем же объектом.

        lastTriggetGo = go;

        if (go.CompareTag("Enemy"))                                                     // Если защитное поле столкнулось с вражеским кораблём,
        {
            ShieldLevel--;                                                              // уменьшить его защиту, 

            Destroy(go);                                                                // и уничтожитьт.
        }
        else
        {
            print($"Triggered by non-Enemy: {go.name}");
        }
    }

    /// <summary>
    /// Ускорения движения главного героя.
    /// </summary>
    /// <param name="position">Позиция charactera/Hero</param>
    /// <param name="factor">Множитель для ускорения.</param>
    /// <param name="xAxis">Ось X</param>
    /// <param name="yAxis">Ось Y</param>
    private void FlightAcceleration(Vector3 position, float factor, float xAxis, float yAxis)
    {
        position.x += xAxis * (speed * factor) * Time.deltaTime;
        position.y += yAxis * (speed * factor) * Time.deltaTime;

        transform.position = position;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }
}