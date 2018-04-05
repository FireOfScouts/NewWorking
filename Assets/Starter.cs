using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {

	public static Starter s;

	public GameObject canvas;
	public GameObject nameInp;
	public GameObject button;

	void Start () {
		Starter.s = this;

		#if UNITY_ANDROID || UNITY_IOS
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		#endif

		canvas = GameObject.Find ("Canvas");
		nameInp = canvas.transform.GetChild (0).gameObject;
		button = canvas.transform.GetChild (1).gameObject;

		nameInp.SetActive (false);
		button.SetActive (false);
	}
}
