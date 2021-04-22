using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPABA;
using OSPRNG;

namespace entities
{
    public class AdminWorker : VaccineCentrumEntity
    {
        /// <summary>
        /// Uniform random variable for registration time of admin worker.
        /// </summary>
        public UniformContinuousRNG RandRegistrationTime { get; set; }

        public AdminWorker(int id, Simulation simRef, Random randomGenerator) : base(id, simRef)
        {
            Id = id;
            IsBusy = false;
            RandRegistrationTime = new UniformContinuousRNG(140, 220, randomGenerator);
        }
    }
}