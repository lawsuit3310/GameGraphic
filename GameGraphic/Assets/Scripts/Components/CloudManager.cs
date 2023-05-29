using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("CloudManager")]
public class CloudManager : MonoBehaviour
{
    [SerializeField] private GameObject cloudGameObject;
    // ���� ���� ����
    [SerializeField] private float distanceBetweenCloudGameObject = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // (CloudManager�� y��)�� (�÷��̾� y�� + ���� ����)���� ���� ��� CloudManager�� y��ǥ�� ���� �ø��� ���� ��ȯ
        if (transform.position.y < GameManager.Instance.player.transform.position.y + distanceBetweenCloudGameObject)
        {
            transform.position = new Vector2(Random.Range(-6.0f,6.0f), transform.position.y + distanceBetweenCloudGameObject);
            Instantiate(cloudGameObject, transform.position, transform.rotation);

        }
    }
}
