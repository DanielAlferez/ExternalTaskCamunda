using Camunda.Api.Client.ExternalTask;
using Camunda.Api.Client;
using MsgFoundation.Data;
using MsgFoundation.Models;

namespace MsgFoundation.Functions
{
    public class CreditFunctions
    {
        public static async Task CreatePayment(ExternalTaskInfo task, MsgFoundationContext dbcontext, CamundaClient camunda)
        {
            Dictionary<string, VariableValue> variables = await camunda.Executions[task.ExecutionId].LocalVariables.GetAll();
            string idUser = variables["idUser"].GetValue<string>();
            string pago = variables["pago"].GetValue<string>();
            string enfermedad = variables["enfermedad"].GetValue<string>();
            string forma = variables["forma"].GetValue<string>();
            string proveedor = variables["proveedor"].GetValue<string>();
            string material = variables["material"].GetValue<string>();

            Payment payment = new Payment
            {
                Id = Guid.NewGuid(),
                UserId = idUser,
                Pago = pago,
                Enfermedad = enfermedad,
                Forma = forma,
                Proveedor = proveedor,
                Material = material

            };

            dbcontext.Payments.Add(payment);
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

            await camunda.ExternalTasks[task.Id].Complete(request);
        }
    }
}
