namespace Crm
{
    public class UsersController
    {
        protected virtual async Task<int> Get()
        {
            return 1;
        }
    }

    public class User : UsersController
    {
        public enum PersonType
        {
            Student,
            Teacher,
            Parent
        }
        public int Age { get; set; } = 18;
        public string Name { get; set; } = "James";
        public string Email { get; set; } = "james@email.com";

        public async Task<object> testFn()
        {
            string ss = "Teacher";
            if (ss.Contains(PersonType.Student.ToString()))
            {
                await Get();
            }

            return new { Name = "James", Age = 18 };
        }
    }
}

