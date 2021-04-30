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

        public List<E> Entities => _entitiesList;
        public E this[int i] => _entitiesList[i];
        public int Count => _entitiesList.Count;
        public int FreeCount => _freeEntitiesList.Count;
        public int BusyCount => _busyEntitiesList.Count;

        public Pool()
        {
            _entitiesList = new List<E>(4);
            _freeEntitiesList = new List<E>(4);
            _busyEntitiesList = new List<E>(4);
        }

        public Pool(int capacity)
        {
            _entitiesList = new List<E>(capacity);
            _freeEntitiesList = new List<E>(capacity);
            _busyEntitiesList = new List<E>(capacity);
        }

        public void Add(E entity)
        {
            _entitiesList.Add(entity);
            _freeEntitiesList.Add(entity);
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
        }
    }
}
