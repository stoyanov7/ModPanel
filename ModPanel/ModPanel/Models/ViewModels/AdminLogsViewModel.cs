namespace ModPanel.Models.ViewModels
{
    using Enums;

    public class AdminLogsViewModel
    {
        public string Admin { get; set; }

        public LogType Type { get; set; }

        public string AdditionalInformation { get; set; }
    }
}