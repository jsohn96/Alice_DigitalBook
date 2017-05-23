using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLink : MonoBehaviour {
	[SerializeField] bool isContinue;


	public void GoTo() {
		Application.OpenURL("http://petmegames.com/what_is_it_but_a_dream.html#download");
//		if (isContinue) {
//			SceneManager.LoadScene (6);	
//		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
