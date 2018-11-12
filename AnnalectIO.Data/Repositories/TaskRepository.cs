using AnnalectIO.Data.Infrastructure;
using AnnalectIO.DomainModel.Task;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnalectIO.Data.Repositories
{
    public class TaskRepository : ITaskRepository<TaskModel>
    {
        private readonly IConnectionFactory _connectionFactory = null;

        public TaskRepository(IConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            using (var connection = _connectionFactory.GetConnection) {
                try
                {
                    var sqlQuery = @"SELECT * FROM [dbo].[Tasks]";
                    var result = await SqlMapper.QueryAsync<TaskModel>(connection, sqlQuery).ConfigureAwait(false);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public async Task<TaskModel> GetById(Guid id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var sqlQuery = @"SELECT * FROM [dbo].[Tasks] where Id = @id";
                    var result = await SqlMapper.QueryAsync<TaskModel>(connection, sqlQuery, new { id = id }).ConfigureAwait(false);
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public async Task ExecuteSP(TaskModel task)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "USP_UpdateTask";
                    var param = new DynamicParameters();
                    param.Add("@Id", Guid.NewGuid());
                    param.Add("@Name", task.Name);
                    param.Add("@Description", task.Description);
                    param.Add("@DateCreated", DateTime.UtcNow);

                    await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, query, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public async Task Create(TaskModel task)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                using (IDbTransaction tran = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlQuery = "INSERT INTO Tasks (Id, Name, Description, DateCreated) VALUES (@Id, @Name, @Description, @DateCreated)";
                        object param = new
                        {
                            Id = Guid.NewGuid(),
                            Name = task.Name,
                            Description = task.Description,
                            DateCreated = DateTime.UtcNow
                        };
                        var result = await SqlMapper.QueryAsync<TaskModel>(connection, sqlQuery, param, transaction: tran).ConfigureAwait(false);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        public async Task Edit(TaskModel task)
        {
            IDbConnection connection = _connectionFactory.GetConnection;
            using (IDbTransaction tran = connection.BeginTransaction())
            {
                try
                {
                    var sqlQuery = "UPDATE [dbo].[Tasks] SET Name = @Name, Description = @Description, DateUpdated = @DateUpdated WHERE Id = @Id";
                    var result = await SqlMapper.QueryAsync<TaskModel>(connection, sqlQuery, task, transaction: tran).ConfigureAwait(false);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public async Task Delete(Guid id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var sqlQuery = @"DELETE * FROM Tasks where Id = @id";
                    await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, sqlQuery, new { id = id }).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
