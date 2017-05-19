using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DrawCard : MonoBehaviour {
	int _totalCardsLeftInDeck;
	Button _activeButton;
	[SerializeField] Transform CardListLayout;
	[SerializeField] GameObject[] _cardPrefab = new GameObject[6];
	List<int> _randomCardOption = new List<int>(12);
	[SerializeField] AudioSource _audioSource;
	// Use this for initialization
	void Start () {
		_randomCardOption.Add (0);
		_randomCardOption.Add (1);
		_randomCardOption.Add (2);
		_randomCardOption.Add (3);
		_randomCardOption.Add (4);
		_randomCardOption.Add (5);
		_randomCardOption.Add (6);
		_randomCardOption.Add (7);
		_randomCardOption.Add (8);
		_randomCardOption.Add (9);
		_totalCardsLeftInDeck = transform.GetChild (0).childCount;
		_activeButton = transform.GetChild (0).GetChild (_totalCardsLeftInDeck - 1).GetComponent<Button> ();
		_activeButton.interactable = true;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void DrawDeckCard(){
		if (!_audioSource.isPlaying) {
			_audioSource.pitch = Random.Range (0.95f, 1.3f);
			_audioSource.Play ();
		}
		_activeButton.gameObject.SetActive (false);
		_totalCardsLeftInDeck = _totalCardsLeftInDeck - 1;
		Debug.Log (_randomCardOption.Count + " length");
		int randomInt = Random.Range (0, _randomCardOption.Count-1);
		GameObject instantiatedCard = Instantiate (_cardPrefab[_randomCardOption[randomInt]], Vector3.zero, Quaternion.identity, CardListLayout);

		//remove the options from array
		_randomCardOption.RemoveAt(randomInt);

		if (_totalCardsLeftInDeck != 0) {
			_activeButton = transform.GetChild (0).GetChild (_totalCardsLeftInDeck - 1).GetComponent<Button> ();
			_activeButton.interactable = true;
		}
			
	}
}
