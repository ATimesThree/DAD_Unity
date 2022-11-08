using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class PopupMeasurement : MonoBehaviour
{

    public GameObject Popup;
    public GameObject TMP_Tagname;
    public GameObject TMP_Measurement;
    public string phpURL;

    TextMeshProUGUI TagnameText;
    TextMeshProUGUI MeasurementText;

    private string url;

    private string GetMeasurementFromDatabase()
    {
        string response;
        using (WebClient client = new WebClient())
        {
            response = client.DownloadString(url);
        }
        string[] parts = response.Split(',');
        return parts[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        Popup.SetActive(false);
        TagnameText = TMP_Tagname.GetComponent<TextMeshProUGUI>();
        MeasurementText = TMP_Measurement.GetComponent<TextMeshProUGUI>();

    }

    void OnTriggerEnter(Collider other)
    {
        TagnameText.text = name;
        url = phpURL + "?tagname=" + name + "&amount=1";
        MeasurementText.text = GetMeasurementFromDatabase();
        Popup.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        Popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
