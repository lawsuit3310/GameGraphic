using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("CloudManager")]
public class CloudManager : MonoBehaviour
{
    [SerializeField] private GameObject cloudGameObject;
    // 구름 사이 간격
    [SerializeField] private float distanceBetweenCloudGameObject = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // (CloudManager의 y값)이 (플레이어 y값 + 구름 간격)보다 낮을 경우 CloudManager의 y좌표를 위로 올리고 구름 소환
        if (transform.position.y < GameManager.Instance.player.transform.position.y + distanceBetweenCloudGameObject)
        {
            transform.position = new Vector2(Random.Range(-6.0f,6.0f), transform.position.y + distanceBetweenCloudGameObject);
            Instantiate(cloudGameObject, transform.position, transform.rotation);

        }
    }
}
