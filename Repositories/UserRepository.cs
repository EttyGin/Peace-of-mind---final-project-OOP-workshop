using loginDb.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace loginDb.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void AddM(Meeting m)
        {
            using (var db = new POMdbEntities())
            {

                m.ClientId = 3257;
                db.Meetings.Add(m);
                db.SaveChanges();
            }


        }

        public void InitNonePayer()
        {
            using (var db = new POMdbEntities())
            {
                Payer none = new Payer { Id = 0, Pname = " -", ContactName = "", ContactEmail = "", TotalPayment = 0 };
                string name = none.Pname;
                var existingPayer = db.Payers.FirstOrDefault(pyr => pyr.Pname == name);
                if (existingPayer == null)
                {
                    db.Payers.Add(none);
                    db.SaveChanges();
                }
            }
        }
        
        public void Add<T>(T entity) where T : class
        {
            using (var db = new POMdbEntities())
            {
                var dbSet = db.Set<T>();
                dbSet.Add(entity);
                db.SaveChanges();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;

                validUser = command.ExecuteScalar() != null;
            }
            return validUser;
        }

        public void Edit<T>(T entity) where T : class
        {
            using (var db = new POMdbEntities())
            {
                var dbSet = db.Set<T>();
                dbSet.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            using (var db = new POMdbEntities())
            {
                return db.Set<T>().ToList();
            }
        }
        
        public Object GetById(int id, string tableName)
        {
            Object ans = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"select *from [{tableName}] where Id=@id";
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        switch (tableName)
                        {
                            case "Payer": {
                                    ans = new Payer()
                                    {
                                        Id = int.Parse(reader[0].ToString()),
                                        Pname = reader[1].ToString(),
                                        ContactName = reader[2].ToString(),
                                        ContactEmail = reader[3].ToString(),
                                        TotalPayment = short.Parse(reader[4].ToString()),
                                    };
                                    break;
                                }
                            case "Client":
                                {
                                    ans = new Client()
                                    {
                                        Id = int.Parse(reader[0].ToString()),
                                        Cname = reader[1].ToString(),
                                        BirthDate = Convert.ToDateTime(reader[2]),
                                        Phone = reader[3].ToString(),
                                        Email = reader[4].ToString(),
                                        PayerId = short.Parse(reader[5].ToString()),
                                    };
                                    break;
                                }
                            case "User":
                                {
                                    ans = new User()
                                    {
                                        Id = int.Parse(reader[0].ToString()),
                                        Username = reader[1].ToString(),
                                        Password = string.Empty,
                                        Name = reader[3].ToString(),
                                        LastName = reader[4].ToString(),
                                        Email = reader[5].ToString(),
                                        Price = int.Parse(reader[6].ToString())
                                    };
                                    break;
                                }
                            default: {
                                ans = null;
                                break;
                            }
                        }
                        
                    }
                }
            }
            return ans;
        }
        public User GetByUsername(string username)
        {
            User user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                            Price = int.Parse(reader[6].ToString())
                        };
                    }
                }
            }
            return user;
        }

        public IEnumerable<T> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var db = new POMdbEntities())
            {
                return db.Set<T>().Where(predicate).ToList();
            }
        }

        public int GetDebtById(int id, bool isClient, int Uid)
        {
            using (var db = new POMdbEntities())
            {
                int UserPrice = db.Users.Where(u =>  u.Id == Uid).FirstOrDefault().Price;
                if (!isClient) //payer
                {
                    int unpaidMeetingsCount = db.Meetings
                                  .Where(m => m.Client.PayerId == id && (m.Status == Status.unpaid || m.Status == Status.clientPaid))
                                  .Count(); 
                    if (unpaidMeetingsCount > 0)
                    {
                        var payer = db.Payers.FirstOrDefault(p => p.Id == id);
                        return unpaidMeetingsCount * payer.TotalPayment;
                    }
                }

                else //Client
                {
                    int unpaidMeetingsCount = db.Meetings
                                            .Where(m => m.ClientId == id && (m.Status == Status.unpaid || m.Status == Status.payerPaid))
                                            .Count();
                    if (unpaidMeetingsCount > 0)
                    {
                        var client = db.Clients.FirstOrDefault(c => c.Id == id);
                        var payer = db.Payers.FirstOrDefault(p => p.Id == client.PayerId);

                        int price = UserPrice - payer.TotalPayment;
                        return unpaidMeetingsCount * price;
                    }
                }
                return 0;                
            }
        }

        public void Remove<TEntity>(TEntity entity, string property) where TEntity : class
        {
            using (var db = new POMdbEntities())
            {

                {
                    var dbSet = db.Set<TEntity>();
                    var entityKey = db.Entry(entity).Property(property).CurrentValue;
                    var existingEntity = dbSet.Find(entityKey);
                    if (existingEntity != null)
                    {
                        dbSet.Remove(existingEntity);
                        db.SaveChanges();
                    }
                }
            }
        }

        public void RemoveMeeting(int userId, int clientId, int number)
        {
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand("DELETE FROM Meeting WHERE UserId = @userId AND ClientID = @clientId AND Number = @number", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@ClientID", clientId);
                command.Parameters.AddWithValue("@Number", number);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DateTime GetMeetingDateForClient(int clientId, int number)
        {
            DateTime lastAppointmentDate = DateTime.MinValue;

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT TOP 1 * FROM Meeting WHERE ClientID = @clientID AND Number = @number ORDER BY Date DESC", connection))
            {
                command.Parameters.AddWithValue("@clientID", clientId);
                command.Parameters.AddWithValue("@Number", number);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lastAppointmentDate = (DateTime)reader["Date"];
                    }
                }
            }

            return lastAppointmentDate;
        }

        public async Task<(int NumOfClients, int NumOfMeetings, int Revenue, int Receivable)> LoadAllAsync(int UserId)
        {
            using (var db = new POMdbEntities())
            {
                int UserPrice = db.Users.Where(u => u.Id == UserId).FirstOrDefault().Price;

                int NumOfClients = await db.Clients.CountAsync();
                int NumOfMeetings = await db.Meetings.CountAsync();
                int Paid = 0, UnPaid = 0;
                var lstMeetings = await db.Meetings
                    .Where(m => m.Status != Status.planned && m.UserId == UserId).ToListAsync();

                foreach (Meeting meeting in lstMeetings)
                {
                    switch (meeting.Status)
                    {
                        case Status.unpaid:
                            {
                                UnPaid += UserPrice;
                                break;
                            }
                        case Status.clientPaid:
                            {
                                int PayerPart = meeting.Client.Payer.TotalPayment;
                                UnPaid += PayerPart;
                                Paid += UserPrice - PayerPart;
                                break;
                            }
                        case Status.payerPaid:
                            {
                                int PayerPart = meeting.Client.Payer.TotalPayment;
                                Paid += PayerPart;
                                UnPaid += UserPrice - PayerPart;
                                break;
                            }
                        case Status.paid:
                            {
                                Paid += UserPrice;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

             return (NumOfClients, NumOfMeetings, Paid, UnPaid);
            }
        }

    }
}