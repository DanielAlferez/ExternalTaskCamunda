using Camunda.Api.Client.ExternalTask;
using Camunda.Api.Client;
using MsgFoundation.Data;
using MsgFoundation.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MsgFoundation.Functions
{
    public class ObservationFunctions
    {
        public static async Task CreateAppointment(ExternalTaskInfo task, MsgFoundationContext dbcontext, CamundaClient camunda)
        {
            Dictionary<string, VariableValue> variables = await camunda.Executions[task.ExecutionId].LocalVariables.GetAll();
            string userId = variables["idUser"].GetValue<string>();
            string appointmentDateString = variables["appointmentDate"].GetValue<string>();
            DateTime appointmentDate = DateTime.ParseExact(appointmentDateString, "yyyy-MM-dd'T'HH:mmK", CultureInfo.InvariantCulture);

            bool isAppointmentConflict = dbcontext.Appointments.Any(a => a.AppointmentDateTime == appointmentDate.ToUniversalTime());
            Console.Write(isAppointmentConflict);

            
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

            if (!isAppointmentConflict)
            {
                request.Variables.Add("disponibilidad", VariableValue.FromObject(true));
                Appointment appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    AppointmentDateTime = appointmentDate.ToUniversalTime(),
                };

                dbcontext.Appointments.Add(appointment);
                dbcontext.SaveChanges();
                await camunda.ExternalTasks[task.Id].Complete(request);
            }
            else
            {
                request.Variables.Add("disponibilidad", VariableValue.FromObject(false));
                await camunda.ExternalTasks[task.Id].Complete(request);
            }


        }
    }
}
