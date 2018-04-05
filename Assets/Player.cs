using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {
    public List <Card>hand;
	[SyncVar]
	public string playerName;

	GameObject cardPrefab;
	GameObject tablePlayer;

    int currentpos = 0;

    void Start () {
		if (isLocalPlayer) {
			if (isServer) {
				Destroy (this.gameObject);
				return;
			}
			cardPrefab = Resources.Load("PrefabCard") as GameObject;
			Starter.s.nameInp.gameObject.SetActive (true);

			CmdAddPlayer ();
			CmdDefaultName ();
			CreateTable ();
		}
	}

	public void InstantiateCard(Card card){
		GameObject SpawnObject = Instantiate(cardPrefab, GameObject.Find("TableHand").transform);
        RectTransform rect = SpawnObject.GetComponent<RectTransform>();
        SpawnObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(card.value + "" + card.type);
        rect.sizeDelta = new Vector2(336, 452);
        rect.position = new Vector2(rect.sizeDelta.x / 2 + Screen.width * 0.1f * currentpos, Screen.height * 0.3f);
		SpawnObject.name = card.value.ToString() + card.type.ToString();
        currentpos++;
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
	void CmdTableCards(char v,char t){
		GameBehaviour.gb.TableCards (v, t);
	}
	[Command]
	public void CmdRecieveCards(char v,char t ){
		if (hand == null)
			hand = new List<Card> ();
		Card card = new Card (v,t); 
		hand.Add (card);
//		RpcResetHand (hand.Count);
//		foreach(Card c in hand)
			RpcRecieveCards (v,t);
	}
    #endregion
    #region ClientRpc's
	public void TableCard(GameObject c){
		CmdTableCards (c.name.ToCharArray()[0],c.name.ToCharArray()[1]);
		Destroy (c);
	}

	void CreateTable(){
		tablePlayer = Instantiate (Resources.Load<GameObject> ("Table"), Starter.s.canvas.transform);
		tablePlayer.transform.GetChild (0).GetComponent<DropZone> ().player = this;
	}

//    [ClientRpc]
//	void RpcResetHand(int count){
//		if (!isLocalPlayer) return;
//		hand = new List<Card> (count);
//	}

	[ClientRpc]
	public void RpcRecieveCards(char v, char t){
		if (!isLocalPlayer) return;
		if (hand == null)
			hand = new List<Card> ();
		Card newCard = new Card (v,t);
		hand.Add (newCard);
		InstantiateCard(newCard);
    }
	[ClientRpc]
	public void RpcName(){
		if (!isLocalPlayer) return;
		Starter.s.nameInp.gameObject.SetActive (false);
		if (Starter.s.nameInp.textComponent.text != "")
			playerName = Starter.s.nameInp.textComponent.text;
		CmdGetPlayerName (playerName);
	}
	#endregion
}
