using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {


	
	void Start () {
		if (isServer && isLocalPlayer)
			Destroy (this.gameObject);
		if(isLocalPlayer)
			CmdAddPlayer ();
	}

	[Command]
	void CmdAddPlayer(){
		Debug.Log ("Server");
		GameBehaviour.gb.AddPlayer (this);
	}
}
