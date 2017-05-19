using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public bool isOver = false;
	[SerializeField] RectTransform _thisRectTransform;
	Vector3 _hoverPosition = new Vector3(0.0f, 12.0f, 0.0f);
	bool _isDragging = false;
	Draggable _draggableScript;
	CardType _whoAmI;

	void Start(){
		_draggableScript = GetComponent<Draggable> ();
		_whoAmI = _draggableScript.WhoIsThis ();
	}

	public void OnPointerEnter(PointerEventData eventData){
		if (!_isDragging) {
			Debug.Log ("Mouse enter");
			isOver = true;
			_thisRectTransform.anchoredPosition3D = _hoverPosition;
			Events.G.Raise (new CardClickedEvent (_whoAmI));
		}
	}

	public void OnPointerExit(PointerEventData eventData){
		Debug.Log ("Mouse exit");
		isOver = false;
		_thisRectTransform.anchoredPosition3D = Vector3.zero;
		if (!_isDragging) {
			Events.G.Raise (new CardClickedEvent (CardType.Null));
		}
	}

	void CardDraggingHandler(CardDraggingEvent e){
		_isDragging = e.IsDragging;
	}

	void OnEnable(){
		Events.G.AddListener<CardDraggingEvent> (CardDraggingHandler);
	}

	void OnDisable(){
		Events.G.RemoveListener<CardDraggingEvent> (CardDraggingHandler);
	}
}
