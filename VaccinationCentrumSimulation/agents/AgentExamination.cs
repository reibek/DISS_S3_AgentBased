using System.Collections.Generic;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using entities;
using OSPRNG;
using OSPStat;
using VaccineCentrum;

//using instantAssistants;
namespace agents
{
	//meta! id="5"
	public class AgentExamination : Agent
    {
        public DataStructures.Queue<MessageForm> QuExamination;
        public WStat StatQuExaminationSize { get; set; }
        public Stat StatQuExaminationTime { get; set; }
		public Pool<EntityDoctor> PoolDoctors { get; set; }
        public List<UniformDiscreteRNG> RandDoctorChoice { get; set; }

		public AgentExamination(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            QuExamination = new DataStructures.Queue<MessageForm>();
            StatQuExaminationSize = new WStat(MySim);
            StatQuExaminationTime = new Stat();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();

            var doctorsCount = ((MySimulation) MySim).ResDoctorsCount;

			QuExamination.Clear();
            StatQuExaminationSize.Clear();
            StatQuExaminationTime.Clear();

			PoolDoctors = new Pool<EntityDoctor>(doctorsCount);
			RandDoctorChoice = new List<UniformDiscreteRNG>(doctorsCount);
            for (int i = 0; i < doctorsCount; i++)
            {
                PoolDoctors.Add(new EntityDoctor(i + 1, MySim, ((MySimulation) MySim).RandSeedGenerator));
                RandDoctorChoice.Add(new UniformDiscreteRNG(0, i, ((MySimulation) MySim).RandSeedGenerator));
            }
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerExamination(SimId.ManagerExamination, MySim, this);
			new SchedulerDoctorBreak(SimId.SchedulerDoctorBreak, MySim, this);
			new ProcessExamination(SimId.ProcessExamination, MySim, this);
			AddOwnMessage(Mc.Initialization);
			AddOwnMessage(Mc.NoticeTimeForBreak);
			AddOwnMessage(Mc.NoticeProcessExaminationEnded);
			AddOwnMessage(Mc.RequestExamination);
			AddOwnMessage(Mc.RequestDoctorBreak);
		}
		//meta! tag="end"

        public void FinalUpdateStatistics()
        {
            StatQuExaminationSize.AddSample(QuExamination.Size);
        }
	}
}