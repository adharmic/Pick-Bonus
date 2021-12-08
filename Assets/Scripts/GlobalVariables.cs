using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalVariables : MonoBehaviour
{
    // TO DO:
    // 1. Set chest buttons disabled until play is pressed
    // 2. Disable chest animations until play is pressed
    // 3. Give chests silhouette until play is pressed
    // 4. Revert all of the above when play is pressed
    // 5. Add delay between opening anticipation and open animation
    // 6. Notify the user if they've won any money (MODAL BOX?)
    // 7. When user gets pooper, allow option to play again and reset game state.



    // private bool _playing = false;
    
    // Minimum denomination value
    private float _mindeno = .25f;

    // Maximum denomination value
    private float _maxdeno = 5f;

    // Denomination increment value
    private float _denoinc = .25f;

    public static float _balance = 10f;
    public static float _denominator = .25f;
    public static float _win = 0f;
    public TextMeshProUGUI Balance;
    public TextMeshProUGUI Denom;
    public TextMeshProUGUI Win;
    public Button Play;
    public Button Add;
    public Button Sub;

    private void Start() {
        // _balance = 10f;
        // _denominator = .25f;
        // _win = 0f;
        Balance.SetText(_balance.ToString("C"));
        Denom.SetText(_denominator.ToString("C"));
        Win.SetText(_win.ToString("C"));
        Sub.interactable = false;
    }

    public void IncreaseDenom() {
        _denominator = Mathf.Clamp(_denominator + _denoinc, _mindeno, _maxdeno);
        Denom.SetText(_denominator.ToString("C"));
        if(_denominator == _maxdeno) {
            Add.interactable = false;
        }
        else {
            Add.interactable = true;
        }
        Sub.interactable = true;
    }

    public void DecreaseDenom() {
        _denominator = Mathf.Clamp(_denominator - _denoinc, _mindeno, _maxdeno);
        Denom.SetText(_denominator.ToString("C"));
        if(_denominator == _mindeno) {
            Sub.interactable = false;
        }
        else {
            Sub.interactable = true;
        }
        Add.interactable = true;
    }

    public void PlayGame() {
        if(SubtractBalance()) {
            // Main gameplay code here
            Play.interactable = false;
        }
        else {
            Debug.Log("Not enough money!");
        }
    }

    public bool SubtractBalance() {
        if(_denominator > _balance) {
            return false;
        }
        _balance -= _denominator;
        Balance.SetText(_balance.ToString("C"));
        return true;
    }

    public void TestOutput() {
        Debug.Log("Mic check 1 2 3");
    }

}
