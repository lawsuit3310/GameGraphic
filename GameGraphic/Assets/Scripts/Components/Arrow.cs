using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Arrow")]
public class Arrow : MonoBehaviour
{
    // 오브젝트 삭제 거리
    [SerializeField] private float destoryArrowDif = 20;

    float time = 0;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0,0,270);
    }

    // Update is called once per frame
    void Update()
    {
        // (오브젝트의 y값 + 오브젝트 삭제 거리)가 (플레이어 y값)보다 낮을 경우 오브젝트 삭제
        if (transform.position.y + destoryArrowDif < GameManager.Instance.player.transform.position.y)
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

        if (rb.velocity.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
    }

}
