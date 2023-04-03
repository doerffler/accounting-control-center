using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACC.ViewModel.Messages;

public class StatusbarMessage
{
    public string Text { get; set; }

    public StatusbarMessage(string text)
    {
        Text = text;
    }
}