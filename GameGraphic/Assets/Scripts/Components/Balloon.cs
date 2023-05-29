using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    //�̵� �ӵ�
    public float speed = 5f;

    //�̵� �Ÿ�
    public float distance = 4f;

    //�̵� ����
    private float direction = 1f;

    //ǳ�� �ʱ� ��ġ
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        //ǳ�� �ʱ� ��ġ ����
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //�¿� �̵� ���
        float moveAmount = direction * speed * Time.deltaTime;
        transform.Translate(moveAmount, 0f, 0f);

        //ǳ���� ������ �Ÿ��� �Ѿ ��� �ݴ� �������� �̵�
        if (Mathf.Abs(transform.position.x - startPosition.x) >= distance)
        {
            direction *= -1f;
        }
    }
}