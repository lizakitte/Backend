namespace BlazorApp
{
    public class UserService : IUserService
    {
        private Dictionary<string, string> users = new Dictionary<string, string>();

        public void Add(string connectionId, string username)
        {
            users.Add(connectionId, username);
        }

        public IEnumerable<(string ConnectionId, string Username)> GetAll()
        {
            return users.Select(u => (u.Key, u.Value));
        }

        public string GetConnectionIdByName(string username)
        {
            foreach (var (id, name) in users)
            {
                if(name == username)
                {
                    return id;
                }
            }
            return "";
        }

        public void RemoveByName(string username)
        {
            foreach (var (id, name) in users)
            {
                if (name == username)
                {
                    users.Remove(id);
                }
            }
        }
    }
}
