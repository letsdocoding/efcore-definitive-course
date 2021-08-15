using System;
using System.Linq;

namespace EfCoreCompleteCourse.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new UserDbContext();
            dbContext.Users.Add(new User()
                {FirstName = "vineet-"+DateTime.Now.ToLongTimeString(), LastName = "yadav", DateOfBirth = new DateTime(2000, 1, 1)});
            dbContext.SaveChanges();

            var allUsers = dbContext.Users.ToList();
            foreach (var user in allUsers)
            {
                Console.WriteLine($"User Found: Id: {user.Id}, Name:  {user.FirstName}");
            }
            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
