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
    Sprite thissprite;
    GameObject currentGameObject;

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

    void Update()
    {
        if (!isLocalPlayer)
            return;
    }

    public void RpcInstantateCard(char type, char value)
    {
        Debug.Log(type + "" + value);
        currentGameObject = Resources.Load("PrefabCard") as GameObject;
        string path = type + "" + value;
        Debug.Log(path);

        GameObject SpawnObject = Instantiate(currentGameObject, GameObject.Find("TableHand").transform.position, Quaternion.identity, transform.parent = GameObject.Find("TableHand").transform);
        SpawnObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(value + "" + type) as Sprite;
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
	void CmdTableCards(char t,char v){
		GameBehaviour.gb.TableCards (t, v);
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
	void TableCard(GameObject c){
		char[] tv = c.name.Split (' ', 2);
		CmdTableCards (tv[1],tv[0]);
	}
    public void RpcSetPlayerDeck(Card currentCard)
    {
        foreach (Card card in hand)
        {
            RpcInstantateCard(currentCard.type, currentCard.value);
        }
    }
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
        RpcSetPlayerDeck(newCard);
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
