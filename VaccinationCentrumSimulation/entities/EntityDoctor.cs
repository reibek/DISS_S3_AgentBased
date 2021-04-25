using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPABA;
using OSPRNG;

namespace entities
{
    public class EntityDoctor : VaccineCentrumEntity
    {
        /// <summary>
        /// Exponential random variable for examination time of doctor.
        /// </summary>
        public ExponentialRNG RandExaminationTime { get; set; }

        /// <summary>
        /// Uniform random variable for decision of doctor, how long will patient wait in waiting room.
        /// </summary>
        public UniformContinuousRNG RandWaitingRoomTimeDecision { get; set; }

        public EntityDoctor(int id, Simulation simRef, Random randomGenerator) : base(id, simRef)
        {
            Id = id;
            IsBusy = false;
            RandExaminationTime = new ExponentialRNG(260, randomGenerator);
            RandWaitingRoomTimeDecision = new UniformContinuousRNG(0, 1, randomGenerator);
        }
    }
}