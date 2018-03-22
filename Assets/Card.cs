using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

	public char type, value;

	public Card( char t, char v){
		type = t;
		value = v;
	}
}
