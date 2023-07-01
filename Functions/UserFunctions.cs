using Camunda.Api.Client;
using MsgFoundation.Data;
using MsgFoundation.Models;
using Camunda.Api.Client.ExternalTask;
using Camunda.Api.Client.ProcessInstance;

namespace MsgFoundation.Functions
{
    public class UserFunctions
    {
        //funcion crear usuario
        public static async Task CreateUser(ExternalTaskInfo task, MsgFoundationContext dbcontext, CamundaClient camunda)
        {

            Dictionary<string, VariableValue> variables = await camunda.Executions[task.ExecutionId].LocalVariables.GetAll();
            string username = variables["user"].GetValue<string>();
            string fullName = variables["fullname"].GetValue<string>();
            string email = variables["email"].GetValue<string>();
            string password = variables["password"].GetValue<string>();


            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = username.ToString(),
                FullName = fullName.ToString(),
                Email = email.ToString(),
                Password = password.ToString()
            };

            dbcontext.Users.Add(user);
            dbcontext.SaveChanges();

            FetchExternalTasks fetch = new FetchExternalTasks();
            fetch.WorkerId = "worker";
            fetch.MaxTasks = 1;
            fetch.UsePriority = true;
            fetch.Topics = new List<FetchExternalTaskTopic>();
            fetch.Topics.Add(new FetchExternalTaskTopic(task.TopicName, 1));

            List<LockedExternalTask> lockedTasks = await camunda.ExternalTasks.FetchAndLock(fetch);

            CompleteExternalTask request = new CompleteExternalTask();
            request.WorkerId = "worker";
            request.Variables = new Dictionary<string, VariableValue>();
            request.Variables.Add("idUser", VariableValue.FromObject(user.Id));

            await camunda.ExternalTasks[task.Id].Complete(request);


        }

        public static async Task ValidateCredentials(ExternalTaskInfo task, MsgFoundationContext dbcontext, CamundaClient camunda)
        {

            Dictionary<string, VariableValue> variables = await camunda.Executions[task.ExecutionId].LocalVariables.GetAll();
            string username = variables["user"].GetValue<string>();
            string password = variables["password"].GetValue<string>();

            User login = dbcontext.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();

            FetchExternalTasks fetch = new FetchExternalTasks();
            fetch.WorkerId = "worker";
            fetch.MaxTasks = 1;
            fetch.UsePriority = true;
            fetch.Topics = new List<FetchExternalTaskTopic>();
            fetch.Topics.Add(new FetchExternalTaskTopic(task.TopicName, 1));

            List<LockedExternalTask> lockedTasks = await camunda.ExternalTasks.FetchAndLock(fetch);

            CompleteExternalTask request = new CompleteExternalTask();
            request.WorkerId = "worker";
            request.Variables = new Dictionary<string, VariableValue>();
            

            VariableResource vars = camunda.ProcessInstances[task.ProcessInstanceId].Variables;
            await vars.SetBinary("Docvar", new BinaryDataContent(File.OpenRead("document.doc")), BinaryVariableType.Bytes);



            if (login != null)
            {
                request.Variables.Add("idUser", VariableValue.FromObject(login.Id));
                request.Variables.Add("login", VariableValue.FromObject(true));
                request.Variables.Add("fullname", VariableValue.FromObject(login.FullName));
                await camunda.ExternalTasks[task.Id].Complete(request);
            }
            else
            {
                request.Variables.Add("login", VariableValue.FromObject(false));
                await camunda.ExternalTasks[task.Id].Complete(request);

            }



        }


        
    }
}

