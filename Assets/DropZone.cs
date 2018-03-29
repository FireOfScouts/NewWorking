using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropZone : MonoBehaviour, IDropHandler{


    public void OnDrop(PointerEventData eventData)
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().TableCard(eventData.pointerDrag.gameObject);
    }
}
