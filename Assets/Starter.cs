using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class Starter : MonoBehaviour {

	public static Starter s;

	public NetworkID netId;
	public NetworkManager manager;
	public GameObject canvas;
	public UnityEngine.UI.InputField nameInp;
	public GameObject button;

	void Start () {
		Starter.s = this;

		#if UNITY_ANDROID || UNITY_IOS
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		#endif

		manager = GetComponent<NetworkManager> ();
		canvas = GameObject.Find ("Canvas");
		nameInp = canvas.transform.GetChild (0).GetComponent<UnityEngine.UI.InputField>();
		button = canvas.transform.GetChild (1).gameObject;

		nameInp.gameObject.SetActive (false);
		button.SetActive (false);
	}

	public void CreateMatch () {
		manager.StartMatchMaker ();
		manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, false, "", "", "", 0, 0, manager.OnMatchCreate);
	}

	public void FindMatch () {
		manager.StartMatchMaker ();
		manager.matchMaker.ListMatches (0, 20, "", false, 0, 0, manager.OnMatchList);
	}

	public void JoinMatch () {
		manager.matchMaker.JoinMatch (netId, "", "", "", 0, 0, manager.OnMatchJoined);
	}
}
