namespace CardGame.Game.Domain;

public class Deck<T>
{
    private int _index = 0;
    private readonly IList<T> _items;

    public Deck(ICollection<T> items)
    {
        _items = items.ToList();
        Shuffle();
    }

    public T Deal()
    {
        return _items[_index++];
    }

    private void Shuffle()
    {
        for (var i = _index; i < _items.Count; i++)
        {
            var j = _index + Random.Shared.Next(_items.Count - _index);
            (_items[i], _items[j]) = (_items[j], _items[i]);
        }
    }
}

