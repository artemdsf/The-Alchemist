using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Image _healthBar;
	[SerializeField] private Text _health;
	[SerializeField] private float _lerpSpeed = 10;

	private float _healthPercent;
	private float _curentHealth;

	private void Awake()
	{
		_healthBar.fillAmount = _playerHealth.Health;
	}

	private void Update()
	{
		_curentHealth = _playerHealth.Health;
		_healthPercent = _curentHealth / _playerHealth.MaxHealth;
		_healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _healthPercent, _lerpSpeed * Time.deltaTime);
		_health.text = _curentHealth.ToString();
	}
}
