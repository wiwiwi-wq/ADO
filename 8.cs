//using System.Data;
//using System.Diagnostics;
//using System.Text;
//using System.Text.RegularExpressions;

//1
//using System;
//using System.Data;
//using System.Linq;

//public class ShopDataSet : DataSet
//{
//    private UsersTable users;

//    public ShopDataSet()
//    {
//        users = new UsersTable();
//        Tables.Add(users);
//    }

//    public UsersTable Users => users;
//}

//public class UsersTable : DataTable
//{
//    public UsersTable()
//    {
//        TableName = "Users";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Name", typeof(string));
//        Columns.Add("Email", typeof(string));
//        Columns.Add("Age", typeof(int));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public UsersRow this[int index] => (UsersRow)Rows[index];

//    public UsersRow AddRow(int id, string name, string email, int age)
//    {
//        UsersRow row = (UsersRow)NewRow();
//        row.Id = id;
//        row.Name = name;
//        row.Email = email;
//        row.Age = age;
//        Rows.Add(row);
//        return row;
//    }

//    public UsersRow[] FindByAgeGreaterThan(int age) =>
//        Rows.Cast<UsersRow>().Where(r => r.Age > age).ToArray();

//    protected override Type GetRowType() => typeof(UsersRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new UsersRow(builder);
//}

//public class UsersRow : DataRow
//{
//    internal UsersRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Name
//    {
//        get => this["Name"] == DBNull.Value ? null : (string)this["Name"];
//        set => this["Name"] = value ?? (object)DBNull.Value;
//    }

//    public string Email
//    {
//        get => this["Email"] == DBNull.Value ? null : (string)this["Email"];
//        set => this["Email"] = value ?? (object)DBNull.Value;
//    }

//    public int Age
//    {
//        get => (int)this["Age"];
//        set
//        {
//            if (value < 0) throw new ArgumentException("Возраст не может быть отрицательным");
//            this["Age"] = value;
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        try
//        {
//            var ds = new ShopDataSet();

//            ds.Users.AddRow(1, "Иван Иванов", "ivan@example.com", 25);
//            ds.Users.AddRow(2, "Мария Петрова", "maria@example.com", 30);
//            ds.Users.AddRow(3, "Пётр Сидоров", "petr@example.com", 17);
//            ds.Users.AddRow(4, "Анна Кузнецова", "anna@example.com", 35);

//            Console.WriteLine("Все пользователи:");
//            foreach (UsersRow row in ds.Users.Rows)
//            {
//                Console.WriteLine($"{row.Id} | {row.Name} | {row.Email} | {row.Age}");
//            }

//            Console.WriteLine("\nПользователи старше 25 лет:");
//            var adults = ds.Users.FindByAgeGreaterThan(25);
//            foreach (UsersRow row in adults)
//            {
//                Console.WriteLine($"{row.Id} | {row.Name} | {row.Age}");
//            }

//            Console.WriteLine($"\nВсего строк: {ds.Users.Rows.Count}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }
//}

//2
//using System;

//namespace TypedDataSetDemo
//{
//    class Program
//    {
//        static void Main()
//        {
//            try
//            {
//                var shop = new ShopDataSet();

//                var p1 = shop.Products.NewProductsRow();
//                p1.Name = "Ноутбук Lenovo";
//                p1.Price = 89990.00m;
//                p1.Category = "Электроника";

//                var p2 = shop.Products.NewProductsRow();
//                p2.Name = "Кофе Арабика";
//                p2.Price = 890.50m;
//                p2.Category = "Продукты";

//                shop.Products.AddProductsRow(p1);
//                shop.Products.AddProductsRow(p2);

//                shop.Products.AddProductsRow("Мышь Logitech", 2490.00m, "Периферия");
//                shop.Products.AddProductsRow("Монитор 27\"", 35990.00m, "Электроника");

//                Console.WriteLine("Продукты в магазине:");
//                Console.WriteLine(new string('-', 70));
//                Console.WriteLine("| {0,-5} | {1,-25} | {2,-12} | {3,-15} |", "Id", "Название", "Цена", "Категория");
//                Console.WriteLine(new string('-', 70));

//                foreach (ShopDataSet.ProductsRow row in shop.Products.Rows)
//                {
//                    Console.WriteLine("| {0,-5} | {1,-25} | {2,-12:C} | {3,-15} |",
//                        row.Id, row.Name, row.Price, row.Category);
//                }

//                Console.WriteLine(new string('-', 70));
//                Console.WriteLine($"Всего товаров: {shop.Products.Count}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Ошибка: " + ex.Message);
//            }

//            Console.WriteLine("\nНажмите любую клавишу...");
//            Console.ReadKey();
//        }
//    }
//}

//3
//using System;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;

//public class TypedDataSet : DataSet
//{
//    private TypedUsersTable users;

//    public TypedDataSet()
//    {
//        users = new TypedUsersTable();
//        Tables.Add(users);
//    }

//    public TypedUsersTable Users => users;
//}

//public class TypedUsersTable : DataTable
//{
//    public TypedUsersTable()
//    {
//        TableName = "Users";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Name", typeof(string));
//        Columns.Add("Email", typeof(string));
//        Columns.Add("Age", typeof(int));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public TypedUsersRow this[int index] => (TypedUsersRow)Rows[index];

//    public TypedUsersRow AddRow(int id, string name, string email, int age)
//    {
//        TypedUsersRow row = (TypedUsersRow)NewRow();
//        row.Id = id;
//        row.Name = name;
//        row.Email = email;
//        row.Age = age;
//        Rows.Add(row);
//        return row;
//    }

//    public TypedUsersRow[] FindByAgeGreaterThan(int age) =>
//        Rows.Cast<TypedUsersRow>().Where(r => r.Age > age).ToArray();

//    protected override Type GetRowType() => typeof(TypedUsersRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new TypedUsersRow(builder);
//}

//public class TypedUsersRow : DataRow
//{
//    internal TypedUsersRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Name
//    {
//        get => this["Name"] == DBNull.Value ? null : (string)this["Name"];
//        set => this["Name"] = value ?? (object)DBNull.Value;
//    }

//    public string Email
//    {
//        get => this["Email"] == DBNull.Value ? null : (string)this["Email"];
//        set => this["Email"] = value ?? (object)DBNull.Value;
//    }

//    public int Age
//    {
//        get => (int)this["Age"];
//        set
//        {
//            if (value < 0) throw new ArgumentException("Возраст не может быть отрицательным");
//            this["Age"] = value;
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        try
//        {
//            var typedDs = new TypedDataSet();
//            var untypedDs = new DataSet();

//            var untypedUsers = new DataTable("Users");
//            untypedUsers.Columns.Add("Id", typeof(int));
//            untypedUsers.Columns.Add("Name", typeof(string));
//            untypedUsers.Columns.Add("Email", typeof(string));
//            untypedUsers.Columns.Add("Age", typeof(int));
//            untypedUsers.PrimaryKey = new DataColumn[] { untypedUsers.Columns["Id"] };
//            untypedDs.Tables.Add(untypedUsers);

//            typedDs.Users.AddRow(1, "Иван Иванов", "ivan@example.com", 25);
//            typedDs.Users.AddRow(2, "Мария Петрова", "maria@example.com", 30);
//            typedDs.Users.AddRow(3, "Пётр Сидоров", "petr@example.com", 17);
//            typedDs.Users.AddRow(4, "Анна Кузнецова", "anna@example.com", 35);

//            untypedUsers.Rows.Add(1, "Иван Иванов", "ivan@example.com", 25);
//            untypedUsers.Rows.Add(2, "Мария Петрова", "maria@example.com", 30);
//            untypedUsers.Rows.Add(3, "Пётр Сидоров", "petr@example.com", 17);
//            untypedUsers.Rows.Add(4, "Анна Кузнецова", "anna@example.com", 35);

//            Console.WriteLine("Демонстрация доступа в типизированном DataSet (IntelliSense, типы):");
//            foreach (TypedUsersRow row in typedDs.Users.Rows)
//            {
//                Console.WriteLine($"{row.Id} | {row.Name} | {row.Email} | {row.Age}");
//            }

//            Console.WriteLine("\nДемонстрация доступа в нетипизированном DataSet (строки, object):");
//            foreach (DataRow row in untypedUsers.Rows)
//            {
//                Console.WriteLine($"{row["Id"]} | {row["Name"]} | {row["Email"]} | {row["Age"]}");
//            }

//            Console.WriteLine("\nДемонстрация ошибок типов:");
//            try
//            {
//                untypedUsers.Rows[0]["Age"] = "тридцать";
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("В нетипизированном: Runtime ошибка - " + ex.Message);
//            }

//            try
//            {
//                typedDs.Users[0].Age = -5;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("В типизированном: Исключение в setter - " + ex.Message);
//            }

//            try
//            {
//                Console.WriteLine(untypedUsers.Rows[0]["NonExistingColumn"]);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("В нетипизированном: Runtime ошибка колонки - " + ex.Message);
//            }

//            int iterations = 1000000;
//            Stopwatch sw = new Stopwatch();

//            sw.Start();
//            for (int i = 0; i < iterations; i++)
//            {
//                int age = typedDs.Users[0].Age;
//            }
//            sw.Stop();
//            long typedTime = sw.ElapsedMilliseconds;

//            sw.Reset();
//            sw.Start();
//            for (int i = 0; i < iterations; i++)
//            {
//                int age = (int)untypedUsers.Rows[0]["Age"];
//            }
//            sw.Stop();
//            long untypedTime = sw.ElapsedMilliseconds;

//            Console.WriteLine("\nИзмерение производительности (1 млн итераций чтения):");
//            Console.WriteLine($"Типизированный: {typedTime} мс");
//            Console.WriteLine($"Нетипизированный: {untypedTime} мс");
//            Console.WriteLine($"Разница: {(untypedTime > typedTime ? "Нетипизированный медленнее на " + (untypedTime - typedTime) + " мс" : "Типизированный медленнее")}");

//            Console.WriteLine("\nОтчёт о сравнении:");
//            Console.WriteLine("Преимущества типизированного DataSet:");
//            Console.WriteLine("- Сильная типизация: IntelliSense, ошибки на компиляции.");
//            Console.WriteLine("- Безопасность: Неверные типы/колонки ловятся рано.");
//            Console.WriteLine("- Удобство: Понятные свойства (row.Name) vs row[\"Name\"].");
//            Console.WriteLine("- Производительность: Быстрее доступ (нет поиска по строке).");
//            Console.WriteLine("Недостатки типизированного DataSet:");
//            Console.WriteLine("- Больше кода/настройки (или использование конструктора VS).");
//            Console.WriteLine("- Менее гибкий для динамических схем.");
//            Console.WriteLine("Преимущества нетипизированного:");
//            Console.WriteLine("- Простота: Быстро создать, гибкий.");
//            Console.WriteLine("Недостатки нетипизированного:");
//            Console.WriteLine("- Runtime ошибки, нет IntelliSense, медленнее.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Общая ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }
//}

//4
//using System;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;

//public class EmployeesDataSet : DataSet
//{
//    private EmployeesTable employees;

//    public EmployeesDataSet()
//    {
//        employees = new EmployeesTable();
//        Tables.Add(employees);
//    }

//    public EmployeesTable Employees => employees;
//}

//public class EmployeesTable : DataTable
//{
//    public EmployeesTable()
//    {
//        TableName = "Employees";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Name", typeof(string));
//        Columns.Add("Position", typeof(string));
//        Columns.Add("Salary", typeof(decimal));
//        Columns.Add("Department", typeof(string));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public EmployeesRow this[int index] => (EmployeesRow)Rows[index];

//    public EmployeesRow AddRow(int id, string name, string position, decimal salary, string department)
//    {
//        EmployeesRow row = (EmployeesRow)NewRow();
//        row.Id = id;
//        row.Name = name;
//        row.Position = position;
//        row.Salary = salary;
//        row.Department = department;
//        Rows.Add(row);
//        return row;
//    }

//    public EmployeesRow[] FindBySalaryGreaterThan(decimal minSalary) =>
//        Rows.Cast<EmployeesRow>().Where(r => r.Salary > minSalary).ToArray();

//    protected override Type GetRowType() => typeof(EmployeesRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new EmployeesRow(builder);
//}

//public class EmployeesRow : DataRow
//{
//    internal EmployeesRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Name
//    {
//        get => this["Name"] == DBNull.Value ? null : (string)this["Name"];
//        set => this["Name"] = value ?? (object)DBNull.Value;
//    }

//    public string Position
//    {
//        get => this["Position"] == DBNull.Value ? null : (string)this["Position"];
//        set => this["Position"] = value ?? (object)DBNull.Value;
//    }

//    public decimal Salary
//    {
//        get => (decimal)this["Salary"];
//        set
//        {
//            if (value < 0) throw new ArgumentException("Зарплата не может быть отрицательной");
//            this["Salary"] = value;
//        }
//    }

//    public string Department
//    {
//        get => this["Department"] == DBNull.Value ? null : (string)this["Department"];
//        set => this["Department"] = value ?? (object)DBNull.Value;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        try
//        {
//            var typedDs = new EmployeesDataSet();

//            typedDs.Employees.AddRow(1, "Иван Иванов", "Разработчик", 150000m, "IT");
//            typedDs.Employees.AddRow(2, "Мария Петрова", "Менеджер", 120000m, "Продажи");
//            typedDs.Employees.AddRow(3, "Пётр Сидоров", "Аналитик", 130000m, "Аналитика");
//            typedDs.Employees.AddRow(4, "Анна Кузнецова", "Дизайнер", 110000m, "Дизайн");

//            Console.WriteLine("Сотрудники:");
//            foreach (EmployeesRow row in typedDs.Employees.Rows)
//            {
//                Console.WriteLine($"{row.Id} | {row.Name} | {row.Position} | {row.Salary:C} | {row.Department}");
//            }

//            Console.WriteLine("\nСотрудники с зарплатой > 120000:");
//            var highSalary = typedDs.Employees.FindBySalaryGreaterThan(120000m);
//            foreach (EmployeesRow row in highSalary)
//            {
//                Console.WriteLine($"{row.Name} | {row.Salary:C}");
//            }

//            DataSet untypedDs = new DataSet();
//            DataTable untypedEmployees = new DataTable("Employees");
//            untypedEmployees.Columns.Add("Id", typeof(int));
//            untypedEmployees.Columns.Add("Name", typeof(string));
//            untypedEmployees.Columns.Add("Position", typeof(string));
//            untypedEmployees.Columns.Add("Salary", typeof(decimal));
//            untypedEmployees.Columns.Add("Department", typeof(string));
//            untypedDs.Tables.Add(untypedEmployees);

//            untypedEmployees.Rows.Add(1, "Иван Иванов", "Разработчик", 150000m, "IT");

//            Console.WriteLine("\nПримеры ошибок в нетипизированном DataSet (runtime):");
//            try
//            {
//                untypedEmployees.Rows[0]["Salary"] = "сто тысяч";
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Ошибка присвоения неверного типа: " + ex.Message);
//            }

//            try
//            {
//                Console.WriteLine(untypedEmployees.Rows[0]["NonExistingColumn"]);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Ошибка доступа к несуществующей колонке: " + ex.Message);
//            }

//            int iterations = 1000000;
//            Stopwatch sw = new Stopwatch();

//            sw.Start();
//            for (int i = 0; i < iterations; i++)
//            {
//                decimal salary = typedDs.Employees[0].Salary;
//            }
//            sw.Stop();
//            long typedTime = sw.ElapsedMilliseconds;

//            sw.Reset();
//            sw.Start();
//            for (int i = 0; i < iterations; i++)
//            {
//                decimal salary = (decimal)untypedEmployees.Rows[0]["Salary"];
//            }
//            sw.Stop();
//            long untypedTime = sw.ElapsedMilliseconds;

//            Console.WriteLine("\nПроизводительность (1 млн чтений):");
//            Console.WriteLine($"Типизированный: {typedTime} мс");
//            Console.WriteLine($"Нетипизированный: {untypedTime} мс");

//            Console.WriteLine("\nОтчёт о практических преимуществах типизированного DataSet:");
//            Console.WriteLine("1. IntelliSense: В IDE (Visual Studio) при вводе 'row.' появляется автодополнение свойств (Id, Name, Salary), что ускоряет разработку и снижает ошибки.");
//            Console.WriteLine("2. Безопасность типов: Ошибки вроде row.Salary = \"string\"; ловятся на компиляции, а не в runtime.");
//            Console.WriteLine("3. Производительность: Прямой доступ к свойствам быстрее, чем поиск по строке [\"Salary\"] (разница видна в тесте).");
//            Console.WriteLine("4. Удобство: Код читаемее: row.Name вместо row[\"Name\"] as string.");
//            Console.WriteLine("5. Рефакторинг: Если переименовать колонку в .xsd, IDE найдёт и предложит обновить все использования (Rename Symbol).");
//            Console.WriteLine("6. Документирование: Свойства явно показывают типы (int, decimal), что помогает в команде.");
//            Console.WriteLine("Практическое применение: В крупных проектах с БД (отчёты, формы) типизированные DataSet снижают баги, ускоряют кодинг и упрощают поддержку.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }
//}

//5
//using System;
//using System.Data;

//public class OrdersDataSet : DataSet
//{
//    private OrdersTable orders;

//    public OrdersDataSet()
//    {
//        orders = new OrdersTable();
//        Tables.Add(orders);
//    }

//    public OrdersTable Orders => orders;
//}

//public class OrdersTable : DataTable
//{
//    public OrdersTable()
//    {
//        TableName = "Orders";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("CustomerName", typeof(string));
//        Columns.Add("Product", typeof(string));
//        Columns.Add("Quantity", typeof(int));
//        Columns.Add("Price", typeof(decimal));
//        Columns.Add("OrderDate", typeof(DateTime));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };

//        Columns["CustomerName"].AllowDBNull = false;
//        Columns["Product"].AllowDBNull = false;
//        Columns["Quantity"].DefaultValue = 1;
//        Columns["Price"].DefaultValue = 0.0m;
//        Columns["OrderDate"].DefaultValue = DateTime.Now;
//    }

//    public OrdersRow this[int index] => (OrdersRow)Rows[index];

//    public OrdersRow AddOrdersRow(int id, string customerName, string product, int quantity, decimal price, DateTime orderDate)
//    {
//        if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentException("Имя клиента обязательно");
//        if (string.IsNullOrWhiteSpace(product)) throw new ArgumentException("Название товара обязательно");
//        if (quantity <= 0) throw new ArgumentException("Количество должно быть > 0");
//        if (price < 0) throw new ArgumentException("Цена не может быть отрицательной");

//        OrdersRow row = (OrdersRow)NewRow();
//        row.Id = id;
//        row.CustomerName = customerName;
//        row.Product = product;
//        row.Quantity = quantity;
//        row.Price = price;
//        row.OrderDate = orderDate;
//        Rows.Add(row);
//        return row;
//    }

//    public void AddOrdersRow(OrdersRow row)
//    {
//        if (row == null) throw new ArgumentNullException(nameof(row));
//        Rows.Add(row);
//    }

//    protected override Type GetRowType() => typeof(OrdersRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new OrdersRow(builder);
//}

//public class OrdersRow : DataRow
//{
//    internal OrdersRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string CustomerName
//    {
//        get => (string)this["CustomerName"];
//        set => this["CustomerName"] = value ?? throw new ArgumentException("Имя клиента не может быть null");
//    }

//    public string Product
//    {
//        get => (string)this["Product"];
//        set => this["Product"] = value ?? throw new ArgumentException("Товар не может быть null");
//    }

//    public int Quantity
//    {
//        get => (int)this["Quantity"];
//        set => this["Quantity"] = value > 0 ? value : throw new ArgumentException("Количество должно быть > 0");
//    }

//    public decimal Price
//    {
//        get => (decimal)this["Price"];
//        set => this["Price"] = value >= 0 ? value : throw new ArgumentException("Цена не может быть отрицательной");
//    }

