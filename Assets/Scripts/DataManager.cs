using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour {

    public static DataManager Instance;
    public string playingUserName;
    public UserRanking userRanking;

    [Serializable]
    public class UserData {
        public string name;
        public int score;
    }

    [Serializable]
    public class UserRanking {
        public List<UserData> usersScore = new List<UserData>();
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void SaveUserScore(UserData userData) {
        UserRanking userRanking = LoadUserRanking();
        userRanking.usersScore.Add(userData);
        string userRankingJson = JsonUtility.ToJson(userRanking);
        File.WriteAllText(Application.persistentDataPath + "/user-ranking-save.json", userRankingJson);
    }

    public UserRanking LoadUserRanking() {
        string path = Application.persistentDataPath + "/user-ranking-save.json";
        if (File.Exists(path)) {
            string userJson = File.ReadAllText(path);
            UserRanking userRanking = JsonUtility.FromJson<UserRanking>(userJson);
            this.userRanking = userRanking;
            Debug.Log("User Ranking load: " + userRanking.usersScore);
            return userRanking;
        }
        return new UserRanking();
    }

    public UserData GetBestScoreUser() {
        if (userRanking == null) {
            LoadUserRanking();
        }
        return userRanking.usersScore.OrderByDescending(userData => userData.score).FirstOrDefault();
    }
}
