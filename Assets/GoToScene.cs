using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {
	[SerializeField] int _nextSceneIndex;
	[SerializeField] Fading _fadeScript;
	// check to call scene change once
	bool _called = false;

	public void GoToSpecifiedScene(){
		if (!_called) {
			_called = true;
			StartCoroutine(ChangeLevel ());
		}
	}


	IEnumerator ChangeLevel(){
		yield return new WaitForSeconds(0.5f);
		float fadeTime = _fadeScript.BeginFade (1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene (_nextSceneIndex);
	}
}
