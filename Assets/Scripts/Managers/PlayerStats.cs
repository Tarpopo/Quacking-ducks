using DefaultNamespace;

public class PlayerStats : ManagerBase, IAwake
{
    public int CoinCount;
    public void OnAwake()
    {
        CoinCount = Toolbox.Get<Save>()._save.MoneyCount;
    }
}
