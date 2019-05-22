using System;
using System.Collections.Generic;
using NBench;
using ProjectManager.Controllers;
using ProjectManager.Models;
using ProjectManager.Test;
using DAC = ProjectManager.DAC;
using BC = ProjectManager.BC;



namespace ProjectManger.NBenchPerformanceTesting
{
    public class ProjectManagerLoadTest 
    {
        private readonly Dictionary<int, int> dictionary = new Dictionary<int, int>();

        private const string AddCounterName = "AddCounter";
        private Counter addCounter;
        //private int key;

        private const int AcceptableMinAddThroughput = 200;
        private const int iteration = 50;
        [PerfSetup]
        public void Setup(BenchmarkContext benchContext)
        {
            addCounter = benchContext.GetCounter(AddCounterName);
           // key = 0;
        }

        [PerfBenchmark(NumberOfIterations = 500,RunMode = RunMode.Iterations, TestMode =TestMode.Test,SkipWarmups =true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestGetProjects_Success(BenchmarkContext benchContext)
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
            projects.Add(new ProjectManager.DAC.Project()
            {
                Project_ID = 32145,
                Project_Name = "MyProject",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 3
            });
            context.Projects = projects;

            var controller = new ProjectController(new ProjectManager.BC.ProjectBC(context));
            var result = controller.RetrieveProjects() as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestInsertProjects_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "414948",
                First_Name = "Pritam",
                Last_Name = "Saha",
                User_ID = 321,
                Task_ID = 321
            });
            context.Users = users;
            var testProject = new ProjectManager.Models.Project()
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
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestUpdateProjects_Success(BenchmarkContext benchContext)
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
            var testProject = new ProjectManager.Models.Project()
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
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestDeleteProjects_Success(BenchmarkContext benchContext)
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

            var testProject = new ProjectManager.Models.Project()
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
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestRetrieveTasks_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();

            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 321456,
                Parent_Task_Name = "PNB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 1
            });
            context.Users = users;
            int projectid = 3214;
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "MYTASK",
                Parent_ID = 321456,
                Project_ID = 3214,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0

            });
            context.Tasks = tasks;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(projectid) as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestRetrieveParentTasks_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 32145,
                Parent_Task_Name = "PTASK"

            });
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 321456,
                Parent_Task_Name = "MYTASK"

            });
            context.ParentTasks = parentTasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveParentTasks() as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestInsertTasks_Success(BenchmarkContext benchContext)
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
            var task = new ProjectManager.Models.Task()
            {

                Task_Name = "ASDQW",
                Parent_ID = 321674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0,
                User = new User()
                {
                    FirstName = "Pritam",
                    LastName = "ChowdSahahury",
                    EmployeeId = "321456",
                    UserId = 321
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestUpdateTasks_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
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
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "ASDQW",
                Parent_ID = 321674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            context.Users = users;
            var testTask = new ProjectManager.Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 321674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 30,
                Status = 0,
                User = new User()
                {
                    FirstName = "Rahul",
                    LastName = "Roy",
                    EmployeeId = "321456",
                    UserId = 321
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(testTask) as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestDeleteTasks_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();

            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "task1",
                Parent_ID = 321674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            var testTask = new ProjectManager.Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 321674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(testTask) as JSendResponse;

        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestRetrieveTaskByProjectId_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 32145,
                Parent_Task_Name = "ANB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "863276",
                First_Name = "Rahul",
                Last_Name = "Roy",
                User_ID = 321,
                Task_ID = 32145,
                Project_ID = 3214
            });
            context.Users = users;
            tasks.Add(new DAC.Task()
            {
                Project_ID = 32145,
                Parent_ID = 32145,
                Task_ID = 32145,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            tasks.Add(new DAC.Task()
            {
                Project_ID = 321,
                Parent_ID = 321,
                Task_ID = 321,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            context.Tasks = tasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(32145) as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestGetUser_Success(BenchmarkContext benchContext)
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

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.GetUser() as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestInsertUser_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var user = new ProjectManager.Models.User();
            user.FirstName = "Pritam";
            user.LastName = "Saha";
            user.EmployeeId = "321456";
            user.UserId = 321;
            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestUpdateUser_Success(BenchmarkContext benchContext)
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();

            users.Add(new DAC.User()
            {
                Employee_ID = "335202",
                First_Name = "Rahulan",
                Last_Name = "Roy",
                Project_ID = 3214,
                Task_ID = 3214,
                User_ID = 335202
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.FirstName = "Rahul";
            user.LastName = "Roy";
            user.EmployeeId = "321";
            user.UserId = 335202;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
            addCounter.Increment();
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode =TestMode.Test, SkipWarmups = true)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        
        [CounterMeasurement(AddCounterName)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void TestDeleteUser_Success(BenchmarkContext benchContext)
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

            var user = new ProjectManager.Models.User();
            user.FirstName = "ABC";
            user.LastName = "XYZ";
            user.EmployeeId = "335202";
            user.UserId = 335202;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
            addCounter.Increment();
        }


        [PerfCleanup]
        public void Cleanup(BenchmarkContext benchContext)
        {
            
        }
    }
}
