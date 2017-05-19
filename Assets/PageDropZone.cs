using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageDropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] bool _isLeft = false;
	[SerializeField] Sprite[] _pageSprites;
	[SerializeField] Sprite _emptyPageSprite;
	[SerializeField] Transform _cardListLayout;
	[SerializeField] CardType _whichCard8 = CardType.Null;
	CardType _whichCard12 = CardType.Null;
	CardType _whichCard16 = CardType.Null;
	[SerializeField] InfoPanelHandler _infoPanelHandlerScript;
	[SerializeField] GameObject _droppedCard8 = null;
	GameObject _droppedCard12 = null;
	GameObject _droppedCard16 = null;
	[SerializeField] AudioSource _audioSource;
	[SerializeField] AudioClip[] _audioClips;

	[SerializeField] Book _bookScript;
	Image _thisImage;
	[SerializeField] Image _nextPage;

	public bool _allCardsFilled = false;

	// Use this for initialization
	void Start () {
		_thisImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (_bookScript.currentPage == 8 || _bookScript.currentPage == 12 || _bookScript.currentPage == 16) {
			_thisImage.enabled = true;
		} else {
			_thisImage.enabled = false;
		}

		if (_whichCard8 != CardType.Null && _whichCard12 != CardType.Null && _whichCard16 != CardType.Null) {
			_allCardsFilled = true;
		} else {
			_allCardsFilled = false;
		}
	}


	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}

	public void OnDrop(PointerEventData eventData) {
		Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.parentToReturnTo = this.transform;
		}

	}

	public void KeepReference(GameObject droppedCard){
		if (_bookScript.currentPage == 8) {
			if (_droppedCard8 != null) {
				if (!_droppedCard8.Equals (droppedCard)) {
					_droppedCard8.SetActive (true);
					_droppedCard8.transform.SetParent (_cardListLayout);
				}
			}
			_droppedCard8 = droppedCard;
			_whichCard8 = _droppedCard8.GetComponent<Draggable> ().WhoIsThis ();
			_droppedCard8.SetActive (false);

			_nextPage.sprite = _pageSprites [(int)_whichCard8];
			if (_isLeft) {
				_bookScript.bookPages[7] =  _pageSprites [(int)_whichCard8];
			} else {
				_bookScript.bookPages[8] =  _pageSprites [(int)_whichCard8];
			}

		} else if (_bookScript.currentPage == 12) {
			if (_droppedCard12 != null) {
				if (!_droppedCard12.Equals (droppedCard)) {
					_droppedCard12.SetActive (true);
					_droppedCard12.transform.SetParent (_cardListLayout);
				}
			}
			_droppedCard12 = droppedCard;
			_whichCard12 = _droppedCard12.GetComponent<Draggable> ().WhoIsThis ();
			_droppedCard12.SetActive (false);

			_nextPage.sprite = _pageSprites [(int)_whichCard12];
			if (_isLeft) {
				_bookScript.bookPages[11] =  _pageSprites [(int)_whichCard12];
			} else {
				_bookScript.bookPages[12] =  _pageSprites [(int)_whichCard12];
			}

		} else if (_bookScript.currentPage == 16) {
			if (_droppedCard16 != null) {
				if (!_droppedCard16.Equals (droppedCard)) {
					_droppedCard16.SetActive (true);
					_droppedCard16.transform.SetParent (_cardListLayout);
				}
			}
			_droppedCard16 = droppedCard;
			_whichCard16 = _droppedCard16.GetComponent<Draggable> ().WhoIsThis ();
			_droppedCard16.SetActive (false);

			_nextPage.sprite = _pageSprites [(int)_whichCard16];
			if (_isLeft) {
				_bookScript.bookPages[15] =  _pageSprites [(int)_whichCard16];
			} else {
				_bookScript.bookPages[16] =  _pageSprites [(int)_whichCard16];
			}
		}
		if (!_audioSource.isPlaying) {
			_audioSource.clip = _audioClips [0];
			_audioSource.Play ();
		}
	}

	public void RemoveCardFromBook(){
		if (_bookScript.currentPage == 8) {
			if (_droppedCard8) {
				_droppedCard8.SetActive (true);
				_droppedCard8.transform.SetParent (_cardListLayout);
				_droppedCard8 = null;
			}
			_whichCard8 = CardType.Null;
			_nextPage.sprite = _emptyPageSprite;
			if (_isLeft) {
				_bookScript.bookPages[7] =  _emptyPageSprite;
			} else {
				_bookScript.bookPages[8] =  _emptyPageSprite;
			}
			
		} else if (_bookScript.currentPage == 12) {
			if (_droppedCard12) {
				_droppedCard12.SetActive (true);
				_droppedCard12.transform.SetParent (_cardListLayout);
				_droppedCard12 = null;
				_whichCard12 = CardType.Null;
			}
			_nextPage.sprite = _emptyPageSprite;
			if (_isLeft) {
				_bookScript.bookPages[11] =  _emptyPageSprite;
			} else {
				_bookScript.bookPages[12] =  _emptyPageSprite;
			}
			
		} else if (_bookScript.currentPage == 16) {
			if (_droppedCard16) {
				_droppedCard16.SetActive (true);
				_droppedCard16.transform.SetParent (_cardListLayout);
				_whichCard16 = CardType.Null;
				_droppedCard16 = null;
			}
			_nextPage.sprite = _emptyPageSprite;
			if (_isLeft) {
				_bookScript.bookPages[15] =  _emptyPageSprite;
			} else {
				_bookScript.bookPages[16] =  _emptyPageSprite;
			}
			
		}
	}

	public void ClickedSlot(){
		if (_bookScript.currentPage == 8) {
			_infoPanelHandlerScript.ClickedOnCardSlot (_whichCard8, this);
		} else if (_bookScript.currentPage == 12) {
			_infoPanelHandlerScript.ClickedOnCardSlot (_whichCard12, this);
		} else if (_bookScript.currentPage == 16) {
			_infoPanelHandlerScript.ClickedOnCardSlot (_whichCard16, this);
		}
	}
}
