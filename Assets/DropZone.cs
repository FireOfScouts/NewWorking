using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropZone : MonoBehaviour, IDropHandler{
	public Player player;

    public void OnDrop(PointerEventData eventData)
    {
		if (player != null)
        player.TableCard(eventData.pointerDrag.gameObject);
    }
}
