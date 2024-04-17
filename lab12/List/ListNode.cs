namespace lab12.List;

public class ListNode<T>
{
    public T? Data { get; set; }
    public ListNode<T> Next { get; set; }
    public ListNode<T> Prev { get; set; }

    public ListNode()
    {
        this.Data = default;
        this.Prev = null;
        this.Next = null;
    }
    
    public ListNode(T data)
    {
        this.Data = data;
        this.Prev = null;
        this.Next = null;
    }

    public override string ToString()
    {
        return Data == null ? "" : Data.ToString();
    }
}