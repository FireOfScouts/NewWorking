using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameBehaviour : NetworkBehaviour {

    List<Card>Table;
	public static GameBehaviour gb;
	List<Player> players;
	List<List<Card>> CardsForEveryPlayer;

	public List<char> CardType = new List<char> (4){'C','D','H','S'};
	public List<char> CardValue = new List<char> (13){'2','3','4','5','6','7','8','9','1','J','Q','K','A'};

    public Button StartGame;
	int amountCardsPerPlayer = 3;

	public List<Card>Deck;

	//Setting 


	void Start () {
		if (GameBehaviour.gb == null)
			GameBehaviour.gb = this;
		else
			Destroy (this.gameObject);
		DontDestroyOnLoad (this);

		Starter.s.button.SetActive (true);
		players = new List<Player> ();

		Table = new List<Card>();
		Deck= new List<Card>(CardValue.Count * CardType.Count);

		for (int i = 0; i < CardValue.Count; i++)
			for (int j = 0; j < CardType.Count; j++)
				Deck.Add (new Card (CardValue [i], CardType[j]));

//        Instantiate(StartGame);
	}

//	void Update () {
////		Debug.Log ("Amount of players connected: " + players.Count);
////		if (Input.GetKeyDown(KeyCode.Q))
//			AskNames ();
//	}

	public void InstantiateCard(Card card){
		GameObject SpawnObject = Instantiate(Resources.Load<GameObject>("PrefabCard"), Starter.s.canvas.transform);
		SpawnObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(card.value + "" + card.type);
		SpawnObject.GetComponent<RectTransform>().sizeDelta = new Vector2(336, 452);
		SpawnObject.name = card.value.ToString() + card.type.ToString();
		StartCoroutine (MoveCard (SpawnObject));
	}

	IEnumerator MoveCard(GameObject go){
		float timer = 0f;
		RectTransform rect = go.GetComponent<RectTransform> ();
		Vector2 endPos = new Vector2 (Screen.width/2,Screen.height/2);
		Vector2 direction = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized;
		Vector2 startPos = -direction * 700f;
		float offCenter = Random.Range (-10f, 10f);
		while (timer < 0.5f) {
			rect.localRotation = new Quaternion(0f, 0f, 3f /timer/0.5f,3f);
			rect.position = Vector2.Lerp(startPos,endPos + direction * offCenter,timer/0.5f);
			yield return 0;
			timer += Time.deltaTime;
		}
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
			if (p != null)
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
	public void TableCards(char v, char t){
		Card c = new Card (v,t);
	  	Table.Add(c);
		InstantiateCard (c);
    }

//	List<Card> Deal(int amountOfCards){
//		List<Card> cards = new List<Card>(amountOfCards);
//		for (int i = 0; i < amountOfCards; i++)
//			cards.Add (RandomCardFromDeck());
//		return cards;
//	}

    public void startGame(){
		if (players.Count < 2)
			return;
		AskNames ();
		foreach (Player p in players)
			if (p != null)
				for (int j = 0; j < amountCardsPerPlayer; j++) {
					Card rndmCard = RandomCardFromDeck ();
					p.CmdRecieveCards (rndmCard.value, rndmCard.type);
				}
		Destroy(Starter.s.button);
   }
}	
