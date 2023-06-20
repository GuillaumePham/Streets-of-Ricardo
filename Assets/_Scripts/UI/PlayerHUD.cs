using UnityEngine;
using UnityEngine.UI;

using SevenGame.Utility;

using TMPro;

public class PlayerHUD : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _decayHealthBar;
    [SerializeField] private Image _healthBar;

    public string playerName { get; private set; }
    public Character playerCharacter { get; private set; }

    public void SetCharacter(Character character) {
        playerCharacter = character;
    }

    public void SetName(string name) {
        playerName = name;
        _nameText.text = name;
    }

    private void Update() {
        _healthBar.fillAmount = playerCharacter.health.amount / playerCharacter.health.maxAmount;
        _decayHealthBar.fillAmount = playerCharacter.health.damagedHealth / playerCharacter.health.maxAmount;
    }
}