//    public DateTime OrderDate
//    {
//        get => (DateTime)this["OrderDate"];
//        set => this["OrderDate"] = value;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var ds = new OrdersDataSet();

//        try
//        {
//            Console.WriteLine("Способ 1: AddOrdersRow() — с полной проверкой и IntelliSense");
//            ds.Orders.AddOrdersRow(1, "Иван Иванов", "Ноутбук", 1, 89990.00m, DateTime.Now);

//            Console.WriteLine("Способ 2: Создание строки через NewOrdersRow() + ручное добавление");
//            var row2 = ds.Orders.NewRow();
//            row2.Id = 2;
//            row2.CustomerName = "Мария Петрова";
//            row2.Product = "Монитор 27\"";
//            row2.Quantity = 2;
//            row2.Price = 35990.00m;
//            ds.Orders.Rows.Add(row2);

//            Console.WriteLine("Способ 3: Прямое использование AddOrdersRow с автозаполнением");
//            ds.Orders.AddOrdersRow(3, "Пётр Сидоров", "Клавиатура", 1, 4500.00m, new DateTime(2025, 12, 8));

//            try
//            {
//                ds.Orders.AddOrdersRow(1, "Дубликат", "Мышь", 1, 1500m, DateTime.Now);
//            }
//            catch (ConstraintException)
//            {
//                Console.WriteLine("ОШИБКА: Нарушение первичного ключа (Id дублируется) — поймано!");
//            }

//            try
//            {
//                ds.Orders.AddOrdersRow(4, null, "Телефон", 1, 59990m, DateTime.Now);
//            }
//            catch (ArgumentException ex)
//            {
//                Console.WriteLine("ОШИБКА: " + ex.Message);
//            }

//            try
//            {
//                var badRow = ds.Orders.NewRow();
//                badRow.Id = 5;
//                badRow.CustomerName = "Тест";
//                badRow.Product = "Планшет";
//                badRow.Quantity = -5;
//                ds.Orders.Rows.Add(badRow);
//            }
//            catch (ArgumentException ex)
//            {
//                Console.WriteLine("ОШИБКА валидации: " + ex.Message);
//            }

//            Console.WriteLine("\nВсе заказы в системе:");
//            Console.WriteLine(new string('-', 90));
//            Console.WriteLine("| {0,-3} | {1,-20} | {2,-20} | {3,-8} | {4,-12} | {5,-19} |",
//                "Id", "Клиент", "Товар", "Кол-во", "Цена", "Дата");
//            Console.WriteLine(new string('-', 90));

//            foreach (OrdersRow row in ds.Orders.Rows)
//            {
//                Console.WriteLine("| {0,-3} | {1,-20} | {2,-20} | {3,-8} | {4,-12:C} | {5,-19:yyyy-MM-dd} |",
//                    row.Id, row.CustomerName, row.Product, row.Quantity, row.Price, row.OrderDate);
//            }

//            Console.WriteLine(new string('-', 90));
//            Console.WriteLine($"Всего заказов: {ds.Orders.Rows.Count}");
//            Console.WriteLine("Типы данных строго соблюдены: Id=int, Price=decimal, OrderDate=DateTime");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Критическая ошибка: " + ex.Message);
//        }

//        Console.WriteLine("\nНажмите любую клавишу...");
//        Console.ReadKey();
//    }
//}

//6
//using System;
//using System.Data;
//using System.Linq;

//public class LibraryDataSet : DataSet
//{
//    private BooksTable books;

//    public LibraryDataSet()
//    {
//        books = new BooksTable();
//        Tables.Add(books);
//    }

//    public BooksTable Books => books;
//}

//public class BooksTable : DataTable
//{
//    public BooksTable()
//    {
//        TableName = "Books";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Title", typeof(string));
//        Columns.Add("Author", typeof(string));
//        Columns.Add("PublicationYear", typeof(int));
//        Columns.Add("Genre", typeof(string));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public BooksRow this[int index] => (BooksRow)Rows[index];

//    public BooksRow FindByBookID(int id)
//    {
//        return Rows.Find(id) as BooksRow;
//    }

//    public BooksRow[] FindByAuthor(string author)
//    {
//        return Rows.Cast<BooksRow>().Where(r => r.Author == author).ToArray();
//    }

//    public BooksRow[] FindByYearRange(int fromYear, int toYear)
//    {
//        return Rows.Cast<BooksRow>().Where(r => r.PublicationYear >= fromYear && r.PublicationYear <= toYear).ToArray();
//    }

//    public BooksRow[] FindByTitleLike(string pattern)
//    {
//        return Rows.Cast<BooksRow>().Where(r => r.Title.Contains(pattern)).ToArray();
//    }

//    public BooksRow[] GetSortedByYear()
//    {
//        return Rows.Cast<BooksRow>().OrderBy(r => r.PublicationYear).ToArray();
//    }

//    protected override Type GetRowType() => typeof(BooksRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new BooksRow(builder);
//}

//public class BooksRow : DataRow
//{
//    internal BooksRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Title
//    {
//        get => this["Title"] == DBNull.Value ? null : (string)this["Title"];
//        set => this["Title"] = value ?? (object)DBNull.Value;
//    }

//    public string Author
//    {
//        get => this["Author"] == DBNull.Value ? null : (string)this["Author"];
//        set => this["Author"] = value ?? (object)DBNull.Value;
//    }

//    public int PublicationYear
//    {
//        get => (int)this["PublicationYear"];
//        set => this["PublicationYear"] = value;
//    }

//    public string Genre
//    {
//        get => this["Genre"] == DBNull.Value ? null : (string)this["Genre"];
//        set => this["Genre"] = value ?? (object)DBNull.Value;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var typedDs = new LibraryDataSet();

//        typedDs.Books.Rows.Add(1, "1984", "George Orwell", 1949, "Dystopian");
//        typedDs.Books.Rows.Add(2, "To Kill a Mockingbird", "Harper Lee", 1960, "Fiction");
//        typedDs.Books.Rows.Add(3, "Animal Farm", "George Orwell", 1945, "Satire");
//        typedDs.Books.Rows.Add(4, "The Great Gatsby", "F. Scott Fitzgerald", 1925, "Fiction");
//        typedDs.Books.Rows.Add(5, "Brave New World", "Aldous Huxley", 1932, "Dystopian");

//        Console.WriteLine("Поиск по первичному ключу (Id=2):");
//        var bookById = typedDs.Books.FindByBookID(2);
//        if (bookById != null)
//            Console.WriteLine($"{bookById.Id} | {bookById.Title} | {bookById.Author} | {bookById.PublicationYear} | {bookById.Genre}");
//        else
//            Console.WriteLine("Книга не найдена.");

//        Console.WriteLine("\nПоиск по автору (George Orwell):");
//        var booksByAuthor = typedDs.Books.FindByAuthor("George Orwell");
//        foreach (var book in booksByAuthor)
//            Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | {book.PublicationYear} | {book.Genre}");

//        Console.WriteLine("\nПоиск по диапазону лет (1940-1950):");
//        var booksByYear = typedDs.Books.FindByYearRange(1940, 1950);
//        foreach (var book in booksByYear)
//            Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | {book.PublicationYear} | {book.Genre}");

//        Console.WriteLine("\nПоиск по части названия (World):");
//        var booksByTitle = typedDs.Books.FindByTitleLike("World");
//        foreach (var book in booksByTitle)
//            Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | {book.PublicationYear} | {book.Genre}");

//        Console.WriteLine("\nСортировка по году (OrderBy):");
//        var sortedBooks = typedDs.Books.GetSortedByYear();
//        foreach (var book in sortedBooks)
//            Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | {book.PublicationYear} | {book.Genre}");

//        var untypedDs = new DataSet();
//        var untypedBooks = new DataTable("Books");
//        untypedBooks.Columns.Add("Id", typeof(int));
//        untypedBooks.Columns.Add("Title", typeof(string));
//        untypedBooks.Columns.Add("Author", typeof(string));
//        untypedBooks.Columns.Add("PublicationYear", typeof(int));
//        untypedBooks.Columns.Add("Genre", typeof(string));
//        untypedBooks.PrimaryKey = new DataColumn[] { untypedBooks.Columns["Id"] };
//        untypedDs.Tables.Add(untypedBooks);

//        untypedBooks.Rows.Add(1, "1984", "George Orwell", 1949, "Dystopian");
//        untypedBooks.Rows.Add(2, "To Kill a Mockingbird", "Harper Lee", 1960, "Fiction");

//        Console.WriteLine("\nНетипизированный поиск (Select по автору):");
//        var untypedByAuthor = untypedBooks.Select("Author = 'George Orwell'");
//        foreach (DataRow row in untypedByAuthor)
//            Console.WriteLine($"{row["Id"]} | {row["Title"]} | {row["Author"]} | {row["PublicationYear"]} | {row["Genre"]}");

//        Console.WriteLine("\nДемонстрация преимуществ типизированного поиска:");
//        Console.WriteLine("- Типизированный: Сильные типы, IntelliSense, ошибки на компиляции (e.g., r.Author == 123 не скомпилируется).");
//        Console.WriteLine("- Нетипизированный: Фильтры строками, runtime ошибки (e.g., Select(\"Author = 123\") — сработает, но неверно).");
//        Console.WriteLine("Пример runtime ошибки в нетипизированном:");
//        try
//        {
//            untypedBooks.Select("NonExistingColumn = 'test'");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }
//}

//7
//using System;
//using System.Data;
//using System.Text.RegularExpressions;

//public class ContactsDataSet : DataSet
//{
//    private ContactsTable contacts;

//    public ContactsDataSet()
//    {
//        contacts = new ContactsTable();
//        Tables.Add(contacts);
//    }

//    public ContactsTable Contacts => contacts;
//}

//public class ContactsTable : DataTable
//{
//    public ContactsTable()
//    {
//        TableName = "Contacts";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Name", typeof(string));
//        Columns.Add("Email", typeof(string));
//        Columns.Add("Phone", typeof(string));
//        Columns.Add("Address", typeof(string));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public ContactsRow FindById(int id)
//    {
//        return Rows.Find(id) as ContactsRow;
//    }

//    public void AddRow(int id, string name, string email, string phone, string address)
//    {
//        ContactsRow row = (ContactsRow)NewRow();
//        row.Id = id;
//        row.Name = name;
//        row.Email = email;
//        row.Phone = phone;
//        row.Address = address;
//        Rows.Add(row);
//    }

//    protected override Type GetRowType() => typeof(ContactsRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new ContactsRow(builder);
//}

//public class ContactsRow : DataRow
//{
//    internal ContactsRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Name
//    {
//        get => this["Name"] == DBNull.Value ? null : (string)this["Name"];
//        set => this["Name"] = value ?? (object)DBNull.Value;
//    }

//    public string Email
//    {
//        get => this["Email"] == DBNull.Value ? null : (string)this["Email"];
//        set
//        {
//            if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
//                throw new ArgumentException("Неверный формат email");
//            this["Email"] = value ?? (object)DBNull.Value;
//        }
//    }

//    public string Phone
//    {
//        get => this["Phone"] == DBNull.Value ? null : (string)this["Phone"];
//        set
//        {
//            if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @"^\+?\d{10,15}$"))
//                throw new ArgumentException("Неверный формат телефона (должен быть +? и 10-15 цифр)");
//            this["Phone"] = value ?? (object)DBNull.Value;
//        }
//    }

//    public string Address
//    {
//        get => this["Address"] == DBNull.Value ? null : (string)this["Address"];
//        set => this["Address"] = value ?? (object)DBNull.Value;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var ds = new ContactsDataSet();

//        try
//        {
//            ds.Contacts.AddRow(1, "Иван Иванов", "ivan@example.com", "+79123456789", "Москва, ул. Ленина 1");
//            ds.Contacts.AddRow(2, "Мария Петрова", "maria@example.com", "+79234567890", "СПб, пр. Невский 2");
//            ds.Contacts.AddRow(3, "Пётр Сидоров", "petr@example.com", "+79345678901", "Екб, ул. Малышева 3");

//            Console.WriteLine("Контакты ДО редактирования:");
//            PrintContacts(ds.Contacts);

//            var contact = ds.Contacts.FindById(2);
//            if (contact != null)
//            {
//                Console.WriteLine("\nРедактирование контакта ID=2:");
//                contact.Name = "Мария Иванова";
//                contact.Email = "maria.ivanova@example.com";
//                contact.Phone = "+79234567891";
//                contact.Address = "СПб, пр. Невский 10";
//            }
//            else
//            {
//                Console.WriteLine("Контакт не найден.");
//            }

//            try
//            {
//                contact.Email = "invalid_email";
//            }
//            catch (ArgumentException ex)
//            {
//                Console.WriteLine("Ошибка валидации: " + ex.Message);
//            }

//            try
//            {
//                contact.Phone = "123";
//            }
//            catch (ArgumentException ex)
//            {
//                Console.WriteLine("Ошибка валидации: " + ex.Message);
//            }

//            Console.WriteLine("\nКонтакты ПОСЛЕ редактирования:");
//            PrintContacts(ds.Contacts);

//            Console.WriteLine("\nОтчёт об изменениях:");
//            foreach (ContactsRow row in ds.Contacts.Rows)
//            {
//                Console.WriteLine($"ID={row.Id}: RowState={row.RowState}");
//            }

//            Console.WriteLine("\nОткат изменений:");
//            ds.RejectChanges();

//            Console.WriteLine("\nКонтакты после отката:");
//            PrintContacts(ds.Contacts);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }

//    static void PrintContacts(ContactsTable table)
//    {
//        Console.WriteLine(new string('-', 100));
//        Console.WriteLine("| {0,-3} | {1,-20} | {2,-30} | {3,-15} | {4,-25} |",
//            "Id", "Имя", "Email", "Телефон", "Адрес");
//        Console.WriteLine(new string('-', 100));

//        foreach (ContactsRow row in table.Rows)
//        {
//            Console.WriteLine("| {0,-3} | {1,-20} | {2,-30} | {3,-15} | {4,-25} |",
//                row.Id, row.Name ?? "(null)", row.Email ?? "(null)", row.Phone ?? "(null)", row.Address ?? "(null)");
//        }

//        Console.WriteLine(new string('-', 100));
//    }
//}

//8
//using System;
//using System.Data;

//public class StudentsDataSet : DataSet
//{
//    private StudentsTable students;
//    private GradesTable grades;

//    public StudentsDataSet()
//    {
//        students = new StudentsTable();
//        grades = new GradesTable();
//        Tables.Add(students);
//        Tables.Add(grades);

//        DataRelation relation = new DataRelation(
//            "StudentGrades",
//            students.Columns["Id"],
//            grades.Columns["StudentId"],
//            false
//        );
//        Relations.Add(relation);
//    }

//    public StudentsTable Students => students;
//    public GradesTable Grades => grades;
//}

//public class StudentsTable : DataTable
//{
//    public StudentsTable()
//    {
//        TableName = "Students";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Name", typeof(string));
//        Columns.Add("Group", typeof(string));
//        Columns.Add("Age", typeof(int));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public StudentsRow FindById(int id)
//    {
//        return Rows.Find(id) as StudentsRow;
//    }

//    public void DeleteById(int id)
//    {
//        StudentsRow row = FindById(id);
//        if (row != null)
//        {
//            if (row.GetGradesRows().Length > 0)
//            {
//                throw new InvalidOperationException($"Нельзя удалить студента ID={id} — есть связанные оценки.");
//            }
//            row.Delete();
//        }
//    }

//    public void DeleteByGroup(string group)
//    {
//        StudentsRow[] rowsToDelete = Rows.Cast<StudentsRow>().Where(r => r.Group == group).ToArray();
//        foreach (StudentsRow row in rowsToDelete)
//        {
//            if (row.GetGradesRows().Length > 0)
//            {
//                throw new InvalidOperationException($"Нельзя удалить студента ID={row.Id} из группы {group} — есть связанные оценки.");
//            }
//            row.Delete();
//        }
//    }

//    protected override Type GetRowType() => typeof(StudentsRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new StudentsRow(builder);
//}

//public class StudentsRow : DataRow
//{
//    internal StudentsRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Name
//    {
//        get => this["Name"] == DBNull.Value ? null : (string)this["Name"];
//        set => this["Name"] = value ?? (object)DBNull.Value;
//    }

//    public string Group
//    {
//        get => this["Group"] == DBNull.Value ? null : (string)this["Group"];
//        set => this["Group"] = value ?? (object)DBNull.Value;
//    }

//    public int Age
//    {
//        get => (int)this["Age"];
//        set => this["Age"] = value;
//    }

//    public GradesRow[] GetGradesRows()
//    {
//        return GetChildRows("StudentGrades") as GradesRow[];
//    }
//}

//public class GradesTable : DataTable
//{
//    public GradesTable()
//    {
//        TableName = "Grades";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("StudentId", typeof(int));
//        Columns.Add("Subject", typeof(string));
//        Columns.Add("Grade", typeof(int));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public void AddRow(int id, int studentId, string subject, int grade)
//    {
//        GradesRow row = (GradesRow)NewRow();
//        row.Id = id;
//        row.StudentId = studentId;
//        row.Subject = subject;
//        row.Grade = grade;
//        Rows.Add(row);
//    }

//    protected override Type GetRowType() => typeof(GradesRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new GradesRow(builder);
//}

//public class GradesRow : DataRow
//{
//    internal GradesRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public int StudentId
//    {
//        get => (int)this["StudentId"];
//        set => this["StudentId"] = value;
//    }

//    public string Subject
//    {
//        get => this["Subject"] == DBNull.Value ? null : (string)this["Subject"];
//        set => this["Subject"] = value ?? (object)DBNull.Value;
//    }

//    public int Grade
//    {
//        get => (int)this["Grade"];
//        set => this["Grade"] = value;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var ds = new StudentsDataSet();

//        try
//        {
//            ds.Students.Rows.Add(1, "Иван Иванов", "Группа A", 20);
//            ds.Students.Rows.Add(2, "Мария Петрова", "Группа B", 21);
//            ds.Students.Rows.Add(3, "Пётр Сидоров", "Группа A", 19);
//            ds.Students.Rows.Add(4, "Анна Кузнецова", "Группа C", 22);

//            ds.Grades.AddRow(1, 1, "Математика", 5);
//            ds.Grades.AddRow(2, 2, "Физика", 4);
//            ds.Grades.AddRow(3, 3, "Информатика", 5);

//            Console.WriteLine("Студенты ДО удаления:");
//            PrintStudents(ds.Students);

//            Console.WriteLine("\nУдаление по ID (ID=4, без зависимостей):");
//            try
//            {
//                ds.Students.DeleteById(4);
//                Console.WriteLine("Удаление успешно.");
//            }
//            catch (InvalidOperationException ex)
//            {
//                Console.WriteLine("Ошибка: " + ex.Message);
//            }

//            Console.WriteLine("\nПопытка удаления с зависимостями (ID=1):");
//            try
//            {
//                ds.Students.DeleteById(1);
//            }
//            catch (InvalidOperationException ex)
//            {
//                Console.WriteLine("Ошибка: " + ex.Message);
//            }

//            Console.WriteLine("\nМассовое удаление по группе (Группа C):");
//            try
//            {
//                Console.WriteLine("Отчёт перед удалением:");
//                StudentsRow[] toDelete = ds.Students.Rows.Cast<StudentsRow>().Where(r => r.Group == "Группа C").ToArray();
//                foreach (var row in toDelete)
//                {
//                    Console.WriteLine($"Будет удалён: ID={row.Id}, {row.Name}, Группа={row.Group}");
//                }
//                ds.Students.DeleteByGroup("Группа C");
//                Console.WriteLine("Массовое удаление успешно.");
//            }
//            catch (InvalidOperationException ex)
//            {
//                Console.WriteLine("Ошибка: " + ex.Message);
//            }

