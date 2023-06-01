using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    //이동 속도
    public float speed = 5f;

    //이동 거리
    public float distance = 4f;

    //이동 방향
    private float direction = 1f;

    //풍선 초기 위치
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        //풍선 초기 위치 저장
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //좌우 이동 계산
        float moveAmount = direction * speed * Time.deltaTime;
        transform.Translate(moveAmount, 0f, 0f);

        //풍선이 지정한 거리를 넘어갈 경우 반대 방향으로 이동
        if (Mathf.Abs(transform.position.x - startPosition.x) >= distance)
        {
            direction *= -1f;
        }
    }
}