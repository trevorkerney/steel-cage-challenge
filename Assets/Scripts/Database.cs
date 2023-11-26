using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static Database Instance;

    private IDbConnection conn;
    private readonly string[] tables = {
        "Account (username CHAR(16) PRIMARY KEY NOT NULL, password CHAR(32) NOT NULL, wins INTEGER NOT NULL CHECK (wins >= 0) DEFAULT 0, losses INTEGER NOT NULL CHECK (losses >= 0) DEFAULT 0, time INTEGER)",
    };

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        string uri = "URI=file:" + Application.persistentDataPath + "/db.sqlite";
        conn = new SqliteConnection(uri);
        conn.Open();

        for (int t = 0; t < 1; t++) {
            IDbCommand command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS " + tables[t];
            command.ExecuteReader();
        }
    }

    public bool IsUsernameUnique(string username) {
        IDbCommand command = conn.CreateCommand();
        command.CommandText = "SELECT * FROM Account;";
        IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            if (username.Equals(reader.GetString(0)))
                return false;
        return true;
    }

    public bool Login(string username, string password) {
        Hash128 hash = new();
        hash.Append(password);
        IDbCommand command = conn.CreateCommand();
        command.CommandText = "SELECT * FROM Account;";
        IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            if (username.Equals(reader.GetString(0)) && hash.ToString().Equals(reader.GetString(1)))
                return true;
        return false;
    }

    public bool Register(string username, string password) {
        if (!IsUsernameUnique(username))
            return false;
        Hash128 hash = new();
        hash.Append(password);
        IDbCommand command = conn.CreateCommand();
        command.CommandText = "INSERT OR REPLACE INTO Account (username, password) VALUES ('" + username + "', '" + hash.ToString() + "')";
        if (command.ExecuteNonQuery() != 1)
        {
            Debug.Log("There was an unexpected error in Register.");
            return false;
        }
        return true;
    }

    public bool RecordWin(string username) {
        IDbCommand command = conn.CreateCommand();
        command.CommandText = "UPDATE Account SET wins = wins + 1 WHERE username = '" + username + "'";
        if (command.ExecuteNonQuery() != 1)
        {
            Debug.Log("There was an unexpected error in RecordWin.");
            return false;
        }
        return true;
    }

    public bool RecordLoss(string username) {
        IDbCommand command = conn.CreateCommand();
        command.CommandText = "UPDATE Account SET losses = losses + 1 WHERE username = '" + username + "'";
        if (command.ExecuteNonQuery() != 1)
        {
            Debug.Log("There was an unexpected error in RecordLoss.");
            return false;
        }
        return true;
    }

    public bool RecordTime(string username, int ms) {
        return false;
    }
}
