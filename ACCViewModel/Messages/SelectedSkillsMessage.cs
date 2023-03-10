using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACC.ViewModel.Messages;

public class SelectedSkillsMessage
{
    public SelectedSkillsMessage(Skill data)
    {
        Data = data;
    }

    public Skill Data { get; }
}