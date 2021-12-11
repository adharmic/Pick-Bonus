using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    // 10. Another random number to decide how many chests the player will be opening this turn - DONE 12/8
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
    public TextMeshProUGUI Title;
    public Button Play;
    public Button Add;
    public Button Sub;
    public Button[] buttons;

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
        ToggleChests(false);
        DisableTexts();
        ToggleMystery(true);
    }

    private void DisableTexts() {
        foreach (var item in winningtexts) {
            item.SetActive(false);
        }
    }

    private void ToggleChests(bool enable) {
        foreach (var item in chests) {
            item.SetActive(enable);
            item.GetComponent<LootBox>().BounceBox(enable);
        }
    }

    private void ToggleMystery(bool enable) {
        foreach (var item in mysteryboxes) {
            item.SetActive(enable);
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

    public void ToggleButtons(bool enabled) {
        foreach (Button item in buttons) {
            item.interactable = enabled;
        }
    }

    public void PlayGame() {
        if(SubtractBalance()) {
            // Main gameplay code here
            Play.interactable = false;
            // chestbuttons.interactable = true;
            ToggleButtons(true);
            ToggleMystery(false);
            ToggleChests(true);
            GetTotal();
            Debug.Log("PRETOTAL: " + pretotal);
            if (pretotal != 0) {
                AssignChests();
            }
            else {
                chestvalues = new int[1] {0};
            }
        }
        else {
            Play.interactable = false;
            Debug.Log("Not enough money!");
        }
    }
    
    public void AssignChests() {
        int numchests = UnityEngine.Random.Range(1, 8);
        int nickels = (int)(pretotal * 20);
        chestvalues = new int[numchests+1]; 
        int[] randomdistro = new int[numchests+1];
        randomdistro[0] = 0;
        randomdistro[randomdistro.Length-1] = nickels - numchests;
        for (int i = 1; i < randomdistro.Length-1; i++) {
            randomdistro[i] = UnityEngine.Random.Range(1, nickels - numchests);
        }
        Array.Sort(randomdistro);
        for(int i = 0; i < randomdistro.Length-1; i++) {
            chestvalues[i] = (randomdistro[i+1] - randomdistro[i]) + 1;
        }

        for(int i = 0; i < randomdistro.Length; i++) {
            Debug.Log("RANDOM VALUE " + i + ": " + randomdistro[i]);
        }

        int sum = 0;
        for(int i = 0; i < chestvalues.Length; i++) {
            Debug.Log("CHEST VALUE " + i + ": " + chestvalues[i]);
            sum += chestvalues[i];
        }

        Debug.Log("EXPECTED TOTAL: " + pretotal*20);

        Debug.Log("ACTUAL TOTAL: " + sum);

    }

    public void GetTotal() {
        float tiernum = UnityEngine.Random.Range(0, 99);
        if(tiernum <= 49) {
            int valnum = UnityEngine.Random.Range(0, multipliers[0].Length);
            pretotal = multipliers[0][valnum] * _denominator;
        }
        else if(tiernum <= 79) {
            int valnum = UnityEngine.Random.Range(0, multipliers[1].Length);
            pretotal = multipliers[1][valnum] * _denominator;
        }
        else if(tiernum <= 94) {
            int valnum = UnityEngine.Random.Range(0, multipliers[2].Length);
            pretotal = multipliers[2][valnum] * _denominator;
        }
        else {
            int valnum = UnityEngine.Random.Range(0, multipliers[3].Length);
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

    public void TestOutput() {
        Debug.Log("Mic check 1 2 3");
    }

    public void ChestOpened(TextMeshProUGUI wonvalue) {
        float currval = chestvalues[chestsopened];
        Debug.Log("CURRENT VALUE: " + currval);
        if (currval == 0) {
            // END THE GAME!
            Debug.Log("GAME OVER");
            wonvalue.SetText("POOPER!");
            chestbuttons.enabled = false;
            Invoke("EndGame", 1.5f);
        }
        else {
            currval /= 20;
            wonvalue.SetText(currval.ToString("C"));
            _win += currval;
            wonvalue.enabled = true;
            chestsopened++;
            Invoke("UpdateWin", 1.5f);
        }
    }

    public void UpdateWin() {
        Win.SetText(_win.ToString("C"));
    }

    public void EndGame() {
        Title.SetText("GAME OVER!");
        ToggleButtons(false);
        Invoke("DelayedFinish", 1.5f);
    }

    public void DelayedFinish() {
        CloseChests();
        DisableTexts();
        Play.interactable = true;
    }

    public void CloseChests() {
        foreach (GameObject item in chests) {
            item.GetComponent<LootBox>().Close();
            item.GetComponent<LootBox>().BounceBox(false);
        }
    }

}
