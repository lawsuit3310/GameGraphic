using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Cloud")]
public class Cloud : MonoBehaviour
{
    // 구름 삭제 거리
    [SerializeField] private float destoryCloudDif = 11;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // (구름의 y값 + 구름 삭제 거리)가 (플레이어 y값)보다 낮을 경우 구름 삭제 
        if (transform.position.y + destoryCloudDif < GameManager.Instance.player.transform.position.y)
            Destroy(gameObject);
    }
}