//            Console.WriteLine("\nСостояние строк после удаления:");
//            foreach (StudentsRow row in ds.Students.Rows)
//            {
//                Console.WriteLine($"ID={row.Id}: RowState={row.RowState}");
//            }

//            Console.WriteLine("\nСтуденты ПОСЛЕ удаления:");
//            PrintStudents(ds.Students);

//            Console.WriteLine("\nОткат изменений:");
//            ds.RejectChanges();
//            Console.WriteLine("Откат выполнен.");

//            Console.WriteLine("\nСтуденты после отката:");
//            PrintStudents(ds.Students);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Общая ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }

//    static void PrintStudents(StudentsTable table)
//    {
//        Console.WriteLine(new string('-', 60));
//        Console.WriteLine("| {0,-3} | {1,-20} | {2,-10} | {3,-3} |",
//            "Id", "Имя", "Группа", "Возраст");
//        Console.WriteLine(new string('-', 60));

//        foreach (StudentsRow row in table.Rows)
//        {
//            if (row.RowState != DataRowState.Deleted)
//            {
//                Console.WriteLine("| {0,-3} | {1,-20} | {2,-10} | {3,-3} |",
//                    row.Id, row.Name ?? "(null)", row.Group ?? "(null)", row.Age);
//            }
//        }

//        Console.WriteLine(new string('-', 60));
//    }
//}

//9
//using System;
//using System.Data;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;
//using System.Text;

//public class EmployeesDataSet : DataSet
//{
//    private EmployeesTable employees;

//    public EmployeesDataSet()
//    {
//        employees = new EmployeesTable();
//        Tables.Add(employees);
//    }

//    public EmployeesTable Employees => employees;
//}

//public class EmployeesTable : DataTable
//{
//    private List<string> validationErrors = new List<string>();

//    public EmployeesTable()
//    {
//        TableName = "Employees";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Name", typeof(string));
//        Columns.Add("Email", typeof(string));
//        Columns.Add("Salary", typeof(decimal));
//        Columns.Add("HireDate", typeof(DateTime));

//        Columns["Id"].AllowDBNull = false;
//        Columns["Id"].Unique = true;
//        Columns["Name"].AllowDBNull = false;
//        Columns["Email"].AllowDBNull = false;
//        Columns["Email"].Unique = true;
//        Columns["Salary"].AllowDBNull = false;
//        Columns["HireDate"].AllowDBNull = false;

//        ColumnChanging += OnColumnChanging;

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public EmployeesRow AddRow(int id, string name, string email, decimal salary, DateTime hireDate)
//    {
//        EmployeesRow row = (EmployeesRow)NewRow();
//        row.Id = id;
//        row.Name = name;
//        row.Email = email;
//        row.Salary = salary;
//        row.HireDate = hireDate;
//        Rows.Add(row);
//        return row;
//    }

//    private void OnColumnChanging(object sender, DataColumnChangeEventArgs e)
//    {
//        try
//        {
//            if (e.Column.ColumnName == "Email")
//            {
//                string email = e.ProposedValue as string;
//                if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
//                {
//                    throw new ArgumentException("Неверный формат email");
//                }
//            }
//            else if (e.Column.ColumnName == "Salary")
//            {
//                decimal salary = (decimal)e.ProposedValue;
//                if (salary < 10000m || salary > 1000000m)
//                {
//                    validationErrors.Add($"Зарплата вне диапазона (10000-1000000): {salary}");
//                    e.ProposedValue = Math.Clamp(salary, 10000m, 1000000m);
//                    Console.WriteLine($"Автоисправление зарплаты: {salary} → {e.ProposedValue}");
//                }
//            }
//            else if (e.Column.ColumnName == "HireDate")
//            {
//                DateTime hireDate = (DateTime)e.ProposedValue;
//                if (hireDate > DateTime.Now)
//                {
//                    throw new ArgumentException("Дата найма не может быть в будущем");
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            validationErrors.Add($"Ошибка валидации в колонке {e.Column.ColumnName}: {ex.Message}");
//            throw;
//        }
//    }

//    public string GetValidationReport()
//    {
//        StringBuilder report = new StringBuilder();
//        report.AppendLine("Отчёт о валидации Employees:");
//        if (validationErrors.Count == 0)
//        {
//            report.AppendLine("Ошибок не обнаружено.");
//        }
//        else
//        {
//            report.AppendLine($"Обнаружено ошибок: {validationErrors.Count}");
//            foreach (string error in validationErrors)
//            {
//                report.AppendLine($"- {error}");
//            }
//        }

//        report.AppendLine("\nВсе сотрудники:");
//        foreach (EmployeesRow row in Rows)
//        {
//            report.AppendLine($"{row.Id} | {row.Name} | {row.Email} | {row.Salary:C} | {row.HireDate:yyyy-MM-dd}");
//        }

//        return report.ToString();
//    }

//    public void ClearErrors()
//    {
//        validationErrors.Clear();
//    }

//    protected override Type GetRowType() => typeof(EmployeesRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new EmployeesRow(builder);
//}

//public class EmployeesRow : DataRow
//{
//    internal EmployeesRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Name
//    {
//        get => (string)this["Name"];
//        set => this["Name"] = value ?? throw new ArgumentNullException("Имя обязательно");
//    }

//    public string Email
//    {
//        get => (string)this["Email"];
//        set => this["Email"] = value ?? throw new ArgumentNullException("Email обязательно");
//    }

//    public decimal Salary
//    {
//        get => (decimal)this["Salary"];
//        set => this["Salary"] = value;
//    }

//    public DateTime HireDate
//    {
//        get => (DateTime)this["HireDate"];
//        set => this["HireDate"] = value;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var ds = new EmployeesDataSet();
//        ds.Employees.ClearErrors();

//        try
//        {
//            ds.Employees.AddRow(1, "Иван Иванов", "ivan@example.com", 50000m, new DateTime(2020, 1, 1));
//            ds.Employees.AddRow(2, "Мария Петрова", "maria@example.com", 60000m, new DateTime(2019, 5, 15));
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ошибка добавления: " + ex.Message);
//        }

//        try
//        {
//            ds.Employees[0].Email = "invalid_email";
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Лог ошибки: " + ex.Message);
//        }

//        try
//        {
//            ds.Employees[0].Salary = 5000m;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Лог ошибки: " + ex.Message);
//        }

//        try
//        {
//            ds.Employees[0].HireDate = DateTime.Now.AddDays(1);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Лог ошибки: " + ex.Message);
//        }

//        try
//        {
//            ds.Employees.AddRow(1, "Дубликат", "dup@example.com", 70000m, new DateTime(2021, 3, 3));
//        }
//        catch (ConstraintException ex)
//        {
//            Console.WriteLine("Лог ошибки уникальности: " + ex.Message);
//        }

//        try
//        {
//            ds.Employees.AddRow(3, null, "null@example.com", 80000m, new DateTime(2022, 4, 4));
//        }
//        catch (ArgumentNullException ex)
//        {
//            Console.WriteLine("Лог ошибки null: " + ex.Message);
//        }

//        Console.WriteLine(ds.Employees.GetValidationReport());
//        Console.ReadKey();
//    }
//}

//10
//using System;
//using System.Data;
//using System.Linq;

//public class SalesDataSet : DataSet
//{
//    private SalesTable sales;

//    public SalesDataSet()
//    {
//        sales = new SalesTable();
//        Tables.Add(sales);
//    }

//    public SalesTable Sales => sales;
//    public SalesDataView CreateDataView() => new SalesDataView(sales);
//}

//public class SalesTable : DataTable
//{
//    public SalesTable()
//    {
//        TableName = "Sales";
//        Columns.Add("Id", typeof(int));
//        Columns.Add("Product", typeof(string));
//        Columns.Add("Amount", typeof(decimal));
//        Columns.Add("SaleDate", typeof(DateTime));
//        Columns.Add("Customer", typeof(string));

//        PrimaryKey = new DataColumn[] { Columns["Id"] };
//    }

//    public SalesRow AddSale(int id, string product, decimal amount, DateTime saleDate, string customer)
//    {
//        SalesRow row = (SalesRow)NewRow();
//        row.Id = id;
//        row.Product = product;
//        row.Amount = amount;
//        row.SaleDate = saleDate;
//        row.Customer = customer;
//        Rows.Add(row);
//        return row;
//    }

//    protected override Type GetRowType() => typeof(SalesRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new SalesRow(builder);
//}

//public class SalesRow : DataRow
//{
//    internal SalesRow(DataRowBuilder builder) : base(builder) { }

//    public int Id
//    {
//        get => (int)this["Id"];
//        set => this["Id"] = value;
//    }

//    public string Product
//    {
//        get => this["Product"] == DBNull.Value ? null : (string)this["Product"];
//        set => this["Product"] = value ?? (object)DBNull.Value;
//    }

//    public decimal Amount
//    {
//        get => (decimal)this["Amount"];
//        set => this["Amount"] = value;
//    }

//    public DateTime SaleDate
//    {
//        get => (DateTime)this["SaleDate"];
//        set => this["SaleDate"] = value;
//    }

//    public string Customer
//    {
//        get => this["Customer"] == DBNull.Value ? null : (string)this["Customer"];
//        set => this["Customer"] = value ?? (object)DBNull.Value;
//    }
//}

//public class SalesDataView : DataView
//{
//    public SalesDataView(SalesTable table) : base(table) { }

//    public SalesRow this[int index] => (SalesRow)base[index].Row;

//    public void FilterByAmount(decimal minAmount, decimal maxAmount)
//    {
//        RowFilter = $"Amount >= {minAmount} AND Amount <= {maxAmount}";
//    }

//    public void FilterByDateRange(DateTime fromDate, DateTime toDate)
//    {
//        RowFilter = $"SaleDate >= #{fromDate:yyyy-MM-dd}# AND SaleDate <= #{toDate:yyyy-MM-dd}#";
//    }

//    public void SortByAmountDescending()
//    {
//        Sort = "Amount DESC";
//    }

//    public void SortByDateAscending()
//    {
//        Sort = "SaleDate ASC";
//    }

//    public SalesTable ToSalesTable()
//    {
//        SalesTable result = new SalesTable();
//        foreach (SalesRow row in this.Cast<DataRowView>().Select(drv => (SalesRow)drv.Row))
//        {
//            result.ImportRow(row);
//        }
//        return result;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        try
//        {
//            var ds = new SalesDataSet();

//            ds.Sales.AddSale(1, "Ноутбук", 85000m, new DateTime(2025, 1, 15), "Иван");
//            ds.Sales.AddSale(2, "Монитор", 35000m, new DateTime(2025, 1, 20), "Мария");
//            ds.Sales.AddSale(3, "Клавиатура", 4500m, new DateTime(2025, 2, 1), "Пётр");
//            ds.Sales.AddSale(4, "Мышь", 2500m, new DateTime(2025, 2, 10), "Анна");
//            ds.Sales.AddSale(5, "Смартфон", 120000m, new DateTime(2025, 2, 15), "Иван");
//            ds.Sales.AddSale(6, "Планшет", 45000m, new DateTime(2025, 3, 1), "Мария");

//            Console.WriteLine("Все продажи:");
//            PrintSales(ds.Sales);

//            var view = ds.CreateDataView();

//            Console.WriteLine("\nФильтрация: Сумма от 30000 до 100000:");
//            view.FilterByAmount(30000m, 100000m);
//            PrintSalesView(view);

//            Console.WriteLine("\nФильтрация: Продажи в феврале 2025:");
//            view.FilterByDateRange(new DateTime(2025, 2, 1), new DateTime(2025, 2, 28));
//            view.SortByAmountDescending();
//            PrintSalesView(view);

//            Console.WriteLine("\nСортировка по дате (по возрастанию):");
//            view.RowFilter = "";
//            view.SortByDateAscending();
//            PrintSalesView(view);

//            Console.WriteLine("\nСоздание новой таблицы из DataView:");
//            SalesTable filteredTable = view.ToSalesTable();
//            Console.WriteLine($"Получено строк: {filteredTable.Rows.Count}");
//            PrintSales(filteredTable);

//            Console.WriteLine("\nПреимущества типизированного DataView:");
//            Console.WriteLine("- Полная типизация: view[0].Product, view[0].Amount — IntelliSense и проверка на компиляции");
//            Console.WriteLine("- Безопасность: нет ошибок вроде row[\"Amoutn\"] или приведения (string)row[\"Amount\"]");
//            Console.WriteLine("- Удобство: view.FilterByAmount(30000, 100000) вместо строк RowFilter = \"Amount >= 30000\"");
//            Console.WriteLine("- Производительность и читаемость кода значительно выше");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }

//    static void PrintSales(SalesTable table)
//    {
//        Console.WriteLine(new string('-', 90));
//        Console.WriteLine("| {0,-3} | {1,-15} | {2,-12} | {3,-12} | {4,-10} |",
//            "Id", "Товар", "Сумма", "Дата", "Клиент");
//        Console.WriteLine(new string('-', 90));

//        foreach (SalesRow row in table.Rows)
//        {
//            Console.WriteLine("| {0,-3} | {1,-15} | {2,-12:C} | {3,-12:yyyy-MM-dd} | {4,-10} |",
//                row.Id, row.Product, row.Amount, row.SaleDate, row.Customer);
//        }
//        Console.WriteLine(new string('-', 90));
//    }

//    static void PrintSalesView(SalesDataView view)
//    {
//        Console.WriteLine(new string('-', 90));
//        Console.WriteLine($"| Строк в представлении: {view.Count} | Фильтр: \"{view.RowFilter}\" | Сортировка: \"{view.Sort}\"");
//        Console.WriteLine(new string('-', 90));
//        Console.WriteLine("| {0,-3} | {1,-15} | {2,-12} | {3,-12} | {4,-10} |",
//            "Id", "Товар", "Сумма", "Дата", "Клиент");
//        Console.WriteLine(new string('-', 90));

//        foreach (SalesRow row in view)
//        {
//            Console.WriteLine("| {0,-3} | {1,-15} | {2,-12:C} | {3,-12:yyyy-MM-dd} | {4,-10} |",
//                row.Id, row.Product, row.Amount, row.SaleDate, row.Customer);
//        }
//        Console.WriteLine(new string('-', 90));
//    }
//}

//11
//using System;
//using System.Data;
//using System.Linq;

//public class ShopDataSet : DataSet
//{
//    private CategoriesTable categories;
//    private ProductsTable products;
//    private OrdersTable orders;

//    public ShopDataSet()
//    {
//        categories = new CategoriesTable();
//        products = new ProductsTable();
//        orders = new OrdersTable();

//        Tables.Add(categories);
//        Tables.Add(products);
//        Tables.Add(orders);

//        DataRelation categoryProductRelation = new DataRelation(
//            "CategoryProducts",
//            categories.Columns["CategoryID"],
//            products.Columns["CategoryID"]
//        );
//        Relations.Add(categoryProductRelation);

//        DataRelation productOrderRelation = new DataRelation(
//            "ProductOrders",
//            products.Columns["ProductID"],
//            orders.Columns["ProductID"]
//        );
//        Relations.Add(productOrderRelation);
//    }

//    public CategoriesTable Categories => categories;
//    public ProductsTable Products => products;
//    public OrdersTable Orders => orders;

//    public decimal GetTotalSalesByCategory(int categoryId)
//    {
//        CategoriesRow category = Categories.FindByCategoryID(categoryId);
//        if (category == null) return 0m;

//        return category.GetProductsRows()
//            .SelectMany(p => p.GetOrdersRows())
//            .Sum(o => o.Quantity * category.GetProductsRows().First(p => p.ProductID == o.ProductID).Price);
//    }

//    public decimal GetAveragePriceByCategory(int categoryId)
//    {
//        CategoriesRow category = Categories.FindByCategoryID(categoryId);
//        if (category == null || category.GetProductsRows().Length == 0) return 0m;

//        return category.GetProductsRows().Average(p => p.Price);
//    }
//}

//public class CategoriesTable : DataTable
//{
//    public CategoriesTable()
//    {
//        TableName = "Categories";
//        Columns.Add("CategoryID", typeof(int));
//        Columns.Add("CategoryName", typeof(string));

//        PrimaryKey = new DataColumn[] { Columns["CategoryID"] };
//    }

//    public CategoriesRow FindByCategoryID(int categoryId)
//    {
//        return Rows.Find(categoryId) as CategoriesRow;
//    }

//    public CategoriesRow AddCategory(int categoryId, string categoryName)
//    {
//        CategoriesRow row = (CategoriesRow)NewRow();
//        row.CategoryID = categoryId;
//        row.CategoryName = categoryName;
//        Rows.Add(row);
//        return row;
//    }

//    protected override Type GetRowType() => typeof(CategoriesRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new CategoriesRow(builder);
//}

//public class CategoriesRow : DataRow
//{
//    internal CategoriesRow(DataRowBuilder builder) : base(builder) { }

//    public int CategoryID
//    {
//        get => (int)this["CategoryID"];
//        set => this["CategoryID"] = value;
//    }

//    public string CategoryName
//    {
//        get => this["CategoryName"] == DBNull.Value ? null : (string)this["CategoryName"];
//        set => this["CategoryName"] = value ?? (object)DBNull.Value;
//    }

//    public ProductsRow[] GetProductsRows()
//    {
//        return GetChildRows("CategoryProducts") as ProductsRow[] ?? new ProductsRow[0];
//    }
//}

//public class ProductsTable : DataTable
//{
//    public ProductsTable()
//    {
//        TableName = "Products";
//        Columns.Add("ProductID", typeof(int));
//        Columns.Add("ProductName", typeof(string));
//        Columns.Add("CategoryID", typeof(int));
//        Columns.Add("Price", typeof(decimal));

//        PrimaryKey = new DataColumn[] { Columns["ProductID"] };
//    }

//    public ProductsRow FindByProductID(int productId)
//    {
//        return Rows.Find(productId) as ProductsRow;
//    }

//    public ProductsRow AddProduct(int productId, string productName, int categoryId, decimal price)
//    {
//        ProductsRow row = (ProductsRow)NewRow();
//        row.ProductID = productId;
//        row.ProductName = productName;
//        row.CategoryID = categoryId;
//        row.Price = price;
//        Rows.Add(row);
//        return row;
//    }

//    protected override Type GetRowType() => typeof(ProductsRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new ProductsRow(builder);
//}

//public class ProductsRow : DataRow
//{
//    internal ProductsRow(DataRowBuilder builder) : base(builder) { }

//    public int ProductID
//    {
//        get => (int)this["ProductID"];
//        set => this["ProductID"] = value;
//    }

//    public string ProductName
//    {
//        get => this["ProductName"] == DBNull.Value ? null : (string)this["ProductName"];
//        set => this["ProductName"] = value ?? (object)DBNull.Value;
//    }

//    public int CategoryID
//    {
//        get => (int)this["CategoryID"];
//        set => this["CategoryID"] = value;
//    }

//    public decimal Price
//    {
//        get => (decimal)this["Price"];
//        set => this["Price"] = value;
//    }

//    public CategoriesRow GetCategoryRow()
//    {
//        return GetParentRow("CategoryProducts") as CategoriesRow;
//    }

//    public OrdersRow[] GetOrdersRows()
//    {
//        return GetChildRows("ProductOrders") as OrdersRow[] ?? new OrdersRow[0];
//    }
//}

//public class OrdersTable : DataTable
//{
//    public OrdersTable()
//    {
//        TableName = "Orders";
//        Columns.Add("OrderID", typeof(int));
//        Columns.Add("ProductID", typeof(int));
//        Columns.Add("Quantity", typeof(int));
//        Columns.Add("OrderDate", typeof(DateTime));

//        PrimaryKey = new DataColumn[] { Columns["OrderID"] };
//    }

//    public OrdersRow FindByOrderID(int orderId)
//    {
//        return Rows.Find(orderId) as OrdersRow;
//    }

