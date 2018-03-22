using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {


	
	void Start () {
		if (isLocalPlayer) {
			if (isServer) {
				Debug.Log ("Destroying ServerSided spawned player.");
				Destroy (this.gameObject);
				return;
			}
			CmdAddPlayer ();
		}
	}

	[Command]
	void CmdAddPlayer(){
		Debug.Log ("Server");
		GameBehaviour.gb.AddPlayer (this);
	}
}
