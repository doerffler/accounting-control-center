using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACC.ViewModel.Messages;

public class SelectedSkillsMessage
{
    public SelectedSkillsMessage(IEnumerable<Skill> data)
    {
        Data = data;
    }

    public IEnumerable<Skill> Data { get; }
}