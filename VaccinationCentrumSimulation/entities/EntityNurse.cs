using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPABA;
using OSPRNG;

namespace entities
{
    public class EntityNurse : VaccineCentrumEntity
    {
        /// <summary>
        /// Triangular random variable for vaccination time of nurse.
        /// </summary>
        public TriangularRNG RandVaccinationTime { get; set; }

        /// <summary>
        /// Triangular random variable for filling syringe with vaccine.
        /// </summary>
        public TriangularRNG RandFillSyringeTime { get; set; }

        public int SyringesFullCount { get; set; }

        public EntityNurse(int id, Simulation simRef, Random randomGenerator) : base(id, simRef)
        {
            Id = id;
            IsBusy = false;
            RandVaccinationTime = new TriangularRNG(20, 75, 100, randomGenerator);
            RandFillSyringeTime = new TriangularRNG(6,10,40, randomGenerator);
            SyringesFullCount = 20;
        }

        public override void Reset()
        {
            base.Reset();
            SyringesFullCount = 20;
        }
    }
}