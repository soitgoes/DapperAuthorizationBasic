namespace BusinessLogic
{
    public interface IUserRepository
    {
        User Get(long id);
        User GetByEmail(string email);
        User Save(User user);
    }
}