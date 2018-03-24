using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {
    public List <Card>hand;
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

	public void RecieveCards(Card addTheseCards){
		hand.Add (addTheseCards);
		RpcResetHand (hand.Count);
		foreach(Card c in hand)
			RpcRecieveCards (/*c as GameObject*/);
	}
	[ClientRpc]
	void RpcResetHand(int count){
		if (isLocalPlayer)
			hand = new List<Card> (count);
	}

	[ClientRpc]
	void RpcRecieveCards(/*GameObject newCard*/){
//		if (isLocalPlayer)
//			hand.Add (newCard.GetComponent<Card>());
	}
    [Command]
    void CmdGiveCard(GameObject card){
//		GameBehaviour.gb.TableCards(card.GetComponent<Card>());
    }
}
