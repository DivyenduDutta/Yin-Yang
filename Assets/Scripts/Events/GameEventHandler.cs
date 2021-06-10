
public class GameEventHandler{
    
    public GameConstants.GameEvents gameEvent { get; set; }
    public string eventHandlerName { get; set; }
    public float delay { get; set; }

    public bool HasEnded;

    public GameEventHandler(GameConstants.GameEvents gameEvent, string eventHandlerName, float delay)
    {
        this.gameEvent = gameEvent;
        this.eventHandlerName = eventHandlerName;
        this.delay = delay;
        this.HasEnded = false;
    }
}
