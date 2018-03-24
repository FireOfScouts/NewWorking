using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameBehaviour : NetworkBehaviour {

    List<Card>Table;
	public static GameBehaviour gb;
	List<Player> players;

	public List<char> CardType = new List<char> (4){'C','D','H','S'};
	public List<char> CardValue = new List<char> (13){'2','3','4','5','6','7','8','9','1','J','Q','K','A'};

    public Button StartGame;

	public List<Card>Deck;


	void Start () {
		if (GameBehaviour.gb == null)
			GameBehaviour.gb = this;
		else
			Destroy (this.gameObject);
		DontDestroyOnLoad (this);
		players = new List<Player> ();

        Instantiate(StartGame);
//		int ie = Enum.GetNames (typeof(CardType)).Length;
//		int je = enum.GetNam
//		for (int i =0; i< j ;i++)

        Table = new List<Card>();
        Deck= new List<Card>(CardType.Count + CardValue.Count);
		for (int i = 0; i < CardType.Count; i++)
			for (int j = 0; j < CardValue.Count; j++)
				Deck.Add (new Card (CardType [i], CardValue [j]));

			
	}

	void Update () {
//		Debug.Log ("Amount of players connected: " + players.Count);
//		if (Input.GetKeyDown(KeyCode.Q))
			AskNames ();
	}

	public Card RandomCardFromDeck(){
		int rndm = Random.Range (0,Deck.Count);
		Card c = Deck [rndm];
		Deck.Remove (Deck [rndm]);
		Deck.TrimExcess ();
		return c;
	}

	void AskNames(){
		foreach (Player p in players)
			p.RpcName();
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
    public void TableCard(Card c){
        Table.Add(c);
    }

	List<Card> Deal(int amountOfCards){
		List<Card> cards = new List<Card>(amountOfCards);
		for (int i = 0; i < amountOfCards; i++)
			cards.Add (RandomCardFromDeck());
		return cards;
	}

    public void startGame(){
        AskNames();
		for (int i = 0; i < players.Count; i++)
			players[i].CmdRecieveCards( Deal (2));
        Destroy(StartGame);
    }
}
