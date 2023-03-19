using CardGame.Shared.Domain.Exceptions;

namespace CardGame.Game.Domain;

internal class Round
{
    private readonly IDictionary<Player, WhiteCard[]> _playerSubmissions;
    private readonly IDictionary<Player, bool> _playerSubmitted;

    public Round(Player czar, ICollection<Player> players, BlackCard blackCard)
    {
        Czar = czar;
        BlackCard = blackCard;
        _playerSubmissions = players.ToDictionary(
            player => player,
            _ => Array.Empty<WhiteCard>());
        _playerSubmitted = players.ToDictionary(
            player => player,
            _ => false);
    }

    public Player Czar { get; }
    public BlackCard BlackCard { get; }

    public IReadOnlyDictionary<Player, bool> PlayerSubmitted => _playerSubmitted.AsReadOnly();

    public void SubmitCards(Player player, WhiteCard[] whiteCards)
    {
        if (whiteCards.Length != BlackCard.BlankCount)
        {
            throw new WrongAmountOfCardsSubmitted(
                expectedCards: BlackCard.BlankCount, 
                submittedCards: whiteCards.Length);
        }

        if (!_playerSubmitted.ContainsKey(player))
        {
            throw new PlayerNotPartOfRound();
        }

        if (_playerSubmitted[player])
        {
            throw new PlayerAlreadySubmittedCards();
        }

        _playerSubmissions[player] = whiteCards;
        _playerSubmitted[player] = true;
    }

    public bool EveryoneSubmitted => _playerSubmitted.All(kvp => kvp.Value);
}

public class WrongAmountOfCardsSubmitted : DomainException
{
    public WrongAmountOfCardsSubmitted(int expectedCards, int submittedCards) : base(nameof(WrongAmountOfCardsSubmitted))
    {
        ExpectedCards = expectedCards;
        SubmittedCards = submittedCards;
    }

    public int ExpectedCards { get; }
    public int SubmittedCards { get; }
}

public class PlayerNotPartOfRound : DomainException
{
    public PlayerNotPartOfRound() : base(nameof(PlayerNotPartOfRound)) { }
}

public class PlayerAlreadySubmittedCards : DomainException
{
    public PlayerAlreadySubmittedCards() : base(nameof(PlayerAlreadySubmittedCards)) { }
}