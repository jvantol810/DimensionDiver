using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //Public Variables to Customize
    public int maxHealth;
    public int maxOxygen;
    public bool isOtherDimension; //If flagged as true, the oxygen meter will deplete.
    //Public Variables
    public Text oxygenText;
    public Text healthText;
    //Private Variables
    int health;
    int oxygen;
    bool oxygenDepletionCooldown;
    bool canBeHurt;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        oxygen = maxOxygen;
        UpdateText();
        oxygenDepletionCooldown = true;
        canBeHurt = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOtherDimension && oxygenDepletionCooldown)
        {
            oxygenDepletionCooldown = false;
            StartCoroutine(OxygenTick());
        }
    }

    private void UpdateText()
    {
        oxygenText.text = oxygen.ToString();
        healthText.text = health.ToString();
    }

    //Takes in an integer amount, and uses the absolute value of it to subtract from health.
    //When subtracting from health, the value is clamped between 0 and max health.
    public void TakeDamage(int amount)
    {
        if(canBeHurt)
        {
            canBeHurt = false;
            health = Mathf.Clamp(health - Mathf.Abs(amount), 0, maxHealth);
            UpdateText();
            StartCoroutine(DamageCooldown());
            if(health == 0)
            {
                GameManager.Instance.ChangeScene(0);
            }
        }
        
    }

    private IEnumerator OxygenTick()
    {
        oxygen = Mathf.Clamp(oxygen - 1, 0, maxOxygen);
        if (oxygen == 0)
        {
            GameManager.Instance.ChangeScene(0);
        }
        UpdateText();
        yield return new WaitForSeconds(2f);
        oxygenDepletionCooldown = true;
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(.5f);
        canBeHurt = true;
    }
}
