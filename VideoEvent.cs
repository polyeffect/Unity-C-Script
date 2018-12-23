using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEvent : MonoBehaviour
{
    public VideoPlayer vp;
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        vp.loopPointReached += DetectVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DetectVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        print("Video is Over.");
        SceneManager.LoadScene(nextScene);
    }
}
