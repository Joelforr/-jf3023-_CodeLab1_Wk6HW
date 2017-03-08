using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public Dropdown locationDropdown;
    public Text currentLocation;
    public Text currentTime;
    public Text sunsetTime;

    [SerializeField]
    private string[] locations = new string[Enum.GetNames(typeof(TODManager.AvailableLocations)).Length];

    public static UIManager instance;
     
	// Use this for initialization
	void Start () {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

        locations = Enum.GetNames(typeof(TODManager.AvailableLocations));

        locationDropdown.ClearOptions();
        foreach(string s in locations) {
            locationDropdown.options.Add(new Dropdown.OptionData(s));
        }
        locationDropdown.RefreshShownValue();
	}
	
	// Update is called once per frame
	void Update () {
        SetLocation();
        UpdateValues();
	}

    public void SetLocation() {
        TODManager.instance.selectedLocation = (TODManager.AvailableLocations) System.Enum.Parse( typeof(TODManager.AvailableLocations), locationDropdown.options[locationDropdown.value].text);
    }

    public void UpdateValues() {
        TODManager.instance.UpdateLocation();
        currentLocation.text = TODManager.instance.selectedLocation.ToString();
        currentTime.text = "Current Time: " + System.DateTime.Now.ToLocalTime().ToString("t");
        sunsetTime.text = "Sunset Time: " + TODManager.instance.sunset;
    }
}
