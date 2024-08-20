using loginDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace loginDb.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void InitNonePayer();
        IEnumerable<T> GetAll<T>() where T : class;
      
        void Edit<T>(T entity) where T : class;
        void AddM(Meeting m);
        void Add<T>(T entity) where T : class;
        void Remove<T>(T entity, string property) where T : class;
        void RemoveMeeting(int userId, int clientId, int num);
        Object GetById(int id, string tableName);
        User GetByUsername(string username);
        IEnumerable<T> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class;
        DateTime GetMeetingDateForClient(int clientId, int number);
        int GetDebtById (int id, bool IsClient, int Uid);
        Task<(int NumOfClients, int NumOfMeetings, int Revenue, int Receivable)> LoadAllAsync(int UserId);
    }
}