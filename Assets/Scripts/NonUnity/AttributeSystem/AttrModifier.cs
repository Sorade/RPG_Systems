
public class AttrModifier : BaseAttribute {
    private AttrModType _type;
    private int _order;
    private object _source;

    public AttrModType type { get { return _type; } }
    public int order { get { return _order; } }
    public object source { get { return _source; } }

    //constructor
    public AttrModifier(float value, AttrModType type, int order, object source)
    {
        baseValue = value;
        _type = type;
        _order = order;
        _source = source;
    }

    //contructor order set to modifier type int by default, and source to null
    public AttrModifier(float value, AttrModType type) : this(value, type, (int)type, null) { }

    //contructor source set to null by default
    public AttrModifier(float value, AttrModType type, int order) : this(value, type, order, null) { }

    //contructor order set to modifier type int by default
    public AttrModifier(float value, AttrModType type, object source) : this(value, type, (int)type, source) { }
}
