using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private WalletDisplay _walletDisplay;
    [SerializeField] private ShopButton _shopButton;
    [SerializeField] private LevelHeader _levelHeader;
    
    private IFirstTapHandler _tapHandler;
    
    public void Initializer(PlayerWallet playerWallet, UIInputData inputData)
    {
        _walletDisplay.Initializer(playerWallet);
        _shopButton.Initialize();
        _levelHeader.Initialize(inputData.LevelsInformant);
        
        _tapHandler = inputData.FirstTapHandler;
        _tapHandler.FirstTap += OnFirstTap;
    }
    
    private void OnFirstTap()
    {
        _shopButton.Hide();
        _levelHeader.Hide();
    }

    private void OnDestroy()
    {
        _tapHandler.FirstTap -= OnFirstTap;
    }
}
