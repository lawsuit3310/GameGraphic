using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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

	//������1
	[SerializeField] private GameObject item1GameObject;

	//������2
	[SerializeField] private GameObject item2GameObject;

	//������3
	[SerializeField] private GameObject item3GameObject;

	// ������Ʈ ���� ����
	[SerializeField] private float distanceBetweenGameObject = 3;

	//������ ������ ������Ʈ ���
	private GameObject previousObject;


    // Start is called before the first frame update
    void Start()
	{
        StartCoroutine(SpawnArrow());
	}


	// Update is called once per frame
	void Update()
	{
		//���� ������Ʈ ��ȯ ��
		int randomValue1 = Random.Range(0, 100);
		int randomValue2 = Random.Range(0, 100);


		// (ObjectManager�� y��)�� (�÷��̾� y�� + ������Ʈ ���� + ���� ��ġ)���� ���� ��� ObjectManager�� y��ǥ�� ���� �ø��� ������Ʈ ��ȯ
		if (transform.position.y < GameManager.Instance.player.transform.position.y + distanceBetweenGameObject + 5)
		{

			transform.position = new Vector2(Random.Range(-8.0f, 8.0f), (transform.position.y) + distanceBetweenGameObject);

			//������ ������Ʈ���� ��ġ
			Vector2 itemPosition = transform.position;
			itemPosition.y += 1f;


			//���� Ȯ���� ��ȯ�Ǵ� ���κ���, �Ȱ�, ǳ��, �Ա���
			if (randomValue1 >= 0 && randomValue1 <= 15)
			{
				Instantiate(rainbowGameObject, transform.position, transform.rotation);
				//��ȯ�� ������Ʈ ���
				previousObject = rainbowGameObject;
			}
			else if (randomValue1 >= 16 && randomValue1 <= 30)
			{
				Instantiate(fogGameObject, transform.position, transform.rotation);
				//��ȯ�� ������Ʈ ���
				previousObject = fogGameObject;
			}
			else if (randomValue1 >= 31 && randomValue1 <= 45)
			{
				Instantiate(balloonGameObject, transform.position, transform.rotation);
				//��ȯ�� ������Ʈ ���
				previousObject = balloonGameObject;
			}
			else if (randomValue1 >= 46 && randomValue1 <= 60)
			{
				// ������ ��ȯ�� ������Ʈ�� �Ա����̸� �⺻ ���� ��ȯ <- �Ա����� �ι� ��ȯ�Ǿ� ���ö󰡴� ��� ����
				if (previousObject == darkCloudGameObject)
				{
					Instantiate(cloudGameObject, transform.position, transform.rotation);
					//��ȯ�� ������Ʈ ���
					previousObject = cloudGameObject;
				}
				else
				{
					Instantiate(darkCloudGameObject, transform.position, transform.rotation);
					//��ȯ�� ������Ʈ ���
					previousObject = darkCloudGameObject;
				}
			}
			//������ Ȯ���� �⺻���� ��ȯ
			else
			{
				Instantiate(cloudGameObject, transform.position, transform.rotation);
				//��ȯ�� ������Ʈ ���
				previousObject = cloudGameObject;
			}


			//������ ��ȯ�� ������Ʈ�� �Ա��� �Ǵ� ǳ���� �ƴϸ� ������ ��ȯ
			if (previousObject != darkCloudGameObject && previousObject != balloonGameObject)
			{
				//���� Ȯ���� ��ȯ�Ǵ� ������1,2,3
				if (randomValue2 >= 0 && randomValue2 <= 8)
				{
					Instantiate(item1GameObject, itemPosition, transform.rotation);
				}
				else if (randomValue2 >= 30 && randomValue2 <= 38)
				{
					Instantiate(item2GameObject, itemPosition, transform.rotation);
				}
				else if (randomValue2 >= 60 && randomValue2 <= 68)
				{
					Instantiate(item3GameObject, itemPosition, transform.rotation);
				}
			}
		}
	}

	IEnumerator SpawnArrow()
	{
        // ȭ�� ����, 1�� ���� �Ҹ� ���
        yield return new WaitForSeconds(5f);
		SoundManager.instance.PlayArrowSound();
        yield return new WaitForSeconds(1f);

        Instantiate(arrowGameObject,
			new Vector2(
				GameManager.Instance.player.gameObject.transform.position.x,
				GameManager.Instance.player.gameObject.transform.position.y - 12),
			transform.rotation
			).transform.rotation = quaternion.RotateZ(180);
		StartCoroutine(SpawnArrow());

	}
}
