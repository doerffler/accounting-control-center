using ACCDataModel.Stammdaten;

namespace ACC.ViewModel.Messages;

public class BusinessYearChangedMessage
{
    public Geschaeftsjahr Year { get; set; }

    public BusinessYearChangedMessage(Geschaeftsjahr year)
    {
        Year = year;
    }
}