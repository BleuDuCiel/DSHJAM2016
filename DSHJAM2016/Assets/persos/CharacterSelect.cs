using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Inputs;


public class CharacterSelect : MonoBehaviour
{

    [SerializeField]
    private Transform[] m_Characters;

    [SerializeField]
    public Sprite[] numbers;

	[SerializeField]
	public SpriteRenderer[] jx;

	public Texture2D graphic;
	private Sprite[] graphics;

    private int nbPlayers = 0;
    private static int nbMaxPlayers = 4;
	private bool[] slots;
	private int[] skins;

	private List<Player> players;

    // Use this for initialization
    void Start()
    {
		players = new List<Player>(nbMaxPlayers); // I hate stacks
		slots = new bool[nbMaxPlayers];
		skins = new int[nbMaxPlayers];
		//for ( int i = 0; i < nbMaxPlayers;i++ ) // I hate C#
		//	slots[i] = -1;

		graphics = Resources.LoadAll<Sprite>(graphic.name);
    }


    // Update is called once per frame
    void Update()
    {
        // Listen for jumps on JS
        for (int i = 0; i < nbMaxPlayers; i++)
        {
            if (Input.GetButtonDown("JS" + "Jump" + i))
                addPlayerJS(i);
            if (Input.GetButtonDown("KB" + "Jump" + i))
                addPlayerKB(i);
        }

        // Listen for items on JS
        for (int i = 0; i < nbMaxPlayers; i++)
        {
            if (Input.GetButtonDown("JS" + "Item" + i))
                delPlayerJS(i);
            if (Input.GetButtonDown("KB" + "Item" + i))
                delPlayerKB(i);
        }
			
		// Listen for move on JS
		for (int i = 0; i < nbMaxPlayers; i++) {
			float axis = Input.GetAxis("JS" + "Move" + i);
			if (axis == 0) continue;
			//skins [i] = (skins[i] + (int) (axis > 0) - (int) (axis < 0)) % (skins.Length);
		}
	
        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("SUBMIT");
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("characterSelect", players);

        }

		// Display players and skins
		for (int i = 0; i < nbMaxPlayers; i++)
			if (!slots[i]) jx[i].sprite = graphics[0];
			else jx[i].sprite = graphics[skins[i]];			
    }

	void addPlayer(string pad, int id=0) {
	}

	void addPlayerJS(int id=0) {
		// First, check slot status
		if (slots[id]) return;

		// Then create a player with his own JS, shitty gameplay will come soon
		//players.Add(new Player(nbPlayers, new LeftHand("JS",id.ToString()), new RightHand("JS",id.ToString())));
		slots [id] = true;
		skins [id] = Random.Range (1, graphics.Length);
		nbPlayers++;
		Debug.Log ("New player with " + Input.GetJoystickNames()[id] + "! Current players : " + nbPlayers + " " + players.Count + "[" + slots[0] + ","+ slots[1]+ "," + slots[2]+ ","+ slots[3] +"]");
	}

	void delPlayerJS(int id=0) {
		// First, check slot status
		if (!slots[id]) return;

		// Then delete a player with his JS id
		//players.RemoveAt(slots[id]);
		slots [id] = false;
		nbPlayers--;
		Debug.Log ("Player abandon :( Current players : " + nbPlayers + " " + players.Count + "[" + slots[0] + "," + slots[1] + ","+ slots[2]+ ","+ slots[3] +"]");
	}

    void addPlayerKB(int id = 0)
    {
        // First, check slot status
        if (slots[id]) return;

        // Then create a player with his own KB, shitty gameplay will come soon
        players.Add(new Player(nbPlayers, new LeftHand("KB", id.ToString()), new RightHand("KB", id.ToString())));
        slots[id] = true;
        nbPlayers++;
        //Debug.Log("New player with " + Input.GetJoystickNames()[id] + "! Current players : " + nbPlayers);
    }
    void delPlayerKB(int id = 0)
    {
        // First, check slot status
        if (!slots[id]) return;

        // Then delete a player with his JS id
        //players.RemoveAt(slots[id]);
        slots[id] = false;
        nbPlayers--;
        Debug.Log("Player abandon :( Current players : " + nbPlayers);
    }
}
