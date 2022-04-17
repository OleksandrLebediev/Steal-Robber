using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputData 
{
    public UIInputData(ILevelsInformant levelsInformant, IBalanceInformant balanceInformant)
    {
        LevelsInformant = levelsInformant;
        BalanceInformant = balanceInformant;
    }

    public ILevelsInformant LevelsInformant { get; }
    public IBalanceInformant BalanceInformant { get; }
}
