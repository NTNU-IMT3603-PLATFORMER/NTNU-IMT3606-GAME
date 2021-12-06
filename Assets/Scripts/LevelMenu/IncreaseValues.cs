using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IncreaseValues : MonoBehaviour {
    GameObject _player;
    [SerializeField]
    Text currentSpiritsText;
    [SerializeField]
    Text currentStrengthText;
    [SerializeField]
    Text currentHealthText;
    [SerializeField]
    Text currentSpeedText;
    [SerializeField]
    Text newSpiritsText;
    [SerializeField]
    Text newStrengthText;
    [SerializeField]
    Text newHealthText;
    [SerializeField]
    Text newSpeedText;
    [SerializeField]
    Text currentPlayerLevelText;
    [SerializeField]
    Text newPlayerLevelText;
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    Text currentCostText;


    int _playerLevel;
    int _strength;
    float _speed;
    int _maxHealth;
    int _spirits;

    int _newPlayerLevel;
    int _newStrength;
    float _newSpeed;
    int _newMaxHealth;
    int _newSpirits;

    int _cost;

    private void Start() {

        _player = PlayerEntity.INSTANCE.gameObject;

        _playerLevel = _player.GetComponent<Entity>().playerLevel;
        _maxHealth = _player.GetComponent<Entity>().maxHealth;
        _strength = _player.GetComponent<EntityCombat>().baseDamage;
        _speed = _player.GetComponent<PlayerMovement>().speed;
        _spirits = _player.GetComponent<Entity>().spirits;

        _newPlayerLevel = _player.GetComponent<Entity>().playerLevel;
        _newMaxHealth = _player.GetComponent<Entity>().maxHealth;
        _newStrength = _player.GetComponent<EntityCombat>().baseDamage;
        _newSpeed = _player.GetComponent<PlayerMovement>().speed;
        _newSpirits = _player.GetComponent<Entity>().spirits;

        currentSpiritsText.text = _spirits.ToString();
        currentHealthText.text = _maxHealth.ToString();
        currentStrengthText.text = _strength.ToString();
        currentSpeedText.text = _speed.ToString();
        currentPlayerLevelText.text = _playerLevel.ToString();

        newSpiritsText.text = _spirits.ToString();
        newHealthText.text = _maxHealth.ToString();
        newStrengthText.text = _strength.ToString();
        newSpeedText.text = _speed.ToString();
        newPlayerLevelText.text = _playerLevel.ToString();

        descriptionText.text = "";
        currentCostText.text = "1 spirit";
    }


    public void incrementHealth() {

        if (calculateNewAmountOfSpirits(true)) { 
            _newMaxHealth += 1;
            newHealthText.text = _newMaxHealth.ToString();
            incrementPlayerLevel();
        }

    }

    public void incrementSpeed() {
        if (calculateNewAmountOfSpirits(true)) { 
            _newSpeed += 1;
            newSpeedText.text = _newSpeed.ToString();
            incrementPlayerLevel();
        }
    }

    public void incrementStrength() {
        if (calculateNewAmountOfSpirits(true)) {
            _newStrength += 1;
            newStrengthText.text = _newStrength.ToString();
            incrementPlayerLevel();
        }

    }

    public void incrementPlayerLevel() {
        _newPlayerLevel += 1;
        newPlayerLevelText.text = _newPlayerLevel.ToString();
    }

    public void decrementStrength() {
        if (_newStrength > _strength) {
            _newStrength -= 1;
            newStrengthText.text = _newStrength.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementHealth() {
        if (_newMaxHealth > _maxHealth) {
            _newMaxHealth -= 1;
            newHealthText.text = _newMaxHealth.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementSpeed() {
        if (_newSpeed > _speed) {
            _newSpeed -= 1;
            newSpeedText.text = _newSpeed.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementPlayerLevel() {
        _newPlayerLevel -= 1;
        newPlayerLevelText.text = _newPlayerLevel.ToString();
    }

    public bool calculateNewAmountOfSpirits(bool isLeveling) {
        _cost = _newPlayerLevel;
        currentCostText.text = (_cost + 1) + " spirits";
        if (isLeveling) {

            if (_newSpirits - _cost <= 0) {
                descriptionText.text = "Not enough spirits";
                Debug.Log("Not enough spirits");
                return false;
            } else {
                _newSpirits = _newSpirits - _cost;
                newSpiritsText.text = _newSpirits.ToString();
                descriptionText.text = "";
                return true;
            }
        } else {
            _newSpirits = _newSpirits + _cost;
            newSpiritsText.text = _newSpirits.ToString();
            descriptionText.text = "";
            currentCostText.text = _cost + " spirits";
            return true;
        }
    }

    public void confirmLevelUp() {

        _playerLevel = _newPlayerLevel;
        _maxHealth = _newMaxHealth;
        _strength = _newStrength;
        _speed = _newSpeed;
        _spirits = _newSpirits;


        _player.GetComponent<Entity>().playerLevel = _newPlayerLevel;
        _player.GetComponent<Entity>().maxHealth = _newMaxHealth;
        _player.GetComponent<Entity>().health = _newMaxHealth;
        _player.GetComponent<EntityCombat>().baseDamage = _newStrength;
        _player.GetComponent<PlayerMovement>().speed = _newSpeed;
        _player.GetComponent<Entity>().spirits = _newSpirits;

        currentSpiritsText.text = _spirits.ToString();
        currentHealthText.text = _maxHealth.ToString();
        currentStrengthText.text = _strength.ToString();
        currentSpeedText.text = _speed.ToString();
        currentPlayerLevelText.text = _playerLevel.ToString();

    }

}
