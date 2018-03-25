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

        MouseRaycast();
//		if(Input.GetKeyDown(KeyCode.Escape))
//			NetworkClient.
	}

    [ClientRpc]
    public void MouseRaycast()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    [ClientRpc]
    public void SetPlayerDeck(List <Card>PlayerCards)
    {
        foreach (Card card in PlayerCards)
        {
            card.MakeCard();
        }
    }

	#region Commands
	[Command]
	void CmdGetPlayerName(string n){
		playerName = n;
		gameObject.name = playerName;
	}
	[Command]
	void CmdAddPlayer(){
		GameBehaviour.gb.AddPlayer (this);
	}
	[Command]
	void CmdDefaultName(){
		playerName = "Player" + GameBehaviour.gb.GetPlayernumber (this);
		gameObject.name = playerName;
	}
	[Command]
	void CmdTableCards(){
		
	}
	[Command]
	public void CmdRecieveCards(char t,char v ){
		if (hand == null)
			hand = new List<Card> ();
		Card card = new Card (t,v); 
		hand.Add (card);
		RpcResetHand (hand.Count);
		foreach(Card c in hand)
			RpcRecieveCards (c.type,c.value);
	}
	#endregion
	#region ClientRpc's
	[ClientRpc]
	void RpcResetHand(int count){
		if (!isLocalPlayer) return;
		hand = new List<Card> (count);
	}
	[ClientRpc]
	void RpcRecieveCards(char t, char v){
		if (!isLocalPlayer) return;
		Card newCard = new Card (t,v);
		hand.Add (newCard);
	}
	[ClientRpc]
	public void RpcName(){
		if (!isLocalPlayer) return;
		nameInput.gameObject.SetActive (false);
		if (nameInput.textComponent.text != "")
			playerName = nameInput.textComponent.text;
		CmdGetPlayerName (playerName);
	}
	#endregion
}
