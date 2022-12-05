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
    public string Url;
    public int SecondsBetweenCalls;

    TextMeshProUGUI TagNameText;
    TextMeshProUGUI MeasurementText;
    private int internalTimer = 0;
    private bool canCallAPI = false;

    void OnTriggerEnter(Collider other)
    {

        PopupObject.SetActive(true);
        canCallAPI = true;

        if (TagnameObject is null || MeasurementObject is null)
        {
            return;
        }

        if (TagNameText is null || String.IsNullOrWhiteSpace(TagNameText.text))
        {
            return;
        }

        MeasurementText.text = GetMeasurementFromAPI();
    }

    string GetMeasurementFromAPI()
    {
        string response = "";
        using (WebClient client = new WebClient())
        {
            response = client.DownloadString(this.Url);
        }
        return response;
    }

    void OnTriggerExit(Collider other)
    {
        PopupObject.SetActive(false);
        canCallAPI = false;
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

        if (canCallAPI)
        {
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
}
