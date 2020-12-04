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
        // Извлечь информацию из класса Input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Изменить transform.position, опираясь на информацию по осям
        Vector3 heroPosition = transform.position;

        heroPosition.x += xAxis * speed * Time.deltaTime;
        heroPosition.y += yAxis * speed * Time.deltaTime;

        transform.position = heroPosition;

        // Повернуть корабль, чтобы придать ощущение динамизма
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }
}