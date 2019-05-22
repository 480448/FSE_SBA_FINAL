using System;

using ProjectManager.Controllers;
using System.Collections.Generic;
using System.Web;
using ProjectManager.Models;
using System.Data.Entity;
using NUnit.Framework;

namespace ProjectManager.Test
{
    class MockProjectManagerEntities : DAC.ProjectManagerContainer
    {
        private DbSet<DAC.User> _users = null;
        private DbSet<DAC.Project> _projects = null;
        private DbSet<DAC.Task> _tasks = null;
        public override DbSet<DAC.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public override DbSet<DAC.Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public override DbSet<DAC.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }
    }

    [TestFixture]
    public class UserControllerTest
    {
        [Test]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 653322
            });
            context.Users = users;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.GetUser() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<User>),result.Data);
            Assert.AreEqual((result.Data as List<User>).Count, 2);
        }

        //[Test]
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var user = new Models.User();
            user.FirstName = "Pritam";
            user.LastName = "Saha";
            user.EmployeeId = "321456";
            user.UserId = 321;
            //var controller = new UserController(new BC.UserBC(context));
            var controller = new UserController(new BC.UserBC());
            var result = controller.InsertUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }

        [Test]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();

            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Rahul";
            user.LastName = "Roy";
            user.EmployeeId = "321";
            user.UserId = 335202;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "RAHUL");
        }

        [Test]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "ABC",
                Last_Name = "XYZ",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "ABC";
            user.LastName = "XYZ";
            user.EmployeeId = "335202";
            user.UserId = 335202;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Local.Count, 1);
        }

        [Test]

        public void TestDeleteUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418229",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503328",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<FormatException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<FormatException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<FormatException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "480448",
                First_Name = "Pritam",
                Last_Name = "Saha",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 480448
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahul",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

    }
}

