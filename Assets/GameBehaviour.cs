using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameBehaviour : NetworkBehaviour {

	public static GameBehaviour gb;
	List<Player> players;

	void Start () {
		if (GameBehaviour.gb == null)
			GameBehaviour.gb = this;
		else
			Destroy (this.gameObject);
		DontDestroyOnLoad (this);
		players = new List<Player> ();
	}

	void Update () {
		Debug.Log ("Amount of players connected: " + players.Count);
		if (Input.GetKeyDown(KeyCode.Q))
		ShowNames ();
	}

	void ShowNames(){
		foreach (Player p in players)
			Debug.Log (p.GetComponent<Player>().GetPlayerName());
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
