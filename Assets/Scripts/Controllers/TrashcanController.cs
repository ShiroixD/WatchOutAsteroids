using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashcanController : MonoBehaviour {

	[SerializeField] int _availableShields = 3;
    [SerializeField] GameObject _shield;
	[SerializeField] Text _shieldsText;

	void Start()
	{
        _shieldsText.text = "";
        _shieldsText.transform.position = transform.position;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(IsPlayer(other))
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			if(!IsPlayerEquipped(player) && _availableShields > 0)
			{
				_availableShields--;
				EquipPlayer(player);
			}

            ShieldCounterText();
		}
	}

	void EquipPlayer(PlayerController player)
	{
		player.EquipShield(_shield);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		StartCoroutine(ClearRemainderText());
	}

	void ShieldCounterText()
	{
		_shieldsText.text = "Remaining shields: " + _availableShields.ToString();
	}

	IEnumerator ClearRemainderText()
	{
        yield return new WaitForSeconds(0.4f);
		_shieldsText.text = "";		
	}

	bool IsPlayerEquipped(PlayerController player)
	{
		return player.HasShield();
	}

	bool IsPlayer(Collider2D other)
	{
		return other.gameObject.CompareTag("Player");
	}

}
