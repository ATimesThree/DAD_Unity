using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class CustomPopupScript : MonoBehaviour
{
    public GameObject PopupObject;
    public GameObject TagnameObject;
    public GameObject MeasurementObject;
    public int SecondsBetweenCalls;

    TextMeshProUGUI TagNameText;
    TextMeshProUGUI MeasurementText;
    private string url = "http://localhost:8085/tunglabb/";
    private int internalTimer = 0;

    void OnTriggerEnter(Collider other)
    {

        PopupObject.SetActive(true);

        if (TagnameObject is null || MeasurementObject is null)
        {
            return;
        }

        if (TagNameText is null || String.IsNullOrWhiteSpace(TagNameText.text))
        {
            return;
        }

        this.url = this.url + "GetMeasurement?tagname=" + TagNameText.text;
        MeasurementText.text = GetMeasurementFromAPI();
    }

    string GetMeasurementFromAPI()
    {
        string response = "";
        using (WebClient client = new WebClient())
        {
            response = client.DownloadString(url);
        }
        return response;
    }

    void OnTriggerExit(Collider other)
    {
        PopupObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        PopupObject.SetActive(false);
        if (TagnameObject != null)
        {
            TagNameText = TagnameObject.GetComponent<TextMeshProUGUI>();

        }
        if (MeasurementObject != null)
        {
            MeasurementText = MeasurementObject.GetComponent<TextMeshProUGUI>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (TagnameObject is null || MeasurementObject is null)
        {
            return;
        }
        if (SecondsBetweenCalls < 1)
        {
            return;
        }
        if (SecondsBetweenCalls * 60 >= internalTimer)
        {
            MeasurementText.text = GetMeasurementFromAPI();
            internalTimer = 0;
        }
        else
        {
            internalTimer += 1;
        }
    }
}
