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
    [SerializeField] private float[] xlimit = {1,2};

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "CLOUD" :
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
            case "DANGERS":
                GameOver();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "DARKCLOUD":
                if (_rigid.velocity.y < 0)
                    Destroy(col.gameObject);
                break;
            case "ARROW":
                GameOver();
                break;
        }
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

    //게임 오버 함수
    private void GameOver()
    {
        Debug.Log("게임 오버 재시작");
        //게임 씬 다시 시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
