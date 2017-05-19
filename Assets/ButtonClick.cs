using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour {
	AudioSource _audioSource;

	void Awake(){
		_audioSource = GetComponent<AudioSource> ();
	}

	public void PlayButtonClickSound(){
		if (!_audioSource.isPlaying) {
			_audioSource.Play ();
		}
	}
}
