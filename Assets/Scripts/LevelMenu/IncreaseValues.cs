using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IncreaseValues : MonoBehaviour {
    GameObject _player;
    public Text currentSpiritsText;
    public Text currentStrengthText;
    public Text currentHealthText;
    public Text currentSpeedText;
    public Text newSpiritsText;
    public Text newStrengthText;
    public Text newHealthText;
    public Text newSpeedText;
    public Text currentPlayerLevelText;
    public Text newPlayerLevelText;
    public Text descriptionText;
    public Text currentCostText;

    public int playerLevel;
    public int strength;
    public float speed;
    public int maxHealth;
    public int spirits;

    public int newPlayerLevel;
    public int newStrength;
    public float newSpeed;
    public int newMaxHealth;
    public int newSpirits;

    public int cost;

    private void Start() {

        _player = PlayerEntity.INSTANCE.gameObject;

        playerLevel = _player.GetComponent<PlayerEntity>().playerLevel;
        maxHealth = _player.GetComponent<Entity>().maxHealth;
        strength = _player.GetComponent<EntityCombat>().baseDamage;
        speed = _player.GetComponent<PlayerMovement>().speed;
        spirits = _player.GetComponent<PlayerEntity>().spirits;

        newPlayerLevel = _player.GetComponent<PlayerEntity>().playerLevel;
        newMaxHealth = _player.GetComponent<Entity>().maxHealth;
        newStrength = _player.GetComponent<EntityCombat>().baseDamage;
        newSpeed = _player.GetComponent<PlayerMovement>().speed;
        newSpirits = _player.GetComponent<PlayerEntity>().spirits;

        currentSpiritsText.text = spirits.ToString();
        currentHealthText.text = maxHealth.ToString();
        currentStrengthText.text = strength.ToString();
        currentSpeedText.text = speed.ToString();
        currentPlayerLevelText.text = playerLevel.ToString();

        newSpiritsText.text = spirits.ToString();
        newHealthText.text = maxHealth.ToString();
        newStrengthText.text = strength.ToString();
        newSpeedText.text = speed.ToString();
        newPlayerLevelText.text = playerLevel.ToString();

        descriptionText.text = "";
        currentCostText.text = "1 spirit";
    }


    public void incrementHealth() {

        if (calculateNewAmountOfSpirits(true)) { 
            newMaxHealth += 1;
            newHealthText.text = newMaxHealth.ToString();
            incrementPlayerLevel();
        }

    }

    public void incrementSpeed() {
        if (calculateNewAmountOfSpirits(true)) { 
            newSpeed += 1;
            newSpeedText.text = newSpeed.ToString();
            incrementPlayerLevel();
        }
    }

    public void incrementStrength() {
        if (calculateNewAmountOfSpirits(true)) {
            newStrength += 1;
            newStrengthText.text = newStrength.ToString();
            incrementPlayerLevel();
        }

    }

    public void incrementPlayerLevel() {
        newPlayerLevel += 1;
        newPlayerLevelText.text = newPlayerLevel.ToString();
    }

    public void decrementStrength() {
        if (newStrength > strength) {
            newStrength -= 1;
            newStrengthText.text = newStrength.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementHealth() {
        if (newMaxHealth > maxHealth) {
            newMaxHealth -= 1;
            newHealthText.text = newMaxHealth.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementSpeed() {
        if (newSpeed > speed) {
            newSpeed -= 1;
            newSpeedText.text = newSpeed.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementPlayerLevel() {
        newPlayerLevel -= 1;
        newPlayerLevelText.text = newPlayerLevel.ToString();
    }

    public bool calculateNewAmountOfSpirits(bool isLeveling) {
        cost = newPlayerLevel;
        currentCostText.text = (cost + 1) + " spirits";
        if (isLeveling) {

            if (newSpirits - cost <= 0) {
                descriptionText.text = "Not enough spirits";
                Debug.Log("Not enough spirits");
                return false;
            } else {
                newSpirits = newSpirits - cost;
                newSpiritsText.text = newSpirits.ToString();
                descriptionText.text = "";
                return true;
            }
        } else {
            newSpirits = newSpirits + cost;
            newSpiritsText.text = newSpirits.ToString();
            descriptionText.text = "";
            currentCostText.text = cost + " spirits";
            return true;
        }
    }

    public void confirmLevelUp() {

        playerLevel = newPlayerLevel;
        maxHealth = newMaxHealth;
        strength = newStrength;
        speed = newSpeed;
        spirits = newSpirits;


        _player.GetComponent<PlayerEntity>().playerLevel = newPlayerLevel;
        _player.GetComponent<Entity>().maxHealth = newMaxHealth;
        _player.GetComponent<Entity>().health = newMaxHealth;
        _player.GetComponent<EntityCombat>().baseDamage = newStrength;
        _player.GetComponent<PlayerMovement>().speed = newSpeed;
        _player.GetComponent<PlayerEntity>().spirits = newSpirits;

        currentSpiritsText.text = spirits.ToString();
        currentHealthText.text = maxHealth.ToString();
        currentStrengthText.text = strength.ToString();
        currentSpeedText.text = speed.ToString();
        currentPlayerLevelText.text = playerLevel.ToString();

    }

}
