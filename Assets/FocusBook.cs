using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusBook : MonoBehaviour {

	bool _focus = false;
	bool _focusCapped = true;
	public bool _disableFocus = false;
	[SerializeField] GameObject[] _turnOff;
	[SerializeField] RectTransform[] _turnOn;
	[SerializeField] Image[] _glowImage;
	// 0: Read Book, 1: Back
	[SerializeField] Sprite[] _buttonSprites;
	Image _buttonImage;
	Color _transparentColor = new Color (1.0f, 1.0f, 1.0f, 0.0f);
	[SerializeField] Book _bookScript;
	[SerializeField] AutoFlip _autoFlipScript;
	// 0: Book, 1: CardAreaLeft, 2: CardAreaRight, 3: btn_next, 4: btn_prev, 5: Button(1)
	Vector3 _bookOrigin = new Vector3 (-78.8f, 30.0f, 0.0f);
	Vector3 _cardAreaLeftOrigin = new Vector3 (-124.6f, 30.0f, 0.0f);
	Vector3 _cardAreaRightOrigin = new Vector3 (-27.5f, 30.0f, 0.0f);
	Vector3 _btn_nextOrigin =  new Vector3 (-12.1f, 67.0f, 0.0f);
	Vector3 _btn_prevOrigin = new Vector3 (-174.3f, 67.0f, 0.0f);
	Vector3 _buttonOrigin = new Vector3 (-80.1f, 111.2f, 0.0f);
	//x: w, y: h
	Vector2 _bookDimensionsOrigin = new Vector2 (210.2f, 126.17f);
	Vector2 _cardAreaLeftDimensionsOrigin = new Vector2 (55.5f, 100.0f);
	Vector2 _cardAreaRightDimensionsOrigin = new Vector2 (55.5f, 100.0f);
	Vector2 _btn_nextDimensionsOrigin = new Vector2 (59.1f, 59.1f);
	Vector2 _btn_prevDimensionsOrigin = new Vector2 (59.1f, 59.1f);
	Vector2 _buttonDimensionsOrigin = new Vector2 (125.0f, 100.0f);

	Vector3 _bookNew = new Vector3 (1.0f, -2.0f, 0.0f);
	Vector3 _cardAreaLeftNew = new Vector3 (-64.0f, -2.0f, 0.0f);
	Vector3 _cardAreaRightNew = new Vector3 (74.0f, -2.0f, 0.0f);
	Vector3 _btn_nextNew = new Vector3 (116.0f, 30.0f, 0.0f);
	Vector3 _btn_prevNew = new Vector3 (-155.0f, 30.0f, 0.0f);
	Vector3 _buttonNew = new Vector3 (-80.1f, 111.2f, 0.0f);

	Vector2 _bookDimensionsNew = new Vector2 (298.0f, 179.0f);
	Vector2 _cardAreaLeftDimensionsNew = new Vector2 (79.0f, 142.0f);
	Vector2 _cardAreaRightDimensionsNew = new Vector2 (78.0f, 142.0f);
	Vector2 _btn_nextDimensionsNew = new Vector2 (84.0f, 84.0f);
	Vector2 _btn_prevDimensionsNew = new Vector2 (84.0f, 84.0f);
	Vector2 _buttonDimensionsNew = new Vector2 (125.0f, 100.0f);

	Timer _focusTimer = new Timer (1.0f);

	[SerializeField] PageDropZone _cardAreaLeft;
	[SerializeField] PageDropZone _cardAreaRight;

	bool _flippedToFirstOnce = false; 

	// Use this for initialization
	void Start () {
		_buttonImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!_focusTimer.IsOffCooldown) {
			if (_focus) {
				_turnOn [0].anchoredPosition3D = Vector3.Lerp (_bookOrigin, _bookNew, _focusTimer.PercentTimePassed);
				_turnOn [0].sizeDelta = Vector2.Lerp (_bookDimensionsOrigin, _bookDimensionsNew, _focusTimer.PercentTimePassed);

				_turnOn [1].anchoredPosition3D = Vector3.Lerp (_cardAreaLeftOrigin, _cardAreaLeftNew, _focusTimer.PercentTimePassed);
				_turnOn [1].sizeDelta = Vector2.Lerp (_cardAreaLeftDimensionsOrigin, _cardAreaLeftDimensionsNew, _focusTimer.PercentTimePassed);

				_turnOn [2].anchoredPosition3D = Vector3.Lerp (_cardAreaRightOrigin, _cardAreaRightNew, _focusTimer.PercentTimePassed);
				_turnOn [2].sizeDelta = Vector2.Lerp (_cardAreaRightDimensionsOrigin, _cardAreaRightDimensionsNew, _focusTimer.PercentTimePassed);

				_turnOn [3].anchoredPosition3D = Vector3.Lerp (_btn_nextOrigin, _btn_nextNew, _focusTimer.PercentTimePassed);
				_turnOn [3].sizeDelta = Vector2.Lerp (_btn_nextDimensionsOrigin, _btn_nextDimensionsNew, _focusTimer.PercentTimePassed);

				_turnOn [4].anchoredPosition3D = Vector3.Lerp (_btn_prevOrigin, _btn_prevNew, _focusTimer.PercentTimePassed);
				_turnOn [4].sizeDelta = Vector2.Lerp (_btn_prevDimensionsOrigin, _btn_prevDimensionsNew, _focusTimer.PercentTimePassed);

				_turnOn [5].anchoredPosition3D = Vector3.Lerp (_buttonOrigin, _buttonNew, _focusTimer.PercentTimePassed);
				_turnOn [5].sizeDelta = Vector2.Lerp (_buttonDimensionsOrigin, _buttonDimensionsNew, _focusTimer.PercentTimePassed);
			} else {
				_turnOn [0].anchoredPosition3D = Vector3.Lerp (_bookNew, _bookOrigin, _focusTimer.PercentTimePassed);
				_turnOn [0].sizeDelta = Vector2.Lerp (_bookDimensionsNew, _bookDimensionsOrigin, _focusTimer.PercentTimePassed);

				_turnOn [1].anchoredPosition3D = Vector3.Lerp (_cardAreaLeftNew, _cardAreaLeftOrigin, _focusTimer.PercentTimePassed);
				_turnOn [1].sizeDelta = Vector2.Lerp (_cardAreaLeftDimensionsNew, _cardAreaLeftDimensionsOrigin, _focusTimer.PercentTimePassed);

				_turnOn [2].anchoredPosition3D = Vector3.Lerp (_cardAreaRightNew, _cardAreaRightOrigin, _focusTimer.PercentTimePassed);
				_turnOn [2].sizeDelta = Vector2.Lerp (_cardAreaRightDimensionsNew, _cardAreaRightDimensionsOrigin, _focusTimer.PercentTimePassed);

				_turnOn [3].anchoredPosition3D = Vector3.Lerp (_btn_nextNew, _btn_nextOrigin, _focusTimer.PercentTimePassed);
				_turnOn [3].sizeDelta = Vector2.Lerp (_btn_nextDimensionsNew, _btn_nextDimensionsOrigin, _focusTimer.PercentTimePassed);

				_turnOn [4].anchoredPosition3D = Vector3.Lerp (_btn_prevNew, _btn_prevOrigin, _focusTimer.PercentTimePassed);
				_turnOn [4].sizeDelta = Vector2.Lerp (_btn_prevDimensionsNew, _btn_prevDimensionsOrigin, _focusTimer.PercentTimePassed);

				_turnOn [5].anchoredPosition3D = Vector3.Lerp (_buttonNew, _buttonOrigin, _focusTimer.PercentTimePassed);
				_turnOn [5].sizeDelta = Vector2.Lerp (_buttonDimensionsNew, _buttonDimensionsOrigin, _focusTimer.PercentTimePassed);
			}
		} else {
			if (!_focusCapped) {
				if (_focus) {
					_turnOn [0].anchoredPosition3D = _bookNew;
					_turnOn [0].sizeDelta = _bookDimensionsNew;

					_turnOn [1].anchoredPosition3D = _cardAreaLeftDimensionsOrigin;
					_turnOn [1].sizeDelta = _cardAreaLeftDimensionsNew;

					_turnOn [2].anchoredPosition3D = _cardAreaRightNew;
					_turnOn [2].sizeDelta = _cardAreaRightDimensionsNew;

					_turnOn [3].anchoredPosition3D = _btn_nextNew;
					_turnOn [3].sizeDelta = _btn_nextDimensionsNew;

					_turnOn [4].anchoredPosition3D = _btn_prevNew;
					_turnOn [4].sizeDelta = _btn_prevDimensionsNew;

					_turnOn [5].anchoredPosition3D = _buttonNew;
					_turnOn [5].sizeDelta = _buttonDimensionsNew;

					_buttonImage.sprite = _buttonSprites [1];
				} else {
					_turnOn [0].anchoredPosition3D = _bookOrigin;
					_turnOn [0].sizeDelta = _bookDimensionsOrigin;

					_turnOn [1].anchoredPosition3D = _cardAreaLeftOrigin;
					_turnOn [1].sizeDelta = _cardAreaLeftDimensionsOrigin;

					_turnOn [2].anchoredPosition3D = _cardAreaRightOrigin;
					_turnOn [2].sizeDelta = _cardAreaRightDimensionsOrigin;

					_turnOn [3].anchoredPosition3D = _btn_nextOrigin;
					_turnOn [3].sizeDelta = _btn_nextDimensionsOrigin;

					_turnOn [4].anchoredPosition3D = _btn_prevOrigin;
					_turnOn [4].sizeDelta = _btn_prevDimensionsOrigin;

					_turnOn [5].anchoredPosition3D = _buttonOrigin;
					_turnOn [5].sizeDelta = _buttonDimensionsOrigin;

					for (int i = 0; i < _turnOff.Length; i++) {
						_turnOff [i].SetActive (true);
					}
					_glowImage [0].color = Color.white;
					_glowImage [1].color = Color.white;

					_buttonImage.sprite = _buttonSprites [0];
				}
				_focusCapped = true;
				_bookScript.CopyOfStart ();
				if (_focus) {
					if (!_flippedToFirstOnce) {
						_flippedToFirstOnce = true;
						_autoFlipScript.FlipToFirst ();
					} else {
						_autoFlipScript.isFlipping = false;
						_bookScript.interactable = true;
					}
				} else {
					_autoFlipScript.isFlipping = false;
					_bookScript.interactable = true;
				}
			}
		}
	}

	void FixedUpdate(){
		if (_cardAreaLeft._allCardsFilled && _cardAreaRight._allCardsFilled) {
			_buttonImage.enabled = true;
		} else {
			_buttonImage.enabled = false;
		}
	}

	public void FocusOnBook(){
		if (!_disableFocus) {
			if (_focusCapped) {
				_bookScript.interactable = false;
				_autoFlipScript.isFlipping = true;

				_focus = !_focus;
				_focusTimer.Reset ();
				_focusCapped = false;

				if (_focus) {
					for (int i = 0; i < _turnOff.Length; i++) {
						_turnOff [i].SetActive (false);
					}
					_glowImage [0].color = _transparentColor;
					_glowImage [1].color = _transparentColor;
				}
			}

			Events.G.Raise (new ClickOnReadEvent ());
		}
	}
}
