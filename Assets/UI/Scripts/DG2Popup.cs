using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;

public class DG2Popup : MonoBehaviour
{
    public GameObject PopupObject;
    public GameObject VoltTagnameObject;
    public GameObject VoltValueObject;
    public GameObject CurrentL1TagnameObject;
    public GameObject CurrentL1ValueObject;
    public GameObject CurrentL2TagnameObject;
    public GameObject CurrentL2ValueObject;
    public GameObject CurrentL3TagnameObject;
    public GameObject CurrentL3ValueObject;
    public GameObject FreqTagnameObject;
    public GameObject FreqValueObject;
    public GameObject PowerTagnameObject;
    public GameObject PowerValueObject;
    public GameObject ReactiveTagnameObject;
    public GameObject ReactiveValueObject;
    public GameObject RPMTagnameObject;
    public GameObject RPMValueObject;

    public string Url;
    public int SecondsBetweenCalls;


    TextMeshProUGUI internalPopupObject;
    TextMeshProUGUI internalVoltTagnameObject;
    TextMeshProUGUI internalVoltValueObject;
    TextMeshProUGUI internalCurrentL1TagnameObject;
    TextMeshProUGUI internalCurrentL1ValueObject;
    TextMeshProUGUI internalCurrentL2TagnameObject;
    TextMeshProUGUI internalCurrentL2ValueObject;
    TextMeshProUGUI internalCurrentL3TagnameObject;
    TextMeshProUGUI internalCurrentL3ValueObject;
    TextMeshProUGUI internalFreqTagnameObject;
    TextMeshProUGUI internalFreqValueObject;
    TextMeshProUGUI internalPowerTagnameObject;
    TextMeshProUGUI internalPowerValueObject;
    TextMeshProUGUI internalReactiveTagnameObject;
    TextMeshProUGUI internalReactiveValueObject;
    TextMeshProUGUI internalRPMTagnameObject;
    TextMeshProUGUI internalRPMValueObject;
    private int internalTimer = 0;
    private bool canCallAPI = false;

    void OnTriggerEnter(Collider other)
    {
        PopupObject.SetActive(true);
        canCallAPI = true;

        this.SetDataInPopup(this.GetMeasurementFromAPI());
    }

    void OnTriggerExit(Collider other)
    {
        PopupObject.SetActive(false);
        canCallAPI = false;
    }

    void SetDataInPopup(TagAndMeasurementPair[] data)
    {
        if (data is null)
        {
            return;
        }
        foreach (var itm in data)
        {
            switch (itm.tagname)
            {
                case "G2-I-L1":
                    internalCurrentL1TagnameObject.text = itm.tagname;
                    internalCurrentL1ValueObject.text = itm.measurement + " A";
                    break;
                case "G2-I-L2":
                    internalCurrentL2TagnameObject.text = itm.tagname;
                    internalCurrentL2ValueObject.text = itm.measurement + " A";
                    break;
                case "G2-I-L3":
                    internalCurrentL3TagnameObject.text = itm.tagname;
                    internalCurrentL3ValueObject.text = itm.measurement + " A";
                    break;
                case "G2-LOAD":
                    internalPowerTagnameObject.text = itm.tagname;
                    internalPowerValueObject.text = itm.measurement + " KW";
                    break;
                case "G2-LOAD-KVAR":
                    internalReactiveTagnameObject.text = itm.tagname;
                    internalReactiveValueObject.text = itm.measurement + " KVAR";
                    break;
                case "DG2-GEN-FRQ":
                    internalFreqTagnameObject.text = itm.tagname;
                    internalFreqValueObject.text = itm.measurement + " HZ";
                    break;
                case "DG2-GEN-V":
                    internalVoltTagnameObject.text = itm.tagname;
                    internalVoltValueObject.text = itm.measurement + " V";
                    break;
                case "DG2-VELOC":
                    internalRPMTagnameObject.text = itm.tagname;
                    internalRPMValueObject.text = itm.measurement + " RPM";
                    break;
                default:
                    break;
            }
        }
    }

    TagAndMeasurementPair[] GetMeasurementFromAPI()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        if (json != null && json != "NoData") 
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TagAndMeasurementPair[]>(json);
        }
        return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        PopupObject.SetActive(false);
        internalPopupObject = PopupObject.GetComponent<TextMeshProUGUI>();
        internalVoltTagnameObject = VoltTagnameObject.GetComponent<TextMeshProUGUI>();
        internalVoltValueObject = VoltValueObject.GetComponent<TextMeshProUGUI>();
        internalCurrentL1TagnameObject = CurrentL1TagnameObject.GetComponent<TextMeshProUGUI>();
        internalCurrentL1ValueObject = CurrentL1ValueObject.GetComponent<TextMeshProUGUI>();
        internalCurrentL2TagnameObject = CurrentL2TagnameObject.GetComponent<TextMeshProUGUI>();
        internalCurrentL2ValueObject = CurrentL2ValueObject.GetComponent<TextMeshProUGUI>();
        internalCurrentL3TagnameObject = CurrentL3TagnameObject.GetComponent<TextMeshProUGUI>();
        internalCurrentL3ValueObject = CurrentL3ValueObject.GetComponent<TextMeshProUGUI>();
        internalFreqTagnameObject = FreqTagnameObject.GetComponent<TextMeshProUGUI>();
        internalFreqValueObject = FreqValueObject.GetComponent<TextMeshProUGUI>();
        internalPowerTagnameObject = PowerTagnameObject.GetComponent<TextMeshProUGUI>();
        internalPowerValueObject = PowerValueObject.GetComponent<TextMeshProUGUI>();
        internalReactiveTagnameObject = ReactiveTagnameObject.GetComponent<TextMeshProUGUI>();
        internalReactiveValueObject = ReactiveValueObject.GetComponent<TextMeshProUGUI>();
        internalRPMTagnameObject = RPMTagnameObject.GetComponent<TextMeshProUGUI>();
        internalRPMValueObject = RPMValueObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canCallAPI)
        {
            if (internalTimer >= 60*5)
            {
                this.SetDataInPopup(this.GetMeasurementFromAPI());
                internalTimer = 0;
            }
            else
            {
                internalTimer++;
            }
        }
    }

    [System.Serializable]
    class TagAndMeasurementPairList
    {
        public TagAndMeasurementPair[] pairs { get; set; }
    }

    [System.Serializable]
    class TagAndMeasurementPair
    {
        public string tagname { get; set; }
        public double measurement { get; set; }
    }
}
