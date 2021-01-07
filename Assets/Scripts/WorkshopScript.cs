using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopScript : MonoBehaviour 
{
	private GameObject ImageShip;
	private GameObject ImageEngine;
	private GameObject ImageArmor;
	private GameObject ImageWeapon;
	private GameObject ImageAmmunition;
	private GameObject ItemImage;
	private GameObject ItemDescription;

	private Color ColorInactive;
	private Color ColorActive;

	public static string CurrentSectionInWorkshop;

	public static Sprite[] Ships_Sprite;
	public static string[] Ships_Description;
	public static string[] Ships_Addresses;
	public static float[] Ships_Speed;
	public static Sprite[] Engines_Sprite;
	public static Sprite[] Weapons_Sprite;
	public static Sprite[] Armors_Sprite;
	public static Sprite[] Ammunitions_Sprite;

	public static int CurrentItemImage;
	public static string CurrentShipAddress;

	// При запуске
	void Start () 
	{	
		Ships_Sprite = new Sprite[3];
		Ships_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Ships/Ship1"), 0);
		Ships_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Ships/Ship2"), 1);
		Ships_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Ships/Ship3"), 2);
		Ships_Addresses = new string[3];
		Ships_Addresses.SetValue("Graphics/Ships/Ship1", 0);
		Ships_Addresses.SetValue("Graphics/Ships/Ship2", 1);
		Ships_Addresses.SetValue("Graphics/Ships/Ship3", 2);
		Ships_Description = new string[3];
		Ships_Description.SetValue("CANOE. There are two weapons, engine and good armor. <b>Armor: 100 <color=green>+ 10</color></b><b>Attack: 15 <color=red>- 5</color> </b><b>Speed: 10 + 0</b>", 0);
		Ships_Description.SetValue("CRUISER. There are four weapons, two engines and enhanced armor.<b>Armor: 100 <color=green>+ 200</color></b><b>Attack: 15 <color=green>+ 15</color> </b><b>Speed: 10 + 10</b>", 1);
		Ships_Description.SetValue("BATTLESHIP. There are eight weapons, five engines and advanced armor.<b>Armor: 100 <color=green>+ 7000</color></b><b>Attack: 15 <color=green>+ 100</color> </b><b>Speed: 10 + 20</b>", 2);
		Ships_Speed = new float[3];
		Ships_Speed.SetValue(3f, 0);
		Ships_Speed.SetValue(5f, 1);
		Ships_Speed.SetValue(7f, 2);
		
		CurrentShipAddress = "Graphics/Ships/Ship1";
		Engines_Sprite = new Sprite[3];
		Engines_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Engines/Engine1"), 0);
		Engines_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Engines/Engine2"), 1);
		Engines_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Engines/Engine3"), 2);
		Weapons_Sprite = new Sprite[7];
		Weapons_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Weapons/Weapon0"), 0);
		Weapons_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Weapons/Weapon1"), 1);
		Weapons_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Weapons/Weapon2"), 2);
		Weapons_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Weapons/Weapon3"), 3);
		Weapons_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Weapons/Weapon4"), 4);
		Weapons_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Weapons/Weapon5"), 5);
		Weapons_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Weapons/Weapon6"), 6);
		Armors_Sprite = new Sprite[4];
		Armors_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Armor/Armor1"), 0);
		Armors_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Armor/Armor2"), 1);
		Armors_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Armor/Armor3"), 2);
		Armors_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Armor/Armor4"), 3);
		Ammunitions_Sprite = new Sprite[1];
		Ammunitions_Sprite.SetValue(Resources.Load<Sprite>("Graphics/Ammunition/Laser"), 0);

		CurrentItemImage = 0;
		CurrentSectionInWorkshop = "Ship";

		CurrentShipAddress = Ships_Addresses[CurrentItemImage];
		PlayerScript.Ship.Speed = Ships_Speed[CurrentItemImage];
	}

	public void ButtonClick(string SectionClicked)
	{
		ImageShip = GameObject.FindGameObjectWithTag("Ship");
		ImageEngine = GameObject.FindGameObjectWithTag("Engine");
		ImageArmor = GameObject.FindGameObjectWithTag("Armor");
		ImageWeapon = GameObject.FindGameObjectWithTag("Weapon");
		ImageAmmunition = GameObject.FindGameObjectWithTag("Ammunition");
		ColorActive = Color.red;
		ColorInactive = Color.white;
		ImageShip.GetComponent<Image>().color = ColorInactive;
		ImageEngine.GetComponent<Image>().color = ColorInactive;
		ImageArmor.GetComponent<Image>().color = ColorInactive;
		ImageWeapon.GetComponent<Image>().color = ColorInactive;
		ImageAmmunition.GetComponent<Image>().color = ColorInactive;
		if (SectionClicked == "Ship") 
		{
			ImageShip.GetComponent<Image>().color = ColorActive;
			CurrentItemImage = 0;
			CurrentSectionInWorkshop = SectionClicked;
			NextItemClick(0);
			return;
		}
		if (SectionClicked == "Engine") 
		{
			CurrentSectionInWorkshop = SectionClicked;
			ImageEngine.GetComponent<Image>().color = ColorActive;
			CurrentItemImage = 0;
			NextItemClick(0);
			return;
		}
		if (SectionClicked == "Armor") 
		{
			CurrentSectionInWorkshop = SectionClicked;
			ImageArmor.GetComponent<Image>().color = ColorActive;
			CurrentItemImage = 0;
			NextItemClick(0);
			return;
		}
		if (SectionClicked == "Weapon") 
		{
			CurrentSectionInWorkshop = SectionClicked;
			ImageWeapon.GetComponent<Image>().color = ColorActive;
			CurrentItemImage = 0;
			NextItemClick(0);
			return;
		}
		if (SectionClicked == "Ammunition") 
		{
			CurrentSectionInWorkshop = SectionClicked;
			ImageAmmunition.GetComponent<Image>().color = ColorActive;
			CurrentItemImage = 0;
			NextItemClick(0);
			return;
		}
	}

	public void NextItemClick(int Change)
	{
		ItemImage = GameObject.FindGameObjectWithTag("ItemImage");
		if ((CurrentItemImage + Change) < 0) 
		{
			return;
		}
		if (CurrentSectionInWorkshop == "Ship") 
		{
			if ((CurrentItemImage + Change) < Ships_Sprite.Length)
			{
				CurrentItemImage = CurrentItemImage + Change;
			}
			ItemImage.GetComponent<Image>().sprite = Ships_Sprite[CurrentItemImage];
			ItemDescription = GameObject.Find("Description");
			ItemDescription.GetComponent<Text>().text = Ships_Description[CurrentItemImage];
			CurrentShipAddress = Ships_Addresses[CurrentItemImage];
			PlayerScript.Ship.Speed = Ships_Speed[CurrentItemImage];
		}

		if (CurrentSectionInWorkshop == "Engine") 
		{
			if ((CurrentItemImage + Change) < Engines_Sprite.Length)
			{
				CurrentItemImage = CurrentItemImage + Change;
			}
			ItemImage.GetComponent<Image>().sprite = Engines_Sprite[CurrentItemImage];
		}

		if (CurrentSectionInWorkshop == "Weapon") 
		{
			if ((CurrentItemImage + Change) < Weapons_Sprite.Length)
			{
				CurrentItemImage = CurrentItemImage + Change;
			}
			ItemImage.GetComponent<Image>().sprite = Weapons_Sprite[CurrentItemImage];
		}

		if (CurrentSectionInWorkshop == "Armor") 
		{
			if ((CurrentItemImage + Change) < Armors_Sprite.Length)
			{
				CurrentItemImage = CurrentItemImage + Change;
			}
			ItemImage.GetComponent<Image>().sprite = Armors_Sprite[CurrentItemImage];
		}

		if (CurrentSectionInWorkshop == "Ammunition") 
		{
			if ((CurrentItemImage + Change) < Ammunitions_Sprite.Length)
			{
				CurrentItemImage = CurrentItemImage + Change;
			}
			ItemImage.GetComponent<Image>().sprite = Ammunitions_Sprite[CurrentItemImage];
		}
	}



}