//    public OrdersRow AddOrder(int orderId, int productId, int quantity, DateTime orderDate)
//    {
//        OrdersRow row = (OrdersRow)NewRow();
//        row.OrderID = orderId;
//        row.ProductID = productId;
//        row.Quantity = quantity;
//        row.OrderDate = orderDate;
//        Rows.Add(row);
//        return row;
//    }

//    protected override Type GetRowType() => typeof(OrdersRow);
//    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => new OrdersRow(builder);
//}

//public class OrdersRow : DataRow
//{
//    internal OrdersRow(DataRowBuilder builder) : base(builder) { }

//    public int OrderID
//    {
//        get => (int)this["OrderID"];
//        set => this["OrderID"] = value;
//    }

//    public int ProductID
//    {
//        get => (int)this["ProductID"];
//        set => this["ProductID"] = value;
//    }

//    public int Quantity
//    {
//        get => (int)this["Quantity"];
//        set => this["Quantity"] = value;
//    }

//    public DateTime OrderDate
//    {
//        get => (DateTime)this["OrderDate"];
//        set => this["OrderDate"] = value;
//    }

//    public ProductsRow GetProductRow()
//    {
//        return GetParentRow("ProductOrders") as ProductsRow;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        try
//        {
//            var ds = new ShopDataSet();

//            ds.Categories.AddCategory(1, "Электроника");
//            ds.Categories.AddCategory(2, "Книги");
//            ds.Categories.AddCategory(3, "Одежда");

//            ds.Products.AddProduct(1, "Ноутбук", 1, 85000m);
//            ds.Products.AddProduct(2, "Смартфон", 1, 120000m);
//            ds.Products.AddProduct(3, "1984", 2, 500m);
//            ds.Products.AddProduct(4, "Футболка", 3, 1500m);

//            ds.Orders.AddOrder(1, 1, 2, new DateTime(2025, 1, 15));
//            ds.Orders.AddOrder(2, 1, 1, new DateTime(2025, 2, 20));
//            ds.Orders.AddOrder(3, 2, 3, new DateTime(2025, 3, 10));
//            ds.Orders.AddOrder(4, 3, 5, new DateTime(2025, 4, 5));
//            ds.Orders.AddOrder(5, 4, 10, new DateTime(2025, 5, 1));

//            Console.WriteLine("Иерархия данных:");
//            foreach (CategoriesRow category in ds.Categories.Rows)
//            {
//                Console.WriteLine($"Категория: {category.CategoryID} - {category.CategoryName}");
//                Console.WriteLine($"  Средняя цена товаров: {ds.GetAveragePriceByCategory(category.CategoryID):C}");
//                Console.WriteLine($"  Общая сумма продаж: {ds.GetTotalSalesByCategory(category.CategoryID):C}");

//                foreach (ProductsRow product in category.GetProductsRows())
//                {
//                    Console.WriteLine($"    Товар: {product.ProductID} - {product.ProductName} ({product.Price:C})");
//                    Console.WriteLine($"      Категория родитель: {product.GetCategoryRow().CategoryName}");

//                    foreach (OrdersRow order in product.GetOrdersRows())
//                    {
//                        Console.WriteLine($"        Заказ: {order.OrderID} - Кол-во: {order.Quantity}, Дата: {order.OrderDate:yyyy-MM-dd}");
//                        Console.WriteLine($"          Товар родитель: {order.GetProductRow().ProductName}");
//                    }
//                }
//                Console.WriteLine();
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ошибка: " + ex.Message);
//        }

//        Console.ReadKey();
//    }
//}

//12
//using System;

//namespace TableAdapterDemo
//{
//    class Program
//    {
//        static void Main()
//        {
//            var ds = new LibraryDataSet();
//            var adapter = new LibraryDataSetTableAdapters.BooksTableAdapter();

//            try
//            {
//                Console.WriteLine("Загрузка всех книг (Fill):");
//                adapter.Fill(ds.Books);
//                PrintBooks(ds.Books);

//                Console.WriteLine("\nПолучение данных (GetData):");
//                var allBooks = adapter.GetData();
//                foreach (var row in allBooks)
//                    Console.WriteLine($"{row.BookID} | {row.Title} | {row.Author} | {row.Year} | {row.Price:C}");

//                Console.WriteLine("\nФильтрация: книги после 1950 года");
//                var modernBooks = adapter.GetDataByYear(1950);
//                foreach (var row in modernBooks)
//                    Console.WriteLine($"{row.Title} ({row.Year})");

//                Console.WriteLine("\nДобавление новой книги...");
//                int newId = adapter.Insert("Dune", "Frank Herbert", 1965, 890.00m);
//                adapter.Fill(ds.Books);
//                Console.WriteLine($"Добавлена книга с ID = {newId}");

//                Console.WriteLine("\nОбновление цены книги ID=1...");
//                adapter.UpdatePrice(999.00m, 1);
//                adapter.Fill(ds.Books);
//                PrintBooks(ds.Books);

//                Console.WriteLine("\nУдаление книги ID=3...");
//                adapter.Delete(3);
//                adapter.Fill(ds.Books);
//                PrintBooks(ds.Books);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Ошибка БД: " + ex.Message);
//            }

//            Console.WriteLine("\nГотово. Нажмите любую клавишу...");
//            Console.ReadKey();
//        }

//        static void PrintBooks(LibraryDataSet.BooksDataTable books)
//        {
//            Console.WriteLine(new string('-', 90));
//            Console.WriteLine("| {0,-4} | {1,-25} | {2,-20} | {3,-6} | {4,-10} |", "ID", "Название", "Автор", "Год", "Цена");
//            Console.WriteLine(new string('-', 90));
//            foreach (var book in books)
//            {
//                Console.WriteLine("| {0,-4} | {1,-25} | {2,-20} | {3,-6} | {4,-10:C} |",
//                    book.BookID, book.Title, book.Author, book.Year, book.Price);
//            }
//            Console.WriteLine(new string('-', 90));
//        }
//    }
//}

//13
//using System;
//using System.Diagnostics;

//namespace TableAdapterFillDemo
//{
//    class Program
//    {
//        static readonly string logFile = "TableAdapterLog.txt";
//        static void Log(string message)
//        {
//            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";
//            Console.WriteLine(entry);
//            System.IO.File.AppendAllText(logFile, entry + Environment.NewLine);
//        }

//        static void Main()
//        {
//            var ds = new HRDataSet();
//            var adapter = new HRDataSetTableAdapters.EmployeesTableAdapter();

//            try
//            {
//                Log("=== Начало работы с TableAdapter ===");

//                Log("1. Fill() — загрузка всех сотрудников");
//                var sw = Stopwatch.StartNew();
//                int count1 = adapter.Fill(ds.Employees);
//                sw.Stop();
//                Log($"   Загружено: {count1} строк, время: {sw.ElapsedMilliseconds} мс");

//                PrintEmployees(ds.Employees, "Все сотрудники");

//                ds.Employees.Clear();

//                Log("2. FillByActive() — только активные сотрудники");
//                sw.Restart();
//                int count2 = adapter.FillByActive(ds.Employees);
//                sw.Stop();
//                Log($"   Загружено: {count2} активных, время: {sw.ElapsedMilliseconds} мс");
//                PrintEmployees(ds.Employees, "Активные сотрудники");

//                ds.Employees.Clear();

//                Log("3. FillBySalaryRange(100000, 200000)");
//                sw.Restart();
//                int count3 = adapter.FillBySalaryRange(ds.Employees, 100000m, 200000m);
//                sw.Stop();
//                Log($"   Загружено: {count3} в диапазоне зарплаты, время: {sw.ElapsedMilliseconds} мс");
//                PrintEmployees(ds.Employees, "Зарплата 100–200 тыс.");

//                ds.Employees.Clear();

//                Log("4. FillByPosition('Разработчик') с сортировкой по фамилии");
//                sw.Restart();
//                int count4 = adapter.FillByPosition(ds.Employees, "Разработчик");
//                sw.Stop();
//                Log($"   Загружено: {count4} разработчиков, время: {sw.ElapsedMilliseconds} мс");
//                PrintEmployees(ds.Employees, "Разработчики (отсортированы по фамилии)");
//            }
//            catch (System.Data.SqlClient.SqlException ex)
//            {
//                Log($"ОШИБКА БД: {ex.Message} (Код: {ex.Number})");
//                Console.WriteLine("Не удалось подключиться к базе данных. Проверьте строку подключения.");
//            }
//            catch (Exception ex)
//            {
//                Log($"Критическая ошибка: {ex.Message}");
//                Console.WriteLine("Произошла непредвиденная ошибка.");
//            }

//            Log("=== Работа завершена ===");
//            Console.WriteLine("\nЛог сохранён в: " + System.IO.Path.GetFullPath(logFile));
//            Console.WriteLine("Нажмите любую клавишу...");
//            Console.ReadKey();
//        }

//        static void PrintEmployees(HRDataSet.EmployeesDataTable table, string title)
//        {
//            Console.WriteLine($"\n=== {title} ({table.Rows.Count} чел.) ===");
//            Console.WriteLine(new string('-', 100));
//            Console.WriteLine("| {0,-4} | {1,-20} | {2,-15} | {3,-12} | {4,-10} | {5,-10} |",
//                "ID", "Имя", "Фамилия", "Должность", "Зарплата", "Статус");
//            Console.WriteLine(new string('-', 100));

//            foreach (var emp in table)
//            {
//                string status = emp.IsActive ? "Активен" : "Уволен";
//                Console.WriteLine("| {0,-4} | {1,-20} | {2,-15} | {3,-12} | {4,-10:C} | {5,-10} |",
//                    emp.EmployeeID,
//                    emp.FirstName,
//                    emp.LastName,
//                    emp.Position ?? "(нет)",
//                    emp.Salary,
//                    status);
//            }
//            Console.WriteLine(new string('-', 100));
//        }
//    }
//}

//14
//using System;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;

//namespace TableAdapterGetDataDemo
//{
//    class Program
//    {
//        static void Main()
//        {
//            var ds = new HRDataSet();
//            var adapter = new HRDataSetTableAdapters.EmployeesTableAdapter();

//            try
//            {
//                Stopwatch sw = new Stopwatch();

//                sw.Start();
//                var allData = adapter.GetData();
//                sw.Stop();
//                Console.WriteLine("GetData() - Все записи:");
//                PrintEmployees(allData);
//                Console.WriteLine($"Производительность: {sw.ElapsedMilliseconds} мс, строк: {allData.Rows.Count}");

//                sw.Restart();
//                var byId = adapter.GetDataByEmployeeID(2);
//                sw.Stop();
//                Console.WriteLine("\nGetDataByEmployeeID(2):");
//                PrintEmployees(byId);
//                if (byId.Rows.Count == 0) Console.WriteLine("Запись не найдена.");
//                Console.WriteLine($"Производительность: {sw.ElapsedMilliseconds} мс, строк: {byId.Rows.Count}");

//                sw.Restart();
//                var byPosition = adapter.GetDataByPosition("Разработчик");
//                sw.Stop();
//                Console.WriteLine("\nGetDataByPosition('Разработчик'):");
//                PrintEmployees(byPosition);
//                if (byPosition.Rows.Count == 0) Console.WriteLine("Записи не найдены.");
//                Console.WriteLine($"Производительность: {sw.ElapsedMilliseconds} мс, строк: {byPosition.Rows.Count}");

//                Console.WriteLine("\nПолучение одной записи (ID=1):");
//                var single = GetSingleById(adapter, 1);
//                if (single != null)
//                    Console.WriteLine($"{single.EmployeeID} | {single.FirstName} {single.LastName} | {single.Position} | {single.Salary:C}");
//                else
//                    Console.WriteLine("Запись не найдена.");

//                Console.WriteLine("\nПолучение с фильтром (Зарплата > 120000):");
//                var filtered = GetBySalaryGreaterThan(adapter, 120000m);
//                PrintEmployees(filtered);

//                sw.Restart();
//                var paged = GetPaged(adapter, 2, 2);
//                sw.Stop();
//                Console.WriteLine("\nПолучение с пагинацией (страница 2, размер 2):");
//                PrintEmployees(paged);
//                Console.WriteLine($"Производительность: {sw.ElapsedMilliseconds} мс, строк: {paged.Rows.Count}");

//                Console.WriteLine("\nОтчёт о данных:");
//                if (allData.Rows.Count > 0)
//                {
//                    decimal avgSalary = allData.Average(r => r.Salary);
//                    int activeCount = allData.Count(r => r.IsActive);
//                    Console.WriteLine($"Всего сотрудников: {allData.Rows.Count}");
//                    Console.WriteLine($"Средняя зарплата: {avgSalary:C}");
//                    Console.WriteLine($"Активных: {activeCount}");
//                    Console.WriteLine($"Макс зарплата: {allData.Max(r => r.Salary):C}");
//                }
//                else
//                {
//                    Console.WriteLine("Данные отсутствуют.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Ошибка: " + ex.Message);
//            }

//            Console.ReadKey();
//        }

//        static HRDataSet.EmployeesRow GetSingleById(HRDataSetTableAdapters.EmployeesTableAdapter adapter, int id)
//        {
//            var table = adapter.GetDataByEmployeeID(id);
//            return table.Rows.Count > 0 ? table[0] : null;
//        }

//        static HRDataSet.EmployeesDataTable GetBySalaryGreaterThan(HRDataSetTableAdapters.EmployeesTableAdapter adapter, decimal minSalary)
//        {
//            var all = adapter.GetData();
//            var filtered = all.Where(r => r.Salary > minSalary).CopyToDataTable();
//            return filtered;
//        }

//        static HRDataSet.EmployeesDataTable GetPaged(HRDataSetTableAdapters.EmployeesTableAdapter adapter, int page, int pageSize)
//        {
//            var all = adapter.GetData();
//            var paged = all.Skip((page - 1) * pageSize).Take(pageSize).CopyToDataTable();
//            return paged;
//        }

//        static void PrintEmployees(DataTable table)
//        {
//            Console.WriteLine(new string('-', 100));
//            Console.WriteLine("| {0,-4} | {1,-10} | {2,-10} | {3,-15} | {4,-10} | {5,-10} | {6,-7} |",
//                "ID", "Имя", "Фамилия", "Должность", "Зарплата", "Дата найма", "Активен");
//            Console.WriteLine(new string('-', 100));

//            foreach (HRDataSet.EmployeesRow row in table.Rows)
//            {
//                Console.WriteLine("| {0,-4} | {1,-10} | {2,-10} | {3,-15} | {4,-10:C} | {5,-10:yyyy-MM-dd} | {6,-7} |",
//                    row.EmployeeID, row.FirstName, row.LastName, row.Position, row.Salary, row.HireDate, row.IsActive ? "Да" : "Нет");
//            }
//            Console.WriteLine(new string('-', 100));
//        }
//    }
//}

//15
//using System;
//using System.Data.SqlClient;
//using System.IO;

//namespace TableAdapterInsertDemo
//{
//    class Program
//    {
//        static readonly string logPath = "InsertLog.txt";
//        static HRDataSet ds = new HRDataSet();
//        static HRDataSetTableAdapters.EmployeesTableAdapter adapter = new HRDataSetTableAdapters.EmployeesTableAdapter();

//        static void Log(string message)
//        {
//            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";
//            Console.WriteLine(entry);
//            File.AppendAllText(logPath, entry + Environment.NewLine);
//        }

//        static void Main()
//        {
//            Log("=== Начало добавления сотрудников через TableAdapter ===");

//            try
//            {
//                Log("Добавление одного сотрудника...");
//                int id1 = AddEmployee("Алексей", "Смирнов", "alex.smirnov@company.com", "Старший разработчик", 180000m);
//                Log($"Успешно добавлен сотрудник ID = {id1}");

//                Log("Массовое добавление 3 сотрудников...");
//                var employeesToAdd = new[]
//                {
//                    ("Ольга", "Козлова", "olga.kozlova@company.com", "HR-менеджер", 110000m),
//                    ("Дмитрий", "Новиков", "dmitry.novikov@company.com", "Тестировщик", 95000m),
//                    ("Екатерина", "Фёдорова", "ekaterina.fedorova@company.com", "Аналитик", 130000m)
//                };

//                int addedCount = 0;
//                foreach (var emp in employeesToAdd)
//                {
//                    try
//                    {
//                        int id = AddEmployee(emp.Item1, emp.Item2, emp.Item3, emp.Item4, emp.Item5);
//                        Log($"  Добавлен: {emp.Item1} {emp.Item2}, ID = {id}");
//                        addedCount++;
//                    }
//                    catch (Exception ex)
//                    {
//                        Log($"  ОШИБКА при добавлении {emp.Item1} {emp.Item2}: {ex.Message}");
//                    }
//                }
//                Log($"Массовое добавление завершено: {addedCount} из {employeesToAdd.Length}");

//                Log("Попытка добавить дубликат Email...");
//                try
//                {
//                    AddEmployee("Иван", "Петров", "alex.smirnov@company.com", "Разработчик", 140000m);
//                }
//                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
//                {
//                    Log($"ОШИБКА: Нарушение уникальности Email (код {ex.Number}) — поймано!");
//                }

//                Log("Попытка добавить с зарплатой ниже минимальной...");
//                try
//                {
//                    AddEmployee("Тест", "Тестов", "test@company.com", "Стажёр", 20000m);
//                }
//                catch (SqlException ex) when (ex.Number == 547)
//                {
//                    Log($"ОШИБКА: Нарушение CHECK constraint (зарплата < 30000) — поймано!");
//                }

//                Log("Попытка добавить с некорректным Email...");
//                try
//                {
//                    AddEmployeeValidated("Михаил", "Иванов", "invalid-email", "Менеджер", 120000m);
//                }
//                catch (ArgumentException ex)
//                {
//                    Log($"ВАЛИДАЦИЯ: {ex.Message} — отклонено на уровне приложения");
//                }

//                adapter.Fill(ds.Employees);
//                Log($"ИТОГО в базе: {ds.Employees.Count} сотрудников");
//                PrintEmployees(ds.Employees);
//            }
//            catch (Exception ex)
//            {
//                Log($"КРИТИЧЕСКАЯ ОШИБКА: {ex.Message}");
//            }

//            Log("=== Добавление завершено ===");
//            Console.WriteLine($"\nЛог сохранён: {Path.GetFullPath(logPath)}");
//            Console.WriteLine("Нажмите любую клавишу...");
//            Console.ReadKey();
//        }

//        static int AddEmployeeValidated(string firstName, string lastName, string email, string position, decimal salary)
//        {
//            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("Имя обязательно");
//            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Фамилия обязательна");
//            if (!email.Contains("@") || !email.Contains(".")) throw new ArgumentException("Неверный формат Email");
//            if (salary < 30000m) throw new ArgumentException("Зарплата не может быть меньше 30 000");

//            return adapter.Insert(firstName, lastName, email, position, salary, DateTime.Today, true);
//        }

//        static int AddEmployee(string firstName, string lastName, string email, string position, decimal salary)
//        {
//            return AddEmployeeValidated(firstName, lastName, email, position, salary);
//        }

//        static void PrintEmployees(HRDataSet.EmployeesDataTable table)
//        {
//            Console.WriteLine("\n=== Сотрудники в базе ===");
//            Console.WriteLine(new string('-', 110));
//            Console.WriteLine("| {0,-4} | {1,-12} | {2,-12} | {3,-30} | {4,-15} | {5,-12} |",
//                "ID", "Имя", "Фамилия", "Email", "Должность", "Зарплата");
//            Console.WriteLine(new string('-', 110));

//            foreach (var emp in table)
//            {
//                Console.WriteLine("| {0,-4} | {1,-12} | {2,-12} | {3,-30} | {4,-15} | {5,-12:C} |",
//                    emp.EmployeeID, emp.FirstName, emp.LastName, emp.Email, emp.Position ?? "(нет)", emp.Salary);
//            }
//            Console.WriteLine(new string('-', 110));
//        }
//    }
//}

