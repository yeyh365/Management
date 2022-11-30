using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person(1, "FlyElephant", "北京");
            User user = new User();
            user.Id = 20;
            user = ConvertObject(user, person);
            Console.WriteLine("Id:" + user.Id + "Name:" + user.Name + "角色:" + user.Group);
            System.Console.Read();
        }
        public class Person
        {
            public Person(int id, string name, string address)
            {
                this.Id = id;
                this.Name = name;
                this.Address = address;
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
        }
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Group { get; set; }
        }
        public static User ConvertObject(User user, Person person)
        {
            PropertyInfo[] userPro = user.GetType().GetProperties();
            PropertyInfo[] personPro = person.GetType().GetProperties();
            if (userPro.Length > 0 && personPro.Length > 0)
            {
                for (int i = 0; i < userPro.Length; i++)
                {
                    for (int j = 0; j < personPro.Length; j++)
                    {           //判断User的属性是不是的Person中
                       if (userPro[i].Name == personPro[j].Name && userPro[i].PropertyType == personPro[j].PropertyType)
                        {
                            Object value = personPro[j].GetValue(person, null);
                            //将Person中属性的值赋值给User<br>　　　　　　　　　　　　　　　
                            userPro[i].SetValue(user,value , null);
                        }
                    }
                }
            }
            return user;
        }
    }
}
