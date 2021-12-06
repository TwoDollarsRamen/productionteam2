using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class HoverableText : MonoBehaviour {
	public Color hoverColor;

	Color originalColor;

	Text text;
	Button button;

	void Start() {
		text = GetComponent<Text>();

		originalColor = text.color;

		button = transform.parent.GetComponent<Button>();
		if (button == null) {
			Debug.LogError("Hoverable text must have a parent that is a button.");
		}

		button.gameObject.AddComponent<EventTrigger>();

		{
			EventTrigger.Entry eventtype = new EventTrigger.Entry();
			eventtype.eventID = EventTriggerType.PointerEnter;
			eventtype.callback.AddListener((eventData) => { OnMouseEntry(); });
			
			button.GetComponent<EventTrigger>().triggers.Add(eventtype);
		}
		
		{
			EventTrigger.Entry eventtype = new EventTrigger.Entry();
			eventtype.eventID = EventTriggerType.PointerExit;
			eventtype.callback.AddListener((eventData) => { OnMouseExit(); });
			
			button.GetComponent<EventTrigger>().triggers.Add(eventtype);
		}

	}

	void OnMouseEntry() {
		text.color = hoverColor;
	}

	void OnMouseExit() {
		text.color = originalColor;
	}
}
