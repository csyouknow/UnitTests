using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace MSTestApp.Mocking
{
    [TestFixture]
    class HouseKeeperServiceTests
    {
        private HouseKeeperService _houseKeeperService;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private readonly DateTime _statementDate = new DateTime(2017, 1, 1);
        private HouseKeeper _houseKeeper;
        private readonly string _filename = "filename";

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new HouseKeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<HouseKeeper>()).Returns(new List<HouseKeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();

            _houseKeeperService = new HouseKeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            
            _houseKeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));

        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_WhereHouseKeepersEmailIsNullOrWhiteSpace_ShouldNotGenerateStatements(string input)
        {

            _houseKeeper.Email = input;

            _houseKeeperService.SendStatementEmails(_statementDate);

            //Check that the SaveStatement never runs if email is null (Times.Never)
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
                Times.Never);

        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(_filename);

            _houseKeeperService.SendStatementEmails(_statementDate);
            
            _emailSender.Verify(es => es.EmailFile(
                _houseKeeper.Email, 
                _houseKeeper.StatementEmailBody, 
                _filename, 
                It.IsAny<string>()));

        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_StatementFileNameIsNullOrWhiteSpace_ShouldNotEmailTheStatement(string input)
        {

            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => input);

            _houseKeeperService.SendStatementEmails(_statementDate);

            //Check that the EmailSender never runs if statement file name is null (Times.Never)
            _emailSender.Verify(sg => sg.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);

        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_ReturnFalse()
        {

            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(_filename);

            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws<Exception>();

            var result = _houseKeeperService.SendStatementEmails(_statementDate);

            Assert.That(result, Is.EqualTo(false));

        }

    }
}
