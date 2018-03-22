using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameBehaviour : NetworkBehaviour {

	public static GameBehaviour gb;
	List<Player> players;

	public enum CardType{}
	public enum CardValue{}
	List<Card> Deck;

	void Start () {
		if (GameBehaviour.gb == null)
			GameBehaviour.gb = this;
		else
			Destroy (this.gameObject);
		DontDestroyOnLoad (this);
		players = new List<Player> ();
//		int ie = Enum.GetNames (typeof(CardType)).Length;
//		int je = enum.GetNam
//		for (int i =0; i< j ;i++)
			
	}

	void Update () {
		Debug.Log ("Amount of players connected: " + players.Count);
		if (Input.GetKeyDown(KeyCode.Q))
			AskNames ();
	}

	void AskNames(){
		foreach (Player p in players)
			p.RpcName();
	}

	public void RecieveNames(string n){
		Debug.Log (n);
	}

	public void AddPlayer(Player p){
		players.Add (p);
	}
	public int GetPlayernumber(Player p){
		for (int i = 0; i < players.Count; i++)
			if (players [i] == p)
				return i;
		return -1;
	}
}
