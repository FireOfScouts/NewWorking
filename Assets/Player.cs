using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {


	
	void Start () {
		if(isLocalPlayer || !isServer)
			CmdAddPlayer ();
	}

	[Command]
	void CmdAddPlayer(){
		Debug.Log ("Server");
		GameBehaviour.gb.AddPlayer (this);
	}
}
