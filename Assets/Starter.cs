using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {

	public static Starter s;

	public GameObject canvas;
	public UnityEngine.UI.InputField nameInp;
	public GameObject button;

	void Start () {
		Starter.s = this;

		#if UNITY_ANDROID || UNITY_IOS
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		#endif

		canvas = GameObject.Find ("Canvas");
		nameInp = canvas.transform.GetChild (0).GetComponent<UnityEngine.UI.InputField>();
		button = canvas.transform.GetChild (1).gameObject;

		nameInp.gameObject.SetActive (false);
		button.SetActive (false);
	}
}
