namespace Test.Abc
{
    public class ChildrenClass
    {
        public string? Name { get; set; }
        public int? Age { get; set; }

    }

    public class ItemClass
    {
        public List<ChildrenClass>? Children { get; set; }
        public string? ScName { get; set; }
    }
    public class TestClass
    {

        public TestClass()
        {
            List<ItemClass> list = new List<ItemClass>{
                new ItemClass
                {
                    ScName = "一班",
                    Children = new List<ChildrenClass>
                    {
                        new ChildrenClass
                        {
                            Name = "小王",
                            Age = 18
                        },
                        new ChildrenClass
                        {
                            Name = "小李",
                            Age = 19
                        }
                    }
                },
                 new ItemClass
                {
                    ScName = "二班",
                    Children = new List<ChildrenClass>
                    {
                        new ChildrenClass
                        {
                            Name = "张三",
                            Age = 18
                        },
                        new ChildrenClass
                        {
                            Name = "李斯",
                            Age = 19
                        }
                    }
                }
            };
            // var sss = list.Where(x =>
            //  {
            //      return x.ScName == "一班";
            //  }).ToArray();
            // System.Console.WriteLine(list[0]);

            var allStudents2 = list.SelectMany(s => s.Children).Select(c => c.Name).ToList();
            var allStudents = list.SelectMany(s => s.Children.Where(d => d.Age > 17), (s, c) => new
            {
                ScName = s.ScName,
                Name = c.Name,
                Age = c.Age
            });
            System.Console.WriteLine("所有学生：");
            foreach (var s in allStudents)
            {
                System.Console.WriteLine($"- {s}");
            }
            System.Console.WriteLine(allStudents2);

            var ages = allStudents.Select(s => s.Age).OrderBy(a => a).OrderByDescending(s => s).ToList();
            System.Console.WriteLine($"所有学生年龄：{string.Join(",", ages)}");


            System.Console.WriteLine($"所有学生年龄数量：{ages.Count()}");
            System.Console.WriteLine($"any{ages.Any(s => s > 18)}");

            var grouped = allStudents.GroupBy(s => s.ScName).ToList();
            System.Console.WriteLine("=============================================================================");

            var distinctAges = ages.Distinct().ToList();
            System.Console.WriteLine($"不同年龄：{string.Join(",", distinctAges)}");
        }
    }
}
