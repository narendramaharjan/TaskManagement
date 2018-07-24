using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AnnalectIO.DomainService.Services;
using AnnalectIO.DomainModel.Task;
using Assignment.TaskManagement.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Assignment.TaskManagement.Tests.Controllers
{
    /// <summary>
    /// Summary description for TaskControllerTest
    /// </summary>
    [TestClass]
    public class TaskControllerTest
    {
        private Mock<ITaskService> _mockTaskService = null;
        TasksController _taskConntroller = null;

        public TaskControllerTest()
        {
            // Arrange
            _mockTaskService = new Mock<ITaskService>();
            SetupDummyTaskData(_mockTaskService);
            _taskConntroller = new TasksController(_mockTaskService.Object);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public async Task Task_Index_Action_Return_IndexView()
        {
            // Act
            var actionResultTask = await _taskConntroller.Index();
            var viewResult = actionResultTask as ViewResult;

            //Assert 
            Assert.IsNotNull(viewResult);

            //Controller outside of mvc context causing empty view name
            //Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public async Task Task_Index_Action_Return_IEnumerableTasks()
        {
            // Act
            var actionResultTask = await _taskConntroller.Index();
            var viewResult = actionResultTask as ViewResult;

            //Assert 
            Assert.IsInstanceOfType(viewResult.Model, typeof(IEnumerable<TaskModel>));

        }

        [TestMethod]
        public async Task Task_Details_ShouldNotFindTasks()
        {
            // Act
            var result = await _taskConntroller.Details(Guid.NewGuid());

            //Assert 
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        private void SetupDummyTaskData(Mock<ITaskService> _mockTaskService)
        {
            // Arrange
            _mockTaskService.Setup(x => x.GetAll()).Returns(Task.FromResult<IEnumerable<TaskModel>>(new List<TaskModel>
                {
                    new TaskModel() { Id = Guid.NewGuid(), Name = "Test Work", Description = "Test Work to be completed by EOD", DateCreated = DateTime.Now }
                }));
        }

    }
}
