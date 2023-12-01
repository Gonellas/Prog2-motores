using System.Collections;
using UnityEngine;

public class SpeedBoost : Power
{
    public PowerUpType Type => PowerUpType.SpeedBoost;

    [SerializeField] private float _speedBoost = 5;
    private string _speedID = "3";
    private string _speedDisplayName = "SpeedBoost";
    public GameObject speedInterface;

    public Player _playerSpeed;

    protected override void PowerDetails(InventoryItemData itemData)
    {
        _speedID = itemData.id;
        _speedDisplayName = itemData.displayName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ApplyItemEffect(_speedID, _speedDisplayName);
        }
    }

    protected override void ApplyPower()
    {
        StartCoroutine(ActivateSpeed());
    }

    private IEnumerator ActivateSpeed()
    {
        _playerSpeed._speed += _speedBoost;
        speedInterface.SetActive(true);
        yield return new WaitForSeconds(5f);
        _playerSpeed._speed -= _speedBoost;
        speedInterface.SetActive(false);
    }
}