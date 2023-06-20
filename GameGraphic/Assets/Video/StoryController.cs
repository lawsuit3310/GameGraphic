using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StoryController : MonoBehaviour
{
    public VideoPlayer video;

    public bool isDone = false;
    // Start is called before the first frame update
    private void Awake()
    {
        video = video == null ? GetComponent<VideoPlayer>() : video;
    }

    private void Start()
    {
        video.loopPointReached +=
            (VideoPlayer video) =>
            {
                isDone = true;
            };
    }

    // Update is called once per frame
    void Update()
    {
        if (video.time is >= 4.7f and <= 4.8f)
        {
            video.Pause();
            video.time = 4.82f;
        }
        else if (video.time is >= 7.7f and <= 7.8f)
        {
            video.Pause();
            video.time = 7.83f;
        }

        if (Input.GetMouseButton(0))
        {
            if (isDone)
                SceneController.LoadScene("Game");
            else if (video.isPaused)
            {
                video.Play();
            }
        }
    }
}
