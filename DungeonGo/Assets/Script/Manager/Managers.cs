using UnityEngine;

public class Managers : Singleton<Managers>
{
    #region Manager Class
    protected TurnManager _turnManager = new TurnManager();
    #endregion

    protected override void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #region Get
    public TurnManager GetTurnManager()
    {
        return _turnManager;
    }
    #endregion
}
