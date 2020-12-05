using UnityEngine;

/// <summary>
/// Предотвращает выход игрового объекта за границы экрана.
/// Важно: работает ТОЛЬКО с ортографической камерой Main Camera в [ 0, 0, 0 ].
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1.0f;
    public bool keepOnScreen = true;                // Когда он не позволяет игровому объекту выйти за границы экрана(true).


    [Header("Set Dynamically")]
    public bool isOnScreen = true;                  // Получает значение false, если игровой объект вышел за границы экрана.
    public float camWidth;
    public float camHeight;

    [HideInInspector] public bool offRight = default;
    [HideInInspector] public bool offLeft  = default;
    [HideInInspector] public bool offUp    = default;
    [HideInInspector] public bool offDown  = default;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;   // Получает значения поля Size главной камеры.
        camWidth = camHeight * Camera.main.aspect;  // Отношение ширины к высоте поля зрения камеры - Portrait (3:4).
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;           // Current position  = [0, 0, 0].
        isOnScreen = true;

        offRight = offLeft = offUp = offDown = false;

        // camWidth  = 30.06211.
        // camHeight =  40.

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;

            isOnScreen = false;
            offRight = true;
        }

        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;

            isOnScreen = false;
            offLeft = true;
        }

        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;

            isOnScreen = false;
            offUp = true;
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;

            isOnScreen = false;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;

            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);

            Gizmos.DrawWireCube(Vector3.zero, boundSize);
        }
    }
}