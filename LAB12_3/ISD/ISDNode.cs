namespace LAB12_3.ISD;

class IsdNode<T> where T : class
{
    public T Value;
    public IsdNode<T> Left;
    public IsdNode<T> Right;
    
    public IsdNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}