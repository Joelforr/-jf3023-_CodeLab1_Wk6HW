using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using SimpleJSON;

public class TODManager : MonoBehaviour {

    public enum AvailableLocations{
    Default,
    NewYork,
    Hawaii
    }

    public AvailableLocations selectedLocation;
    public string sunset;

    WebClient client = new WebClient();


    public static TODManager instance;

	// Use this for initialization
	void Start () {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

        JSONClass defaultJSON = new JSONClass();

        defaultJSON["Location"] = "Default Example";
        defaultJSON["Current Time"] = "3:10 pm";
        defaultJSON["Sunset Time"] = "5:54 pm";

        Utils.WriteJSONtToFile(Application.dataPath, "default.json", defaultJSON);

        Debug.Log(Utils.ReadJSONFromFile(Application.dataPath, "default.json"));
        Debug.Log(Utils.GetSunsetTime(Utils.ReadJSONFromFile(Application.dataPath, "default.json")));
    }
	
	// Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            UpdateLocation();
        }
    }

	public void UpdateLocation () {
		if(selectedLocation == AvailableLocations.NewYork) {
            try {
                string content = client.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20astronomy.sunset%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22newyork%2C%20ny%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

                JSONNode newYork = JSON.Parse(content);
                sunset = Utils.GetSunsetTime(newYork);
            }
            catch(WebException e) {
                Debug.Log("<color=red>ERROR: </color>"+ e.Message + " RETURNING DEFAULT VALUES");
                sunset = Utils.GetSunsetTime(Utils.ReadJSONFromFile(Application.dataPath, "default.json"));
                selectedLocation = AvailableLocations.Default;
            }

        }

        if (selectedLocation == AvailableLocations.Hawaii) {
            try {
                string content = client.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20astronomy.sunset%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22maui%2C%20hi%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

                JSONNode hawaii = JSON.Parse(content);
                sunset = Utils.GetSunsetTime(hawaii);
            }
            catch(WebException e) {
                Debug.Log("<color=red>ERROR: </color>" + e.Message + " RETURNING DEFAULT VALUES");
                sunset = Utils.GetSunsetTime(Utils.ReadJSONFromFile(Application.dataPath, "default.json"));
                selectedLocation = AvailableLocations.Default;
            }
        }

        if(selectedLocation == AvailableLocations.Default) {
            sunset = Utils.GetSunsetTime(Utils.ReadJSONFromFile(Application.dataPath, "default.json"));
        }
    }
}
