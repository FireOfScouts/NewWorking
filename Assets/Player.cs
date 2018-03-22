using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	InputField nameInput;
	[SyncVar]
	public string playerName;
	
	void Start () {
		nameInput = GameObject.Find("NameInput").GetComponent<InputField>();
		if (isLocalPlayer) {
			if (isServer) {
				Destroy (nameInput.transform.parent.gameObject);
				Destroy (this.gameObject);
				return;
			}
			CmdAddPlayer ();
			CmdDefaultName ();
		}
	}

//	[Command]
//	public void CmdShowName(string n){
//		Game
//	
//	}

	[Command]
	void CmdAddPlayer(){
		GameBehaviour.gb.AddPlayer (this);
	}

	[Command]
	void CmdDefaultName(){
		playerName = "Player" + GameBehaviour.gb.GetPlayernumber (this);
	}
}
