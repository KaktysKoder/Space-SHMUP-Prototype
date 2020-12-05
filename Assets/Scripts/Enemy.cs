using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 10.0f;         // Скорость в м/с.
    public float fireRotate = 0.3f;     // Секунд между выстрелами.
    public float health = 10.0f;        // Кол-во здоровья.

    public int score = 100;             // Очки за уничтожение этого коробля.

    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    private void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.offDown)  // Удаление вражеского корабля после выхода за границу.
        {
            Destroy(gameObject);                   // Корабль за нижней границей, поэтому его нужно уничтожить. 
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = Pos;

        tempPos.y -= speed * Time.deltaTime;

        Pos = tempPos;
    }

    public Vector3 Pos
    {
        get
        {
            return (this.transform.position);
        }

        set
        {
            this.transform.position = value;
        }
    }
}