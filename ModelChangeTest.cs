using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class ModelChangeTest : MonoBehaviour {

    public MediaPlayer _meidaPlayer;
	// Use this for initialization
	void Start () {
            Debug.Log(_meidaPlayer.m_StereoPacking);

    }

    // Update is called once per frame
    void Update () {
		
	}
    void OnGUI()
    {
        if (GUILayout.Button("Test"))
        {
            _meidaPlayer.m_StereoPacking = StereoPacking.TopBottom;
            //_meidaPlayer.CloseVideo();
            Debug.Log(_meidaPlayer.m_StereoPacking);
        }

        if (GUILayout.Button("Test2"))
        {
            _meidaPlayer.m_StereoPacking = StereoPacking.LeftRight;
            Debug.Log(_meidaPlayer.m_StereoPacking);
        }

        if (GUILayout.Button("Stop"))
        {
            _meidaPlayer.Rewind(true);
        }
        if (GUILayout.Button("Start"))
        {
            _meidaPlayer.Control.Play();
        }
    }
}
