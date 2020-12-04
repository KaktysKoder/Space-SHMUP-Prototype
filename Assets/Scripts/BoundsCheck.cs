using UnityEngine;

/// <summary>
/// Предотвращает выход игрового объекта за границы экрана.
/// Важно: работает ТОЛЬКО с ортографической камерой Main Camera в [ 0, 0, 0 ].
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1.0f;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;   // Получает значения поля Size главной камеры.
        camWidth = camHeight * Camera.main.aspect;  // Отношение ширины к высоте поля зрения камеры - Portrait (3:4).
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position; // Current position  = [0, 0, 0]

        // camWidth  = 30.06211
        // camHeight =  40

        if (pos.x > camWidth - radius)
            pos.x = camWidth - radius;

        if (pos.x < -camWidth + radius)
            pos.x = -camWidth + radius;

        if (pos.y > camHeight - radius)
            pos.y = camHeight - radius;

        if (pos.y < -camHeight + radius)
            pos.y = -camHeight + radius;

        transform.position = pos;
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