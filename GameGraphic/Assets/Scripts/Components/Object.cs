using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Object")]
public class Object : MonoBehaviour
{
    // 오브젝트 삭제 거리
    [SerializeField] private float destoryCloudDif = 11;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // (오브젝트의 y값 + 오브젝트 삭제 거리)가 (플레이어 y값)보다 낮을 경우 오브젝트 삭제
        if (transform.position.y + destoryCloudDif < GameManager.Instance.player.transform.position.y)
            Destroy(gameObject);
    }
}
