using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MSTestApp.Mocking
{

    public class HouseKeeperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatementGenerator _statementGenerator;
        private readonly IEmailSender _emailSender;

        public HouseKeeperService(IUnitOfWork unitOfWork, IStatementGenerator statementGenerator, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _statementGenerator = statementGenerator;
            _emailSender = emailSender;
        }

        public bool SendStatementEmails(DateTime statementDate)
        {

            bool success = true;
            var housekeepers = _unitOfWork.Query<HouseKeeper>();

            foreach (var housekeeper in housekeepers)
            {
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                    continue;

                var statementFilename = _statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {
                    _emailSender.EmailFile(emailAddress, emailBody, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                }
                catch (Exception e)
                {
                    success = false;
                    break;
                }

            }

            return success;

        }
        
    }


    //Logic added below to make the above build


    public class HouseKeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class SystemSettingHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFrom { get; set; }
    }

    public class HousekeeperStatementReport
    {
        private int housekeeperOid;
        private DateTime statementDate;
        public bool HasData;

        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
            this.housekeeperOid = housekeeperOid;
            this.statementDate = statementDate;
        }

        public void CreateDocument()
        {
            throw new NotImplementedException();
        }

        public void ExportToPdf(string filename)
        {
            throw new NotImplementedException();
        }
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }

    }
}