//16
//using System;
//using System.ComponentModel;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Formats.Asn1;
//using System.Reflection;
//using System.Xml;

//public class EmployeeTableAdapter
//{
//    private string connectionString;

//    public EmployeeTableAdapter(string connectionString)
//    {
//        this.connectionString = connectionString;
//    }

//    Загрузка данных в DataTable
//    public void Fill(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//    }

//    Обновление данных в базе
//    public void Update(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);

//            Настройка команд для обновления
//            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//            adapter.UpdateCommand = builder.GetUpdateCommand();
//            adapter.InsertCommand = builder.GetInsertCommand();
//            adapter.DeleteCommand = builder.GetDeleteCommand();

//            adapter.Update(employees);
//        }
//    }
//}

//Пример использования для редактирования данных
//using System;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeDataSet employeeDataSet = new EmployeeDataSet();
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        Загрузка данных
//        adapter.Fill(employeeDataSet.Employees);

//        Редактирование одной записи
//       EmployeeRow employee = employeeDataSet.Employees.FindByID(1);
//        if (employee != null)
//        {
//            employee.Name = "Иван Иванов";
//            employee.Salary = 50000;
//        }

//        Массовое редактирование
//        foreach (EmployeeRow emp in employeeDataSet.Employees.GetActiveEmployees())
//        {
//            emp.Salary *= 1.1m; // Увеличение зарплаты на 10%
//        }

//        Отчёт об изменениях
//        Console.WriteLine("Изменения перед сохранением:");
//        foreach (EmployeeRow emp in employeeDataSet.Employees.GetActiveEmployees())
//        {
//            if (emp.RowState == DataRowState.Modified)
//                Console.WriteLine($"ID: {emp.EmployeeID}, Новое имя: {emp.Name}, Новая зарплата: {emp.Salary:C}");
//        }

//        Сохранение изменений
//        adapter.Update(employeeDataSet.Employees);
//        employeeDataSet.AcceptChanges();

//        Console.WriteLine("Изменения сохранены!");
//    }
//}



//17
//using System;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeDataSet employeeDataSet = new EmployeeDataSet();
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        Загрузка данных
//        adapter.Fill(employeeDataSet.Employees);

//        Удаление одной записи
//       EmployeeRow employee = employeeDataSet.Employees.FindByID(2);
//        if (employee != null)
//        {
//            employee.Row.Delete();
//        }

//        Массовое удаление
//        foreach (EmployeeRow emp in employeeDataSet.Employees.GetByDepartment("HR"))
//        {
//            emp.Row.Delete();
//        }

//        Отчёт об удалённых данных
//        Console.WriteLine("Удаленные записи:");
//        foreach (DataRow row in employeeDataSet.Employees.Select(null, null, DataViewRowState.Deleted))
//        {
//            Console.WriteLine($"ID: {row["EmployeeID", DataRowVersion.Original]}");
//        }

//        Сохранение удалений
//        adapter.Update(employeeDataSet.Employees);
//        employeeDataSet.AcceptChanges();

//        Console.WriteLine("Удаления сохранены!");
//    }
//}


//18
//using System;
//using System.Diagnostics;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeDataSet employeeDataSet = new EmployeeDataSet();
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        Загрузка данных
//        adapter.Fill(employeeDataSet.Employees);

//        Добавление новой записи
//        employeeDataSet.Employees.AddEmployeeRow(
//            "Петр Петров",
//            "IT",
//            60000,
//            DateTime.Now,
//            "petr.petrov@example.com",
//            "Active"
//        );

//        Редактирование записи
//        EmployeeRow employee = employeeDataSet.Employees.FindByID(3);
//        if (employee != null)
//        {
//            employee.Department = "Finance";
//        }

//        Удаление записи
//        employee = employeeDataSet.Employees.FindByID(4);
//        if (employee != null)
//        {
//            employee.Row.Delete();
//        }

//        Проверка изменений
//        DataTable changes = employeeDataSet.Employees.GetChanges();
//        if (changes != null)
//        {
//            Console.WriteLine($"Обнаружено {changes.Rows.Count} изменений.");
//            foreach (DataRow row in changes.Rows)
//            {
//                Console.WriteLine($"ID: {row["EmployeeID"]}, RowState: {row.RowState}");
//            }
//        }

//        Синхронизация с БД
//       Stopwatch stopwatch = Stopwatch.StartNew();
//        adapter.Update(employeeDataSet.Employees);
//        stopwatch.Stop();

//        employeeDataSet.AcceptChanges();
//        Console.WriteLine($"Синхронизация завершена за {stopwatch.ElapsedMilliseconds} мс!");
//    }
//}


//19
//using System;
//using System.Data;
//using System.Data.SqlClient;

//public class EmployeeTableAdapter
//{
//    private string connectionString;

//    public EmployeeTableAdapter(string connectionString)
//    {
//        this.connectionString = connectionString;
//    }

//    Загрузка всех сотрудников
//    public void Fill(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//    }

//    Обновление данных в базе
//    public void Update(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//            adapter.UpdateCommand = builder.GetUpdateCommand();
//            adapter.InsertCommand = builder.GetInsertCommand();
//            adapter.DeleteCommand = builder.GetDeleteCommand();
//            adapter.Update(employees);
//        }
//    }

//    Метод: Получить сотрудников по отделу
//    public EmployeesDataTable GetEmployeesByDepartment(string department)
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT * FROM Employees WHERE Department = @Department",
//                connection);
//            command.Parameters.AddWithValue("@Department", department);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//        return employees;
//    }

//    Метод: Получить сотрудников по датам найма
//    public EmployeesDataTable GetEmployeesByHireDate(DateTime fromDate, DateTime toDate)
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT * FROM Employees WHERE HireDate BETWEEN @FromDate AND @ToDate",
//                connection);
//            command.Parameters.AddWithValue("@FromDate", fromDate);
//            command.Parameters.AddWithValue("@ToDate", toDate);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//        return employees;
//    }

//    Метод: Получить топ сотрудников по зарплате
//    public EmployeesDataTable GetTopSalaryEmployees(int count)
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT TOP (@Count) * FROM Employees ORDER BY Salary DESC",
//                connection);
//            command.Parameters.AddWithValue("@Count", count);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//        return employees;
//    }

//    Метод: Получить количество сотрудников по отделам
//    public DataTable GetEmployeeCountByDepartment()
//    {
//        DataTable result = new DataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT Department, COUNT(*) AS Count FROM Employees GROUP BY Department",
//                connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(result);
//        }
//        return result;
//    }
//}


//Примеры методов использования
//using System;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        try
//        {
//            Пример 1: Получить сотрудников отдела "IT"
//            EmployeesDataTable itEmployees = adapter.GetEmployeesByDepartment("IT");
//            Console.WriteLine("Сотрудники отдела IT:");
//            foreach (EmployeeRow emp in itEmployees.GetActiveEmployees())
//                Console.WriteLine(emp);

//            Пример 2: Получить сотрудников, нанятых с 2020 по 2023 год
//           EmployeesDataTable hiredEmployees = adapter.GetEmployeesByHireDate(
//               new DateTime(2020, 1, 1),
//               new DateTime(2023, 12, 31));
//            Console.WriteLine("\nСотрудники, нанятые с 2020 по 2023:");
//            foreach (EmployeeRow emp in hiredEmployees.GetActiveEmployees())
//                Console.WriteLine(emp);

//            Пример 3: Получить топ-3 сотрудников по зарплате
//           EmployeesDataTable topSalaryEmployees = adapter.GetTopSalaryEmployees(3);
//            Console.WriteLine("\nТоп-3 сотрудников по зарплате:");
//            foreach (EmployeeRow emp in topSalaryEmployees.GetActiveEmployees())
//                Console.WriteLine(emp);

//            Пример 4: Получить количество сотрудников по отделам
//           DataTable departmentCounts = adapter.GetEmployeeCountByDepartment();
//            Console.WriteLine("\nКоличество сотрудников по отделам:");
//            foreach (DataRow row in departmentCounts.Rows)
//                Console.WriteLine($"{row["Department"]}: {row["Count"]}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//    }
//}


//20
//using System;
//using System.Data;
//using System.Windows.Forms;

//namespace EmployeeManagementApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeDataSet employeeDataSet;
//        private EmployeeTableAdapter employeeTableAdapter;
//        private string connectionString = "Your_Connection_String";

//        public MainForm()
//        {
//            InitializeComponent();
//            InitializeDataComponents();
//            LoadData();
//        }

//        private void InitializeDataComponents()
//        {
//            employeeDataSet = new EmployeeDataSet();
//            employeeTableAdapter = new EmployeeTableAdapter(connectionString);
//            dataGridView1.DataSource = employeeDataSet.Employees;
//        }

//        private void LoadData()
//        {
//            try
//            {
//                employeeTableAdapter.Fill(employeeDataSet.Employees);
//                comboBoxDepartment.DataSource = employeeDataSet.Employees
//                    .AsEnumerable()
//                    .Select(row => row.Department)
//                    .Distinct()
//                    .ToList();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        Поиск сотрудников
//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            string searchText = textBoxSearch.Text.Trim();
//            if (!string.IsNullOrEmpty(searchText))
//            {
//                DataRow[] foundRows = employeeDataSet.Employees.Select($"Name LIKE '%{searchText}%'");
//                dataGridView1.DataSource = foundRows.Any() ? foundRows.CopyToDataTable() : null;
//            }
//            else
//            {
//                dataGridView1.DataSource = employeeDataSet.Employees;
//            }
//        }

//        Фильтрация по отделу
//        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            string selectedDepartment = comboBoxDepartment.SelectedValue.ToString();
//            if (!string.IsNullOrEmpty(selectedDepartment))
//            {
//                DataRow[] filteredRows = employeeDataSet.Employees.Select($"Department = '{selectedDepartment}'");
//                dataGridView1.DataSource = filteredRows.Any() ? filteredRows.CopyToDataTable() : null;
//            }
//        }

//        Добавление новой записи
//        private void btnAdd_Click(object sender, EventArgs e)
//        {
//            EmployeeRow newEmployee = employeeDataSet.Employees.AddEmployeeRow(
//                "Новый сотрудник",
//                comboBoxDepartment.SelectedValue.ToString(),
//                0,
//                DateTime.Now,
//                "new@example.com",
//                "Active");
//            dataGridView1.Refresh();
//        }

//        Редактирование выбранной записи
//        private void btnEdit_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                DataGridViewRow row = dataGridView1.CurrentRow;
//                EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                if (employee != null)
//                {
//                    employee.Name = "Измененное имя";
//                    employee.Salary = 50000;
//                }
//            }
//        }

//        Удаление выбранной записи
//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                DataGridViewRow row = dataGridView1.CurrentRow;
//                EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                if (employee != null)
//                {
//                    employee.Row.Delete();
//                }
//            }
//        }

//        Сохранение изменений
//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                employeeTableAdapter.Update(employeeDataSet.Employees);
//                employeeDataSet.AcceptChanges();
//                MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                employeeDataSet.RejectChanges();
//            }
//        }
//    }
//}


//21
//EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;
//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


//EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnDepartment = new DataColumn("Department", typeof(string)) { AllowDBNull = false, MaxLength = 50 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };
//        DataColumn columnHireDate = new DataColumn("HireDate", typeof(DateTime)) { AllowDBNull = false };
//        DataColumn columnEmail = new DataColumn("Email", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnStatus = new DataColumn("Status", typeof(string)) { AllowDBNull = false, DefaultValue = "Active" };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnName, columnDepartment, columnSalary, columnHireDate, columnEmail, columnStatus });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(string name, string department, decimal salary, DateTime hireDate, string email, string status)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.Name = name;
//        row.Department = department;
//        row.Salary = salary;
//        row.HireDate = hireDate;
//        row.Email = email;
//        row.Status = status;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public EmployeeRow FindByID(int employeeID)
//    {
//        DataRow row = this.Rows.Find(employeeID);
//        return row != null ? new EmployeeRow(row) : null;
//    }
//}


//EmployeeRow.cs
//using System;
//using System.Data;

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }

//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public string Department { get => (string)row["Department"]; set => row["Department"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DateTime HireDate { get => (DateTime)row["HireDate"]; set => row["HireDate"] = value; }
//    public string Email { get => (string)row["Email"]; set => row["Email"] = value; }
//    public string Status { get => (string)row["Status"]; set => row["Status"] = value; }
//    public DataRow Row => row;
//}


//EmployeeTableAdapter.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;

//public class EmployeeTableAdapter
//{
//    private string connectionString;

//    public EmployeeTableAdapter(string connectionString)
//    {
//        this.connectionString = connectionString;
//    }

//    public void Fill(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//    }

//    public void Update(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//            adapter.UpdateCommand = builder.GetUpdateCommand();
//            adapter.InsertCommand = builder.GetInsertCommand();
//            adapter.DeleteCommand = builder.GetDeleteCommand();
//            adapter.Update(employees);
//        }
//    }
//}


//EmployeeService.cs
//using System;
//using System.Data;

//public class EmployeeService
//{
//    private EmployeeTableAdapter employeeTableAdapter;

//    public EmployeeService(string connectionString)
//    {
//        employeeTableAdapter = new EmployeeTableAdapter(connectionString);
//    }

//    public EmployeesDataTable GetAllEmployees()
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        employeeTableAdapter.Fill(employees);
//        return employees;
//    }

//    public void SaveEmployees(EmployeesDataTable employees)
//    {
//        employeeTableAdapter.Update(employees);
//    }

//    public void ValidateEmployee(EmployeeRow employee)
//    {
//        if (employee.Salary < 0)
//            throw new ArgumentException("Зарплата не может быть отрицательной.");
//        if (string.IsNullOrEmpty(employee.Email) || !employee.Email.Contains("@"))
//            throw new ArgumentException("Некорректный email.");
//    }
//}


//MainForm.cs
//using System;
//using System.Windows.Forms;

//namespace EmployeeManagementApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeService employeeService;
//        private EmployeeDataSet employeeDataSet;

//        public MainForm()
//        {
//            InitializeComponent();
//            string connectionString = "Your_Connection_String";
//            employeeService = new EmployeeService(connectionString);
//            employeeDataSet = new EmployeeDataSet();
//            LoadData();
//        }

//        private void LoadData()
//        {
//            try
//            {
//                employeeDataSet.Employees.Clear();
//                EmployeesDataTable employees = employeeService.GetAllEmployees();
//                foreach (EmployeeRow employee in employees)
//                    employeeDataSet.Employees.ImportRow(employee.Row);
//                dataGridView1.DataSource = employeeDataSet.Employees;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnAdd_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                EmployeeRow newEmployee = employeeDataSet.Employees.AddEmployeeRow(
//                    "Новый сотрудник",
//                    "IT",
//                    50000,
//                    DateTime.Now,
//                    "new@example.com",
//                    "Active");
//                dataGridView1.Refresh();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnEdit_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                try
//                {
//                    DataGridViewRow row = dataGridView1.CurrentRow;
//                    EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                    if (employee != null)
//                    {
//                        employee.Name = "Измененное имя";
//                        employee.Salary = 60000;
//                        employeeService.ValidateEmployee(employee);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка редактирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                try
//                {
//                    DataGridViewRow row = dataGridView1.CurrentRow;
//                    EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                    if (employee != null)
//                        employee.Row.Delete();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                employeeService.SaveEmployees(employeeDataSet.Employees);
//                MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}


//22
//OrderDataSet.cs
//using System;
//using System.Data;

//public partial class OrderDataSet : DataSet
//{
//    private OrdersDataTable tableOrders;
//    private CustomersDataTable tableCustomers;

//    public OrdersDataTable Orders => tableOrders;
//    public CustomersDataTable Customers => tableCustomers;

//    public OrderDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableOrders = new OrdersDataTable("Orders");
//        tableCustomers = new CustomersDataTable("Customers");
//        this.Tables.Add(tableOrders);
//        this.Tables.Add(tableCustomers);
//    }
//}


//OrdersDataTable.cs
//using System;
//using System.Data;

//public partial class OrdersDataTable : DataTable
//{
//    public OrdersDataTable() : base("Orders") { InitializeColumns(); }
//    public OrdersDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnOrderID = new DataColumn("OrderID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnCustomerID = new DataColumn("CustomerID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnAmount = new DataColumn("Amount", typeof(decimal)) { AllowDBNull = false };
//        DataColumn columnOrderDate = new DataColumn("OrderDate", typeof(DateTime)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnOrderID, columnCustomerID, columnAmount, columnOrderDate });
//        this.PrimaryKey = new DataColumn[] { columnOrderID };
//    }
//}


//CustomersDataTable.cs
//using System;
//using System.Data;

//public partial class CustomersDataTable : DataTable
//{
//    public CustomersDataTable() : base("Customers") { InitializeColumns(); }
//    public CustomersDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnCustomerID = new DataColumn("CustomerID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnCustomerID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnCustomerID };
//    }
//}


//OrderService.cs
//using System;
//using System.Data;
//using System.Linq;

//public class OrderService
//{
//    private OrderDataSet orderDataSet;

//    public OrderService()
//    {
//        orderDataSet = new OrderDataSet();
//    }

//    public DataTable GetTotalOrdersByCustomer()
//    {
//        var query = from order in orderDataSet.Orders.AsEnumerable()
//                    join customer in orderDataSet.Customers.AsEnumerable()
//                    on order.Field<int>("CustomerID") equals customer.Field<int>("CustomerID")
//                    group order by customer.Field<string>("Name") into g
//                    select new
//                    {
//                        CustomerName = g.Key,
//                        TotalOrders = g.Count(),
//                        TotalAmount = g.Sum(o => o.Field<decimal>("Amount"))
//                    };

//        DataTable result = new DataTable();
//        result.Columns.Add("CustomerName", typeof(string));
//        result.Columns.Add("TotalOrders", typeof(int));
//        result.Columns.Add("TotalAmount", typeof(decimal));

//        foreach (var item in query)
//        {
//            result.Rows.Add(item.CustomerName, item.TotalOrders, item.TotalAmount);
//        }

//        return result;
//    }

//    public decimal GetAverageOrderValue()
//    {
//        return orderDataSet.Orders.AsEnumerable().Average(o => o.Field<decimal>("Amount"));
//    }

//    public DataTable GetTopCustomers(int count)
//    {
//        var query = from order in orderDataSet.Orders.AsEnumerable()
//                    join customer in orderDataSet.Customers.AsEnumerable()
//                    on order.Field<int>("CustomerID") equals customer.Field<int>("CustomerID")
//                    group order by customer.Field<string>("Name") into g
//                    orderby g.Sum(o => o.Field<decimal>("Amount")) descending
//                    select new
//                    {
//                        CustomerName = g.Key,
//                        TotalAmount = g.Sum(o => o.Field<decimal>("Amount"))
//                    };

//        DataTable result = new DataTable();
//        result.Columns.Add("CustomerName", typeof(string));
//        result.Columns.Add("TotalAmount", typeof(decimal));

//        foreach (var item in query.Take(count))
//        {
//            result.Rows.Add(item.CustomerName, item.TotalAmount);
//        }

//        return result;
//    }

//    public DataTable GetOrdersByDateRange(DateTime fromDate, DateTime toDate)
//    {
//        var query = from order in orderDataSet.Orders.AsEnumerable()
//                    where order.Field<DateTime>("OrderDate") >= fromDate && order.Field<DateTime>("OrderDate") <= toDate
//                    select order;

//        DataTable result = query.Any() ? query.CopyToDataTable() : new DataTable();
//        return result;
//    }
//}


//Programm.cs
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        OrderService orderService = new OrderService();

//        Пример 1: Общее количество заказов по клиентам
//       DataTable totalOrdersByCustomer = orderService.GetTotalOrdersByCustomer();
//        Console.WriteLine("Общее количество заказов по клиентам:");
//        foreach (DataRow row in totalOrdersByCustomer.Rows)
//            Console.WriteLine($"{row["CustomerName"]}: {row["TotalOrders"]} заказов на сумму {row["TotalAmount"]:C}");

