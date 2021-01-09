using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
	// Параметры уровня
	public static string SceneName = "GameMoonScene";
	private bool Start_Wait_Indicator = false;

	public struct Level_Struct
	{
		public int Level_Index;
		public string Level_Name;
		public int Enemy_Meteor_quantity;
		//public string Scene_Name;
	}
	
	// Параметры оружия
	public struct Weapon_Struct
	{
		public string Name;
		public int Power;
		public int Speed;
		public int Ammo;
		public GameObject Weapon_GameObject;
		public Texture2D  Weapon_Texture; 
		public Vector2    Weapon_Position;
	}

	// Параметры игрока
	public  struct Player_Struct
	{
		public int  Life;
		public int  Armor; 
		public float Speed;
		public int  Fuel;
		public int  Score;
		public Weapon_Struct[] Weapons;		
	}

	public Transform Player_Sprite;
	//private float Player_Movement_Speed = 3.0f;  

	public static Player_Struct Ship;
	public Level_Struct[] Levels = new Level_Struct[2];

	public static int Player_Score = 0;
	public static int Player_Life = 3; 

	public static GameObject Player;
	public static GameObject LaserBullet_Gameobject= null;
	private SpriteRenderer LaserBuller_SpriteRenderer = null;
	private SpriteRenderer ShipSpriteRenderer = null;

	private Texture2D Laser_Texture2D = null;
	private Texture2D ShipTexture2D = null;
	//private int Player_Health =3;

	private GameObject[] LaserBullet_List;
	private GameObject[] Player_List;

	private GameObject Current_Laser;
	private BoxCollider2D LaserBullet_BoxCollider2D;

	private Rigidbody2D LaserBullet_rigidBody2D;

	private Object Indicators;

