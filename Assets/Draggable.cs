using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public enum CardType {
	Bunny = 0, Cat = 1, Caterpillar = 2, Jabberwock = 3, Sheep = 4, Twins = 5, Flowers = 6, House = 7, Queen = 8, Walrus = 9, Null = 10
}
	
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	[SerializeField] CardType _whoAmI = CardType.Cat;

	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;

	GameObject placeholder = null;

	public CardType WhoIsThis(){
		return _whoAmI;
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("OnBeginDrag");
		Events.G.Raise (new CardDraggingEvent (true, _whoAmI));
		
		placeholder = new GameObject();
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex( this.transform.GetSiblingIndex() );
		
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent( this.transform.parent.parent );
		
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");
		
		this.transform.position = eventData.position;

		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);

		int newSiblingIndex = placeholderParent.childCount;

		for(int i=0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {

				newSiblingIndex = i;

				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;

				break;
			}
		}

		placeholder.transform.SetSiblingIndex(newSiblingIndex);

	}
	
	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("OnEndDrag");
		Events.G.Raise (new CardDraggingEvent (false, _whoAmI));
		if (parentToReturnTo.tag == "CardDrop") {
			parentToReturnTo.GetComponent<PageDropZone> ().KeepReference (this.gameObject);
		} else {
			this.transform.SetParent (parentToReturnTo);
			this.transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex ());
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy(placeholder);
	}
}
