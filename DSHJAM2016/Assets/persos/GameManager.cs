using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] characterPerfabs ;

    public static GameManager instance = null;

	private List<Player> players;

    private bool gameStarted = false;

    void Awake()
    {
        //singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (!gameStarted && SceneManager.GetActiveScene ().name == "zbra") {
			setupCharacters ();
		}
    }

	void characterSelect(List<Player> p)
    {
        players = p;

        SceneManager.LoadScene("zbra");
    }
    void setupCharacters()
    {
        int i = 0;
        foreach (Player pl in players)
        {
            Vector3 pos = Vector3.left * 5 * i;
            //TODO character selection
            Debug.Log("waddup?");
			Transform p = Instantiate(characterPerfabs[pl.skin]);
            p.position = pos;
			p.SendMessage ("SetupPlayer", pl);
            i++;
        }

       
        gameStarted = true;
    }
}
