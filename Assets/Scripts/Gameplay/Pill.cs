using System;

public class Pill : Collectible
{
    public int boostAmount;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.PlayerSpawned += ReactivateObject;
    }

    private void ReactivateObject()
    {
        if (!isActive && _gameManager.BoostsRemaining <= 0)
            SetObjectActive(true);
    }

    protected override void Collect(PlayerController playerController)
    {
        _gameManager.BoostsRemaining += boostAmount;
    }

    private void OnDestroy()
    {
        _gameManager.PlayerSpawned -= ReactivateObject;
    }
}
