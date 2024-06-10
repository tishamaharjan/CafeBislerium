using System.Text.Json;

namespace CafeBislerium.Data
{
    public class UserDetails
    {
        //Assigning main or username username and password
        public const string mainUsername = "admin";
        public const string mainPassword = "admin";

        public static void DefaultUser() //Creating main or default user
        {
            var users = GetUserDetails().FirstOrDefault(x => x.Role == Roles.Admin);

            if (users == null)
            {
                NewUser(Guid.Empty, mainUsername, mainPassword, Roles.Admin);
            }
        }

        //Method for saving the user details in .json file
        private static void SaveUserDetail(List<User> users)
        {
            string projectPath = Utils.GetAppFolder();//Getting folder path for saving data
            string userFile = Utils.GetUserFile();

            if (!Directory.Exists(projectPath))//If the directory does not exists, create a directory for saving user details
            {
                Directory.CreateDirectory(projectPath);
            }
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(userFile, json);
        }

        //Method for reading saved user details
        public static List<User> GetUserDetails()
        {
            string userFile = Utils.GetUserFile();

            if (!File.Exists(userFile))
            {
                return new List<User>();
            }
            var json = File.ReadAllText(userFile);
            var users = JsonSerializer.Deserialize<List<User>>(json);
            return users;
        }


        //Method for adding new user in users.json file
        public static List<User> NewUser(Guid userID, string username, string password, Roles role)
        {
            List<User> users = GetUserDetails();
            bool usernameExist = users.Any(x => x.Username == username);

            if (usernameExist)
            {
                throw new Exception("Username already in use!");
            }
            users.Add(
                new User
                {
                    Username = username,
                    Password = Utils.HashKey(password),
                    Role = role
                }
                );
            SaveUserDetail(users);
            return users;
        }

        
        //Method for deleting user
        public static List<User> DeleteUser(Guid ID)
        {
            List<User> users = GetUserDetails();
            User user = users.FirstOrDefault(x => x.UserID == ID);
            if (user == null)
            {
                throw new Exception("There are not any users!");
            }
            users.Remove(user);
            SaveUserDetail(users);
            return users;

        }

        //Method for getting user details, verifying password and logging in
        public static User Login(string username, string password)
        {
            List<User> users = GetUserDetails();
            User user = users.FirstOrDefault(x => x.Username == username);
            var errorMessage = "Invalid Input!!!";
            if (user == null)
            {
                throw new Exception(errorMessage);
            }
            bool samePassword = Utils.VerifyPassword(password, user.Password);
            if (!samePassword)
            {
                throw new Exception(errorMessage);
            }
            return user;

        }
    }
}