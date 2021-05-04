using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entities;

namespace VaccineCentrum
{
    public class Pool<E> where E : VaccineCentrumEntity
    {
        private List<E> _entitiesList;
        private List<E> _freeEntitiesList;
        private List<E> _busyEntitiesList;
        private List<E> _hungryEntitiesList;

        public List<E> Entities => _entitiesList;
        public List<E> HungryEntities => _hungryEntitiesList;
        public E this[int i] => _entitiesList[i];
        public int Count => _entitiesList.Count;
        public int FreeCount => _freeEntitiesList.Count;
        public int BusyCount => _busyEntitiesList.Count;
        public bool IsBreakTime { get; set; }
        public int OnBreakCount { get; set; }

        public Pool()
        {
            _entitiesList = new List<E>(4);
            _freeEntitiesList = new List<E>(4);
            _busyEntitiesList = new List<E>(4);
            _hungryEntitiesList = new List<E>(4);
            IsBreakTime = false;
            OnBreakCount = 0;
        }

        public Pool(int capacity)
        {
            _entitiesList = new List<E>(capacity);
            _freeEntitiesList = new List<E>(capacity);
            _busyEntitiesList = new List<E>(capacity);
            _hungryEntitiesList = new List<E>(capacity);
            IsBreakTime = false;
            OnBreakCount = 0;
        }

        public void Add(E entity)
        {
            _entitiesList.Add(entity);
            _freeEntitiesList.Add(entity);
            _hungryEntitiesList.Add(entity);
        }

        public E Assign(int indexOfFreeEntity, EntityPatient patient)
        {
            E entity = _freeEntitiesList[indexOfFreeEntity];
            entity.Assign(patient);
            _freeEntitiesList.RemoveAt(indexOfFreeEntity);
            _busyEntitiesList.Add(entity);

            return entity;
        }

        public void Release(E entity)
        {
            entity.Release();
            _freeEntitiesList.Add(entity);
            _busyEntitiesList.Remove(entity);
        }

        public void GetBreak(E entity)
        {
            entity.GetBreak();
            _freeEntitiesList.Remove(entity);
            _hungryEntitiesList.Remove(entity);
            _busyEntitiesList.Add(entity);
        }

        public double AverageUtilization()
        {
            double sum = 0.0;
            Entities.ForEach(e => sum += e.AverageUtilization);
            return sum / Count;
        }

        public double AverageWorkingTime()
        {
            double sum = 0.0;
            Entities.ForEach(e => sum += e.WorkingTime);
            return sum / Count;
        }

        public void Reset()
        {
            Entities.ForEach(e => e.Reset());
            _freeEntitiesList = new List<E>(_entitiesList);
            _busyEntitiesList.Clear();
            _hungryEntitiesList = new List<E>(_entitiesList);
            IsBreakTime = false;
            OnBreakCount = 0;
        }
    }
}
