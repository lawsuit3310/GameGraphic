using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

[AddComponentMenu("Player")]
public class Player : MonoBehaviour
{
	private static Rigidbody2D _rigid;
	private static Collider2D _col;

	[SerializeField] private float jumpScale = 1;
	[SerializeField] private float speed = 1;
	[SerializeField] private float[] xlimit = { 1, 2 };

	//기존 속도 저장
	private float startSpeed;
	//속도 부스트 지속 시간
	private float speedBoostDuration = 5f;
	//속도 부스트 타이머
	public float speedBoostTimer;

	//점프 높이
	private float jumpHeight = 15f;
	private float fallingGravity = 2.5f;
	private float jumpGravity = 2f;

	//속도가 부스트 되었는지
	private bool isSpeedBoosted;

	// 점프 사용 여부
	public bool isJumpUsed = false;

	private void Awake()
	{
		try
		{
			_rigid = GetComponent<Rigidbody2D>();
			_col = GetComponent<Collider2D>();
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
			throw;
		}

		Debug.Log(_rigid.gameObject.name);
	}

	// Start is called before the first frame update
	void Start()
	{
		//기존 속도 저장
		startSpeed = speed;
	}

	// Update is called once per frame
	void Update()
	{
		if (isJumpUsed == true)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				JumpPhysics();
				isJumpUsed = false;
			}
		}
	}

	private void FixedUpdate()
	{
		MoveToMouse();
		RestrictPosition();

		// 플레이어의 Y 좌표가 바닥으로 떨어지면
		if (transform.position.y <= -5)
		{
			//게임 오버
			GameOver();
		}

		//속도가 부스트 되었으면
		if (isSpeedBoosted)
		{
			//타이머 시작
			BoostSpeedOverTime();
		}

		var score = (int)(transform.position.y / 3) - 1;
		GameManager.score = score >= GameManager.score ? score : GameManager.score;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		switch (col.gameObject.tag)
		{
			case "CLOUD":
				_rigid.velocity =
					new Vector2(
						_rigid.velocity.x,
						Vector2.up.y * Time.deltaTime * jumpScale * 110
					);
				break;
			case "BALLOON":
				_rigid.velocity =
					new Vector2(
						_rigid.velocity.x,
						Vector2.up.y * Time.deltaTime * jumpScale * 110
					);
				break;
			case "FOG":
				_rigid.velocity =
					new Vector2(
						_rigid.velocity.x,
						Vector2.up.y * Time.deltaTime * jumpScale * 110
					);
				//안개를 밟았을때 제거
				Destroy(col.gameObject);
				break;
			//레인보우 밟았을때 점프 부스트
			case "RAINBOW":
				_rigid.velocity =
					new Vector2(
						_rigid.velocity.x,
						Vector2.up.y * Time.deltaTime * jumpScale * 220
					);
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		BlackHand blackHand = FindObjectOfType<BlackHand>();

		switch (col.gameObject.tag)
		{
			case "DARKCLOUD":
				if (_rigid.velocity.y < 0)
					Destroy(col.gameObject);
				break;
			case "ITEM1":
				if (blackHand != null)
				{
					//*난이도 감소 아이템 함수 -> BlackHand.cs
					blackHand.ReduceDifficulty();
				}
				Destroy(col.gameObject);
				break;
			case "ITEM2":
				//*속도 부스트 아이템 함수
				StartSpeedBoost();
				Destroy(col.gameObject);
				break;
			case "ITEM3":
				//*점프 아이템 함수
				Jump();
				Destroy(col.gameObject);
				break;
			case "ARROW":
				GameOver();
				break;
			case "DANGERS":
				GameOver();
				break;
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		OnCollisionEnter2D(other);
	}

	private void MoveToMouse()
	{
		var targetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		var pos = this.transform.position;
		// 마우스 좌표에서 플레이어 좌표를 빼서 차를 저장하는 변수
		var coefficient = targetX - pos.x;
		int direction = coefficient > 0 ? 1 : -1;


		//실제로 적용되는 속도 - Vector2.right에 곱해주세요
		var absSpeed =
			speed * Time.deltaTime * Mathf.Sqrt(Mathf.Abs(coefficient));

		_rigid.velocity =
			new Vector2(
				Vector2.right.x * absSpeed * direction, _rigid.velocity.y
			);
	}

	//플레이어가 화면 밖으로 나가지 않도록 위치 제한
	private void RestrictPosition()
	{
		var posX = this.transform.position.x;

		if (posX >= xlimit[1] || posX <= xlimit[0])
		{
			this.transform.position =
				new Vector3()
				{
					x = posX >= xlimit[1] ? xlimit[1] : xlimit[0],
					y = this.transform.position.y,
					z = 0
				};
		}

		//플레이어가 점프 중 일땐 구름과 충돌하지 않도록 처리
		_col.isTrigger = _rigid.velocity.y > 0;
	}

	//*속도 부스트 아이템 함수
	private void StartSpeedBoost()
	{
		//속도 부스트 시작
		speed *= 2; //속도 부스트 값
		isSpeedBoosted = true;

		//타이머 초기화
		speedBoostTimer = 0f;
	}

	//이동 속도 강화 효과 지속 시간 동안 감소
	private void BoostSpeedOverTime()
	{
		speedBoostTimer += Time.deltaTime;
		//속도 부스트 타이머가 지정한 지속시간까지 가면
		if (speedBoostTimer >= speedBoostDuration)
		{
			//속도 부스트 끝
			isSpeedBoosted = false;
			//원래 속도로 복구
			speed = startSpeed;
		}
	}

	//*점프 아이템 함수
	private void Jump()
	{
		isJumpUsed = true;
	}

	//점프 로직
	private void JumpPhysics()
	{
		//수직 방향의 속도
		_rigid.velocity = new Vector2(_rigid.velocity.x, jumpHeight);

		if (_rigid.velocity.y < 0)
		{
			//점프 후 아래로 내려갈때 중력 보정
			_rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallingGravity - 1) * Time.deltaTime;
		}
		//점프 동작중에 스페이스바를 누르지 않고 상승하는 단계
		else if (_rigid.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
		{
			//점프 후 위로 올라갈때 중력 보정
			_rigid.velocity += Vector2.up * Physics2D.gravity.y * (jumpGravity - 1) * Time.deltaTime;
		}
	}

	//*게임 오버 함수
	private void GameOver()
	{
		Debug.Log("게임 오버 재시작");
		GameManager.score = 0;
		//게임 씬 다시 시작
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
