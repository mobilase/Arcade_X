using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LaserScript : MonoBehaviour 
{
	// При запуске
	void Start () {
	
	}
	
	// При обновлении
	void Update () {
	
	}

	// При столкновении лазера с объектами
	void OnTriggerEnter2D(Collider2D  collision) 
	{
		// При столкновении лазера с метеором увеличивается счёт, уменьшается количество лазера
		// Уничтожается лазер и метеор
		if (collision.name == "Meteor")
		{
			PlayerScript.Ship.Score += 10;
			EnemyScript.Enemy_Meteor_Kills_Counter++;
			EnemyScript.Enemy_OnScreen_Counter--;
			Destroy(this.gameObject);
			Destroy(collision.gameObject);
		}
	}
}
