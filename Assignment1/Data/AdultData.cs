using System;
using System.Collections.Generic;
using System.Linq;

using Assignment1.Persistence;
using Models;


namespace Assignment1.Data
{
    public class AdultData 
    {
        private IList<Adult> adults;
        private FileContext fileContext;

        public AdultData()
        {
            fileContext = new FileContext();
            adults = fileContext.Adults;
        }
        public IList<Adult> GetAdults() 
        {
            IList<Adult> tmp = new List<Adult>(fileContext.Adults);
            return tmp;
        }

        public void AddAdult(Adult adult)
        {
            int max = adults.Max(adult => adult.Id);
            adult.Id = (++max);
            adults.Add(adult);
            fileContext.SaveChanges();
        }

        public void RemoveAdult(string firstName, string lastName)
        {
            Adult toRemove = adults.First(t => t.FirstName.Equals(firstName) && t.LastName.Equals(lastName));
            adults.Remove(toRemove);
            fileContext.SaveChanges();
        }

        public void Update(Adult adult)
        {
            Adult toUpdate = adults.First(t => t.FirstName.Equals(adult.FirstName) && t.LastName.Equals(adult.LastName));
            toUpdate.JobTitle.JobTitle = adult.JobTitle.JobTitle;
            toUpdate.Age = adult.Age;
            toUpdate.Height = adult.Height;
            toUpdate.Id = adult.Id;
            toUpdate.Sex = adult.Sex;
            toUpdate.Weight = adult.Weight;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.JobTitle.Salary = adult.JobTitle.Salary;
            fileContext.SaveChanges();
        }

        public Adult Get(int id)
        {
            return adults.FirstOrDefault(t => t.Id == id);
        }
        
    }
}