//        Пример 2: Средняя стоимость заказа
//        decimal averageOrderValue = orderService.GetAverageOrderValue();
//        Console.WriteLine($"\nСредняя стоимость заказа: {averageOrderValue:C}");

//        Пример 3: Топ - 3 клиентов по сумме заказов
//        DataTable topCustomers = orderService.GetTopCustomers(3);
//        Console.WriteLine("\nТоп-3 клиентов по сумме заказов:");
//        foreach (DataRow row in topCustomers.Rows)
//            Console.WriteLine($"{row["CustomerName"]}: {row["TotalAmount"]:C}");

//        Пример 4: Заказы за последний месяц
//        DataTable ordersLastMonth = orderService.GetOrdersByDateRange(
//            DateTime.Now.AddMonths(-1),
//            DateTime.Now);
//        Console.WriteLine("\nЗаказы за последний месяц:");
//        foreach (DataRow row in ordersLastMonth.Rows)
//            Console.WriteLine($"ID заказа: {row["OrderID"]}, Сумма: {row["Amount"]:C}, Дата: {row["OrderDate"]:d}");
//    }
//}


//23
//CompanyDataSet.cs
//using System;
//using System.Data;

//public partial class CompanyDataSet : DataSet
//{
//    private CompaniesDataTable tableCompanies;
//    private DepartmentsDataTable tableDepartments;
//    private EmployeesDataTable tableEmployees;

//    public CompaniesDataTable Companies => tableCompanies;
//    public DepartmentsDataTable Departments => tableDepartments;
//    public EmployeesDataTable Employees => tableEmployees;

//    public CompanyDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableCompanies = new CompaniesDataTable("Companies");
//        tableDepartments = new DepartmentsDataTable("Departments");
//        tableEmployees = new EmployeesDataTable("Employees");

//        this.Tables.Add(tableCompanies);
//        this.Tables.Add(tableDepartments);
//        this.Tables.Add(tableEmployees);

//        Создание отношений
//        DataRelation companyDepartmentRelation = new DataRelation(
//            "CompanyDepartments",
//            tableCompanies.Columns["CompanyID"],
//            tableDepartments.Columns["CompanyID"]);
//        this.Relations.Add(companyDepartmentRelation);

//        DataRelation departmentEmployeeRelation = new DataRelation(
//            "DepartmentEmployees",
//            tableDepartments.Columns["DepartmentID"],
//            tableEmployees.Columns["DepartmentID"]);
//        this.Relations.Add(departmentEmployeeRelation);
//    }
//}


//CompaniesDataTable.cs
//using System;
//using System.Data;

//public partial class CompaniesDataTable : DataTable
//{
//    public CompaniesDataTable() : base("Companies") { InitializeColumns(); }
//    public CompaniesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnCompanyID = new DataColumn("CompanyID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnCompanyID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnCompanyID };
//    }

//    public CompanyRow AddCompanyRow(string name)
//    {
//        CompanyRow row = new CompanyRow(this.NewRow());
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public CompanyRow FindByID(int companyID)
//    {
//        DataRow dataRow = this.Rows.Find(companyID);
//        return dataRow != null ? new CompanyRow(dataRow) : null;
//    }
//}


//DepartmentsDataTable.cs
//using System;
//using System.Data;

//public partial class DepartmentsDataTable : DataTable
//{
//    public DepartmentsDataTable() : base("Departments") { InitializeColumns(); }
//    public DepartmentsDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnCompanyID = new DataColumn("CompanyID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnDepartmentID, columnCompanyID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnDepartmentID };
//    }

//    public DepartmentRow AddDepartmentRow(int companyID, string name)
//    {
//        DepartmentRow row = new DepartmentRow(this.NewRow());
//        row.CompanyID = companyID;
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public DepartmentRow[] GetDepartmentsByCompany(int companyID)
//    {
//        DataRow[] rows = this.Select($"CompanyID = {companyID}");
//        DepartmentRow[] result = new DepartmentRow[rows.Length];
//        for (int i = 0; i < rows.Length; i++)
//            result[i] = new DepartmentRow(rows[i]);
//        return result;
//    }
//}


//EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnDepartmentID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(int departmentID, string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.DepartmentID = departmentID;
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public EmployeeRow[] GetEmployeesByDepartment(int departmentID)
//    {
//        DataRow[] rows = this.Select($"DepartmentID = {departmentID}");
//        EmployeeRow[] result = new EmployeeRow[rows.Length];
//        for (int i = 0; i < rows.Length; i++)
//            result[i] = new EmployeeRow(rows[i]);
//        return result;
//    }
//}


//Классы строк(CompanyRow, DepartmentRow, СотрудникRow)
//public class CompanyRow
//{
//    private DataRow row;
//    public CompanyRow(DataRow row) { this.row = row; }
//    public int CompanyID { get => (int)row["CompanyID"]; set => row["CompanyID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public DataRow Row => row;
//}

//public class DepartmentRow
//{
//    private DataRow row;
//    public DepartmentRow(DataRow row) { this.row = row; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public int CompanyID { get => (int)row["CompanyID"]; set => row["CompanyID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public DataRow Row => row;
//}

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }
//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DataRow Row => row;
//}


//CompanyService.cs
//using System;
//using System.Data;

//public class CompanyService
//{
//    private CompanyDataSet companyDataSet;

//    public CompanyService()
//    {
//        companyDataSet = new CompanyDataSet();
//    }

//    public DepartmentRow[] GetDepartmentsByCompany(int companyID)
//    {
//        CompanyRow company = companyDataSet.Companies.FindByID(companyID);
//        if (company == null)
//            throw new ArgumentException("Компания не найдена.");

//        return companyDataSet.Departments.GetDepartmentsByCompany(companyID);
//    }

//    public EmployeeRow[] GetEmployeesByDepartment(int departmentID)
//    {
//        DepartmentRow department = companyDataSet.Departments.FindByID(departmentID);
//        if (department == null)
//            throw new ArgumentException("Отдел не найден.");

//        return companyDataSet.Employees.GetEmployeesByDepartment(departmentID);
//    }

//    public DataTable GetCompanyHierarchy(int companyID)
//    {
//        DataTable hierarchy = new DataTable();
//        hierarchy.Columns.Add("CompanyName", typeof(string));
//        hierarchy.Columns.Add("DepartmentName", typeof(string));
//        hierarchy.Columns.Add("EmployeeName", typeof(string));
//        hierarchy.Columns.Add("Salary", typeof(decimal));

//        CompanyRow company = companyDataSet.Companies.FindByID(companyID);
//        if (company == null)
//            throw new ArgumentException("Компания не найдена.");

//        foreach (DepartmentRow department in GetDepartmentsByCompany(companyID))
//        {
//            foreach (EmployeeRow employee in GetEmployeesByDepartment(department.DepartmentID))
//            {
//                hierarchy.Rows.Add(
//                    company.Name,
//                    department.Name,
//                    employee.Name,
//                    employee.Salary);
//            }
//        }

//        return hierarchy;
//    }

//    public decimal GetTotalSalaryByDepartment(int departmentID)
//    {
//        EmployeeRow[] employees = companyDataSet.Employees.GetEmployeesByDepartment(departmentID);
//        return employees.Sum(e => e.Salary);
//    }

//    public decimal GetTotalSalaryByCompany(int companyID)
//    {
//        DepartmentRow[] departments = GetDepartmentsByCompany(companyID);
//        decimal totalSalary = 0;
//        foreach (DepartmentRow department in departments)
//            totalSalary += GetTotalSalaryByDepartment(department.DepartmentID);
//        return totalSalary;
//    }
//}


//Programm.cs
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        CompanyService companyService = new CompanyService();

//        Пример 1: Получение департаментов по компании
//        try
//        {
//            DepartmentRow[] departments = companyService.GetDepartmentsByCompany(1);
//            Console.WriteLine("Департаменты компании:");
//            foreach (DepartmentRow department in departments)
//                Console.WriteLine($"{department.DepartmentID}: {department.Name}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 2: Получение сотрудников по департаменту
//        try
//        {
//            EmployeeRow[] employees = companyService.GetEmployeesByDepartment(1);
//            Console.WriteLine("\nСотрудники департамента:");
//            foreach (EmployeeRow employee in employees)
//                Console.WriteLine($"{employee.EmployeeID}: {employee.Name}, Зарплата: {employee.Salary:C}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 3: Получение иерархии компании
//        try
//        {
//            DataTable hierarchy = companyService.GetCompanyHierarchy(1);
//            Console.WriteLine("\nИерархия компании:");
//            foreach (DataRow row in hierarchy.Rows)
//                Console.WriteLine($"{row["CompanyName"]} -> {row["DepartmentName"]} -> {row["EmployeeName"]} ({row["Salary"]:C})");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 4: Агрегация зарплат по департаменту
//        try
//        {
//            decimal totalSalary = companyService.GetTotalSalaryByDepartment(1);
//            Console.WriteLine($"\nОбщая зарплата по департаменту: {totalSalary:C}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 5: Агрегация зарплат по компании
//        try
//        {
//            decimal totalSalary = companyService.GetTotalSalaryByCompany(1);
//            Console.WriteLine($"\nОбщая зарплата по компании: {totalSalary:C}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//    }
//}


//24
//OrderDataSet.cs
//using System;
//using System.Data;

//public partial class OrderDataSet : DataSet
//{
//    private OrdersDataTable tableOrders;

//    public OrdersDataTable Orders => tableOrders;

//    public OrderDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableOrders = new OrdersDataTable("Orders");
//        this.Tables.Add(tableOrders);
//    }
//}


//OrdersDataTable.cs
//using System;
//using System.Data;

//public partial class OrdersDataTable : DataTable
//{
//    public OrdersDataTable() : base("Orders") { InitializeColumns(); }
//    public OrdersDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnOrderID = new DataColumn("OrderID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnQuantity = new DataColumn("Quantity", typeof(int)) { AllowDBNull = false };
//        DataColumn columnPrice = new DataColumn("Price", typeof(decimal)) { AllowDBNull = false };

//        Вычисляемые поля
//        DataColumn columnTotal = new DataColumn("Total", typeof(decimal), "Quantity * Price");
//        DataColumn columnTax = new DataColumn("Tax", typeof(decimal), "Total * 0.18");
//        DataColumn columnTotalWithTax = new DataColumn("TotalWithTax", typeof(decimal), "Total + Tax");

//        this.Columns.AddRange(new DataColumn[] { columnOrderID, columnQuantity, columnPrice, columnTotal, columnTax, columnTotalWithTax });
//        this.PrimaryKey = new DataColumn[] { columnOrderID };
//    }

//    public OrderRow AddOrderRow(int quantity, decimal price)
//    {
//        OrderRow row = new OrderRow(this.NewRow());
//        row.Quantity = quantity;
//        row.Price = price;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


//OrderRow.cs
//using System;
//using System.Data;

//public class OrderRow
//{
//    private DataRow row;
//    public OrderRow(DataRow row) { this.row = row; }

//    public int OrderID { get => (int)row["OrderID"]; set => row["OrderID"] = value; }
//    public int Quantity { get => (int)row["Quantity"]; set => row["Quantity"] = value; }
//    public decimal Price { get => (decimal)row["Price"]; set => row["Price"] = value; }
//    public decimal Total => (decimal)row["Total"];
//    public decimal Tax => (decimal)row["Tax"];
//    public decimal TotalWithTax => (decimal)row["TotalWithTax"];
//    public DataRow Row => row;
//}


//OrderService.cs
//using System;
//using System.Data;

//public class OrderService
//{
//    private OrderDataSet orderDataSet;

//    public OrderService()
//    {
//        orderDataSet = new OrderDataSet();
//    }

//    public void AddOrder(int quantity, decimal price)
//    {
//        orderDataSet.Orders.AddOrderRow(quantity, price);
//    }

//    public DataTable GetAllOrders()
//    {
//        return orderDataSet.Orders;
//    }

//    public decimal GetTotalRevenue()
//    {
//        return orderDataSet.Orders.AsEnumerable().Sum(row => row.Field<decimal>("TotalWithTax"));
//    }

//    public decimal GetTotalTax()
//    {
//        return orderDataSet.Orders.AsEnumerable().Sum(row => row.Field<decimal>("Tax"));
//    }
//}


//Programm.cs
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        OrderService orderService = new OrderService();

//        Пример 1: Добавление заказа
//        orderService.AddOrder(5, 100);
//        orderService.AddOrder(3, 200);

//        Пример 2: Отображение всех заказов с вычисляемыми полями
//        DataTable orders = orderService.GetAllOrders();
//        Console.WriteLine("Список заказов:");
//        foreach (DataRow row in orders.Rows)
//            Console.WriteLine($"ID: {row["OrderID"]}, Количество: {row["Quantity"]}, Цена: {row["Price"]:C}, Итого: {row["Total"]:C}, Налог: {row["Tax"]:C}, Итого с налогом: {row["TotalWithTax"]:C}");

//        Пример 3: Получение общей выручки
//        decimal totalRevenue = orderService.GetTotalRevenue();
//        Console.WriteLine($"\nОбщая выручка: {totalRevenue:C}");

//        Пример 4: Получение общей суммы налогов
//        decimal totalTax = orderService.GetTotalTax();
//        Console.WriteLine($"\nОбщая сумма налогов: {totalTax:C}");
//    }
//}


//25
//EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;
//    private DepartmentsDataTable tableDepartments;

//    public EmployeesDataTable Employees => tableEmployees;
//    public DepartmentsDataTable Departments => tableDepartments;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableDepartments = new DepartmentsDataTable("Departments");
//        tableEmployees = new EmployeesDataTable("Employees");

//        this.Tables.Add(tableDepartments);
//        this.Tables.Add(tableEmployees);

//        Создание отношения с внешним ключом
//       DataRelation departmentEmployeeRelation = new DataRelation(
//           "DepartmentEmployees",
//           tableDepartments.Columns["DepartmentID"],
//           tableEmployees.Columns["DepartmentID"],
//           false); // Не обновлять дочерние записи при изменении родительского ключа
//        this.Relations.Add(departmentEmployeeRelation);
//    }
//}


//DepartmentsDataTable.cs
//using System;
//using System.Data;

//public partial class DepartmentsDataTable : DataTable
//{
//    public DepartmentsDataTable() : base("Departments") { InitializeColumns(); }
//    public DepartmentsDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int))
//        {
//            AllowDBNull = false,
//            AutoIncrement = true,
//            Unique = true // Уникальное ограничение
//        };

//        DataColumn columnName = new DataColumn("Name", typeof(string))
//        {
//            AllowDBNull = false,
//            MaxLength = 100
//        };

//        this.Columns.AddRange(new DataColumn[] { columnDepartmentID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnDepartmentID }; // Первичный ключ
//    }

//    public DepartmentRow AddDepartmentRow(string name)
//    {
//        DepartmentRow row = new DepartmentRow(this.NewRow());
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public DepartmentRow FindByID(int departmentID)
//    {
//        DataRow dataRow = this.Rows.Find(departmentID);
//        return dataRow != null ? new DepartmentRow(dataRow) : null;
//    }
//}


//EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int))
//        {
//            AllowDBNull = false,
//            AutoIncrement = true,
//            Unique = true // Уникальное ограничение
//        };

//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int))
//        {
//            AllowDBNull = false // Внешний ключ, не может быть NULL
//        };

//        DataColumn columnName = new DataColumn("Name", typeof(string))
//        {
//            AllowDBNull = false,
//            MaxLength = 100
//        };

//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal))
//        {
//            AllowDBNull = false,
//            DefaultValue = 0
//        };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnDepartmentID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID }; // Первичный ключ

//    Ограничение: Зарплата не может быть отрицательной
//        this.ColumnChanging += (sender, e) =>
//        {
//            if (e.Column.ColumnName == "Salary" && (decimal)e.ProposedValue < 0)
//                throw new ArgumentException("Зарплата не может быть отрицательной.");
//        };
//    }

//    public EmployeeRow AddEmployeeRow(int departmentID, string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.DepartmentID = departmentID;
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public EmployeeRow FindByID(int employeeID)
//    {
//        DataRow dataRow = this.Rows.Find(employeeID);
//        return dataRow != null ? new EmployeeRow(dataRow) : null;
//    }
//}


//Классы строк(DepartmentRow, СотрудникRow)
//public class DepartmentRow
//{
//    private DataRow row;
//    public DepartmentRow(DataRow row) { this.row = row; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public DataRow Row => row;
//}

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }
//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DataRow Row => row;
//}


//EmployeeService.cs
//using System;
//using System.Data;

//public class EmployeeService
//{
//    private EmployeeDataSet employeeDataSet;

//    public EmployeeService()
//    {
//        employeeDataSet = new EmployeeDataSet();
//    }

//    public void AddDepartment(string name)
//    {
//        employeeDataSet.Departments.AddDepartmentRow(name);
//    }

//    public void AddEmployee(int departmentID, string name, decimal salary)
//    {
//        Проверка существования отдела
//        if (employeeDataSet.Departments.FindByID(departmentID) == null)
//            throw new ArgumentException("Отдел не найден.");

//        employeeDataSet.Employees.AddEmployeeRow(departmentID, name, salary);
//    }

//    public void ValidateEmployee(EmployeeRow employee)
//    {
//        if (employee.Salary < 0)
//            throw new ArgumentException("Зарплата не может быть отрицательной.");
//    }

//    public void PrintConstraintsReport()
//    {
//        Console.WriteLine("Отчёт об ограничениях:");
//        Console.WriteLine("1. Первичный ключ: Employees.EmployeeID, Departments.DepartmentID");
//        Console.WriteLine("2. Внешний ключ: Employees.DepartmentID -> Departments.DepartmentID");
//        Console.WriteLine("3. Уникальное ограничение: Employees.EmployeeID, Departments.DepartmentID");
//        Console.WriteLine("4. Ограничение NOT NULL: Employees.DepartmentID, Employees.Name, Employees.Salary");
//        Console.WriteLine("5. Ограничение на значение: Employees.Salary >= 0");
//    }
//}


//Programm.cs
//using System;

//class Program
//{
//    static void Main()
//    {
//        EmployeeService employeeService = new EmployeeService();

//        Пример 1: Добавление отдела
//        try
//        {
//            employeeService.AddDepartment("IT");
//            employeeService.AddDepartment("HR");
//            Console.WriteLine("Отделы добавлены.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 2: Добавление сотрудника
//        try
//        {
//            employeeService.AddEmployee(1, "Иван Иванов", 50000);
//            employeeService.AddEmployee(1, "Петр Петров", 60000);
//            Console.WriteLine("Сотрудники добавлены.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 3: Попытка нарушить ограничение(отрицательная зарплата)
//        try
//        {
//            employeeService.AddEmployee(1, "Сидор Сидоров", -1000);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 4: Попытка нарушить внешний ключ(несуществующий отдел)
//        try
//        {
//            employeeService.AddEmployee(999, "Несуществующий сотрудник", 50000);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Пример 5: Отчёт об ограничениях
//        employeeService.PrintConstraintsReport();
//    }
//}


//26
//TypedEmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class TypedEmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public TypedEmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


//UntypedEmployeeDataSet.cs
//using System;
//using System.Data;

//public class UntypedEmployeeDataSet : DataSet
//{
//    public DataTable Employees { get; private set; }

//    public UntypedEmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        Employees = new DataTable("Employees");

//        Employees.Columns.Add("EmployeeID", typeof(int));
//        Employees.Columns.Add("Name", typeof(string));
//        Employees.Columns.Add("Salary", typeof(decimal));

//        Employees.PrimaryKey = new DataColumn[] { Employees.Columns["EmployeeID"] };

//        this.Tables.Add(Employees);
//    }
//}


