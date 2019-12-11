using System;
using System.Collections.Generic;
using Complex.Task1.ThirdParty;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Complex.Task1
{
    [TestClass]
    public class PrintTest
    {
        private readonly IDataSet dataSet = new DataSet();
        private ICommand _command;
        private IDatabaseManager _manager;
        private IView _view;

        [TestInitialize]
        public void SetUp()
        {
            _manager = Mock.Of<IDatabaseManager>();
            _view = Mock.Of<IView>();
            _command = new Print(_view, _manager);
        }

        [TestMethod]
        public void ShouldPrintTableWithOneColumn()
        {
            //given
            dataSet.Put("id", 1);
            PrepareSingleResult();
            //when
            _command.Process("print test");
            //then
            AssertPrinted("╔════╗\n" +
                          "║ id ║\n" +
                          "╠════╣\n" +
                          "║ 1  ║\n" +
                          "╚════╝\n");
        }

        [TestMethod]
        public void ShouldPrintTableWithPaddingWhenOneShortColumn()
        {
            //given
            dataSet.Put("i", 1);
            PrepareSingleResult();
            //when
            _command.Process("print test");
            //then
            AssertPrinted("╔════╗\n" +
                          "║ i  ║\n" +
                          "╠════╣\n" +
                          "║ 1  ║\n" +
                          "╚════╝\n");
        }

        [TestMethod]
        public void ShouldPrintAllColumnLengthsWithTheLongestValue()
        {
            //given
            dataSet.Put("i", 1);
            dataSet.Put("j", 1234567890);
            PrepareSingleResult();
            //when
            _command.Process("print test");
            //then
            AssertPrinted("╔════════════╦════════════╗\n" +
                          "║     i      ║     j      ║\n" +
                          "╠════════════╬════════════╣\n" +
                          "║     1      ║ 1234567890 ║\n" +
                          "╚════════════╩════════════╝\n");
        }

        [TestMethod]
        public void ShouldPrintMessageForNotExistingTable()
        {
            //given
            Mock.Get(_manager).Setup(p => p.GetTableData("testing")).Returns(new List<IDataSet>());
            //when
            _command.Process("print testing");
            //then
            AssertPrinted("╔════════════════════════════════════════════╗\n" +
                          "║ Table 'testing' is empty or does not exist ║\n" +
                          "╚════════════════════════════════════════════╝\n");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldTrowExceptionWhenCommandIsWrong()
        {
            _command.Process("print");
        }

        [TestMethod]
        public void ShouldProcessValidCommand()
        {
            //when
            var canProcess = _command.CanProcess("print test");
            //then
            Assert.IsTrue(canProcess);
        }

        [TestMethod]
        public void ShouldNotProcessInvalidCommand()
        {
            //when
            var canProcess = _command.CanProcess("qwe");
            //then
            Assert.IsFalse(canProcess);
        }

        [TestMethod]
        public void ShouldPrintTableWithMultiDataSets()
        {
            //given
            CreateUserDataSets(CreateUser(1, "Steven Seagal", "123456"), CreateUser(2, "Eva Song", "789456"));
            //when
            _command.Process("print users");
            //then
            AssertPrinted("╔════════════════╦════════════════╦════════════════╗\n" +
                          "║       id       ║      name      ║    password    ║\n" +
                          "╠════════════════╬════════════════╬════════════════╣\n" +
                          "║       1        ║ Steven Seagal  ║     123456     ║\n" +
                          "╠════════════════╬════════════════╬════════════════╣\n" +
                          "║       2        ║    Eva Song    ║     789456     ║\n" +
                          "╚════════════════╩════════════════╩════════════════╝\n");
        }


        private void CreateUserDataSets(params IDataSet[] users)
        {
            IList<IDataSet> dataSets = new List<IDataSet>();
            foreach (var usersSet in users)
                dataSets.Add(usersSet);
            Mock.Get(_manager).Setup(p => p.GetTableData("users")).Returns(dataSets);
        }

        private IDataSet CreateUser(int id, string name, string password)
        {
            var user = new DataSet();
            user.Put("id", id);
            user.Put("name", name);
            user.Put("password", password);
            return user;
        }

        private void PrepareSingleResult()
        {
            IList<IDataSet> dataSets = new List<IDataSet>();
            dataSets.Add(dataSet);
            Mock.Get(_manager).Setup(p => p.GetTableData("test")).Returns(dataSets);
        }


        private void AssertPrinted(string expected)
        {
            Mock.Get(_view).Verify(p => p.Write(expected), () => Times.AtLeast(1));
        }
    }
}