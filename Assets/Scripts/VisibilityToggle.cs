using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DelayedToggle() {
        Invoke("Toggle", 2f);
    }

    public void Toggle() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
