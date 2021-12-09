using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalVariables : MonoBehaviour
{
    // TO DO:
    // 0. Initial graphical setup - DONE sometime the week of 11/29
    // 1. Set chest buttons disabled until play is pressed - DONE 12/7
    // 2. Disable chest animations until play is pressed - DONE 12/7
    // 3. Give chests silhouette until play is pressed - DONE 12/7
    // 4. Revert all of the above when play is pressed - DONE 12/7
    // 5. Add delay between opening anticipation and open animation - DONE 12/8
    // 6. Show reward from chest in text in front
    // 7. When user gets pooper, update balance and allow option to play again and reset game state.
    // 8. Add weighted random number generation for multiplier value tier - DONE 12/8
    // 9. Pick random number again to decide exact multiplier value and assign that to our predetermined total - DONE 12/8
    // 10. Another random number to decide how many chests the player will be opening this turn
    // 11. Divide the pretotal by the number of chests (randomly) in increments of 5 cents. 
    // 12. Create graphic for pooper

    // private bool _playing = false;
    
    // Minimum denomination value
    private float _mindeno = .25f;

    // Maximum denomination value
    private float _maxdeno = 5f;

    // Denomination increment value
    private float _denoinc = .25f;

    private int chestsopened = 0;

    private int[][] multipliers = new int[4][];

    private int[] chestvalues;
    private float pretotal = 0;

    public GameObject[] chests;

    public GameObject[] mysteryboxes;

    public GameObject[] winningtexts;
    public GameObject pooper;

    public CanvasGroup chestbuttons;

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
        multipliers[0] = new int[]{0};
        multipliers[1] = new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        multipliers[2] = new int[]{12, 16, 24, 32, 48, 64};
        multipliers[3] = new int[]{100, 200, 300, 400, 500};

        // _balance = 10f;
        // _denominator = .25f;
        // _win = 0f;
        Balance.SetText(_balance.ToString("C"));
        Denom.SetText(_denominator.ToString("C"));
        Win.SetText(_win.ToString("C"));
        Sub.interactable = false;
        DisableChests();
        DisableTexts();
        MysteryOn();
    }

    private void ResetGame() {
        chestbuttons.interactable = false;
    }

    private void DisableChests() {
        foreach (var item in chests) {
            item.SetActive(false);
        }
    }

    private void DisableTexts() {
        foreach (var item in winningtexts) {
            item.SetActive(false);
        }
    }

    private void EnableChests() {
        foreach (var item in chests) {
            item.SetActive(true);
            item.GetComponent<LootBox>().BounceBox(true);
        }
    }

    private void MysteryOn() {
        foreach (var item in mysteryboxes) {
            item.SetActive(true);
        }
    }

    private void MysteryOff() {
        foreach (var item in mysteryboxes) {
            item.SetActive(false);
        }
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
            chestbuttons.interactable = true;
            MysteryOff();
            EnableChests();
            GetTotal();
        }
        else {
            Debug.Log("Not enough money!");
        }
    }
    
    public void AssignChests() {
        int numchests = Random.Range(1, 10);
        chestvalues = new int[numchests];
    }

    public void GetTotal() {
        float tiernum = Random.Range(0, 100);
        if(tiernum <= 49) {
            int valnum = Random.Range(0, multipliers[0].Length);
            pretotal = multipliers[0][valnum] * _denominator;
        }
        else if(tiernum <= 79) {
            int valnum = Random.Range(0, multipliers[1].Length);
            pretotal = multipliers[1][valnum] * _denominator;
        }
        else if(tiernum <= 94) {
            int valnum = Random.Range(0, multipliers[2].Length);
            pretotal = multipliers[2][valnum] * _denominator;
        }
        else {
            int valnum = Random.Range(0, multipliers[3].Length);
            pretotal = multipliers[3][valnum] * _denominator;
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

    public void DisableChest(GameObject button) {
        
    }

    public void TestOutput() {
        Debug.Log("Mic check 1 2 3");
    }

}
