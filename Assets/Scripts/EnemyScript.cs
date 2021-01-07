using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour 
{
	public static GameObject Current_Enemy = null;
	//private Sprite Current_Enemy_Sprite = null;
	private int Random_Value = 0;
	private int Random_Meteor = 0;
	//private int Random_Value2 = 0;
	private GameObject[] EnemyList;
	private GameObject Enemy_GameObject; 
	private SpriteRenderer Enemy_SpriteRenderer;
	private Vector2 Enemy_Position;
	private BoxCollider2D Enemy_BoxCollider2D;
	private Rigidbody2D Enemy_RigidBody2D;
	
	public static int Enemy_OnScreen_Counter = 0;
	private int Max_Enemy_OnScreen = 2;

	//счетчик уничтоженных на уровне врагов
	public static int Enemy_Meteor_Kills_Counter = 0;
	
	public struct Enemy_Struct
	{
		public GameObject Enemy_GameObject;
		public Texture2D  Enemy_Texture2D; 
		public Vector2    Enemy_Position;
		public int        Enemy_Health;	
	}

	public Enemy_Struct Enemy_Meteor = new Enemy_Struct();
	public Enemy_Struct Enemy_Meteor1 = new Enemy_Struct();
	public Enemy_Struct Enemy_Meteor2 = new Enemy_Struct();
	public Enemy_Struct Enemy_Meteor3 = new Enemy_Struct();



	// При запуске
	void Start() 
	{
//		Enemy_Meteor.Enemy_Texture2D = Resources.Load("Graphics/Game/Meteor", typeof(Texture2D)) as Texture2D;

		Enemy_Meteor1.Enemy_Texture2D = Resources.Load("Graphics/Meteors/Meteor1", typeof(Texture2D)) as Texture2D;
		Enemy_Meteor2.Enemy_Texture2D = Resources.Load("Graphics/Meteors/Meteor2", typeof(Texture2D)) as Texture2D;
		Enemy_Meteor3.Enemy_Texture2D = Resources.Load("Graphics/Meteors/Meteor3", typeof(Texture2D)) as Texture2D;

		Enemy_Meteor.Enemy_Health = 1;
		Enemy_Meteor1.Enemy_Health = 1;
		//Enemy_Meteor2.Enemy_Health = 1;
		//Enemy_Meteor3.Enemy_Health = 1;


	}
	
	// При обновлении
	void Update() 
	{

		//Debug.Log (Enemy_OnScreen_Counter.ToString ());


		if (Enemy_OnScreen_Counter < Max_Enemy_OnScreen) 
		{
			Random_Value = Random.Range(-5,5);
			Random_Meteor =  Random.Range(1, 4);
			Enemy_Position = new Vector2 (Random_Value , 5.0f);
			Enemy_GameObject = new GameObject("Meteor");
			Enemy_GameObject.transform.position = new Vector3(Enemy_Position.x, Enemy_Position.y, 0.0f);
			Enemy_GameObject.transform.localPosition = new Vector3(Enemy_Position.x, Enemy_Position.y, 0.0f);
			Enemy_SpriteRenderer = Enemy_GameObject.AddComponent<SpriteRenderer>(); 

			//Enemy_Meteor.Enemy_Texture2D = Resources.Load("Graphics/Game/Meteor"+Random_Meteor.ToString(), typeof(Texture2D)) as Texture2D;


			if ( Random_Meteor == 1) 
			{
			Enemy_SpriteRenderer.sprite = Sprite.Create(Enemy_Meteor1.Enemy_Texture2D, new Rect (0.0f, 0.0f, 256.0f, 256.0f), new Vector2 (0.0f, 0.0f), 256.0f);
			}

			if ( Random_Meteor == 2) 
			{
				Enemy_SpriteRenderer.sprite = Sprite.Create(Enemy_Meteor2.Enemy_Texture2D, new Rect (0.0f, 0.0f, 256.0f, 256.0f), new Vector2 (0.0f, 0.0f), 256.0f);
			}

			if ( Random_Meteor == 3) 
			{
				Enemy_SpriteRenderer.sprite = Sprite.Create(Enemy_Meteor3.Enemy_Texture2D, new Rect (0.0f, 0.0f, 256.0f, 256.0f), new Vector2 (0.0f, 0.0f), 256.0f);
			}

			Enemy_SpriteRenderer.sortingOrder = 0;
			Enemy_BoxCollider2D = Enemy_GameObject.AddComponent<BoxCollider2D>();
			Enemy_BoxCollider2D.isTrigger = true;
			//Enemy_GameObject.AddComponent("EnemyScript");
			Enemy_RigidBody2D = Enemy_GameObject.AddComponent<Rigidbody2D>();
			Enemy_RigidBody2D.gravityScale = 0;
			Enemy_RigidBody2D.isKinematic = true;
			Enemy_OnScreen_Counter++;
			PlayerScript.Ship.Armor++;
		}

		EnemyList = FindObjectsOfType<GameObject>();
		for (int i=0; i < EnemyList.Length; i++) 
		{
			switch (EnemyList[i].name) 
			{
				case "Meteor":
					Current_Enemy = EnemyList[i];
					Current_Enemy.transform.position = new Vector2 (Current_Enemy.transform.position.x, Current_Enemy.transform.position.y - 0.5f * Time.deltaTime);
					if ((Current_Enemy.transform.position.y > 5.0f) || (Current_Enemy.transform.position.y < -5.0f)) 
					{
						Destroy(Current_Enemy);
						Enemy_OnScreen_Counter--;
					 
					}
				break;
			}
		}
	}
	      
	// При столкновении врага(метеорита) с объектами
	void OnTriggerEnter2D(Collider2D collision) 
	{
		// При столкновении метеорита с лазером - метеорит уничтожается
		if (collision.name == "Laser")
		{

			Destroy(Current_Enemy);
			Enemy_OnScreen_Counter--;

		}		
	}
}
