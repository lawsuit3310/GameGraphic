using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[AddComponentMenu("ObjectManager")]
public class ObjectManager : MonoBehaviour
{
	//기본 구름
	[SerializeField] private GameObject cloudGameObject;

	//먹구름
	[SerializeField] private GameObject darkCloudGameObject;

	//레인보우
	[SerializeField] private GameObject rainbowGameObject;

	//안개
	[SerializeField] private GameObject fogGameObject;

	//풍선
	[SerializeField] private GameObject balloonGameObject;

	//화살
	[SerializeField] private GameObject arrowGameObject;

	//아이템1
	[SerializeField] private GameObject item1GameObject;

	//아이템2
	[SerializeField] private GameObject item2GameObject;

	//아이템3
	[SerializeField] private GameObject item3GameObject;

	// 오브젝트 사이 간격
	[SerializeField] private float distanceBetweenGameObject = 3;

	//이전에 스폰된 오브젝트 기록
	private GameObject previousObject;


    // Start is called before the first frame update
    void Start()
	{
        StartCoroutine(SpawnArrow());
	}


	// Update is called once per frame
	void Update()
	{
		//랜덤 오브젝트 소환 값
		int randomValue1 = Random.Range(0, 100);
		int randomValue2 = Random.Range(0, 100);


		// (ObjectManager의 y값)이 (플레이어 y값 + 오브젝트 간격 + 조정 수치)보다 낮을 경우 ObjectManager의 y좌표를 위로 올리고 오브젝트 소환
		if (transform.position.y < GameManager.Instance.player.transform.position.y + distanceBetweenGameObject + 5)
		{

			transform.position = new Vector2(Random.Range(-8.0f, 8.0f), (transform.position.y) + distanceBetweenGameObject);

			//아이템 오브젝트위로 배치
			Vector2 itemPosition = transform.position;
			itemPosition.y += 1f;


			//적은 확률로 소환되는 레인보우, 안개, 풍선, 먹구름
			if (randomValue1 >= 0 && randomValue1 <= 15)
			{
				Instantiate(rainbowGameObject, transform.position, transform.rotation);
				//소환한 오브젝트 기록
				previousObject = rainbowGameObject;
			}
			else if (randomValue1 >= 16 && randomValue1 <= 30)
			{
				Instantiate(fogGameObject, transform.position, transform.rotation);
				//소환한 오브젝트 기록
				previousObject = fogGameObject;
			}
			else if (randomValue1 >= 31 && randomValue1 <= 45)
			{
				Instantiate(balloonGameObject, transform.position, transform.rotation);
				//소환한 오브젝트 기록
				previousObject = balloonGameObject;
			}
			else if (randomValue1 >= 46 && randomValue1 <= 60)
			{
				// 이전에 소환된 오브젝트가 먹구름이면 기본 구름 소환 <- 먹구름만 두번 소환되어 못올라가는 경우 방지
				if (previousObject == darkCloudGameObject)
				{
					Instantiate(cloudGameObject, transform.position, transform.rotation);
					//소환한 오브젝트 기록
					previousObject = cloudGameObject;
				}
				else
				{
					Instantiate(darkCloudGameObject, transform.position, transform.rotation);
					//소환한 오브젝트 기록
					previousObject = darkCloudGameObject;
				}
			}
			//나머지 확률은 기본구름 소환
			else
			{
				Instantiate(cloudGameObject, transform.position, transform.rotation);
				//소환한 오브젝트 기록
				previousObject = cloudGameObject;
			}


			//이전에 소환된 오브젝트가 먹구름 또는 풍선이 아니면 아이템 소환
			if (previousObject != darkCloudGameObject && previousObject != balloonGameObject)
			{
				//적은 확률로 소환되는 아이템1,2,3
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
        // 화살 스폰, 1초 전에 소리 재생
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
