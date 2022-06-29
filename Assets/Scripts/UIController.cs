using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Making script accessable within other scripts
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    // UI elements
    public TMP_Text overheatedMessage;
    public Slider weaponTempSlider;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
