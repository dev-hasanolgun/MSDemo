using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI TMPText;
    
    private void IncreaseCounter(Dictionary<string,object> message)
    {
        var count = int.Parse(TMPText.text)+1;
        TMPText.text = count.ToString("###");
    }

    private void Start()
    {
        TMPText.text = "0";
    }

    private void OnEnable()
    {
        EventManager.StartListening("OnBarrelCollect", IncreaseCounter);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening("OnBarrelCollect", IncreaseCounter);
    }
}
