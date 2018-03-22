using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {
    List <Card>hand;
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

	void Update(){
		if (!isLocalPlayer)
			return;
//		if(Input.GetKeyDown(KeyCode.Escape))
//			NetworkClient.
	}

	[ClientRpc]
	public void RpcName(){
		if(isLocalPlayer)
			CmdGetPlayerName(nameInput.textComponent.text);
	}

	[Command]
	void CmdGetPlayerName(string n){
		playerName = n;
		GameBehaviour.gb.RecieveNames (playerName);
	}

	[Command]
	void CmdAddPlayer(){
		GameBehaviour.gb.AddPlayer (this);
	}

	[Command]
	void CmdDefaultName(){
		playerName = "Player" + GameBehaviour.gb.GetPlayernumber (this);
	}
    [Command]
    void CmdGiveCard(GameObject card){
        GameBehaviour.gb.RecieveCard(card.GetComponent<Card>());
    }
}
