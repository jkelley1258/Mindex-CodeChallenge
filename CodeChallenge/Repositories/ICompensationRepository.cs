using CodeChallenge.Data;
using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Add(Compensation compensation);

        Compensation GetById(string id);

        Compensation GetByEmployeeId(string id);

        public Task SaveAsync();
    }
}