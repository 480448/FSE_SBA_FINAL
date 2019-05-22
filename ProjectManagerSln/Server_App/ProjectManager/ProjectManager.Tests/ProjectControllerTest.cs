
using NUnit.Framework;
using ProjectManager.Controllers;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Test
{

    [TestFixture]
    public class ProjectControllerTest
    {
        //[Test]
        public void TestGetProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<DAC.Project>();
            projects.Add(new DAC.Project()
            {
                Project_ID = 3214,
                Project_Name = "MyProject",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 3
            });
            projects.Add(new DAC.Project()
            {
                Project_ID = 32145,
                Project_Name = "MyProject",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 3
            });
            context.Projects = projects;

            var controller = new ProjectController(new BC.ProjectBC(context));
            var result = controller.RetrieveProjects() as JSendResponse;

            Assert.IsNotNull(result);                     
            Assert.IsInstanceOf(typeof(List<Project>),result.Data);
            Assert.AreEqual((result.Data as List<Project>).Count, 2);
        }

        //[Test]
        public void TestInsertProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Pritam",
                Last_Name = "Saha",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            var testProject = new Models.Project()
            {
                ProjectId = 32145,
                ProjectName = "MyProject",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(5),
                Priority = 3,
                NoOfCompletedTasks = 3,
                NoOfTasks = 5,
                User = new User()
                {
                    FirstName = "Pritam",
                    LastName = "Saha",
                    EmployeeId = "321456",
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var result = controller.InsertProjectDetails(testProject) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsNotNull((context.Users.Local[0]).Project_ID);
        }

        [Test]
        public void TestUpdateProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<DAC.Project>();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = 480448.ToString(),
                First_Name = "TEST",
                Last_Name = "TEST2",
                Project_ID = 321,
                Task_ID = 321,
                User_ID = 321
            });
            projects.Add(new DAC.Project()
            {
                Project_Name = "MyTestProject",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 2,
                Project_ID = 321
            });
            context.Projects = projects;
            context.Users = users;
            var testProject = new Models.Project()
            {
                ProjectId = 321,
                Priority = 3,
                NoOfCompletedTasks = 2,
                NoOfTasks = 5,
                ProjectName = "ProjectTest",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(10),
                User = new User()
                {
                    EmployeeId = 480448.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = 321,
                    UserId = 321
                }
            };

            var controller = new ProjectController(new BC.ProjectBC(context));
            var result = controller.UpdateProjectDetails(testProject) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Projects.Local[0]).Project_Name.ToUpper(), "PROJECTTEST");
        }

        [Test]
        public void TestDeleteProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<DAC.Project>();
            projects.Add(new DAC.Project()
            {
                Project_ID = 321,
                Project_Name = "TEST",
                Priority = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            projects.Add(new DAC.Project()
            {
                Project_ID = 234,
                Project_Name = "TEST2",
                Priority = 2,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(10)
            });
            context.Projects = projects;
            var controller = new ProjectController(new BC.ProjectBC(context));

            var testProject = new Models.Project()
            {
                ProjectId = 321,
                Priority = 3,
                NoOfCompletedTasks = 2,
                NoOfTasks = 5,
                ProjectName = "ProjectTest",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(10),
                User = new User()
                {
                    EmployeeId = 480448.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = 321,
                    UserId = 321
                }
            };

            var result = controller.DeleteProjectDetails(testProject) as JSendResponse;
            Assert.IsNotNull(result);
            Assert.AreEqual(context.Projects.Local.Count, 1);
        }

        [Test]        
        public void TestInsertProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new BC.ProjectBC(context));
            //var result = controller.InsertProjectDetails(testProject) as JSendResponse;
            var ex = Assert.Throws<ArgumentNullException>(() => controller.InsertProjectDetails(testProject)) ;
            Assert.That(ex.Message, Is.Not.Null);
        }

        [Test]
        public void TestInsertProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = -234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);

        }

        [Test]       
        public void TestInsertProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Malhotra",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = null
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.InsertProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
           
        }

        [Test]        
        public void TestInsertProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Pritam",
                Last_Name = "Saha",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = -234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestInsertProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Pritam",
                Last_Name = "Saha",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = 234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArgumentException>(() => controller.InsertProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestUpdateProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Pritam",
                Last_Name = "Saha",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.UpdateProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestUpdateProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Pritam",
                Last_Name = "Saha",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = -234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestUpdateProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = null
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.UpdateProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestUpdateProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = -234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestUpdateProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Pritam",
                Last_Name = "Saha",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = 234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));

            var ex = Assert.Throws<ArgumentException>(() => controller.UpdateProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestDeleteProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.DeleteProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
           
        }

        [Test]        
        public void TestDeleteProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = -234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestDeleteProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = null
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.DeleteProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]        
        public void TestDeleteProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Pritam",
                    LastName = "Saha",
                    ProjectId = -234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        public void TestDeleteProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = 321.ToString(),
                    FirstName = "Rahul",
                    LastName = "Roy",
                    ProjectId = 234,
                    UserId = 321
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var ex = Assert.Throws<ArgumentException>(() => controller.DeleteProjectDetails(testProject));
            Assert.That(ex.Message, Is.Not.Null);
            
        }
    }
}
