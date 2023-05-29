using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Object")]
public class Object : MonoBehaviour
{
    // ������Ʈ ���� �Ÿ�
    [SerializeField] private float destoryCloudDif = 11;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // (������Ʈ�� y�� + ������Ʈ ���� �Ÿ�)�� (�÷��̾� y��)���� ���� ��� ������Ʈ ����
        if (transform.position.y + destoryCloudDif < GameManager.Instance.player.transform.position.y)
            Destroy(gameObject);
    }
}
