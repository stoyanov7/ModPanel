namespace ModPanel.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Enums;
    using Models.ViewModels;

    public interface ILogService
    {
        void Create(string admin, LogType type, string additionalInformation);

        IEnumerable<AdminLogsViewModel> AllLogs();
    }
}