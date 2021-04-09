using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace BusinessLogic
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection connection;

        public UserRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Gets the user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(long id)
        {
            return connection.QueryFirstOrDefault<User>("select * from user where id = @id", new { id });
        }

        /// <summary>
        /// Queries the user table for user by email.  Ensures that email is lowercase before query
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return connection.QueryFirstOrDefault<User>("select * from user where email = @email", new { email = email.ToLower() });
        }

        public User Save(User user)
        {
            if (user.Id != 0)
                connection.Update(new UserDao(user));
            else
                user.Id = connection.Insert(new UserDao(user));
            return user;
        }
    }

}
