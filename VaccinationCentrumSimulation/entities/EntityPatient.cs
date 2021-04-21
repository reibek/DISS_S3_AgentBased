using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPABA;
using simulation;

namespace entities
{
    public class EntityPatient : Entity
    {
        /// <summary>
        /// Arrival time of patient.
        /// </summary>
        public double ArrivalTime { get; }

        /// <summary>
        /// Time, when patient start waiting in registration queue.
        /// </summary>
        public double RegistrationQuStartTime { get; set; }

        /// <summary>
        /// Time, when patient start waiting in registration queue.
        /// </summary>
        public double ExaminationQuStartTime { get; set; }

        /// <summary>
        /// Time, when patient start waiting in registration queue.
        /// </summary>
        public double VaccinationQuStartTime { get; set; }

        /// <summary>
        /// Time to spend in waiting room.
        /// </summary>
        public int WaitingRoomTime { get; set; }

        public EntityPatient(int id, Simulation mySim) : base(id, mySim)
        {
            Id = id;
            ArrivalTime = mySim.CurrentTime;
            RegistrationQuStartTime = -1;
            ExaminationQuStartTime = -1;
            VaccinationQuStartTime = -1;
            WaitingRoomTime = -1;
        }
    }
}