//PerformanceTest.cs
//using System;
//using System.Diagnostics;
//using System.Data;

//public class PerformanceTest
//{
//    private TypedEmployeeDataSet typedDataSet;
//    private UntypedEmployeeDataSet untypedDataSet;

//    public PerformanceTest()
//    {
//        typedDataSet = new TypedEmployeeDataSet();
//        untypedDataSet = new UntypedEmployeeDataSet();
//    }

//    public void FillDataSets(int recordCount)
//    {
//        Заполнение типизированного DataSet
//        for (int i = 0; i < recordCount; i++)
//        {
//            typedDataSet.Employees.AddEmployeeRow($"Employee {i}", i * 1000);
//        }

//        Заполнение нетипизированного DataSet
//        for (int i = 0; i < recordCount; i++)
//        {
//            DataRow row = untypedDataSet.Employees.NewRow();
//            row["EmployeeID"] = i;
//            row["Name"] = $"Employee {i}";
//            row["Salary"] = i * 1000;
//            untypedDataSet.Employees.Rows.Add(row);
//        }
//    }

//    public void MeasureAccessPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        Типизированный доступ
//        stopwatch.Start();
//        for (int i = 0; i < typedDataSet.Employees.Rows.Count; i++)
//        {
//            string name = typedDataSet.Employees[i].Name;
//            decimal salary = typedDataSet.Employees[i].Salary;
//        }
//        stopwatch.Stop();
//        long typedAccessTime = stopwatch.ElapsedMilliseconds;

//        Нетипизированный доступ
//        stopwatch.Restart();
//        foreach (DataRow row in untypedDataSet.Employees.Rows)
//        {
//            string name = (string)row["Name"];
//            decimal salary = (decimal)row["Salary"];
//        }
//        stopwatch.Stop();
//        long untypedAccessTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Доступ к значениям: Типизированный = {typedAccessTime} мс, Нетипизированный = {untypedAccessTime} мс");
//    }

//    public void MeasureSearchPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        Типизированный поиск
//        stopwatch.Start();
//        var typedResult = typedDataSet.Employees.AsEnumerable()
//            .Where(row => row.Salary > 500000);
//        stopwatch.Stop();
//        long typedSearchTime = stopwatch.ElapsedMilliseconds;

//        Нетипизированный поиск
//        stopwatch.Restart();
//        var untypedResult = untypedDataSet.Employees.AsEnumerable()
//            .Where(row => (decimal)row["Salary"] > 500000);
//        stopwatch.Stop();
//        long untypedSearchTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Поиск по критериям: Типизированный = {typedSearchTime} мс, Нетипизированный = {untypedSearchTime} мс");
//    }

//    public void MeasureAddPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        Типизированное добавление
//        stopwatch.Start();
//        typedDataSet.Employees.AddEmployeeRow("New Employee", 100000);
//        stopwatch.Stop();
//        long typedAddTime = stopwatch.ElapsedMilliseconds;

//        Нетипизированное добавление
//        stopwatch.Restart();
//        DataRow row = untypedDataSet.Employees.NewRow();
//        row["EmployeeID"] = untypedDataSet.Employees.Rows.Count;
//        row["Name"] = "New Employee";
//        row["Salary"] = 100000;
//        untypedDataSet.Employees.Rows.Add(row);
//        stopwatch.Stop();
//        long untypedAddTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Добавление новой строки: Типизированный = {typedAddTime} мс, Нетипизированный = {untypedAddTime} мс");
//    }

//    public void MeasureEditPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        Типизированное редактирование
//        stopwatch.Start();
//        if (typedDataSet.Employees.Rows.Count > 0)
//            typedDataSet.Employees[0].Salary = 200000;
//        stopwatch.Stop();
//        long typedEditTime = stopwatch.ElapsedMilliseconds;

//        Нетипизированное редактирование
//        stopwatch.Restart();
//        if (untypedDataSet.Employees.Rows.Count > 0)
//            untypedDataSet.Employees.Rows[0]["Salary"] = 200000;
//        stopwatch.Stop();
//        long untypedEditTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Редактирование строки: Типизированный = {typedEditTime} мс, Нетипизированный = {untypedEditTime} мс");
//    }

//    public void MeasureDeletePerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        Типизированное удаление
//        stopwatch.Start();
//        if (typedDataSet.Employees.Rows.Count > 0)
//            typedDataSet.Employees[0].Row.Delete();
//        stopwatch.Stop();
//        long typedDeleteTime = stopwatch.ElapsedMilliseconds;

//        Нетипизированное удаление
//        stopwatch.Restart();
//        if (untypedDataSet.Employees.Rows.Count > 0)
//            untypedDataSet.Employees.Rows[0].Delete();
//        stopwatch.Stop();
//        long untypedDeleteTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Удаление строки: Типизированный = {typedDeleteTime} мс, Нетипизированный = {untypedDeleteTime} мс");
//    }
//}


//Programm.cs
//using System;

//class Program
//{
//    static void Main()
//    {
//        PerformanceTest performanceTest = new PerformanceTest();

//        Заполнение DataSet 1,000,000 записями
//        performanceTest.FillDataSets(1000000);

//        Измерение производительности
//        performanceTest.MeasureAccessPerformance();
//        performanceTest.MeasureSearchPerformance();
//        performanceTest.MeasureAddPerformance();
//        performanceTest.MeasureEditPerformance();
//        performanceTest.MeasureDeletePerformance();

//        Таблица сравнения
//        Console.WriteLine("\nТаблица сравнения производительности:");
//        Console.WriteLine("| Операция               | Типизированный (мс) | Нетипизированный (мс) |");
//        Console.WriteLine("|-------------------------|----------------------|------------------------|");
//        Console.WriteLine("| Доступ к значениям     | {0}                    | {1}                     |", GetTime(performanceTest, "Access"));
//        Console.WriteLine("| Поиск по критериям     | {0}                    | {1}                     |", GetTime(performanceTest, "Search"));
//        Console.WriteLine("| Добавление строки      | {0}                    | {1}                     |", GetTime(performanceTest, "Add"));
//        Console.WriteLine("| Редактирование строки  | {0}                    | {1}                     |", GetTime(performanceTest, "Edit"));
//        Console.WriteLine("| Удаление строки        | {0}                    | {1}                     |", GetTime(performanceTest, "Delete"));
//    }

//    private static string GetTime(PerformanceTest test, string operation)
//    {
//        Для упрощения возвращаем фиктивные значения
//        return "N/A";
//    }
//}


//27
//EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


//28
//EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


//EmployeeRow.cs
//using System;
//using System.Data;

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }

//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DataRow Row => row;
//}


//MainForm.cs
//using System;
//using System.ComponentModel;
//using System.Data;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace AsyncDataLoadingApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeDataSet employeeDataSet;
//        private CancellationTokenSource cancellationTokenSource;

//        public MainForm()
//        {
//            InitializeComponent();
//            employeeDataSet = new EmployeeDataSet();
//        }

//        Асинхронная загрузка с использованием Task
//        private async void btnLoadWithTask_Click(object sender, EventArgs e)
//        {
//            btnLoadWithTask.Enabled = false;
//            btnCancel.Enabled = true;
//            progressBar1.Value = 0;
//            cancellationTokenSource = new CancellationTokenSource();

//            try
//            {
//                await LoadDataWithTaskAsync(cancellationTokenSource.Token);
//                dataGridView1.DataSource = employeeDataSet.Employees;
//                MessageBox.Show("Данные загружены успешно!");
//            }
//            catch (OperationCanceledException)
//            {
//                MessageBox.Show("Загрузка отменена пользователем.");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка: {ex.Message}");
//            }
//            finally
//            {
//                btnLoadWithTask.Enabled = true;
//                btnCancel.Enabled = false;
//                cancellationTokenSource.Dispose();
//            }
//        }

//        private async Task LoadDataWithTaskAsync(CancellationToken cancellationToken)
//        {
//            await Task.Run(() =>
//            {
//                for (int i = 0; i < 10000; i++)
//                {
//                    if (cancellationToken.IsCancellationRequested)
//                        cancellationToken.ThrowIfCancellationRequested();

//                    employeeDataSet.Employees.AddEmployeeRow($"Employee {i}", i * 100);
//                    int progress = (i + 1) * 100 / 10000;
//                    this.Invoke((MethodInvoker)delegate { progressBar1.Value = progress; });
//                    Thread.Sleep(1); // Имитация задержки
//                }
//            }, cancellationToken);
//        }

//        Асинхронная загрузка с использованием BackgroundWorker
//        private void btnLoadWithBackgroundWorker_Click(object sender, EventArgs e)
//        {
//            btnLoadWithBackgroundWorker.Enabled = false;
//            btnCancel.Enabled = true;
//            progressBar1.Value = 0;

//            BackgroundWorker worker = new BackgroundWorker
//            {
//                WorkerReportsProgress = true,
//                WorkerSupportsCancellation = true
//            };

//            worker.DoWork += (s, args) =>
//            {
//                BackgroundWorker bw = s as BackgroundWorker;
//                for (int i = 0; i < 10000; i++)
//                {
//                    if (bw.CancellationPending)
//                    {
//                        args.Cancel = true;
//                        return;
//                    }

//                    employeeDataSet.Employees.AddEmployeeRow($"Employee {i}", i * 100);
//                    bw.ReportProgress((i + 1) * 100 / 10000);
//                    Thread.Sleep(1); // Имитация задержки
//                }
//            };

//            worker.ProgressChanged += (s, args) =>
//            {
//                progressBar1.Value = args.ProgressPercentage;
//            };

//            worker.RunWorkerCompleted += (s, args) =>
//            {
//                btnLoadWithBackgroundWorker.Enabled = true;
//                btnCancel.Enabled = false;

//                if (args.Cancelled)
//                {
//                    MessageBox.Show("Загрузка отменена пользователем.");
//                }
//                else if (args.Error != null)
//                {
//                    MessageBox.Show($"Ошибка: {args.Error.Message}");
//                }
//                else
//                {
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Данные загружены успешно!");
//                }
//            };

//            worker.RunWorkerAsync();
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            if (cancellationTokenSource != null)
//                cancellationTokenSource.Cancel();

//            Для BackgroundWorker отмена реализована в DoWork
//        }
//    }
//}


//28
//EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


//DataExporter.cs
//using System;
//using System.Data;
//using System.IO;
//using System.Xml;
//using Newtonsoft.Json;
//using CsvHelper;
//using OfficeOpenXml;

//public static class DataExporter
//{
//    Экспорт в XML
//    public static void ExportToXml(DataTable dataTable, string filePath)
//    {
//        dataTable.WriteXml(filePath);
//    }

//    Импорт из XML
//    public static void ImportFromXml(DataTable dataTable, string filePath)
//    {
//        dataTable.ReadXml(filePath);
//    }

//    Экспорт в CSV
//    public static void ExportToCsv(DataTable dataTable, string filePath)
//    {
//        using (var writer = new StreamWriter(filePath))
//        using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
//        {
//            foreach (DataColumn column in dataTable.Columns)
//                csv.WriteField(column.ColumnName);
//            csv.NextRecord();

//            foreach (DataRow row in dataTable.Rows)
//            {
//                foreach (DataColumn column in dataTable.Columns)
//                    csv.WriteField(row[column]);
//                csv.NextRecord();
//            }
//        }
//    }

//    Импорт из CSV
//    public static void ImportFromCsv(DataTable dataTable, string filePath)
//    {
//        using (var reader = new StreamReader(filePath))
//        using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
//        {
//            csv.Read();
//            csv.ReadHeader();

//            while (csv.Read())
//            {
//                DataRow row = dataTable.NewRow();
//                foreach (DataColumn column in dataTable.Columns)
//                    row[column.ColumnName] = csv.GetField(column.ColumnName);
//                dataTable.Rows.Add(row);
//            }
//        }
//    }

//    Экспорт в Excel
//    public static void ExportToExcel(DataTable dataTable, string filePath)
//    {
//        using (var package = new ExcelPackage(new FileInfo(filePath)))
//        {
//            var worksheet = package.Workbook.Worksheets.Add("Employees");
//            worksheet.Cells.LoadFromDataTable(dataTable, true);
//            package.Save();
//        }
//    }

//    Импорт из Excel
//    public static void ImportFromExcel(DataTable dataTable, string filePath)
//    {
//        using (var package = new ExcelPackage(new FileInfo(filePath)))
//        {
//            var worksheet = package.Workbook.Worksheets[0];
//            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
//            {
//                DataRow dataRow = dataTable.NewRow();
//                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
//                    dataRow[col - 1] = worksheet.Cells[row, col].Value;
//                dataTable.Rows.Add(dataRow);
//            }
//        }
//    }

//    Экспорт в JSON
//    public static void ExportToJson(DataTable dataTable, string filePath)
//    {
//        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
//        File.WriteAllText(filePath, json);
//    }

//    Импорт из JSON
//    public static void ImportFromJson(DataTable dataTable, string filePath)
//    {
//        string json = File.ReadAllText(filePath);
//        DataTable importedTable = JsonConvert.DeserializeObject<DataTable>(json);
//        dataTable.Merge(importedTable);
//    }
//}


//MainForm.csusing System;
//using System.Data;
//using System.Windows.Forms;

//namespace DataExportImportApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeDataSet employeeDataSet;

//        public MainForm()
//        {
//            InitializeComponent();
//            employeeDataSet = new EmployeeDataSet();
//            LoadSampleData();
//        }

//        private void LoadSampleData()
//        {
//            employeeDataSet.Employees.AddEmployeeRow("Иван Иванов", 50000);
//            employeeDataSet.Employees.AddEmployeeRow("Петр Петров", 60000);
//            dataGridView1.DataSource = employeeDataSet.Employees;
//        }

//        private void btnExportXml_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "XML files (*.xml)|*.xml" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToXml(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в XML выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportXml_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "XML files (*.xml)|*.xml" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromXml(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из XML выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnExportCsv_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToCsv(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в CSV выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportCsv_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromCsv(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из CSV выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnExportExcel_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToExcel(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в Excel выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportExcel_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromExcel(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из Excel выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnExportJson_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "JSON files (*.json)|*.json" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToJson(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в JSON выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportJson_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "JSON files (*.json)|*.json" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromJson(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из JSON выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }
//    }
//}


//29
//IEmployeeService.cs
//using System;
//using System.Data;
//using System.ServiceModel;

//[ServiceContract]
//public interface IEmployeeService
//{
//    [OperationContract]
//    EmployeeDataSet GetEmployees();

//    [OperationContract]
//    bool SaveEmployees(EmployeeDataSet employees);

//    [OperationContract]
//    string GetLastError();
//}


//EmployeeService.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.ServiceModel;

//public class EmployeeService : IEmployeeService
//{
//    private string connectionString = "Your_Connection_String";
//    private string lastError = string.Empty;

//    public EmployeeDataSet GetEmployees()
//    {
//        EmployeeDataSet dataSet = new EmployeeDataSet();
//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                adapter.Fill(dataSet, "Employees");
//            }
//        }
//        catch (Exception ex)
//        {
//            lastError = $"Ошибка при получении данных: {ex.Message}";
//            throw new FaultException(lastError);
//        }
//        return dataSet;
//    }

//    public bool SaveEmployees(EmployeeDataSet employees)
//    {
//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//                adapter.Update(employees.Employees);
//            }
//            return true;
//        }
//        catch (DBConcurrencyException)
//        {
//            lastError = "Конфликт при сохранении данных: запись была изменена другим пользователем.";
//            throw new FaultException(lastError);
//        }
//        catch (Exception ex)
//        {
//            lastError = $"Ошибка при сохранении данных: {ex.Message}";
//            throw new FaultException(lastError);
//        }
//    }

//    public string GetLastError()
//    {
//        return lastError;
//    }
//}


//EmployeeClient.cs
//using System;
//using System.ServiceModel;
//using System.Windows.Forms;

//namespace EmployeeClientApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeServiceClient client;
//        private EmployeeDataSet employeeDataSet;

//        public MainForm()
//        {
//            InitializeComponent();
//            client = new EmployeeServiceClient();
//            employeeDataSet = new EmployeeDataSet();
//        }

//        private void btnLoad_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                employeeDataSet = client.GetEmployees();
//                dataGridView1.DataSource = employeeDataSet.Employees;
//            }
//            catch (FaultException<ExceptionDetail> ex)
//            {
//                MessageBox.Show($"Ошибка сервера: {ex.Detail.Message}");
//            }
//            catch (CommunicationException ex)
//            {
//                MessageBox.Show($"Ошибка связи: {ex.Message}");
//            }
//            catch (TimeoutException ex)
//            {
//                MessageBox.Show($"Таймаут: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка: {ex.Message}");
//            }
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                bool success = client.SaveEmployees(employeeDataSet);
//                if (success)
//                {
//                    MessageBox.Show("Данные успешно сохранены!");
//                    employeeDataSet.AcceptChanges();
//                }
//            }
//            catch (FaultException<ExceptionDetail> ex)
//            {
//                MessageBox.Show($"Ошибка сервера: {ex.Detail.Message}");
//                employeeDataSet.RejectChanges();
//            }
//            catch (CommunicationException ex)
//            {
//                MessageBox.Show($"Ошибка связи: {ex.Message}");
//            }
//            catch (TimeoutException ex)
//            {
//                MessageBox.Show($"Таймаут: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка: {ex.Message}");
//            }
//        }
//    }
//}


//30
//EmployeeDataSetCache.cs
//using System;
//using System.Data;
//using System.Runtime.Caching;

//public class EmployeeDataSetCache
//{
//    private static ObjectCache cache = MemoryCache.Default;
//    private static string cacheKey = "EmployeeDataSetCache";
//    private static DateTimeOffset cacheExpiration = DateTimeOffset.Now.AddMinutes(10);

//    public static EmployeeDataSet GetFromCache()
//    {
//        return cache.Get(cacheKey) as EmployeeDataSet;
//    }

//    public static void AddToCache(EmployeeDataSet dataSet)
//    {
//        cache.Set(cacheKey, dataSet, cacheExpiration);
//    }

//    public static void ClearCache()
//    {
//        cache.Remove(cacheKey);
//    }

//    public static bool IsCacheValid()
//    {
//        return cache.Get(cacheKey) != null;
//    }
//}


//EmployeeServiceWithCache.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;

//public class EmployeeServiceWithCache
//{
//    private string connectionString = "Your_Connection_String";

//    public EmployeeDataSet GetEmployees()
//    {
//        EmployeeDataSet cachedDataSet = EmployeeDataSetCache.GetFromCache();
//        if (cachedDataSet != null)
//        {
//            Console.WriteLine("Данные получены из кэша.");
//            return cachedDataSet;
//        }

//        EmployeeDataSet dataSet = new EmployeeDataSet();
//        Stopwatch stopwatch = Stopwatch.StartNew();

//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                adapter.Fill(dataSet, "Employees");
//            }

//            EmployeeDataSetCache.AddToCache(dataSet);
//            stopwatch.Stop();
//            Console.WriteLine($"Данные загружены из базы данных за {stopwatch.ElapsedMilliseconds} мс.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
//        }

//        return dataSet;
//    }

//    public bool SaveEmployees(EmployeeDataSet employees)
//    {
//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//                adapter.Update(employees.Employees);
//            }

//            EmployeeDataSetCache.ClearCache();
//            Console.WriteLine("Кэш очищен после сохранения данных.");
//            return true;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
//            return false;
//        }
//    }

//    public void RefreshCache()
//    {
//        EmployeeDataSetCache.ClearCache();
//        Console.WriteLine("Кэш обновлён.");
//    }
//}


//Programm.cs
//using System;
//using System.Diagnostics;

//class Program
//{
//    static void Main()
//    {
//        EmployeeServiceWithCache service = new EmployeeServiceWithCache();

//        Первая загрузка данных(из базы данных)
//        EmployeeDataSet firstLoad = service.GetEmployees();

