using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //이전에 스폰된 구름 기록 저장용
    private GameObject previousCloud;

    // 오브젝트 사이 간격
    [SerializeField] private float distanceBetweenGameObject = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //랜덤 소환 값
        int randomvalue = Random.Range(0, 100);

        // (ObjectManager의 y값)이 (플레이어 y값 + 오브젝트 간격)보다 낮을 경우 ObjectManager의 y좌표를 위로 올리고 오브젝트 소환
        if (transform.position.y < GameManager.Instance.player.transform.position.y + distanceBetweenGameObject)
        {
            transform.position = new Vector2(Random.Range(-8.0f,8.0f), (transform.position.y) + distanceBetweenGameObject);

            //적은 확률로 소환되는 레인보우, 안개, 풍선, 먹구름
            if (randomvalue >= 0 && randomvalue <= 15)
            {
                Instantiate(rainbowGameObject, transform.position, transform.rotation);
                //소환한 오브젝트 기록
                previousCloud = rainbowGameObject;
            }
            else if (randomvalue >= 16 && randomvalue <= 30)
            {
                Instantiate(fogGameObject, transform.position, transform.rotation);
                //소환한 오브젝트 기록
                previousCloud = fogGameObject;
            }
            else if (randomvalue >= 31 && randomvalue <= 45)
            {
                Instantiate(balloonGameObject, transform.position, transform.rotation);
                //소환한 오브젝트 기록
                previousCloud = balloonGameObject;
            }
            else if (randomvalue >= 46 && randomvalue <= 60)
            {
                // 이전에 소환된 오브젝트가 먹구름이면 기본 구름 소환 <- 먹구름만 두번 소환되어 못올라가는 경우 방지
                if (previousCloud == darkCloudGameObject)
                {
                    Instantiate(cloudGameObject, transform.position, transform.rotation);
                    //소환한 오브젝트 기록
                    previousCloud = cloudGameObject;
                }
                else
                {
                    Instantiate(darkCloudGameObject, transform.position, transform.rotation);
                    //소환한 오브젝트 기록
                    previousCloud = darkCloudGameObject;
                }
            }
            //나머지 확률은 기본구름 소환
            else
            {
                Instantiate(cloudGameObject, transform.position, transform.rotation);
                //소환한 오브젝트 기록
                previousCloud = cloudGameObject;
            }
        }
    }
}
