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
	}

	[ClientRpc]
	public void RpcName(){
		if (isLocalPlayer) {
			nameInput.gameObject.SetActive (false);
			CmdGetPlayerName (nameInput.textComponent.text);
		}
	}

	[Command]
	void CmdGetPlayerName(string n){
		playerName = n;
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
	void CmdTableCards(){
//		foreach (Card c in card)
//			GameBehaviour.gb.TableCards(c);
	}
	[Command]
	public void CmdRecieveCards(/*List<Card> addTheseCards*/){
//		foreach (Card c in addTheseCards)
//			hand.Add (c);
//		RpcRecieveCards (hand);
	}
	[ClientRpc]
	void RpcRecieveCards(/*List<Card> newHand*/){
//		if (isLocalPlayer)
//			hand = newHand;
//			Debug.Log ("Player ( " + this.gameObject.name + " recieved a card" );
	}
    [Command]
    void CmdGiveCard(GameObject card){
//		GameBehaviour.gb.TableCards(card.GetComponent<Card>());
    }
}