//        Вторая загрузка данных(из кэша)
//        EmployeeDataSet secondLoad = service.GetEmployees();

//        Сохранение изменений и очистка кэша
//        service.SaveEmployees(firstLoad);

//        Обновление кэша
//        service.RefreshCache();

//        Третья загрузка данных(из базы данных, так как кэш очищен)
//        EmployeeDataSet thirdLoad = service.GetEmployees();
//    }
//}


//31
//EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


//EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };
//        DataColumn columnSource = new DataColumn("Source", typeof(string)) { AllowDBNull = false, MaxLength = 50 };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnName, columnSalary, columnSource });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(int employeeID, string name, decimal salary, string source)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.EmployeeID = employeeID;
//        row.Name = name;
//        row.Salary = salary;
//        row.Source = source;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


//EmployeeRow.cs
//using System;
//using System.Data;

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }

//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public string Source { get => (string)row["Source"]; set => row["Source"] = value; }
//    public DataRow Row => row;
//}


//DataLoader.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Xml;
//using OfficeOpenXml;
//using System.IO;

//public static class DataLoader
//{
//    Загрузка данных из SQL Server
//    public static void LoadFromSql(EmployeesDataTable employees, string connectionString)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT EmployeeID, Name, Salary FROM Employees", connection);
//            DataTable tempTable = new DataTable();
//            adapter.Fill(tempTable);

//            foreach (DataRow row in tempTable.Rows)
//            {
//                employees.AddEmployeeRow(
//                    (int)row["EmployeeID"],
//                    (string)row["Name"],
//                    (decimal)row["Salary"],
//                    "SQL");
//            }
//        }
//    }

//    Загрузка данных из Excel
//    public static void LoadFromExcel(EmployeesDataTable employees, string filePath)
//    {
//        using (var package = new ExcelPackage(new FileInfo(filePath)))
//        {
//            var worksheet = package.Workbook.Worksheets[0];
//            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
//            {
//                employees.AddEmployeeRow(
//                    Convert.ToInt32(worksheet.Cells[row, 1].Value),
//                    worksheet.Cells[row, 2].Value.ToString(),
//                    Convert.ToDecimal(worksheet.Cells[row, 3].Value),
//                    "Excel");
//            }
//        }
//    }

//    Загрузка данных из XML
//    public static void LoadFromXml(EmployeesDataTable employees, string filePath)
//    {
//        DataSet tempDataSet = new DataSet();
//        tempDataSet.ReadXml(filePath);

//        foreach (DataRow row in tempDataSet.Tables[0].Rows)
//        {
//            employees.AddEmployeeRow(
//                (int)row["EmployeeID"],
//                (string)row["Name"],
//                (decimal)row["Salary"],
//                "XML");
//        }
//    }
//}


//DataMerger.cs
//using System;
//using System.Data;

//public static class DataMerger
//{
//    public static void MergeDataSets(EmployeeDataSet target, EmployeeDataSet source)
//    {
//        foreach (EmployeeRow sourceRow in source.Employees)
//        {
//            EmployeeRow targetRow = target.Employees.FindByID(sourceRow.EmployeeID);

//            if (targetRow == null)
//            {
//                Если записи нет, добавляем новую
//                target.Employees.AddEmployeeRow(
//                    sourceRow.EmployeeID,
//                    sourceRow.Name,
//                    sourceRow.Salary,
//                    sourceRow.Source);
//            }
//            else
//            {
//                Если запись есть, разрешаем конфликт(например, берем данные из источника с приоритетом)
//                if (sourceRow.Source == "SQL")
//                {
//                    targetRow.Name = sourceRow.Name;
//                    targetRow.Salary = sourceRow.Salary;
//                    targetRow.Source = sourceRow.Source;
//                }
//            }
//        }
//    }

//    public static void SyncDataSets(EmployeeDataSet target, EmployeeDataSet source)
//    {
//        Синхронизация данных: обновление измененных записей
//        foreach (EmployeeRow sourceRow in source.Employees)
//        {
//            EmployeeRow targetRow = target.Employees.FindByID(sourceRow.EmployeeID);

//            if (targetRow != null)
//            {
//                targetRow.Name = sourceRow.Name;
//                targetRow.Salary = sourceRow.Salary;
//                targetRow.Source = sourceRow.Source;
//            }
//        }
//    }
//}


//Programm.cs
//using System;

//class Program
//{
//    static void Main()
//    {
//        EmployeeDataSet sqlDataSet = new EmployeeDataSet();
//        EmployeeDataSet excelDataSet = new EmployeeDataSet();
//        EmployeeDataSet xmlDataSet = new EmployeeDataSet();
//        EmployeeDataSet mergedDataSet = new EmployeeDataSet();

//        Загрузка данных из разных источников
//        DataLoader.LoadFromSql(sqlDataSet.Employees, "Your_SQL_Connection_String");
//        DataLoader.LoadFromExcel(excelDataSet.Employees, "Employees.xlsx");
//        DataLoader.LoadFromXml(xmlDataSet.Employees, "Employees.xml");

//        Объединение данных
//        DataMerger.MergeDataSets(mergedDataSet, sqlDataSet);
//        DataMerger.MergeDataSets(mergedDataSet, excelDataSet);
//        DataMerger.MergeDataSets(mergedDataSet, xmlDataSet);

//        Вывод объединенных данных
//        Console.WriteLine("Объединенные данные:");
//        foreach (EmployeeRow row in mergedDataSet.Employees)
//        {
//            Console.WriteLine($"{row.EmployeeID}: {row.Name}, {row.Salary:C}, Источник: {row.Source}");
//        }
//    }
//}


//32
//EmployeeDataSetWithHistory.cs
//using System;
//using System.Data;
//using System.Collections.Generic;

//public partial class EmployeeDataSetWithHistory : DataSet
//{
//    private EmployeesDataTable tableEmployees;
//    private List<DataSet> history = new List<DataSet>();

//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSetWithHistory()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//        this.Employees.RowChanged += Employees_RowChanged;
//    }

//    private void Employees_RowChanged(object sender, DataRowChangeEventArgs e)
//    {
//        SaveToHistory();
//    }

//    public void SaveToHistory()
//    {
//        EmployeeDataSetWithHistory copy = new EmployeeDataSetWithHistory();
//        copy.Merge(this);
//        history.Add(copy);
//    }

//    public List<DataSet> GetHistory()
//    {
//        return history;
//    }

//    public void UndoChange()
//    {
//        if (history.Count > 0)
//        {
//            this.Clear();
//            this.Merge(history[history.Count - 1]);
//            history.RemoveAt(history.Count - 1);
//        }
//    }

//    public void RestoreVersion(int versionIndex)
//    {
//        if (versionIndex >= 0 && versionIndex < history.Count)
//        {
//            this.Clear();
//            this.Merge(history[versionIndex]);
//        }
//    }
//}


//Programm.cs
//using System;

//class Program
//{
//    static void Main()
//    {
//        EmployeeDataSetWithHistory dataSet = new EmployeeDataSetWithHistory();

//        Добавление записи
//        dataSet.Employees.AddEmployeeRow(1, "Иван Иванов", 50000);
//        Console.WriteLine("Добавлен Иван Иванов");

//        Изменение записи
//        EmployeeRow employee = dataSet.Employees.FindByID(1);
//        if (employee != null)
//        {
//            employee.Name = "Иван Петров";
//            employee.Salary = 60000;
//            Console.WriteLine("Изменено имя и зарплата");
//        }

//        Просмотр истории
//        Console.WriteLine("\nИстория изменений:");
//        var history = dataSet.GetHistory();
//        for (int i = 0; i < history.Count; i++)
//        {
//            Console.WriteLine($"Версия {i + 1}:");
//            foreach (EmployeeRow row in ((EmployeeDataSetWithHistory)history[i]).Employees)
//            {
//                Console.WriteLine($"{row.EmployeeID}: {row.Name}, {row.Salary:C}");
//            }
//        }

//        Откат последнего изменения
//        Console.WriteLine("\nОткат последнего изменения:");
//        dataSet.UndoChange();
//        foreach (EmployeeRow row in dataSet.Employees)
//        {
//            Console.WriteLine($"{row.EmployeeID}: {row.Name}, {row.Salary:C}");
//        }

//        Восстановление версии
//        Console.WriteLine("\nВосстановление версии 1:");
//        dataSet.RestoreVersion(0);
//        foreach (EmployeeRow row in dataSet.Employees)
//        {
//            Console.WriteLine($"{row.EmployeeID}: {row.Name}, {row.Salary:C}");
//        }
//    }
//}


//33
//EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    События
//    public event EventHandler<RowEventArgs> RowAdded;
//    public event EventHandler<RowEventArgs> RowChanged;
//    public event EventHandler<RowEventArgs> RowDeleted;
//    public event EventHandler<DataSetEventArgs> DataSetOpened;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);

//        Подписка на события DataTable
//        tableEmployees.RowChanged += (sender, e) =>
//        {
//            if (e.Action == DataRowAction.Add)
//                RowAdded?.Invoke(this, new RowEventArgs(e.Row));
//            else if (e.Action == DataRowAction.Change)
//                RowChanged?.Invoke(this, new RowEventArgs(e.Row));
//            else if (e.Action == DataRowAction.Delete)
//                RowDeleted?.Invoke(this, new RowEventArgs(e.Row));
//        };
//    }

//    public void Open()
//    {
//        DataSetOpened?.Invoke(this, new DataSetEventArgs());
//    }
//}

//Аргументы событий
//public class RowEventArgs : EventArgs
//{
//    public DataRow Row { get; }

//    public RowEventArgs(DataRow row)
//    {
//        Row = row;
//    }
//}

//public class DataSetEventArgs : EventArgs
//{
//    public DateTime OpenTime { get; } = DateTime.Now;
//}


//EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


//EmployeeRow.cs
//using System;
//using System.Data;

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }

//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DataRow Row => row;
//}


//EventLogger.cs
//Programm.cs
//using System;

//class Program
//{
//    static void Main()
//    {
//        EmployeeDataSet dataSet = new EmployeeDataSet();

//        Подписка на события
//        dataSet.RowAdded += (sender, e) =>
//        {
//            Console.WriteLine($"Добавлена строка: ID {e.Row["EmployeeID"]}");
//            EventLogger.LogRowEvent("Добавлена строка", e.Row);
//        };

//        dataSet.RowChanged += (sender, e) =>
//        {
//            Console.WriteLine($"Изменена строка: ID {e.Row["EmployeeID"]}");
//            EventLogger.LogRowEvent("Изменена строка", e.Row);
//        };

//        dataSet.RowDeleted += (sender, e) =>
//        {
//            Console.WriteLine($"Удалена строка: ID {e.Row["EmployeeID", DataRowVersion.Original]}");
//            EventLogger.LogRowEvent("Удалена строка", e.Row);
//        };

//        dataSet.DataSetOpened += (sender, e) =>
//        {
//            Console.WriteLine($"DataSet открыт в {e.OpenTime}");
//            EventLogger.LogEvent($"DataSet открыт в {e.OpenTime}");
//        };

//        Открытие DataSet
//        dataSet.Open();

//        Добавление записи
//        dataSet.Employees.AddEmployeeRow("Иван Иванов", 50000);

//        Изменение записи
//        EmployeeRow employee = dataSet.Employees[0];
//        employee.Name = "Иван Петров";

//        Удаление записи
//        employee.Row.Delete();
//    }
//}


//34
//CompanyDataSet.cs
//using System;
//using System.Data;

//public partial class CompanyDataSet : DataSet
//{
//    private CompaniesDataTable tableCompanies;
//    private DepartmentsDataTable tableDepartments;
//    private EmployeesDataTable tableEmployees;

//    public CompaniesDataTable Companies => tableCompanies;
//    public DepartmentsDataTable Departments => tableDepartments;
//    public EmployeesDataTable Employees => tableEmployees;

//    public CompanyDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableCompanies = new CompaniesDataTable("Companies");
//        tableDepartments = new DepartmentsDataTable("Departments");
//        tableEmployees = new EmployeesDataTable("Employees");

//        this.Tables.Add(tableCompanies);
//        this.Tables.Add(tableDepartments);
//        this.Tables.Add(tableEmployees);

//        Создание отношений
//        DataRelation companyDepartmentRelation = new DataRelation(
//            "CompanyDepartments",
//            tableCompanies.Columns["CompanyID"],
//            tableDepartments.Columns["CompanyID"]);
//        this.Relations.Add(companyDepartmentRelation);

//        DataRelation departmentEmployeeRelation = new DataRelation(
//            "DepartmentEmployees",
//            tableDepartments.Columns["DepartmentID"],
//            tableEmployees.Columns["DepartmentID"]);
//        this.Relations.Add(departmentEmployeeRelation);
//    }
//}


//CompaniesDataTable.cs
//using System;
//using System.Data;

//public partial class CompaniesDataTable : DataTable
//{
//    public CompaniesDataTable() : base("Companies") { InitializeColumns(); }
//    public CompaniesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnCompanyID = new DataColumn("CompanyID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnCompanyID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnCompanyID };
//    }

//    public CompanyRow AddCompanyRow(string name)
//    {
//        CompanyRow row = new CompanyRow(this.NewRow());
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


//DepartmentsDataTable.cs
//using System;
//using System.Data;

//public partial class DepartmentsDataTable : DataTable
//{
//    public DepartmentsDataTable() : base("Departments") { InitializeColumns(); }
//    public DepartmentsDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnCompanyID = new DataColumn("CompanyID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnDepartmentID, columnCompanyID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnDepartmentID };
//    }

//    public DepartmentRow AddDepartmentRow(int companyID, string name)
//    {
//        DepartmentRow row = new DepartmentRow(this.NewRow());
//        row.CompanyID = companyID;
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


//EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnDepartmentID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(int departmentID, string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.DepartmentID = departmentID;
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


//QueryService.cs
//using System;
//using System.Data;
//using System.Linq;

//public static class QueryService
//{
//    Пример 1: JOIN между таблицами
//    public static DataTable GetCompanyDepartments(CompanyDataSet dataSet, int companyID)
//    {
//        var query = from company in dataSet.Companies.AsEnumerable()
//                    join department in dataSet.Departments.AsEnumerable()
//                    on company.Field<int>("CompanyID") equals department.Field<int>("CompanyID")
//                    where company.Field<int>("CompanyID") == companyID
//                    select new
//                    {
//                        CompanyName = company.Field<string>("Name"),
//                        DepartmentName = department.Field<string>("Name")
//                    };

//        DataTable result = new DataTable();
//        result.Columns.Add("CompanyName", typeof(string));
//        result.Columns.Add("DepartmentName", typeof(string));

//        foreach (var item in query)
//        {
//            result.Rows.Add(item.CompanyName, item.DepartmentName);
//        }

//        return result;
//    }

//    Пример 2: Агрегация(GROUP BY)
//    public static DataTable GetSalaryByDepartment(CompanyDataSet dataSet)
//    {
//        var query = from employee in dataSet.Employees.AsEnumerable()
//                    join department in dataSet.Departments.AsEnumerable()
//                    on employee.Field<int>("DepartmentID") equals department.Field<int>("DepartmentID")
//                    group employee by department.Field<string>("Name") into g
//                    select new
//                    {
//                        DepartmentName = g.Key,
//                        TotalSalary = g.Sum(e => e.Field<decimal>("Salary")),
//                        AverageSalary = g.Average(e => e.Field<decimal>("Salary"))
//                    };

//        DataTable result = new DataTable();
//        result.Columns.Add("DepartmentName", typeof(string));
//        result.Columns.Add("TotalSalary", typeof(decimal));
//        result.Columns.Add("AverageSalary", typeof(decimal));

//        foreach (var item in query)
//        {
//            result.Rows.Add(item.DepartmentName, item.TotalSalary, item.AverageSalary);
//        }

//        return result;
//    }

//    Пример 3: Подзапрос
//    public static DataTable GetEmployeesWithAboveAverageSalary(CompanyDataSet dataSet)
//    {
//        decimal averageSalary = dataSet.Employees.AsEnumerable().Average(e => e.Field<decimal>("Salary"));

//        var query = from employee in dataSet.Employees.AsEnumerable()
//                    where employee.Field<decimal>("Salary") > averageSalary
//                    select employee;

//        DataTable result = query.Any() ? query.CopyToDataTable() : new DataTable();
//        return result;
//    }

//    Пример 4: Использование HAVING
//    public static DataTable GetDepartmentsWithHighTotalSalary(CompanyDataSet dataSet, decimal minTotalSalary)
//    {
//        var query = from employee in dataSet.Employees.AsEnumerable()
//                    join department in dataSet.Departments.AsEnumerable()
//                    on employee.Field<int>("DepartmentID") equals department.Field<int>("DepartmentID")
//                    group employee by new { DepartmentID = department.Field<int>("DepartmentID"), DepartmentName = department.Field<string>("Name") } into g
//                    where g.Sum(e => e.Field<decimal>("Salary")) > minTotalSalary
//                    select new
//                    {
//                        DepartmentName = g.Key.DepartmentName,
//                        TotalSalary = g.Sum(e => e.Field<decimal>("Salary"))
//                    };

//        DataTable result = new DataTable();
//        result.Columns.Add("DepartmentName", typeof(string));
//        result.Columns.Add("TotalSalary", typeof(decimal));

//        foreach (var item in query)
//        {
//            result.Rows.Add(item.DepartmentName, item.TotalSalary);
//        }

//        return result;
//    }
//}


//Programm.cs
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        CompanyDataSet dataSet = new CompanyDataSet();

//        Заполнение данными
//        CompanyRow company1 = dataSet.Companies.AddCompanyRow("Company A");
//        DepartmentRow department1 = dataSet.Departments.AddDepartmentRow(company1.CompanyID, "IT");
//        DepartmentRow department2 = dataSet.Departments.AddDepartmentRow(company1.CompanyID, "HR");

//        dataSet.Employees.AddEmployeeRow(department1.DepartmentID, "Иван Иванов", 50000);
//        dataSet.Employees.AddEmployeeRow(department1.DepartmentID, "Петр Петров", 60000);
//        dataSet.Employees.AddEmployeeRow(department2.DepartmentID, "Сидор Сидоров", 55000);

//        Пример 1: JOIN между таблицами
//       DataTable companyDepartments = QueryService.GetCompanyDepartments(dataSet, company1.CompanyID);
//        Console.WriteLine("Отделы компании:");
//        foreach (DataRow row in companyDepartments.Rows)
//        {
//            Console.WriteLine($"{row["CompanyName"]} - {row["DepartmentName"]}");
//        }

//        Пример 2: Агрегация(GROUP BY)
//        DataTable salaryByDepartment = QueryService.GetSalaryByDepartment(dataSet);
//        Console.WriteLine("\nЗарплаты по отделам:");
//        foreach (DataRow row in salaryByDepartment.Rows)
//        {
//            Console.WriteLine($"{row["DepartmentName"]}: Общая зарплата = {row["TotalSalary"]:C}, Средняя зарплата = {row["AverageSalary"]:C}");
//        }

//        Пример 3: Подзапрос
//       DataTable aboveAverageEmployees = QueryService.GetEmployeesWithAboveAverageSalary(dataSet);
//        Console.WriteLine("\nСотрудники с зарплатой выше средней:");
//        foreach (DataRow row in aboveAverageEmployees.Rows)
//        {
//            Console.WriteLine($"{row["Name"]}: {row["Salary"]:C}");
//        }

//        Пример 4: Использование HAVING
//        DataTable highSalaryDepartments = QueryService.GetDepartmentsWithHighTotalSalary(dataSet, 100000);
//        Console.WriteLine("\nОтделы с высокой общей зарплатой:");
//        foreach (DataRow row in highSalaryDepartments.Rows)
//        {
//            Console.WriteLine($"{row["DepartmentName"]}: {row["TotalSalary"]:C}");
//        }
//    }
//}
