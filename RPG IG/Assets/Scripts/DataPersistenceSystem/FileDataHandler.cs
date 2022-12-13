using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace PatataStudio.DataPersitence.FileHandler
{
	public class FileDataHandler
	{
		private string dataDirPath = "";
		private string dataFileName = "";
		private bool useEncryption = false;
		private readonly string encryptionCodeWord = "nigger";
		private readonly string backupExtension = ".backup";

		public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
		{
			this.dataDirPath = dataDirPath;
			this.dataFileName = dataFileName;
			this.useEncryption = useEncryption;
		}
		#region Load Game
		public GameData Load(string profileId, bool allowRestoreFromBackup = true)
		{
			if (profileId == null)
			{
				return null;
			}

			string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
			GameData loadedData = null;

			if (File.Exists(fullPath))
			{
				try
				{
					string dataToLoad = "";
					using (FileStream stream = new FileStream(fullPath, FileMode.Open))
					{
						using (StreamReader reader = new StreamReader(stream))
						{
							dataToLoad = reader.ReadToEnd();
						}
					}

					if (useEncryption)
					{
						dataToLoad = EncryptDecrypt(dataToLoad);
					}

					loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

				}
				catch (Exception e)
				{
					if (allowRestoreFromBackup)
					{
						Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
						bool rollbackSuccess = AttemptRollback(fullPath);
						if (rollbackSuccess)
						{
							loadedData = Load(profileId, false);
						}
					}
					else
					{
						Debug.LogError("Error occured when trying to load file at path: " + fullPath + " and backup didn't work.\n" + e);
					}
				}
			}
			return loadedData;
		}
		#endregion

		#region Save Game
		public void Save(GameData data, string profileId)
		{
			if (profileId == null)
			{
				return;
			}

			string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
			string backupFilePath = fullPath + backupExtension;
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

				string dataToStore = JsonUtility.ToJson(data, true);

				if (useEncryption)
				{
					dataToStore = EncryptDecrypt(dataToStore);
				}

				using (FileStream stream = new FileStream(fullPath, FileMode.Create))
				{
					using (StreamWriter writer = new StreamWriter(stream))
					{
						writer.Write(dataToStore);
					}
				}

				GameData verifiedGameData = Load(profileId);

				if (verifiedGameData != null)
				{
					File.Copy(fullPath, backupFilePath, true);
				}
				else
				{
					throw new Exception("Save file could not be verified and backup could not be created.");
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Error occured when trying to save data to file:" + fullPath + "\n" + e);
			}
		}
		#endregion

		public Dictionary<string, GameData> LoadAllProfiles()
		{
			Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

			IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
			foreach (DirectoryInfo dirInfo in dirInfos)
			{
				string profileId = dirInfo.Name;



				string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
				if (!File.Exists(fullPath))
				{
					Debug.LogWarning("Skipping directory when loading all profiles because it doesn't contain data: " + profileId);
					continue;
				}

				GameData profileData = Load(profileId);

				if (profileData != null)
				{
					profileDictionary.Add(profileId, profileData);
				}
				else
				{
					Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
				}
			}

			return profileDictionary;
		}

		public string GetMostRecenltyUpdatedProfileId()
		{
			string mostRecentProfileId = null;

			Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
			foreach (KeyValuePair<string, GameData> pair in profilesGameData)
			{
				string profilesId = pair.Key;
				GameData gameData = pair.Value;

				if (gameData == null)
				{
					continue;
				}

				if (mostRecentProfileId == null)
				{
					mostRecentProfileId = profilesId;
				}
				else
				{
					DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
					DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);

					if (newDateTime > mostRecentDateTime)
					{
						mostRecentProfileId = profilesId;
					}
				}
			}
			return mostRecentProfileId;
		}

		private string EncryptDecrypt(string data)
		{
			string modifiedData = "";
			for (int i = 0; i < data.Length; i++) modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
			return modifiedData;

		}

		private bool AttemptRollback(string fullPath)
		{
			bool success = false;
			string backupFilePath = fullPath + backupExtension;
			try
			{
				if (File.Exists(backupFilePath))
				{
					File.Copy(backupFilePath, fullPath, true);
					success = true;
					Debug.LogWarning("Had to roll back to backup file at: " + backupFilePath);
				}
				else
				{
					throw new Exception("Tried to roll back, but no backup file exists to roll back to.");
				}

			}
			catch (Exception e)
			{
				Debug.LogError("Error occured when trying to roll back to backup file at: " + backupFilePath + "\n" + e);
			}

			return success;
		}
	}
}