using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("ObjectManager")]
public class ObjectManager : MonoBehaviour
{
    //�⺻ ����
    [SerializeField] private GameObject cloudGameObject;

    //�Ա���
    [SerializeField] private GameObject darkCloudGameObject;

    //���κ���
    [SerializeField] private GameObject rainbowGameObject;

    //�Ȱ�
    [SerializeField] private GameObject fogGameObject;

    //ǳ��
    [SerializeField] private GameObject balloonGameObject;

    //ȭ��
    [SerializeField] private GameObject arrowGameObject;

    //������ ������ ���� ��� �����
    private GameObject previousCloud;

    // ������Ʈ ���� ����
    [SerializeField] private float distanceBetweenGameObject = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnArrow());
    }

    // Update is called once per frame
    void Update()
    {
        //���� ��ȯ ��
        int randomvalue = Random.Range(0, 100);

        // (ObjectManager�� y��)�� (�÷��̾� y�� + ������Ʈ ���� + ���� ��ġ)���� ���� ��� ObjectManager�� y��ǥ�� ���� �ø��� ������Ʈ ��ȯ
        if (transform.position.y < GameManager.Instance.player.transform.position.y + distanceBetweenGameObject + 5)
        {
            transform.position = new Vector2(Random.Range(-8.0f,8.0f), (transform.position.y) + distanceBetweenGameObject);

            //���� Ȯ���� ��ȯ�Ǵ� ���κ���, �Ȱ�, ǳ��, �Ա���
            if (randomvalue >= 0 && randomvalue <= 15)
            {
                Instantiate(rainbowGameObject, transform.position, transform.rotation);
                //��ȯ�� ������Ʈ ���
                previousCloud = rainbowGameObject;
            }
            else if (randomvalue >= 16 && randomvalue <= 30)
            {
                Instantiate(fogGameObject, transform.position, transform.rotation);
                //��ȯ�� ������Ʈ ���
                previousCloud = fogGameObject;
            }
            else if (randomvalue >= 31 && randomvalue <= 45)
            {
                Instantiate(balloonGameObject, transform.position, transform.rotation);
                //��ȯ�� ������Ʈ ���
                previousCloud = balloonGameObject;
            }
            else if (randomvalue >= 46 && randomvalue <= 60)
            {
                // ������ ��ȯ�� ������Ʈ�� �Ա����̸� �⺻ ���� ��ȯ <- �Ա����� �ι� ��ȯ�Ǿ� ���ö󰡴� ��� ����
                if (previousCloud == darkCloudGameObject)
                {
                    Instantiate(cloudGameObject, transform.position, transform.rotation);
                    //��ȯ�� ������Ʈ ���
                    previousCloud = cloudGameObject;
                }
                else
                {
                    Instantiate(darkCloudGameObject, transform.position, transform.rotation);
                    //��ȯ�� ������Ʈ ���
                    previousCloud = darkCloudGameObject;
                }
            }
            //������ Ȯ���� �⺻���� ��ȯ
            else
            {
                Instantiate(cloudGameObject, transform.position, transform.rotation);
                //��ȯ�� ������Ʈ ���
                previousCloud = cloudGameObject;
            }
        }
    }

    IEnumerator SpawnArrow()
    {
        // ȭ�� ����
        yield return new WaitForSeconds(Random.Range(6.0f,6.0f));
        Instantiate(arrowGameObject,
            new Vector2(
                GameManager.Instance.player.gameObject.transform.position.x,
                GameManager.Instance.player.gameObject.transform.position.y-12),
            transform.rotation
            );
        StartCoroutine(SpawnArrow());

    }
}
