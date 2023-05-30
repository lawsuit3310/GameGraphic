using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Arrow")]
public class Arrow : MonoBehaviour
{
    // ������Ʈ ���� �Ÿ�
    [SerializeField] private float destoryCloudDif = 20;

    public float time = 0;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // (������Ʈ�� y�� + ������Ʈ ���� �Ÿ�)�� (�÷��̾� y��)���� ���� ��� ������Ʈ ����
        if (transform.position.y + destoryCloudDif < GameManager.Instance.player.transform.position.y)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (time < 1)
        {
            rb.AddForce(Vector2.up, ForceMode2D.Impulse);
            time += Time.fixedDeltaTime;
        }
        else
            rb.AddForce(Vector2.down, ForceMode2D.Impulse);
    }

}
