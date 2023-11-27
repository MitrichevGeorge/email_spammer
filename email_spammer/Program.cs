/*switch (key.Key)
            {
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    break;
            }
*/

using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace email_spammer
{
    class Program
    {
        static void Main(string[] args)
        {
            string smtp = "smtp.mail.ru";
            ConsoleKeyInfo key;
            Console.WriteLine(@"");
            Console.WriteLine(@"   ____    ╔════╕   ____");
            Console.WriteLine(@"  /    \   ║       /    \");
            Console.WriteLine(@" │         ║       │     │");
            Console.WriteLine(@" │         ╠════╡  │     │");
            Console.WriteLine(@" │    ═╕   ║       │     │");
            Console.WriteLine(@" │     │   ║       │     │");
            Console.WriteLine(@"  \___/    ╚════╛   \___/");
            Console.WriteLine(@"");
            Console.WriteLine(" ".Multiply((Console.BufferWidth-22)/2) + "Geo email spammer v1.0");
            while (true)
            {
                Console.WriteLine("Необходимо зарегистрероваться. Выберите ваш вид ящика:\n1.почта mail.ru\n2.другая почта");
                Console.Write("нажмите число:");
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.D1) { Console.WriteLine(""); break; }
                if (key.Key == ConsoleKey.D2) {
                    Console.WriteLine("\nВведите имя или IP-адрес вашего smtp сервера");
                    smtp = Console.ReadLine();
                    break;
                }
                Console.WriteLine("\nНеправильный ввод");
            }
            var smtpServer = new SmtpClient(smtp);
            if(key.Key == ConsoleKey.D1)
            {
                smtpServer.Port = 587;
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Введите порт (число)");
                    try
                    {
                        smtpServer.Port = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Вы ввели что-то не то. Код ошибки:" + e);
                    }
                }
            }
            string login, pass;
            while (true)
            {
                while (true)
                {
                    Console.Write("Введите имя почтового ящика(ваш логин):");
                    login = Console.ReadLine();
                    if(login.Contains('@') && login.Contains('.'))
                    {
                        break;
                    }
                    Console.WriteLine("имя ящика должно быть вида <str>@<str>.<str>. Например: example@mail.ru");
                }
                Console.Write("Введите пароль(используйте пароли для внешних приложений):");
                pass = Console.ReadLine();
                try
                {
                    smtpServer.Credentials = new NetworkCredential(login, pass);
                    break;
                }
                catch(Exception e)
                {
                    Console.Write("Не удалось зарегистрироваться. Проверьте ");
                    Console.WriteLine("разрешён ли доступ внешних приложений к вашей почте.");
                    Console.WriteLine("код ошибки:" + e);
                }
            }
            smtpServer.EnableSsl = true;
            Console.WriteLine("Успешная регистрация.");
            while (true)
            {
                while (true)
                {
                    Console.WriteLine("Выберите действие:\n1.запуск спама по адресу\n2.выход");
                    Console.Write("нажмите число:");
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.D2) { Console.WriteLine(""); break; }
                    Console.WriteLine("\nНеправильный ввод");
                }
                if (key.Key == ConsoleKey.D2)
                {
                    break;
                }
                else
                {
                    int count;
                    var mail = new MailMessage();

                    mail.From = new MailAddress(login);
                    Console.Write("Введите адрес кому:");
                    mail.To.Add(Console.ReadLine());
                    Console.Write("Введите тему письма:");
                    mail.Subject = Console.ReadLine();
                    Console.Write("Введите тело письма:");
                    mail.Body = Console.ReadLine();
                    while (true)
                    {
                        Console.Write("Введите количество писем:");
                        try
                        {
                            count = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Вы ввели что - то не то. Введите число.");
                        }
                    }
                    if(count > 0)
                    {
                        Console.WriteLine("Запущено");
                        for(int i = 0; i < count; i++)
                        {
                            smtpServer.Send(mail);
                            Console.WriteLine("отправлено " + (i+1).ToString() + " письмо");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели невозможное кол-во писем");
                    }
                }
            }
        }
    }

    public static class str
    {
        public static string Multiply(this string source, int multiplier)
        {
            StringBuilder sb = new StringBuilder(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(source);
            }
            return sb.ToString();
        }
    }
}
