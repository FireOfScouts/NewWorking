using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card {

    public char type, value;
    Sprite thissprite;
    GameObject currentGameObject;

    public Card(char t, char v)
    {
        type = t;
        value = v;

        currentGameObject = Resources.Load("2H") as GameObject;
        string path = "Cards/" + type + value;
        thissprite = Resources.Load(path) as Sprite;
    }

	public Card( char t, char v){
		type = t;
		value = v;
	}
    public void MakeCard()
    {
        //GameObject SpawnObject = Instantiate(currentGameObject, GameObject.Find("TableHand").transform.position, Quaternion.identity, transform.parent = GameObject.Find("TableHand").transform);
        SpawnObject.GetComponent<SpriteRenderer>().sprite = thissprite;
    }
}
