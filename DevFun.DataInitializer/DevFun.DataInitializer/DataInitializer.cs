using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFun.DataInitializer.Dtos;

namespace DevFun.DataInitializer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "ok for sample")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "ok for sample")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "ok for sample")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "ok for sample")]
    public class DataInitializer
    {
        private readonly DevFunService service;

        public DataInitializer(DevFunService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task InitializeData()
        {
            Console.WriteLine("Start initializing test data...");

            await InitializeCategories().ConfigureAwait(false);

            await InitializeJokes().ConfigureAwait(false);

            Console.WriteLine("Initializing test data finished.");
        }

        private async Task InitializeCategories()
        {
            Console.WriteLine("Check for category data");

            var categories = await service.GetCategories().ConfigureAwait(false);
            if (!categories.Any())
            {
                Console.WriteLine("No data found, initialize category data");

                try
                {
                    await service.AddCategory(
                        new CategoryDto()
                        {
                            //Id = 0,
                            Name = "General"
                        }).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating category: {ex.ToString()}");
                }

                try
                {
                    await service.AddCategory(
                        new CategoryDto()
                        {
                            //Id = 1,
                            Name = ".NET"
                        }).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating category: {ex.ToString()}");
                }

                try
                {
                    await service.AddCategory(
                        new CategoryDto()
                        {
                            //Id = 2,
                            Name = "Java"
                        }).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating category: {ex.ToString()}");
                }
            }

            Console.WriteLine("category data initialized");
        }

        private async Task InitializeJokes()
        {
            var categories = await service.GetCategories().ConfigureAwait(false);
            var generalId = categories.First(c => c.Name.Equals("General", StringComparison.OrdinalIgnoreCase)).Id;
            var netId = categories.First(c => c.Name.Equals(".NET", StringComparison.OrdinalIgnoreCase)).Id;
            var javaId = categories.First(c => c.Name.Equals("Java", StringComparison.OrdinalIgnoreCase)).Id;

            Console.WriteLine("Check for jokes data");

            var existingJokes = await service.GetJokes().ConfigureAwait(false);
            if (!existingJokes.Any())
            {
                Console.WriteLine("No data found, initialize jokes data");
                var jokes = new List<JokeDto>()
                {
                    new JokeDto() { Text = @"Programmer\r\nA machine that turns coffee into code.", CategoryId=generalId },
                    new JokeDto() { Text = @"Programmer\r\nA person who fixed a problem that you don't know your have, in a way you don't understand.", CategoryId=generalId },
                    new JokeDto() { Text = @"Algorithm\r\nWord used by programmers when... they do not want to explain what they did.", CategoryId=generalId },
                    new JokeDto() { Text = @"Q: What's the object-oriented way to become wealthy?\r\nA: Inheritance", CategoryId=generalId },
                    new JokeDto() { Text = @"Q: What's the programmer's favourite hangout place?\r\nA: Foo Bar", CategoryId=generalId },
                    new JokeDto() { Text = @"Q: How to you tell an introverted computer scientist from an extroverted computer scientist?\r\nA: An extroverted computer scientist looks at your shoes when he talks to you.", CategoryId=generalId },
                    new JokeDto() { Text = @"Q: Why do Java programmers wear glasses?\r\nA: Because they don't C#", CategoryId=netId },
                    new JokeDto() { Text = @"Have you heard about the new Cray super computer?\r\nIt’s so fast, it executes an infinite loop in 6 seconds.", CategoryId=generalId },
                    new JokeDto() { Text = @"There are three kinds of lies: Lies, damned lies, and benchmarks.", CategoryId=generalId },
                    new JokeDto() { Text = @"“Knock, knock“.\r\n“Who’s there ?“\r\nvery long pause….\r\n“Java.“" , CategoryId=javaId}
                };

                foreach (var joke in jokes)
                {
                    try
                    {
                        await service.AddJoke(joke).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating category: {ex.ToString()}");
                    }
                }
            }

            Console.WriteLine("jokes data initialized");
        }
    }
}