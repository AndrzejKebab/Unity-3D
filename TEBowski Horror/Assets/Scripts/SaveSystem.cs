using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	public static void SavePlayer(PlayerHealth player)		//Zapisuje i koduje dane w formacie binnarnym
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.nicecock";
		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData(player);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static PlayerData LoadPlayer()
	{
		string path = Application.persistentDataPath + "/player.nicecock"; //Odczytuje i Dekoduje dane
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			
			stream.Close();
			return data;
		}
		else
		{
			Debug.LogError("save not found!"); //zwraca b³¹d jeœli nie znaleziono save
			return null;
		}
	}
}