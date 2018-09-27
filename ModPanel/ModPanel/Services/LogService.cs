namespace ModPanel.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;
    using Models.ViewModels;

    public class LogService : ILogService
    {
        private readonly ModPanelContext context;

        public LogService() => this.context = new ModPanelContext();

        public void Create(string admin, LogType type, string additionalInformation)
        {
            using (this.context)
            {
                var log = new Log
                {
                    Admin = admin,
                    Type = type,
                    AdditionalInformation = additionalInformation
                };

                this.context.Logs.Add(log);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<AdminLogsViewModel> AllLogs()
        {
            using (this.context)
            {
                return this.context
                    .Logs
                    .OrderByDescending(l => l.Id)
                    .Select(l => new AdminLogsViewModel
                    {
                        Admin = l.Admin,
                        Type = l.Type,
                        AdditionalInformation = l.AdditionalInformation
                    })
                    .ToList();
            }
        }
    }
}