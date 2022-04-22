using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private WalletDisplay _walletDisplay;

    public void Initializer(PlayerWallet playerWallet)
    {
        _walletDisplay.Initializer(playerWallet);
    }
}
