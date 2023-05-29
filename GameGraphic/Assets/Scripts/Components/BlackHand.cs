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
    [SerializeField] private float chasingCefficient= 0.2f;
    private static Rigidbody2D _rigid;

    // Start is called before the first frame update
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChasingPlayer();
    }

    private void ChasingPlayer()
    {
        var coefficient = player.transform.position.y - transform.position.y;
        _rigid.velocity =
            new Vector2()
            {
                x = 0,
                y = Mathf.Log(coefficient,1 + chasingCefficient) * chasingSpd * Time.deltaTime 
            };
    }
}
