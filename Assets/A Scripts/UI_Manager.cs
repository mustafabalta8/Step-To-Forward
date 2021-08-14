using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject EscapePanel;
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenEscapePanel();
    }

    void OpenEscapePanel()
    {
        EscapePanel.SetActive(true);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    
}
