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
		if (isLocalPlayer) {
			nameInput = GameObject.Find("NameInput").GetComponent<InputField>();
			if (isServer) {
				Destroy (nameInput.gameObject);
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
	[ClientRpc]
	public void RpcName(){
		if(isLocalPlayer)
			CmdGetPlayerName(nameInput.gameObject.transform.GetChild(1).GetComponent<Text>().text);
	}

	[Command]
	void CmdGetPlayerName(string n){
		playerName = n;
		Debug.Log (playerName);
	}

	[Command]
	void CmdAddPlayer(){
		GameBehaviour.gb.AddPlayer (this);
	}

	[Command]
	void CmdDefaultName(){
		playerName = "Player" + GameBehaviour.gb.GetPlayernumber (this);
	}
}
