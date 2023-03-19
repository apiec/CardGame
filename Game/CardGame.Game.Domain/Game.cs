namespace CardGame.Game.Domain;

public enum GamePhase
{
    None = 0,
    NotStarted,
}

public class Game
{
    private readonly List<Player> _players = new();
    protected Deck<WhiteCard> _whiteCardsDeck = new(Array.Empty<WhiteCard>());
    protected Deck<BlackCard> _blackCardsDeck = new(Array.Empty<BlackCard>());

    private GameState _gameState = new NotStartedGame();

    public GamePhase Phase => _gameState.Phase;

    public IReadOnlyList<Player> Players => _players;
    public void AddPlayer(Player player) => _players.Add(player);
    public void RemovePlayer(Player player) => _players.Remove(player);
}

public abstract class GameState
{
    public abstract GamePhase Phase { get; }
}

public class NotStartedGame : GameState
{
    public override GamePhase Phase => GamePhase.NotStarted;

    public GameState StartGame()
    {
        return new GameStarted(this);
    }
}

public class GameStarted : GameState
{
    public GameStarted(NotStartedGame notStartedGameState)
    {
        
    }

    public override GamePhase Phase => GamePhase.None;
}