using UnityEngine;
using System.Collections;

//public  class KeyGivenToPersonEvent : GameEvent {
//	public PersonId PersonId { get; private set; }
//	public KeyGivenToPersonEvent (PersonId personId){
//		PersonId = personId;
//	}
//}

public class StartEvent : GameEvent {
}



public class BeginDrinkingEvent : GameEvent {
	public bool IsDrinking { get; private set; }
	public BeginDrinkingEvent (bool isDrinking) {
		IsDrinking = isDrinking;
	}
}

public class TapCreditsEvent:GameEvent{
	public bool IsZoomIn { get; private set; }
	public TapCreditsEvent (bool iszoomin) {
		IsZoomIn = iszoomin;
	}
}

public class SwitchToOrderEvent:GameEvent{
}

public class SwitchToMenuEvent:GameEvent{	
}


public class CardClickedEvent:GameEvent{
	public CardType DraggingCard { get; private set; }
	public CardClickedEvent (CardType draggingCard) {
		DraggingCard = draggingCard;
	}
}

public class CardDraggingEvent:GameEvent{
	public bool IsDragging { get; private set; }
	public CardType DraggingCard { get; private set; }
	public CardDraggingEvent (bool isDragging, CardType draggingCard) {
		IsDragging = isDragging;
		DraggingCard = draggingCard;
	}
}

public class ToturialStateEvent:GameEvent{
	public int CurrentState{ get; private set;}
	public ToturialStateEvent(int curstate){
		CurrentState = curstate;	
	}
}

public class ClickOnReadEvent:GameEvent{
	
}


