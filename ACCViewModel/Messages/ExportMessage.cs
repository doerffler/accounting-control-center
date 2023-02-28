namespace ACC.ViewModel.Messages;

public class ExportMessage
{
    public ExportMessage(dynamic data)
    {
        Data = data;
    }

    public dynamic Data { get; }
}