using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DigitalBookMainMenu : MonoBehaviour {
	[SerializeField] GameObject _StartMenu;
	[SerializeField] GameObject _DownloadMenu;
	Animator _menuAnimator;
	[SerializeField] AudioSource _audioSource;

	[SerializeField] Fading _fadeScript;

	// Use this for initialization
	void Start () {
		InitMenu ();
		_menuAnimator = gameObject.GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitMenu(){
		_StartMenu.SetActive (false);
		_DownloadMenu.SetActive (true);
		
	}

	public void SwitchToStart(){
		//_StartMenu.SetActive (false);
		Application.OpenURL("http://petmegames.com/what_is_it_but_a_dream.html#download");
		//_DownloadMenu.SetActive (true);
		//_menuAnimator.Play ("menu-start");
		StartCoroutine(ChangeLevel());
	}

	public void AlreadyHave(){
		//_menuAnimator.Play ("menu-start");
		StartCoroutine(ChangeLevel());
	}

	public void StartGame(){
		_menuAnimator.Play ("menu-start");
		if (!_audioSource.isPlaying) {
			_audioSource.Play ();
		}
		//SceneManager.LoadScene ("Alice_DigitalBook");
		
	}

	IEnumerator ChangeLevel(){
		yield return new WaitForSeconds(0.5f);
		float fadeTime = _fadeScript.BeginFade (1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene ("Alice_DigitalBook");
	}
}
