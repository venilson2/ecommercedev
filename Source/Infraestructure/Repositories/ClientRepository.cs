using System.Data.SqlClient;
using System.Data;
using Ecommercedev.Source.Core.Entites;
using Ecommercedev.Source.Core.Interfaces.Repositories;
using Dapper;

namespace Ecommercedev.Source.Infraestructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ClientEntity>> FindAllAsync()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string query = @"SELECT * FROM Client";
            var clients = await db.QueryAsync<ClientEntity>(query);
            return clients;
        }

        public async Task<ClientEntity> CreateAsync(ClientEntity entity)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string query = @"
                INSERT INTO 
                    Client (Id, Name, Email, LogoUrl) 
                    OUTPUT INSERTED.*
                    VALUES (@Id, @Name, @Email, @LogoUrl);";

            return await db.QueryFirstOrDefaultAsync<ClientEntity>(query, entity);
        }

        public async Task<ClientEntity> FindByIdAsync(Guid id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string query = @"SELECT * FROM Client WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<ClientEntity>(query, new { Id = id });
        }

        public async Task<ClientEntity> UpdateAsync(Guid id, ClientEntity entity)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string query = @"
                                UPDATE 
                                    Client SET 
                                    Name = @Name, 
                                    Email = @Email, 
                                    Logo = @Logo,
                                    UpdatedAt = @UpdatedAt 
                                OUTPUT INSERTED.*
                                WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<ClientEntity>(query, entity);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string query = @"DELETE FROM Client WHERE Id = @Id";
            return await db.ExecuteAsync(query, new { Id = id });
        }
    }
}
