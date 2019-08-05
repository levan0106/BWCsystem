using BWC.Model;

namespace BWC.Core.Interfaces
{
    public interface IUserToken : IRepository<UserToken>
    {
        bool ValiddateRefreshToken(UserToken entity);
    }
}
