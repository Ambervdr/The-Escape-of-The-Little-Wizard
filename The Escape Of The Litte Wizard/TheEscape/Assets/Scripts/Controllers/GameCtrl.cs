using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;                                   // Gets access to the Unity UI elements
using System.IO;                                        // For working with files
using System.Runtime.Serialization.Formatters.Binary;   // Helps for Serialization

/// <summary>
/// Manages the important gameplay features like keeping the score, restarting levels, health etc.
/// </summary>
public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    public GameData data;               // To work with the game data in the inspector
    public UI ui;                       // For  neatly arranging UI elements
    public GameObject bigCoin;          // Reward the player when an enemy is killed
    public int coinValue;               // Value of a coin
    public int bigCoinValue;            // Value of one big coin
    public int enemyValue;              // Value of one enemy
    public float maxTime;               // Max time allowed to complete the level

    public enum Item
    {
        Coin,
        BigCoin,
        Enemy
    }

    string dataFilePath;                // Path to store the data file
    BinaryFormatter bf;                 // Helps in saving/loading to binary files
    float timeLeft;                     // Time left before the timer goes off;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        bf = new BinaryFormatter();
        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }

    // Use this for initialization
    void Start ()
    {
        timeLeft = maxTime;

        HandleFirstBoot();

        UpdateHearts();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetData();
        }

        if (timeLeft > 0)
        {
            UpdateTimer();
        }
	}

    public void SaveData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);

        fs.Close(); //Always close it to not get errors
    }

    public void LoadData()
    {
        if (File.Exists(dataFilePath))
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);

            //Debug.Log("Number of coins = " + data.coinCount);
            ui.txtCoinCount.text = " x " + data.coinCount;
            ui.txtScore.text = "Score: " + data.score;
            fs.Close();
        }
    }

    void OnEnable()
    {
        Debug.Log("Data Loaded");
        LoadData();
    }

    private void OnDisable()
    {
        Debug.Log("Data Saved");
        SaveData();
    }

    void ResetData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);

        //Reset all the data items
        data.coinCount = 0;
        ui.txtCoinCount.text = " x 0";

        data.score = 0;
        ui.txtScore.text = "Score: " + data.score;

        for (int keyNumber = 0; keyNumber <= 2; keyNumber++)
        {
            data.keyFound[keyNumber] = false;
        }

        data.lives = 3;
        UpdateHearts();

        bf.Serialize(fs, data);
        fs.Close();

        Debug.Log("Data Reset");
    }

    /// <summary>
    /// Restarts the level when the player dies
    /// </summary>
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);

        CheckLives();
        //Invoke("RestartLevel", restartDelay);
    }

    public void PlayerDiedAnimation(GameObject player)
    {
        // throw the player back in the air
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-150f, 400f));

        // rotate the player
        player.transform.Rotate(new Vector3(0, 0, 45f));

        // disable the PlayerManager script
        player.GetComponent<PlayerManager>().enabled = false;

        // disable the colliders attached to the player so that the player can fall through the ground
        foreach (Collider2D c2d in player.transform.GetComponents<Collider2D>())
        {
            c2d.enabled = false;
        }

        // disable the child gameObjects attached to the player cat
        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }

        // disable the camera attached with the player
        Camera.main.GetComponent<CameraCtrl>().enabled = false;

        // set the velocity of the cat to zero
        rb.velocity = Vector2.zero;

        // restart the level
        StartCoroutine("PauseBeforeReload", player);
    }

    public void PlayerStompsEnemy(GameObject enemy)
    {
        // change the enemy's tag so that the enemy can't kill the player
        enemy.tag = "Untagged";

        // destroy the enemy
        Destroy(enemy);

        // update the score
        UpdateScore(Item.Enemy);
    }

    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(1.5f); // causes the specified delay
        PlayerDied(player);
    }

    public void UpdateCoinCount()
    {
        data.coinCount += 1;

        // Updates the coins in the HUD
        ui.txtCoinCount.text = " x " + data.coinCount;
    }

    public void UpdateScore(Item item)
    {
        int itemValue = 0;

        switch (item)
        {
            case Item.BigCoin:
                itemValue = bigCoinValue;
                break;

            case Item.Coin:
                itemValue = coinValue;
                break;

            case Item.Enemy:
                itemValue = enemyValue;
                break;

            default:
                break;
        }

        data.score += itemValue;

        // Updates the score in the HUD
        ui.txtScore.text = "Score: " + data.score;
    }

    /// <summary>
    /// Called when the player bullter hits the enemy
    /// </summary>
    public void BulletHitEnemy(Transform enemy)
    {
        // show the enemy explosion SFX
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);

        // show the big coin
        Instantiate(bigCoin, pos, Quaternion.identity);

        // destroy the enemy
        Destroy(enemy.gameObject);

        // play the explode sound
        AudioCtrl.instance.EnemyExplosion(pos);

    }

    public void UpdateKeyFound(int keyNumber)
    {
        data.keyFound[keyNumber] = true;

        if (keyNumber == 0)
        {
            ui.key0.sprite = ui.key0Full;
        }
        else if (keyNumber == 1)
        {
            ui.key1.sprite = ui.key1Full;
        }
        else if (keyNumber == 2)
        {
            ui.key2.sprite = ui.key2Full;
        }
    }

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;

        ui.txtTimer.text = "Timer: " + (int)timeLeft;

        if (timeLeft <= 0)
        {
            ui.txtTimer.text = "Timer: 0";

            // Inform the GameCtrl to do the needful
            GameObject player = GameObject.FindGameObjectWithTag("Player") as GameObject;
            PlayerDied(player);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("Game");
    }

    void HandleFirstBoot()
    {
        if (data.isFirstBoot)
        {
            // set lives to 3
            data.lives = 3;

            // set number of coins to 0
            data.coinCount = 0;

            // set keys collected to 0
            data.keyFound[0] = false;
            data.keyFound[1] = false;
            data.keyFound[2] = false;

            // set score to 0
            data.score = 0;

            // set isFirstBoot to false
            data.isFirstBoot = false;
        }
    }

    void UpdateHearts()
    {
        if (data.lives == 3)
        {
            ui.heart1.sprite = ui.fullHeart;
            ui.heart2.sprite = ui.fullHeart;
            ui.heart3.sprite = ui.fullHeart;
        }

        if (data.lives == 2)
        {
            ui.heart1.sprite = ui.emptyHeart;
        }

        if (data.lives == 1)
        {
            ui.heart2.sprite = ui.emptyHeart;
            ui.heart1.sprite = ui.emptyHeart;
        }
    }

    void CheckLives()
    {
        int updatedLives = data.lives;
        updatedLives -= 1;
        data.lives = updatedLives;

        if (data.lives == 0)
        {
            data.lives = 3;
            SaveData();
            Invoke("GameOver", restartDelay);
        }
        else
        {
            SaveData();
            Invoke("RestartLevel", restartDelay);
        }
    }

    void GameOver()
    {
        ui.panelGameOver.SetActive(true);
    }
}
