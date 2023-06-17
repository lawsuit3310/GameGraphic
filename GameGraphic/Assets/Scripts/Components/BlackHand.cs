using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("BlackHand")]
public class BlackHand : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private float chasingSpd = 1;
	//이 값이 작을 수록 난이도가 증가하며 범위는 0 < x <= 1
	[SerializeField] private float chasingCefficient = 0.2f;

	private static Rigidbody2D _rigid;
	//기존 난이도 저장
	private float startChasingCefficient;
    //난이도 감소 지속 시간
    public static float reduceDifficultyDuration = 2f;
	//난이도 감소 타이머
	public static float reduceDifficultyTimer;

	//난이도가 감소 되었는지
	private bool isReduceDifficulty;

	// Start is called before the first frame update
	void Awake()
	{
		_rigid = GetComponent<Rigidbody2D>();
		//기존 난이도 저장
		startChasingCefficient = chasingCefficient;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//난이도 감소가 진행되고 있으면
		if (isReduceDifficulty)
		{
			//타이머 시작
			ReduceDifficultyOverTime();
		}
		ChasingPlayer();
	}

	private void ChasingPlayer()
	{
		var coefficient = player.transform.position.y - transform.position.y;
		_rigid.velocity =
			new Vector2()
			{
				x = 0,
				y = Mathf.Log(coefficient, 1 + chasingCefficient) * chasingSpd * Time.deltaTime
			};
	}

	//*난이도 감소 아이템 함수
	public void ReduceDifficulty()
	{
		//난이도 감소 시작
		chasingCefficient += 0.8f; //난이도 감소 값
		isReduceDifficulty = true;

		//타이머 초기화
		reduceDifficultyTimer = 0f;
	}

	private void ReduceDifficultyOverTime()
	{
		reduceDifficultyTimer += Time.deltaTime;
		//난이도 감소 타이머가 지정한 지속시간까지 가면
		if (reduceDifficultyTimer >= reduceDifficultyDuration)
		{
			//난이도 감소 끝
			isReduceDifficulty = false;
			//난이도 다시 원래상태로 복구
			chasingCefficient = startChasingCefficient;
		}
	}
}
