
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Te : MonoBehaviour
{
    public static GameObject Player;
    
    public bool isActive = true;
    private int _direction = 1;

    private static float _margin = 0f;
    private void Awake()
    {
        Player = GameObject.FindWithTag("PLAYER");
        _margin = 0;
    }

    void Start()
    {
        ChangeActiveStatus();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        {
            if (!isActive)
            {
                _margin += (4f * _direction * Time.deltaTime);
                this.transform.position = new Vector3()
                {
                    x = -9.5f + Player.transform.position.x,
                    y = Player.transform.position.y + _margin,
                    z = this.transform.position.z
                };
            }
                
            else
            {
                this.transform.position = new Vector3()
                {
                    x = -3 + Player.transform.position.x,
                    y = Player.transform.position.y + _margin,
                    z = this.transform.position.z
                };
            }

            if (_margin >= 8f || _margin <= -8f) _direction *= -1;
        }
    }
    
    void ChangeActiveStatus()
    {
        isActive = !isActive;
        Invoke(nameof(ChangeActiveStatus) , 5f);
    }
    
}
