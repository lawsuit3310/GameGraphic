using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Cloud")]
public class Cloud : MonoBehaviour
{
    // ���� ���� �Ÿ�
    [SerializeField] private float destoryCloudDif = 11;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // (������ y�� + ���� ���� �Ÿ�)�� (�÷��̾� y��)���� ���� ��� ���� ���� 
        if (transform.position.y + destoryCloudDif < GameManager.Instance.player.transform.position.y)
            Destroy(gameObject);
    }
}
