using System.Collections.Generic;

namespace ACC.ViewModel.Messages;

public class RefreshMessage
{
    public IEnumerable<string> Windows { get; set; }
    
    public RefreshMessage(params string[] windows)
    {
        Windows = windows;
    }
}