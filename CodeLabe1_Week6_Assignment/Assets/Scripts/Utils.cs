using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Utils : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void WriteStringToFile(string path, string fileName, string content) {
        StreamWriter sw = new StreamWriter(path + "/" + fileName);
        sw.Write(content);
        sw.Close();
    }

    public static void WriteJSONtToFile(string path, string fileName, JSONClass json) {
        WriteStringToFile(path, fileName, json.ToString());
    }

    public static string ReadStringFromFile(string path, string fileName) {
        StreamReader sr = new StreamReader(path + "/" + fileName);
        string results = sr.ReadToEnd();

        sr.Close();

        return results;

    }

    public static JSONNode ReadJSONFromFile(string path, string fileName) {
        string result = ReadStringFromFile(path, fileName);

        JSONNode readJSON = JSON.Parse(result);
        
        return readJSON;
    }

    public static string GetSunsetTime(JSONNode jsonNode) {

        return jsonNode["query"]["results"]["channel"]["astronomy"]["sunset"] != null ? 
            jsonNode["query"]["results"]["channel"]["astronomy"]["sunset"].ToString() : jsonNode["Sunset Time"].ToString();
        
    }
}
