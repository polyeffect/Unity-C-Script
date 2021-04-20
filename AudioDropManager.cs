using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using B83.Win32;
using NAudio;
using NAudio.Wave;

[RequireComponent (typeof (AudioSource))]
public class AudioDropManager : MonoBehaviour
{
    public GameObject GUIPanel;
    public Button pauseBtn;
    public Sprite play;
    public Sprite pause;

    // log
    List<string> log = new List<string>();

    AudioSource audioSource;
    List<string> url = new List<string>();
    int maxTrackNum = 0;
    int trackNum = 0;

    bool isPause = false;
    bool isGUIEnable = true;

    DropInfo dropInfo = null;
    class DropInfo
    {
        public string file;
        public Vector2 pos;
    }

    void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;
    }

    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }

    void OnFiles(List<string> aFiles, POINT aPos)
    {
        string str = "Dropped" + aFiles.Count + "\n" + aFiles.Aggregate((a, b) => a + "\n" + b);
        log.Add(str);
        //log.Add(aFiles[0]);

        
        url.Clear();
        maxTrackNum = aFiles.Count;
        for (int i = 0; i < aFiles.Count; i++)
        {
            url.Add(aFiles[i]);
        }

        string file = "";

        foreach (var f in aFiles)
        {
            var fi = new System.IO.FileInfo(f);
            var ext = fi.Extension.ToLower();
            
            if (ext == ".ogg" || ext == ".mp3" || ext == ".wav" || ext == ".aiff" || ext ==".aif")
            {
                file = f;
                StartCoroutine(loadAudio());
                break;
            }
        }

        if (file != "")
        {
            var info = new DropInfo
            {
                file = file,
                pos = new Vector2(aPos.x, aPos.y)
            };
            dropInfo = info;
        }

        isPause = false;
    }

    public void audioPause()
    {
        if (isPause)
        {
            isPause = false;
            audioSource.Play();
            pauseBtn.GetComponent<Image>().sprite = pause;
        }
        else
        {
            isPause = true;
            audioSource.Pause();
            pauseBtn.GetComponent<Image>().sprite = play;
        }
    }

    private void OnGUI()
    {
        if (isGUIEnable)
        {
            if (GUILayout.Button("clear log"))
                log.Clear();
            foreach (var s in log)
                GUILayout.Label(s);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isGUIEnable = !isGUIEnable;
            GUIPanel.SetActive(isGUIEnable);
        }
    }

    IEnumerator loadAudio()
    {
        WWW www = new WWW("file://" + url[trackNum]);

        while (!www.isDone)
            yield return null;

        audioSource.clip = www.GetAudioClip(false, false);
        audioSource.Play();

        if (maxTrackNum > 0)
            Invoke("PlayNextTrack", audioSource.clip.length);
    }

    void PlayNextTrack()
    {
        audioSource.Stop();

        if (trackNum < maxTrackNum)
            trackNum++;
        else
            trackNum = 0;

        StartCoroutine(loadAudio());
    }
}