//	//ingame indicators
//	private Text Life_Text;
//	private Text Fuel_Text;
//	private Text Weapon_Name_Text;
//	private Text Weapon_Ammo_Text;
//	private Text Score_text;

	//private int Curent_Level = 1;
	public int Curent_Weapon;
	private Weapon_Struct Weapon;

	public IEnumerator Wait()
	{
		yield return new WaitForSeconds(2);   
		InterfaceScript.Display_Text.text =	 "";
		Ship.Life = 3;
		EnemyScript.Enemy_Meteor_Kills_Counter = 0;
		EnemyScript.Enemy_OnScreen_Counter = 0;
		Application.LoadLevel("MenuScene");
	}

	//Текст на экран - миссия пройдена
 


	// При запуске
	void Start () 
	{
		Laser_Texture2D = Resources.Load("Graphics/Ammunition/Laser", typeof(Texture2D)) as Texture2D;
		Indicators = Resources.Load("Prefabs/IntefacePrefab");
		Indicators = Instantiate(Indicators);

		// Настройка параметров оружия и корабля
		ShipTexture2D = Resources.Load(WorkshopScript.CurrentShipAddress, typeof(Texture2D)) as Texture2D;
		ShipSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
		ShipSpriteRenderer.sprite = Sprite.Create(ShipTexture2D, new Rect (0.0f, 0.0f, 256.0f, 256.0f), new Vector2 (0.0f, 0.0f), 128.0f);

		Curent_Weapon = 0;
		Weapon.Ammo = 0;
		Weapon.Name = "LASER GUN";
//		Ship.Weapons[0] = Weapon;

		Ship.Armor = 0;
		Ship.Fuel = 100;
		Ship.Life = 3;
		Ship.Score = 0;
		//Ship.Speed = 3.0f;

		// Настройка параметров уровня
		Levels [0].Enemy_Meteor_quantity = 15;
		Levels [0].Level_Index = 1;
		Levels [0].Level_Name = "Moon";
		//Levels [0].Scene_Name = "Main_Game";
		Levels [1].Enemy_Meteor_quantity = 15;
		Levels [1].Level_Index = 2;
		Levels [1].Level_Name = "Mars";
		//Levels [1].Scene_Name = "Main_Game";

		// Обновление индикаоторов
		InterfaceScript.Life_Text = GameObject.Find("Life_Indicator").GetComponent<Text>();
		InterfaceScript.Fuel_Text = GameObject.Find("Fuel_Indicator").GetComponent<Text>();
		InterfaceScript.Weapon_Name_Text = GameObject.Find("Weapon_name_Indicator").GetComponent<Text>();
		InterfaceScript.Weapon_Ammo_Text = GameObject.Find("Weapon_Ammo_Indicator").GetComponent<Text>();
		InterfaceScript.Score_Text = GameObject.Find("Score_Indicator").GetComponent<Text>();
		InterfaceScript.Display_Text = GameObject.Find("Display_Text").GetComponent<Text>();

	}


	void Update ()
	{
		if (Start_Wait_Indicator == true) 
		{
			StartCoroutine (Wait ());
			Start_Wait_Indicator = false;
			InterfaceScript.Display_Text.text =	 "";
			//Time.timeScale = 1;
		}
	}

	// Перед обновлением
	void FixedUpdate() 
	{
//		Debug.Log (Curent_Weapon.ToString());
//		Debug.Log (Ship.Life.ToString());
//		Debug.Log (Ship.Fuel.ToString());
		//Debug.Log (Ship.Score.ToString());
		//Debug.Log (Ship.Armor.ToString ());
		float Player_Current_Movement = Input.GetAxis("Horizontal") * Ship.Speed*Time.deltaTime;

		// indicators
		//Life_Text.text = "LIFE ";
		InterfaceScript.Life_Text.text = "Life " + Ship.Life.ToString();
		InterfaceScript.Fuel_Text.text = "Fuel " + Ship.Fuel.ToString();
		//Weapon = Ship.Weapons[Curent_Weapon];
		//InterfaceScript.Weapon_Name_Text.text = Weapon.Name;
		//InterfaceScript.Weapon_Ammo_Text.text = Weapon.Ammo.ToString();
		InterfaceScript.Weapon_Name_Text.text = "LASER GUN";
		InterfaceScript.Weapon_Ammo_Text.text =  "FULL";

		InterfaceScript.Score_Text.text = "Score " + Ship.Score.ToString();
		//indicators

		//Player move and fire
		//float Player_Current_Movement = Input.GetAxis("Horizontal") * Ship.Speed*Time.deltaTime;
		Player_Sprite.transform.position = new Vector2 (Player_Sprite.transform.position.x + Player_Current_Movement, Player_Sprite.transform.position.y);

		if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0)) 
		{
			LaserBullet_Gameobject = new GameObject("Laser");
			LaserBullet_Gameobject.transform.localPosition = new Vector3(Player_Sprite.transform.position.x ,Player_Sprite.transform.position.y+1.2f, 0.0f);
			LaserBuller_SpriteRenderer = LaserBullet_Gameobject.AddComponent<SpriteRenderer>(); 
			LaserBuller_SpriteRenderer.sprite = Sprite.Create(Laser_Texture2D, new Rect (0.0f, 0.0f, 256.0f, 256.0f), new Vector2 (0.0f, 0.0f), 256.0f);
			LaserBuller_SpriteRenderer.sortingOrder = 0;
			LaserBullet_BoxCollider2D = LaserBullet_Gameobject.AddComponent<BoxCollider2D>(); 
			LaserBullet_BoxCollider2D.isTrigger = true;
			LaserBullet_rigidBody2D = LaserBullet_Gameobject.AddComponent<Rigidbody2D>();
			LaserBullet_Gameobject.AddComponent<LaserScript>();
			LaserBullet_rigidBody2D.gravityScale = 0;
			LaserBullet_rigidBody2D.isKinematic = true;
		}

		LaserBullet_List =  FindObjectsOfType<GameObject>();
		
		for (int i=0; i<LaserBullet_List.Length; i++) 
		{
			switch (LaserBullet_List [i].name) 
			{
				case "Laser":
					Current_Laser = LaserBullet_List [i];				
					Current_Laser.transform.position = new Vector2 (Current_Laser.transform.position.x, Current_Laser.transform.position.y + 2.5f * Time.deltaTime);
					if ((Current_Laser.transform.position.y > 5.0f) || (Current_Laser.transform.position.y < -5.0f)) 
					{
						Destroy (Current_Laser);
					}
				break;			
			}
		}
		//Player move and fire		
				
		//Level Structure
		//Debug.Log (EnemyScript.Enemy_Meteor_Kills_Counter.ToString ());
		if (EnemyScript.Enemy_Meteor_Kills_Counter >= Levels [0].Enemy_Meteor_quantity) {
			//Time.timeScale =0;
		InterfaceScript.Display_Text.text = "MISSION COMPLETE" ;
			// Time.timeScale = 0;

			//Start_Wait_Indicator = true;
			StartCoroutine(Wait());
			//InterfaceScript.Display_Text.text =	 "";

		}
		//Level Structure
	}

	// При столкновении игрока с объектами
	void OnTriggerEnter2D(Collider2D  collision) 
	{
		// При столкновении с метеором - тратится жизнь и обновляется счёт
		// И если жизней становится 0 или меньше нуля - то уничтожается корабль
		if (collision.name == "Meteor" )
		{
			Ship.Life--;
			if (Ship.Life <=0)
			{
				Destroy(transform.gameObject);
			}
//			InterfaceScript.Score_Text.text = "Score: " + Ship.Score.ToString ();
		}
	}
}
