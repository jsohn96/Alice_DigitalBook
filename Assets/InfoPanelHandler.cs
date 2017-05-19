using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelHandler : MonoBehaviour {
	[SerializeField] Image _cardRenderer;
	[SerializeField] GameObject _introText;
	[SerializeField] Sprite[] _sprites;
	[SerializeField] Book _bookScript;
	CardType _currentCard = CardType.Null;
	bool _isHover = false;
	PageDropZone _selectedPageDropZoneScript;
	[SerializeField] GameObject _removeButton;
	// Bunny = 0, Cat = 1, Caterpillar = 2, Jabberwock = 3, Sheep = 4, Twins = 5, Null = 6

	[SerializeField] AudioSource _audioSource;
	[SerializeField] AudioClip[] _audioClips;

	int _priorBookPage;

	// Use this for initialization
	void Start () {
		_priorBookPage = _bookScript.currentPage;
	}
	
	// Update is called once per frame
	void Update () {
		if (_priorBookPage != _bookScript.currentPage) {
			_priorBookPage = _bookScript.currentPage;
			HidePanelCards ();
		}

		if (_bookScript.currentPage == 4) {
			_removeButton.SetActive (false);
			_cardRenderer.enabled = false;
			_introText.SetActive (true);
		}
	}

	public void ClickedOnCardSlot(CardType clickedCardType, PageDropZone selectedPageDropZoneScript){
		_selectedPageDropZoneScript = selectedPageDropZoneScript;
		if (clickedCardType != CardType.Null) {
			_removeButton.SetActive (true);
		} else {
			_removeButton.SetActive (false);
			_cardRenderer.enabled = false;
		}
		ClickedOnCard (clickedCardType);
	}

	void ClickedOnCard(CardType clickedCardType){
		_introText.SetActive (false);
		if (clickedCardType != CardType.Null) {
			_cardRenderer.sprite = _sprites [(int)clickedCardType];
			_cardRenderer.enabled = true;
		}
	}

	void ClickState(CardClickedEvent e){
		_removeButton.SetActive (false);
		if (e.DraggingCard != CardType.Null) {
			_isHover = true;
			if (_currentCard != e.DraggingCard) {
				ClickedOnCard (e.DraggingCard);
			}
		} else {
			_isHover = false;
			_cardRenderer.enabled = false;
		}
	}

	void DragState(CardDraggingEvent e){
		_isHover = false;
		if (e.IsDragging && e.DraggingCard != CardType.Null) {
			if (_currentCard != e.DraggingCard) {
				ClickedOnCard (e.DraggingCard);
			}
			if (!_audioSource.isPlaying) {
				_audioSource.clip = _audioClips [2];
				_audioSource.Play ();
			}
		} else {
			_cardRenderer.enabled = false;
			_removeButton.SetActive (false);
			if (!_audioSource.isPlaying) {
				_audioSource.clip = _audioClips [0];
				_audioSource.Play ();
			}
		}
	}

	public void RemoveFromCardSlot(){
		_cardRenderer.enabled = false;
		_selectedPageDropZoneScript.RemoveCardFromBook ();
		_removeButton.SetActive (false);
		if (!_audioSource.isPlaying) {
			_audioSource.clip = _audioClips [1];
			_audioSource.Play ();
		}
	}

	public void HidePanelCards(){
		_removeButton.SetActive (false);
		_cardRenderer.enabled = false;
	}

	void OnEnable(){
		Events.G.AddListener<CardDraggingEvent> (DragState);
		Events.G.AddListener<CardClickedEvent> (ClickState);
	}

	void OnDisable(){
		Events.G.RemoveListener<CardDraggingEvent> (DragState);
		Events.G.RemoveListener<CardClickedEvent> (ClickState);
	}
}
