using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class build_btn : MonoBehaviour
{
    [SerializeField] private GameObject[] showObjects;
    private Button buildin_Btn;

    protected void Hide()
    {
        foreach (GameObject gameObject in showObjects)
        {
            foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }
    protected void Show()
    {
        foreach(GameObject gameObject in showObjects)
        {
            foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }
        }
    }
    private void Start()
    {
        buildin_Btn = gameObject.GetComponent<Button>();
        buildin_Btn.onClick.AddListener(ButtonClick1);

    }

    private void ButtonClick1()
    {
        Debug.LogError("Button Event!");
        Hide();
       // Show();
        
    }
}