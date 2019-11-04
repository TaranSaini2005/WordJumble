using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataControl : MonoBehaviour
{
	public static DataControl dataControl;

	public int highScore = 0;
	public int currentScore = 0;
    public string correctWord = "";

	void Awake()
	{
		if (dataControl == null)
		{
			DontDestroyOnLoad (gameObject);
			dataControl = this;
		}
		else if (dataControl == this) 
		{
			Destroy (gameObject);
		}

        DataControl.dataControl.Load();
    }

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/DataInfo.dat");

		GameData data = new GameData ();
		data.highScore = highScore;
		data.currentScore = currentScore;
        data.correctWord = correctWord;

        bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/DataInfo.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/DataInfo.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			highScore = data.highScore;
			currentScore = data.currentScore;
            correctWord = data.correctWord;
        }
	}
}

[Serializable] class GameData
{
	public int highScore;
	public int currentScore;
    public string correctWord;
}
