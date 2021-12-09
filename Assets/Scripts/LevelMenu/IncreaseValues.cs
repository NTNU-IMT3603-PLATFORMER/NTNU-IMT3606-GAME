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

    public static IncreaseValues INSTANCE { get; private set; }

    int playerLevel {
        get => PlayerEntity.INSTANCE.playerLevel;
        set => PlayerEntity.INSTANCE.playerLevel = value;
    }
    int strength {
        get => PlayerEntity.INSTANCE.GetComponent<PlayerCombat>().baseDamage;
        set => PlayerEntity.INSTANCE.GetComponent<PlayerCombat>().baseDamage = value;
    }
    float speed {
        get => PlayerEntity.INSTANCE.GetComponent<PlayerMovement>().speed;
        set => PlayerEntity.INSTANCE.GetComponent<PlayerMovement>().speed = value;
    }
    int maxHealth {
        get => PlayerEntity.INSTANCE.maxHealth;
        set => PlayerEntity.INSTANCE.maxHealth = value;
    }
    int spirits {
        get => PlayerEntity.INSTANCE.spirits;
        set => PlayerEntity.INSTANCE.spirits = value;
    }
    int health {
        get => PlayerEntity.INSTANCE.health;
        set => PlayerEntity.INSTANCE.health = value;
    }





    int _newPlayerLevel;
    int _newStrength;
    float _newSpeed;
    int _newMaxHealth;
    int _newSpirits;

    int _cost;

    private void Start() {
        INSTANCE = this;

        UpdateValues();

        descriptionText.text = "";
        currentCostText.text = "1 spirit";
    }

    public void UpdateValues() {
        Debug.Log("yes");
        _newPlayerLevel = playerLevel;
        _newMaxHealth = maxHealth;
        _newStrength = strength;
        _newSpeed = speed;
        _newSpirits = spirits;

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
        if (_newStrength > strength) {
            _newStrength -= 1;
            newStrengthText.text = _newStrength.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementHealth() {
        if (_newMaxHealth > maxHealth) {
            _newMaxHealth -= 1;
            newHealthText.text = _newMaxHealth.ToString();
            decrementPlayerLevel();
            calculateNewAmountOfSpirits(false);
        }
    }

    public void decrementSpeed() {
        if (_newSpeed > speed) {
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
        Debug.Log(_newSpirits);
        _cost = _newPlayerLevel;

        if (isLeveling) {
            if (_newSpirits - _cost < 0) {
                descriptionText.text = "Not enough spirits";
                Debug.Log("Not enough spirits");
                return false;
            } else {
                currentCostText.text = (_cost + 1) + " spirits";
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

        if (_newPlayerLevel > playerLevel) {

            playerLevel = _newPlayerLevel;
            maxHealth = _newMaxHealth;
            strength = _newStrength;
            speed = _newSpeed;
            spirits = _newSpirits;


            playerLevel = _newPlayerLevel;
            maxHealth = _newMaxHealth;
            health = _newMaxHealth;
            strength = _newStrength;
            speed = _newSpeed;
            spirits = _newSpirits;

            currentSpiritsText.text = spirits.ToString();
            currentHealthText.text = maxHealth.ToString();
            currentStrengthText.text = strength.ToString();
            currentSpeedText.text = speed.ToString();
            currentPlayerLevelText.text = playerLevel.ToString();
        } else {
            descriptionText.text = "You haven't leveled up";
        }

    }

}
