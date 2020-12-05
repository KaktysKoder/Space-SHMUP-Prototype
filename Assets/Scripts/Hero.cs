using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero Singleton;

    [Header("Set in Inspector")]
    public float speed = 30.0f;
    public float rollMult = -45.0f;
    public float pitchMult = 30;

    [Header("Set Dynamically")]
    public float shieldLevel = 1;

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
    }
}