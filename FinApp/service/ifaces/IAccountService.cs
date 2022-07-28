
namespace FinApp.service.ifaces
{
    public interface IAccountService
    {
        void login(string username, string password);
        void register(string name, string password, string email);
        void logout();
        void rename(string name, string idUser);
        void changePassword(string old_password, string new_password, string userName);
        void removeAccount(string password, string userName);
    }
}
