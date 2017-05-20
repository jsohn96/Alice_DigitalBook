using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class OverlayToturial : MonoBehaviour {

	[SerializeField] GameObject[] _overlayImages;
	[SerializeField] Canvas _myCanvas;
	[SerializeField] Camera _myCamera;
	[SerializeField] RectTransform _trackPoint;

	bool _isSkipped = false;
	bool _isOverlayOn = false;
	bool _isCardClicked = false;
	bool _isReadClicked = false;
	bool _isZooming = false;
	int _currentImage = 0;

	void OnEnable(){
		Events.G.AddListener<ToturialStateEvent> (OnToturialStateEvent);
		Events.G.AddListener<CardClickedEvent> (OnClickCard);
		Events.G.AddListener<ClickOnReadEvent> (OnClickRead);
	}

	void OnDisable(){
		Events.G.RemoveListener<ToturialStateEvent> (OnToturialStateEvent);
		Events.G.RemoveListener<CardClickedEvent> (OnClickCard);
		Events.G.RemoveListener<ClickOnReadEvent> (OnClickRead);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// 
//		if(Input.GetMouseButtonDown(0)){
//			print ("mouse input: " + Input.mousePosition);
//			print ("canvas position: " + _myCamera.WorldToScreenPoint(Input.mousePosition));
//			FollowMouse (Input.mousePosition);
//
//		}


		if (!_isSkipped  && Input.GetMouseButtonDown (0) && !_isZooming) {
			// raycasting 
			if (!_isOverlayOn) {
				if (_currentImage == 0) {
					EnableToturial ();
				} else if (_currentImage == 2) {
					if (_isCardClicked) {
						_currentImage += 1;
					} else {
						EnableToturial ();
					}

				} else if (_currentImage == 4) {
//					if (_isReadClicked) {
//						_currentImage += 1;
//					} else {
//						EnableToturial ();
//					}
				}

			} 

//			else {
//				DisableToturial ();
//			}
		}

		if (_isZooming) {
			if (_isOverlayOn) {
				_overlayImages [_currentImage].SetActive (false);
				//_currentImage -= 1;
				_isOverlayOn = false;	
			}
		} 
		
	}
		
	//proceed the toturial 
	void EnableToturial(){
		if(!_isOverlayOn && _currentImage <  _overlayImages.Length){
			_overlayImages [_currentImage].SetActive (true);
			_isOverlayOn = true;	
		}

		
	}

	public void DisableToturial(){
		if (_isOverlayOn) {
			_overlayImages [_currentImage].SetActive (false);
			_isOverlayOn = false;
			if (_currentImage < _overlayImages.Length) {
				_currentImage += 1;
			} else if (_currentImage == _overlayImages.Length) {
				_isSkipped = true;
			}
		}
	}

	public void SkipTorutial(){
		if (!_isSkipped) {
			_isSkipped = true;
			print ("skip");
			DisableToturial ();
		}
		
	}

	void OnToturialStateEvent(ToturialStateEvent e){
		if (!_isSkipped) {
			if (e.CurrentState == 1) {
				print ("show new");
				EnableToturial();
			}else if (e.CurrentState == 3) {
				print ("show new");
				EnableToturial();
			}
		
		}

	}

	void OnClickCard(CardClickedEvent e){
		_isCardClicked = true;
	}

	void OnClickRead(ClickOnReadEvent e){
		_isReadClicked = true;
		_isZooming = !_isZooming;
	}

	void FollowMouse(Vector3 screenpos){
		Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, screenpos);
		_trackPoint.anchoredPosition = screenPoint;
	}

}
